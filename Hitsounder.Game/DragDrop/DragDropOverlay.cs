using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Logging;
using osu.Framework.Utils;
using osuTK;
using osuTK.Input;

namespace Hitsounder.Game.DragDrop;

public partial class DragDropOverlay : Container
{
    private readonly Container content;
    private readonly Container overlayContent;

    protected override Container<Drawable> Content => content;

    public DragDropOverlay()
    {
        RelativeSizeAxes = Axes.Both;
        InternalChildren =
        [
            content = new Container { RelativeSizeAxes = Axes.Both },
            overlayContent = new Container { RelativeSizeAxes = Axes.Both }
        ];
    }

    private InputManager? inputManager;

    private IDraggable? currentTarget;
    private Drawable? currentDisplayContent;

    protected override void LoadComplete()
    {
        base.LoadComplete();

        inputManager = GetContainingInputManager();
    }

    protected override void Update()
    {
        base.Update();

        if (inputManager != null)
        {
            var buttonManager = inputManager.GetButtonEventManagerFor(MouseButton.Left);

            var didUpdate = updateContent(buttonManager.DraggedDrawable as IDraggable);

            updatePosition(inputManager.CurrentState.Mouse.Position, didUpdate);
        }
    }

    private bool updateContent(IDraggable? newTarget)
    {
        if (newTarget == currentTarget)
            return false;

        if (currentTarget != null)
        {
            currentDisplayContent?
                .FadeOut(200)
                .MoveToOffset(new Vector2(0, 20), 200, Easing.In)
                .ScaleTo(0.9f, 200, Easing.In)
                .Expire();
            currentDisplayContent = null;
            Logger.Log("Ended drag overlay");
        }

        if (newTarget != null)
        {
            Logger.Log("Began new drag overlay");

            overlayContent.Add(currentDisplayContent = newTarget.GetDragRepresentation());
            currentDisplayContent
                .FadeInFromZero(100)
                .ScaleTo(0)
                .ScaleTo(1, 350, Easing.OutBack);
        }

        currentTarget = newTarget;

        return true;
    }

    private void updatePosition(Vector2 mousePosition, bool immediate)
    {
        if (currentDisplayContent == null)
            return;

        var position = ToLocalSpace(mousePosition);

        if (immediate)
            currentDisplayContent.Position = position;
        else
        {
            var previousPosition = currentDisplayContent.Position;

            currentDisplayContent.Position = Vector2.Lerp(position, currentDisplayContent.Position, (float)Math.Exp(-0.03 * Time.Elapsed));

            var delta = currentDisplayContent.Position - previousPosition;

            float targetRotation = (float)(delta.X / Time.Elapsed) * 10;

            currentDisplayContent.Rotation = (float)Interpolation.Lerp(targetRotation, currentDisplayContent.Rotation, (float)Math.Exp(-0.02 * Time.Elapsed));
        }
    }
}
