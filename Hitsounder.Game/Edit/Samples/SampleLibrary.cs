using Hitsounder.Game.Core;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleLibrary : CompositeDrawable
{
    [Resolved]
    private Project project { get; set; } = null!;

    private FillFlowContainer content = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        Width = 180;
        RelativeSizeAxes = Axes.Y;

        InternalChildren =
        [
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#222228")
            },
            new BasicScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = content = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                }
            }
        ];

        foreach (var source in project.SampleSources)
        {
            content.Add(new DrawableSampleSource(source));
        }
    }
}
