using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Hitsounder.Game.UserInterface;

public partial class LEDToggle : CompositeDrawable, IHasCurrentValue<bool>
{
    public ColourInfo ActiveColour = Color4Extensions.FromHex("#a6ff66");

    public ColourInfo InactiveColour = Color4Extensions.FromHex("#1a2b1c");

    private readonly BindableWithCurrent<bool> current = new BindableWithCurrent<bool>();

    public Bindable<bool> Current
    {
        get => current;
        set => current.Current = value;
    }

    private CircularContainer led;

    public LEDToggle()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = led = new CircularContainer
        {
            Size = new Vector2(8),
            Masking = true,
            MaskingSmoothness = 2f,
            BorderThickness = 1.5f,
            BorderColour = Color4.Gray,
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Radius = 24f
            },
            Children =
            [
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                }
            ]
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        current.BindValueChanged(active =>
        {
            if (active.NewValue)
            {
                led.FadeColour(ActiveColour, 50)
                   .FadeEdgeEffectTo(ActiveColour.MultiplyAlpha(0.1f), 100);
            }
            else
            {
                led.FadeColour(InactiveColour, 50)
                   .FadeEdgeEffectTo(0, 50);
            }
        }, true);
    }

    protected override bool OnClick(ClickEvent e)
    {
        current.Value = !current.Value;

        return true;
    }

    public new MarginPadding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }
}
