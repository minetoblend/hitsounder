using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternTimelineLayer(PatternLayer item) : TimelineLayer<PatternLayer>(item, 30)
{
    private PatternLayerHeader header = null!;

    protected override TimelineLayerHeader CreateHeader() => header = new PatternLayerHeader(Model);

    protected override Drawable CreateTimelineContent()
    {
        return new PatternTimelineContent(Model);
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() => FinishTransforms(), 1);
    }

    protected override bool OnDragStart(DragStartEvent e)
    {
        if (!base.OnDragStart(e))
            return false;

        header.OnDragStart();
        return true;
    }

    protected override void OnDragEnd(DragEndEvent e)
    {
        base.OnDragEnd(e);

        header.OnDragEnd();
    }
}
