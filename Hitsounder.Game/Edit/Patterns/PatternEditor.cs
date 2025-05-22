using Hitsounder.Game.Core;
using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternEditor : CompositeDrawable
{
    [Resolved]
    private Project project { get; set; } = null!;

    private PatternTimeline timeline = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.Both;

        AddInternal(timeline = new PatternTimeline
        {
            RelativeSizeAxes = Axes.Both,
        });

        foreach (var sample in project.DefaultSkinSamples.GetAllSamples())
        {
            if (sample.DefaultSampleType == SampleType.SliderSlide)
                continue;

            var layer = new PatternLayer { Sample = sample };

            timeline.Layers.Add(layer);
        }
    }
}
