using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;

namespace Hitsounder.Game.Edit.Patterns;

public partial class DrawableSample : CompositeDrawable
{
    public DrawableSample(PatternLayer layer)
    {
        RelativePositionAxes = Axes.X;
        Width = 15;
        Height = 25;
        Anchor = Anchor.CentreLeft;
        Origin = Anchor.CentreLeft;

        InternalChildren =
        [
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 5,
                Colour = ThemeColours.ForSampleSet(layer.Sample?.DefaultSampleSet ?? SampleSet.Auto),
                BorderColour = Color4.Gray,
                BorderThickness = 3,

                EdgeEffect = new EdgeEffectParameters
                {
                    Colour = ThemeColours.ForSampleSet(layer.Sample?.DefaultSampleSet ?? SampleSet.Auto).Opacity(0.3f),
                    Radius = 50,
                    Type = EdgeEffectType.Glow,
                },
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                }
            },
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding { Vertical = 2 },
                Child = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    Width = 5,
                    Masking = true,
                    Child = new Box { RelativeSizeAxes = Axes.Both }
                }
            }
        ];
    }
}
