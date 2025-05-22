using System;
using System.Collections.Generic;
using System.Linq;
using Hitsounder.Game.Core.Samples;

namespace Hitsounder.Game.Edit.Samples;

public abstract class SampleTreeEntry : IComparable<SampleTreeEntry>
{
    public readonly string Name;

    protected SampleTreeEntry(string name)
    {
        Name = name;
    }

    public int CompareTo(SampleTreeEntry? other)
    {
        return string.Compare(Name, other?.Name, StringComparison.InvariantCulture);
    }
}

public class SampleTree : SampleTreeEntry
{
    public readonly Dictionary<string, SampleTreeEntry> Children = new Dictionary<string, SampleTreeEntry>();

    public SampleTree(string name)
        : base(name)
    {
    }

    public static SampleTree FromCollection(ISampleCollection collection)
    {
        var samples = collection.Samples.ToList();

        var root = new SampleTree(collection.Name);

        foreach (var sample in samples)
        {
            var pathSegments = sample.Name.Split("/");

            var tree = getForPath(pathSegments.AsSpan().Slice(0, pathSegments.Length - 1), root);

            if (tree != null)
                tree.Children[sample.Name] = new SampleTreeSample(sample);
        }

        return root;

        SampleTree? getForPath(ReadOnlySpan<string> segments, SampleTree current)
        {
            if (segments.IsEmpty)
                return current;

            string currentSegment = segments[0];

            if (current.Children.TryGetValue(currentSegment, out var child))
            {
                if (child is not SampleTree childTree)
                    return null;

                return getForPath(segments.Slice(1), childTree);
            }

            var newEntry = new SampleTree(currentSegment);

            current.Children[newEntry.Name] = newEntry;

            return newEntry;
        }
    }
}

public class SampleTreeSample : SampleTreeEntry
{
    public readonly ISampleCollectionEntry Sample;

    public SampleTreeSample(ISampleCollectionEntry sample)
        : base(sample.ShortName)
    {
        Sample = sample;
    }
}
