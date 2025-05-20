using Hitsounder.Game.Core;
using Hitsounder.Game.Core.Patterns;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternEditor : CompositeDrawable
{
    [Resolved]
    private Project project { get; set; } = null!;

    private PatternLayerContainer layers = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.Both;

        AddInternal(layers = new PatternLayerContainer
        {
            RelativeSizeAxes = Axes.Y,
            Width = 180,
        });

        foreach (var sample in project.SkinSamples.Samples)
        {
            var layer = new PatternLayer { Sample = sample };

            layers.Items.Add(layer);
        }
    }
}
