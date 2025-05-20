using Hitsounder.Game.Core.Samples;
using osu.Framework.Bindables;

namespace Hitsounder.Game.Core.Patterns;

public class PatternLayer
{
    public readonly Bindable<IHitSoundSample> SampleBindable = new Bindable<IHitSoundSample>();

    public IHitSoundSample Sample
    {
        get => SampleBindable.Value;
        set => SampleBindable.Value = value;
    }

    public readonly Bindable<float> VolumeBindable = new BindableFloat
    {
        Value = 1f,
        Default = 1f,
        MinValue = 0f,
        MaxValue = 1.5f,
    };

    public float Volume
    {
        get => VolumeBindable.Value;
        set => VolumeBindable.Value = value;
    }

    public readonly BindableBool EnabledBindable = new BindableBool(true);

    public bool Enabled
    {
        get => EnabledBindable.Value;
        set => EnabledBindable.Value = value;
    }
}
