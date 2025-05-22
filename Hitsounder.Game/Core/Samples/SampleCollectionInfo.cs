using System;
using System.Collections.Generic;

namespace Hitsounder.Game.Core.Samples;

public class SampleCollectionInfo
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required bool Global { get; set; }

    public ICollection<HitSoundSampleInfo> Samples { get; set; } = new List<HitSoundSampleInfo>();
}

public class HitSoundSampleInfo
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Path { get; set; }

    public SampleSet DefaultSampleSet { get; set; }

    public SampleType DefaultSampleType { get; set; }
}
