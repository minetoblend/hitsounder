using System;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;

namespace Hitsounder.Game.Graphics.Containers;

[Cached]
[Cached(type: typeof(ITimeline))]
public abstract partial class LayeredTimeline<TModel> : CompositeDrawable, ITimeline
    where TModel : notnull
{
    private readonly LayerContainer layerContainer;

    protected FillFlowContainer<RearrangeableListItem<TModel>> LayersFlow => layerContainer.ListContainer;

    public BindableList<TModel> Layers => layerContainer.Items;

    public readonly float HeaderWidth;

    protected virtual Drawable CreateBackground() => new Box
    {
        RelativeSizeAxes = Axes.Both,
        Colour = Color4Extensions.FromHex("#333339")
    };

    private readonly Container headerContainer;

    public readonly Container TimelineContainer;

    protected LayeredTimeline(float headerWidth)
    {
        HeaderWidth = headerWidth;
        RelativeSizeAxes = Axes.Both;

        InternalChildren =
        [
            TimelineContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding { Left = headerWidth },
            },
            layerContainer = new LayerContainer(this)
            {
                RelativeSizeAxes = Axes.Both,
            },
            headerContainer = new Container
            {
                RelativeSizeAxes = Axes.Y,
                Width = headerWidth,
                Child = CreateBackground().With(d => d.Depth = float.MaxValue)
            }
        ];

        layerContainer.OnNewLayer += layer =>
        {
            layer.OnLoadComplete += _ =>
            {
                headerContainer.Add(layer.HeaderProxy);
            };
        };
    }

    protected abstract TimelineLayer<TModel> CreateLayer(TModel item);

    protected partial class LayerContainer(LayeredTimeline<TModel> parent) : RearrangeableListContainer<TModel>
    {
        public event Action<TimelineLayer<TModel>>? OnNewLayer;

        protected override ScrollContainer<Drawable> CreateScrollContainer() => new TimelineScrollContainer(parent)
        {
            DistanceDecayScroll = 1,
            ClampExtension = 0,
        };

        protected override RearrangeableListItem<TModel> CreateDrawable(TModel item)
        {
            var layer = parent.CreateLayer(item);
            OnNewLayer?.Invoke(layer);
            return layer;
        }

        public new FillFlowContainer<RearrangeableListItem<TModel>> ListContainer => base.ListContainer;

        private partial class TimelineScrollContainer(LayeredTimeline<TModel> parent) : HitsounderScrollContainer
        {
            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
            {
                return base.ReceivePositionalInputAt(screenSpacePos) && parent.headerContainer.Contains(screenSpacePos);
            }
        }
    }

    public double StartTime { get; private set; } = 0;
    public double VisibleDuration { get; private set; } = 2000;
    public double TotalDuration { get; } = 10_000;

    private float timelineWidth => DrawWidth - HeaderWidth;

    public float DurationToSize(double duration) => (float)(duration / VisibleDuration * timelineWidth);

    public double SizeToDuration(float size) => size * VisibleDuration / timelineWidth;

    public void ApplyToContent<T>(Container<T> content) where T : Drawable
    {
        content.X = -DurationToSize(StartTime);
        content.Width = DurationToSize(TotalDuration);
        content.RelativeChildSize = new Vector2((float)TotalDuration, 1);
    }

    public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) =>
        base.ReceivePositionalInputAt(screenSpacePos)
        && !headerContainer.Contains(screenSpacePos);

    protected override bool OnScroll(ScrollEvent e)
    {
        if (e.ControlPressed)
        {
            VisibleDuration *= (1 - e.ScrollDelta.Y * 0.1f);
            return true;
        }

        StartTime = Math.Max(StartTime - e.ScrollDelta.Y * 10f, 0);

        return true;
    }

    protected override bool OnDragStart(DragStartEvent e)
    {
        return true;
    }

    protected override void OnDrag(DragEvent e)
    {
        StartTime = Math.Max(StartTime - SizeToDuration(e.Delta.X), 0);
    }
}
