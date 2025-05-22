using System.Collections.Generic;

namespace Hitsounder.Game.Core.Samples;

public class SampleDirectory : List<ISampleEntry>, ISampleDirectory
{
    public string Name { get; }

    public IList<ISampleEntry> Children => this;

    public SampleDirectory(string name)
    {
        Name = name;
    }
}
