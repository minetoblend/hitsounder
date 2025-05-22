using System.IO;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace Hitsounder.Game.Core.Samples;

public class DatabaseBackedSampleCollection : ISampleCollection
{
    public string Name => collection.Name;

    public IBindableList<ISampleCollectionEntry> Samples => samples;

    private readonly SampleCollectionInfo collection;

    private readonly ISampleStore sampleStore;

    private readonly IResourceStore<byte[]> store;

    private readonly BindableList<ISampleCollectionEntry> samples = new BindableList<ISampleCollectionEntry>();

    public DatabaseBackedSampleCollection(SampleCollectionInfo collection, AudioManager audioManager)
    {
        this.collection = collection;

        store = new StorageBackedResourceStore(new NativeStorage("/"));

        sampleStore = audioManager.GetSampleStore(store);

        foreach (var sampleInfo in collection.Samples)
        {
            var sample = sampleStore.Get(sampleInfo.Path);

            samples.Add(new SampleEntry(sampleInfo, sample, store));
        }
    }

    public void Dispose()
    {
        sampleStore.Dispose();
    }

    private class SampleEntry(HitSoundSampleInfo sampleInfo, Sample? sample, IResourceStore<byte[]> store) : ISampleCollectionEntry
    {
        public string Name => sampleInfo.Name;

        public bool Available => sample != null;

        public Sample? Sample => sample;

        public SampleSet DefaultSampleSet => sampleInfo.DefaultSampleSet;

        public SampleType DefaultSampleType => sampleInfo.DefaultSampleType;

        public Stream? GetStream() => store.GetStream(sampleInfo.Path);
    }
}
