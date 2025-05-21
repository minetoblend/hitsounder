using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Graphics;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternTimelineLayer(PatternLayer item) : TimelineLayer<PatternLayer>(item, 30)
{
    protected override TimelineLayerHeader CreateHeader() => new PatternLayerHeader(Model);

    protected override Drawable CreateTimelineContent()
    {
        return new TimelineContent();
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() => FinishTransforms(), 1);
    }
}
