using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Patterns;

[Cached]
public partial class PatternTimeline : LayeredTimeline<PatternLayer>
{
    public PatternTimeline()
        : base(180)
    {
        TimelineContainer.AddRange([
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#111118"),
            },
            new PatternTickDisplay(),
        ]);

        LayersFlow.Padding = new MarginPadding { Top = 12 };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() =>
        {
            LayersFlow.LayoutDuration = 200;
            LayersFlow.LayoutEasing = Easing.OutExpo;
        }, 1);
    }

    protected override TimelineLayer<PatternLayer> CreateLayer(PatternLayer item) => new PatternTimelineLayer(item);
}
