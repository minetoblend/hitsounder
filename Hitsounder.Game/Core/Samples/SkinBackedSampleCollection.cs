using System;
using System.ComponentModel;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.IO.Stores;

namespace Hitsounder.Game.Core.Samples;

public class SkinBackedSampleCollection : ISampleCollection
{
    public string Name { get; init; } = "Default skin";

    public IBindableList<ISampleCollectionEntry> Samples => samples;

    private readonly BindableList<ISampleCollectionEntry> samples = new BindableList<ISampleCollectionEntry>();

    private readonly ResourceStore<byte[]> store = new ResourceStore<byte[]>();

    private readonly ISampleStore sampleStore;

    public SkinBackedSampleCollection(ResourceStore<byte[]> resourceStore, AudioManager audioManager)
    {
        store.AddStore(resourceStore);
        store.AddExtension("mp3");
        store.AddExtension("wav");
        store.AddExtension("ogg");

        sampleStore = audioManager.GetSampleStore(this.store, audioManager.SampleMixer);

        foreach (var sampleSet in Enum.GetValues<SampleSet>())
        {
            if (sampleSet == SampleSet.Auto)
                continue;

            foreach (var sampleType in Enum.GetValues<SampleType>())
            {
                if (sampleType == SampleType.None)
                    continue;

                loadSample(sampleSet, sampleType);
            }
        }
    }

    private void loadSample(SampleSet sampleSet, SampleType sampleType)
    {
        string lookupName = $"{getBankName(sampleSet)}-{getSampleTypeName(sampleType)}";

        var sample = sampleStore.Get(lookupName);

        var path = $"{sampleSet}/{sampleType}";

        samples.Add(new SkinSample(path, sample, sampleSet, sampleType));
    }

    private string getBankName(SampleSet sampleSet) => sampleSet switch
    {
        SampleSet.Normal => "normal",
        SampleSet.Soft => "soft",
        SampleSet.Drum => "drum",
        _ => throw new InvalidEnumArgumentException($"Invalid {nameof(SampleSet)} {sampleSet}")
    };

    private string getSampleTypeName(SampleType type) => type switch
    {
        SampleType.Normal => "hitnormal",
        SampleType.Whistle => "hitwhistle",
        SampleType.Finish => "hitfinish",
        SampleType.Clap => "hitclap",
        _ => throw new InvalidEnumArgumentException($"Invalid {nameof(SampleType)} {type}")
    };

    public void Dispose()
    {
        store.Dispose();
        sampleStore.Dispose();
    }

    private class SkinSample(string name, Sample? sample, SampleSet sampleSet, SampleType sampleType) : ISampleCollectionEntry
    {
        public string Name { get; } = name;
        public Sample? Sample { get; } = sample;

        public bool Available => Sample != null;

        public SampleSet DefaultSampleSet { get; } = sampleSet;

        public SampleType DefaultSampleType { get; } = sampleType;
    }
}
