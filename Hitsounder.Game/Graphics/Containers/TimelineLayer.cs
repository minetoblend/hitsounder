using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Hitsounder.Game.Graphics.Containers;

public abstract partial class TimelineLayer<TModel> : RearrangeableListItem<TModel>
    where TModel : notnull
{
    protected TimelineLayer(TModel item, float height)
        : base(item)
    {
        RelativeSizeAxes = Axes.X;
        Height = height;

        InternalChildren =
        [
            timelineContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
            },
            headerContainer = new Container
            {
                RelativeSizeAxes = Axes.Y,
            }
        ];
    }

    protected abstract TimelineLayerHeader CreateHeader();

    protected abstract Drawable CreateTimelineContent();

    private readonly Container headerContainer;
    private readonly Container timelineContainer;

    protected TimelineLayerHeader Header { get; private set; } = null!;

    protected Drawable Timeline { get; private set; } = null!;

    [Resolved]
    private LayeredTimeline<TModel> parentTimeline { get; set; } = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        headerContainer.Width = parentTimeline.HeaderWidth;
        timelineContainer.Padding = new MarginPadding { Left = parentTimeline.HeaderWidth };

        timelineContainer.Child = CreateTimelineContent();
        headerContainer.Child = Header = CreateHeader();
    }

    protected override bool IsDraggableAt(Vector2 screenSpacePos) => Header.IsDraggableAt(screenSpacePos);

    private Drawable? headerProxy;

    public Drawable HeaderProxy => headerProxy ??= Header.CreateProxy();
}
