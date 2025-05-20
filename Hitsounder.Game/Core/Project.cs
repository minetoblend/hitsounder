using System.Collections.Generic;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Graphics;

namespace Hitsounder.Game.Core;

public partial class Project : Component
{
    public readonly ISampleSource SkinSamples;

    public readonly CustomSampleSource CustomSamples = new CustomSampleSource();

    public Project(ISampleSource skinSamples)
    {
        SkinSamples = skinSamples;
    }

    public IEnumerable<ISampleSource> SampleSources => [SkinSamples, CustomSamples];
}
