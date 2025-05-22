using osu.Framework.Audio.Sample;

namespace Hitsounder.Game.Core.Samples;

public interface ISampleFile : ISampleEntry
{
    public Sample? Sample { get; }

    public bool Available => Sample != null;

    public SampleSet DefaultSampleSet { get; }

    public SampleType DefaultSampleType { get; }
}
