using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Hitsounder.Game.UserInterface;

public partial class LEDToggle : CompositeDrawable, IHasCurrentValue<bool>
{
    public ColourInfo ActiveColour = Color4Extensions.FromHex("#a6ff66");

    private readonly BindableWithCurrent<bool> current = new BindableWithCurrent<bool>();

    public Bindable<bool> Current
    {
        get => current;
        set => current.Current = value;
    }

    private Sprite ledOff = null!;
    private Sprite ledOn = null!;
    private CircularContainer glow = null!;

    public LEDToggle()
    {
        Size = new Vector2(18);
        Padding = new MarginPadding(4);
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        Colour = ActiveColour;
        InternalChildren =
        [
            glow = new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                EdgeEffect = new EdgeEffectParameters
                {
                    Radius = 32f,
                    Colour = ActiveColour.MultiplyAlpha(0f),
                    Type = EdgeEffectType.Glow,
                },
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    AlwaysPresent = true,
                }
            },
            ledOff = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                Texture = textures.Get("UI/led-off"),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = new Color4(0.35f, 0.35f, 0.35f, 1f),
            },
            ledOn = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                Texture = textures.Get("UI/led-on"),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Blending = BlendingParameters.Additive,
            }
        ];
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        current.BindValueChanged(active =>
        {
            if (active.NewValue)
            {
                ledOff.FadeColour(Color4.White, 50);
                ledOn.FadeIn(50);
                glow.FadeEdgeEffectTo(0.1f, 50);
            }
            else
            {
                ledOff.FadeColour(new Color4(0.35f, 0.35f, 0.35f, 1f), 50);
                ledOn.FadeOut(50);
                glow.FadeEdgeEffectTo(0, 50);
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
