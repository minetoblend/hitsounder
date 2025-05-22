using System.Collections.Generic;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Graphics;

namespace Hitsounder.Game.Core;

public partial class Project : Component
{
    public readonly ISampleCollection SkinSamples;

    public Project(ISampleCollection skinSamples)
    {
        SkinSamples = skinSamples;
    }

    public IEnumerable<ISampleCollection> SampleCollections => [SkinSamples];
}
