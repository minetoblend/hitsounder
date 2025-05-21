using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Graphics.Containers;

public partial class TimelineContent : TimelineContent<Drawable>;

public partial class TimelineContent<T> : Container<T>
    where T : Drawable
{
    [Resolved]
    private ITimeline timeline { get; set; } = null!;

    protected override Container<T> Content { get; }

    public TimelineContent()
    {
        RelativeSizeAxes = Axes.Both;

        AddInternal(Content = new Container<T>
        {
            RelativeSizeAxes = Axes.Y,
        });
    }

    protected override void Update()
    {
        base.Update();

        timeline.ApplyToContent(Content);
    }
}
