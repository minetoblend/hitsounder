using System;
using osu.Framework.Bindables;

namespace Hitsounder.Game.Core.Samples;

public interface ISampleCollection : IDisposable
{
    public string Name { get; }

    public IBindableList<ISampleCollectionEntry> Samples { get; }
}
