using System.Collections.Generic;

namespace Hitsounder.Game.Core.Samples;

public class CustomSampleSource : ISampleSource
{
    public string Name => "Custom";

    private readonly List<IHitSoundSample> samples = new List<IHitSoundSample>();
    public IEnumerable<IHitSoundSample> Samples => samples;
}
