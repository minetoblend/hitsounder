using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace Hitsounder.Game.Graphics;

public partial class HoverHighlight : CompositeDrawable
{
    public bool StopEventPropagation = true;

    public HoverHighlight()
    {
        RelativeSizeAxes = Axes.Both;
        InternalChild = new Box
        {
            RelativeSizeAxes = Axes.Both,
            Alpha = 0
        };
    }

    protected override bool OnHover(HoverEvent e)
    {
        InternalChild.FadeTo(0.15f)
                     .FadeTo(0.1f, 300);
        return StopEventPropagation;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        ClearTransforms(true);
        InternalChild.FadeOut(100);
    }
}
