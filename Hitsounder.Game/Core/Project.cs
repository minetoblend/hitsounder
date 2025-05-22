using System.Collections.Generic;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Graphics;

namespace Hitsounder.Game.Core;

public partial class Project : Component
{
    public readonly ISampleDirectory DefaultSkinSamples;

    public Project(DefaultSkinSamples defaultSkinSamples)
    {
        DefaultSkinSamples = defaultSkinSamples;
    }

    public IEnumerable<ISampleDirectory> SampleCollections => [DefaultSkinSamples];
}
