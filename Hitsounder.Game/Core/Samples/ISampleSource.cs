using System.Collections.Generic;

namespace Hitsounder.Game.Core.Samples;

public interface ISampleSource
{
    public string Name { get; }

    public IEnumerable<IHitSoundSample> Samples { get; }
}
