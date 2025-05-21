using System;

namespace Hitsounder.Game.Core.Patterns;

public class PatternSample : IComparable<PatternSample>
{
    public double StartTime;

    public int CompareTo(PatternSample? other) => StartTime.CompareTo(other?.StartTime);
}
