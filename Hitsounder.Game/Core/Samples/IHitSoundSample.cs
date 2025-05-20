using osu.Framework.Audio.Sample;

namespace Hitsounder.Game.Core.Samples;

public interface IHitSoundSample
{
    public ISampleSource Source { get; }

    public string Name { get; }

    public ISample Sample { get; }
}
