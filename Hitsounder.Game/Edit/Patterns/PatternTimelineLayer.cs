using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternTimelineLayer(PatternLayer item) : TimelineLayer<PatternLayer>(item, 30)
{
    protected override TimelineLayerHeader CreateHeader() => new PatternLayerHeader(Model);

    protected override Drawable CreateTimelineContent()
    {
        return new Container
        {
            RelativeSizeAxes = Axes.Both,
            Padding = new MarginPadding(2),
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#222228")
            }
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() => FinishTransforms(), 1);
    }
}
