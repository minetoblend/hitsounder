using System.Collections.Generic;

namespace Hitsounder.Game.Core.Samples;

public interface ISampleDirectory : ISampleEntry
{
    public IList<ISampleEntry> Children { get; }

    public IEnumerable<ISampleFile> GetAllSamples()
    {
        foreach (var entry in Children)
        {
            if (entry is ISampleFile sampleFile)
                yield return sampleFile;

            if (entry is ISampleDirectory directory)
            {
                foreach (var nestedSample in directory.GetAllSamples())
                    yield return nestedSample;
            }
        }
    }
}
