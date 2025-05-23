﻿using System;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Bindables;
using osu.Framework.Lists;

namespace Hitsounder.Game.Core.Patterns;

public class PatternLayer
{
    public readonly Bindable<ISampleFile> SampleBindable = new Bindable<ISampleFile>();

    public readonly SortedList<PatternSample> Samples = new SortedList<PatternSample>();

    public ISampleFile Sample
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

    public event Action? SamplePlayed;

    public void Play()
    {
        var channel = Sample?.Sample?.GetChannel();
        if (channel == null)
            return;

        channel.Volume.Value = Volume;
        channel.Play();

        SamplePlayed?.Invoke();
    }
}
