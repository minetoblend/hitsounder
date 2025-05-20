using System.Collections.Generic;
using osu.Framework.Audio.Sample;
using osu.Game.Audio;
using osu.Game.Skinning;

namespace Hitsounder.Game.Core.Samples;

public class SkinSampleSource : ISampleSource
{
    private readonly Skin skin;

    public string Name => skin.Name;

    public SkinSampleSource(Skin skin)
    {
        this.skin = skin;

        foreach (var bank in HitSampleInfo.ALL_BANKS)
        {
            loadSample(new HitSampleInfo(HitSampleInfo.HIT_NORMAL, bank));

            foreach (var addition in HitSampleInfo.ALL_ADDITIONS)
                loadSample(new HitSampleInfo(addition, bank));

            // loadSample(new HitSampleInfo("sliderslide", bank));
        }
    }

    private void loadSample(HitSampleInfo sampleInfo)
    {
        var sample = skin.GetSample(sampleInfo);
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
