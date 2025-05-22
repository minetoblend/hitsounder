using System.Collections.Generic;
using Hitsounder.Game.Core.Samples;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Framework.Threading;
using osu.Game.Database;
using osu.Game.IO;
using osu.Game.Skinning;

namespace Hitsounder.Game.Core;

public partial class ProjectManager : CompositeComponent, IStorageResourceProvider
{
    private readonly GameHost host;
    private readonly Storage storage;
    private readonly IResourceStore<byte[]> resources;
    private readonly AudioManager audio;
    private readonly Scheduler scheduler;
    private readonly IResourceStore<byte[]> userFiles;
    private readonly RealmAccess realm;

    private DefaultLegacySkin defaultSkin = null!;

    public ProjectManager(Storage storage, RealmAccess realm, GameHost host, IResourceStore<byte[]> resources, AudioManager audio, Scheduler scheduler)
    {
        this.audio = audio;
        this.storage = storage;
        this.host = host;
        this.resources = resources;
        this.scheduler = scheduler;
        this.realm = realm;

        userFiles = userFiles = new StorageBackedResourceStore(storage.GetStorageForDirectory("projects"));
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        defaultSkin = new DefaultLegacySkin(this);

        CreateProject();
    }

    public Project CreateProject()
    {
        var skinResources = new NamespacedResourceStore<byte[]>(resources, "Samples/Gameplay");

        var project = new Project(new SkinSampleSource(skinResources, audio));

        return project;
    }

    #region IResourceStorageProvider

    IRenderer IStorageResourceProvider.Renderer => host.Renderer;
    AudioManager IStorageResourceProvider.AudioManager => audio;
    IResourceStore<byte[]> IStorageResourceProvider.Resources => resources;
    IResourceStore<byte[]> IStorageResourceProvider.Files => userFiles;
    RealmAccess IStorageResourceProvider.RealmAccess => realm;
    IResourceStore<TextureUpload> IStorageResourceProvider.CreateTextureLoaderStore(IResourceStore<byte[]> underlyingStore) => host.CreateTextureLoaderStore(underlyingStore);

    #endregion

    public IEnumerable<ProjectInfo> GetProjects()
    {
        return new List<ProjectInfo>();
    }
}
