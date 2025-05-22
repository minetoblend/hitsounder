using System.Collections.Generic;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.IO.Stores;
using osu.Game.Audio;

namespace Hitsounder.Game.Core.Samples;

public class SkinSampleSource : ISampleSource
{
    private readonly ResourceStore<byte[]> store = new ResourceStore<byte[]>();

    private readonly ISampleStore sampleStore;

    public string Name { get; init; } = "Default skin";

    public SkinSampleSource(ResourceStore<byte[]> store, AudioManager audioManager)
    {
        store.AddStore(store);
        store.AddExtension("mp3");
        store.AddExtension("wav");
        store.AddExtension("ogg");

        sampleStore = audioManager.GetSampleStore(store, audioManager.SampleMixer);

        foreach (var bank in HitSampleInfo.ALL_BANKS)
        {
            loadSample(new HitSampleInfo(HitSampleInfo.HIT_NORMAL, bank));

            foreach (var addition in HitSampleInfo.ALL_ADDITIONS)
                loadSample(new HitSampleInfo(addition, bank));
        }
    }

    private void loadSample(HitSampleInfo sampleInfo)
    {
        var sample = sampleStore.Get($"{sampleInfo.Bank}-{sampleInfo.Name}");
        if (sample != null)
            samples.Add(new SkinSample($"{sampleInfo.Bank}-{sampleInfo.Name}", sample, this));
    }

    private readonly List<IHitSoundSample> samples = new List<IHitSoundSample>();

    public IEnumerable<IHitSoundSample> Samples => samples;

    private class SkinSample(string name, ISample sample, ISampleSource source) : IHitSoundSample
    {
        public ISampleSource Source { get; } = source;
        public string Name { get; } = name;
        public ISample Sample { get; } = sample;
    }
}
