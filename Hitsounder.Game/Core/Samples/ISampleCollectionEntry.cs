using System.IO;
using osu.Framework.Audio.Sample;

namespace Hitsounder.Game.Core.Samples;

public interface ISampleCollectionEntry
{
    public string Name { get; }

    public string ShortName => Path.GetFileName(Name);

    public bool Available { get; }

    public Sample? Sample { get; }

    public SampleSet DefaultSampleSet { get; }

    public SampleType DefaultSampleType { get; }
}
