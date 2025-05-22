using Hitsounder.Game.Core.Patterns;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;

namespace Hitsounder.Game.Edit.Patterns;

public partial class SamplePlaybackIndicator : CompositeDrawable
{
    private readonly PatternLayer layer;

    private readonly Container content;
    private readonly Drawable overlay;
    private readonly Drawable background;

    public SamplePlaybackIndicator(PatternLayer layer)
    {
        this.layer = layer;

        RelativeSizeAxes = Axes.Y;
        Width = 6;
        Anchor = Anchor.TopRight;
        Origin = Anchor.TopRight;
        Padding = new MarginPadding { Horizontal = 1, Vertical = 1 };
        InternalChild = content = new Container
        {
            RelativeSizeAxes = Axes.Both,
            Masking = true,
            CornerRadius = 1.5f,
            Children =
            [
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4Extensions.FromHex("#111117")
                },
                overlay = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Blending = BlendingParameters.Additive,
                    Alpha = 0,
                }
            ],
            EdgeEffect = new EdgeEffectParameters
            {
                Colour = Color4.White,
                Radius = 32,
                Type = EdgeEffectType.Glow
            }
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        layer.SamplePlayed += samplePlayed;

        layer.SampleBindable.BindValueChanged(sample =>
        {
            if (sample.NewValue != null)
            {
                background.Colour = ThemeColours.ForSampleSet(sample.NewValue.DefaultSampleSet).Darken(4f);
                overlay.Colour = ThemeColours.ForSampleSet(sample.NewValue.DefaultSampleSet);
                content.EdgeEffect = content.EdgeEffect with
                {
                    Colour = ThemeColours.ForSampleSet(sample.NewValue.DefaultSampleSet).Opacity(0)
                };
            }
        }, true);
    }

    private void samplePlayed()
    {
        InternalChild.FinishTransforms();
        overlay.FadeTo(1f)
               .FadeOut(400, Easing.Out);

        content.FadeEdgeEffectTo(0.25f)
               .FadeEdgeEffectTo(0, 400, Easing.Out);
    }

    protected override void Dispose(bool isDisposing)
    {
        base.Dispose(isDisposing);

        layer.SamplePlayed -= samplePlayed;
    }
}
