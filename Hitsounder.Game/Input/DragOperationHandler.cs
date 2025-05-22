using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Logging;

namespace Hitsounder.Game.Input;

[Cached]
public partial class DragOperationHandler : Container
{
    private readonly Container content;
    private readonly Container overlayContent;

    protected override Container<Drawable> Content => content;

    public DragOperationHandler()
    {
        RelativeSizeAxes = Axes.Both;
        InternalChildren =
        [
            content = new Container { RelativeSizeAxes = Axes.Both },
            overlayContent = new Container { RelativeSizeAxes = Axes.Both }
        ];
    }

    private Dictionary<Type, DragHandler> eventManagers = new Dictionary<Type, DragHandler>();
    private DragHandler? currentHandler;

    public void BeginDragOperation<T>(Drawable source, T data, Drawable? dragRepresentation = null)
    {
        currentHandler?.EndDrag(false);

        overlayContent.Add(currentHandler = new DragEventManager<T>(this, source, data, dragRepresentation));
        if (dragRepresentation != null)
            overlayContent.Add(dragRepresentation);
    }

    private partial class DragEventManager<T>(DragOperationHandler parent, Drawable source, T data, Drawable? dragRepresentation) : DragHandler
    {
        private readonly List<IDragEventHandler<T>> lastHoveredDrawables = new List<IDragEventHandler<T>>();
        private readonly List<IDragEventHandler<T>> hoveredDrawables = new List<IDragEventHandler<T>>();

        private IDragEventHandler<T>? hoverHandledDrawable;

        private InputManager inputManager = null!;

        private bool ended;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            inputManager = GetContainingInputManager();

            if (dragRepresentation is IDragRepresentation d)
                d.OnDragStart(ToLocalSpace(inputManager!.CurrentState.Mouse.Position));
        }

        protected override void Update()
        {
            base.Update();

            if (!source.IsDragged)
                EndDrag(true);

            if (ended)
                return;

            var targets = inputManager.PositionalInputQueue.OfType<IDragEventHandler<T>>();

            IDragEventHandler<T>? lastHoverHandledDrawable = hoverHandledDrawable;
            hoverHandledDrawable = null;

            lastHoveredDrawables.Clear();
            lastHoveredDrawables.AddRange(hoveredDrawables);

            hoveredDrawables.Clear();

            var evt = new EditorDragEvent<T>(source, data);

            foreach (var target in targets)
            {
                hoveredDrawables.Add(target);
                bool wasHovered = lastHoveredDrawables.Remove(target);

                if (target == lastHoverHandledDrawable)
                {
                    hoverHandledDrawable = target;
                    break;
                }

                if (!wasHovered && target.OnDragEnter(evt))
                {
                    Logger.Log($"Dragenter {target}");
                    hoverHandledDrawable = target;
                    break;
                }
            }

            foreach (var d in lastHoveredDrawables)
                d.OnDragLeave(evt);

            if (dragRepresentation is IDragRepresentation ghost)
                ghost.UpdatePosition(ToLocalSpace(inputManager.CurrentState.Mouse.Position));
        }

        public override void EndDrag(bool wasDrop)
        {
            if (ended)
                return;

            ended = true;

            var evt = new EditorDragEvent<T>(source, data);

            Logger.Log("Ending drag operation");

            bool dropHandled = false;

            if (wasDrop)
            {
                foreach (var target in hoveredDrawables)
                {
                    if (target.OnDrop(evt))
                    {
                        dropHandled = true;
                        break;
                    }
                }
            }

            foreach (var target in hoveredDrawables)
            {
                target.OnDragLeave(evt);
                Logger.Log($"OnDragoverLost {target}");
            }

            hoveredDrawables.Clear();

            if (dragRepresentation is IDragRepresentation ghost)
            {
                ghost.OnDragEnd(dropHandled);

                dragRepresentation.Expire();

                if (dragRepresentation.LifetimeEnd != double.MaxValue)
                {
                    LifetimeEnd = dragRepresentation.LifetimeEnd;
                    return;
                }
            }

            Expire();
        }
    }

    private abstract partial class DragHandler : CompositeDrawable
    {
        protected DragHandler()
        {
            RelativeSizeAxes = Axes.Both;
        }

        public abstract void EndDrag(bool wasDrop);
    }
}
