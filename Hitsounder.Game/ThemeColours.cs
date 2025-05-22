using System;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Extensions.Color4Extensions;
using osuTK.Graphics;

namespace Hitsounder.Game;

public class ThemeColours
{
    public static Color4 ForSampleSet(SampleSet sampleSet)
    {
        return sampleSet switch
        {
            SampleSet.Normal => Color4Extensions.FromHex("#5cf7e5"),
            SampleSet.Soft => Color4Extensions.FromHex("#923ad6"),
            SampleSet.Drum => Color4Extensions.FromHex("#f7c143"),
            SampleSet.Auto => Color4.Gray,

            _ => throw new ArgumentOutOfRangeException(nameof(sampleSet), sampleSet, null)
        };
    }
}
