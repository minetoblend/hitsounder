using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Edit.Patterns;

[Cached]
public partial class PatternTimeline : LayeredTimeline<PatternLayer>
{
    public PatternTimeline()
        : base(180)
    {
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

    protected override ScrollContainer<Drawable> CreateScrollContainer() => new HitsounderScrollContainer();
}
