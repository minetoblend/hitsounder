using System;
using System.Collections.Generic;
using osu.Framework.Audio.Sample;

namespace Hitsounder.Game.Core.Samples;

public class DefaultSkinSamples : ISampleDirectory
{
    public string Name => "Default Skin";

    public IList<ISampleEntry> Children => children;

    private readonly List<ISampleEntry> children = [];

    public DefaultSkinSamples(ISampleStore samples)
    {
        foreach (var sampleSet in Enum.GetValues<SampleSet>())
        {
            if (sampleSet == SampleSet.Auto)
                continue;

            children.Add(new SampleDirectory(sampleSet.ToString())
            {
                createEntry(sampleSet, SampleType.Normal, true),
                createEntry(sampleSet, SampleType.Whistle, true),
                createEntry(sampleSet, SampleType.Finish, true),
                createEntry(sampleSet, SampleType.Clap, true),
            });
        }

        children.Add(new SampleDirectory("Extras")
        {
            createEntry(SampleSet.Normal, SampleType.SliderSlide),
            createEntry(SampleSet.Soft, SampleType.SliderSlide),
            createEntry(SampleSet.Drum, SampleType.SliderSlide),
        });

        SkinSampleFile createEntry(SampleSet sampleSet, SampleType sampleType, bool useShortName = false) =>
            new(sampleSet, sampleType, samples.Get((sampleSet, sampleType).GetLookupName()));
    }

    private class SkinSampleFile(SampleSet sampleSet, SampleType sampleType, Sample sample, bool useShortName = false) : ISampleFile
    {
        public string Name { get; } = useShortName
            ? sampleType.GetLookupName()
            : (sampleSet, sampleType).GetLookupName();

        public Sample? Sample { get; } = sample;

        public SampleSet DefaultSampleSet { get; } = sampleSet;

        public SampleType DefaultSampleType { get; } = sampleType;
    }
}
