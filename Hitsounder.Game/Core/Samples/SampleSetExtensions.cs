using System.ComponentModel;

namespace Hitsounder.Game.Core.Samples;

public static class SampleSetExtensions
{
    public static string GetLookupName(this SampleSet sampleSet) => sampleSet switch
    {
        SampleSet.Normal => "normal",
        SampleSet.Soft => "soft",
        SampleSet.Drum => "drum",
        SampleSet.Auto => throw new InvalidEnumArgumentException($"Cannot get sample lookup name for {nameof(SampleSet)} {sampleSet}")
    };

    public static string GetLookupName(this SampleType sampleSet) => sampleSet switch
    {
        SampleType.Normal => "hitnormal",
        SampleType.Whistle => "hitwhistle",
        SampleType.Finish => "hitfinish",
        SampleType.Clap => "hitclap",
        SampleType.SliderSlide => "sliderslide",
        _ => throw new InvalidEnumArgumentException($"Cannot get sample lookup name for {nameof(SampleSet)} {sampleSet}")
    };

    public static string GetLookupName(this (SampleSet, SampleType) value) => $"{value.Item1.GetLookupName()}-{value.Item2.GetLookupName()}";
}
