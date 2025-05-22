using System.ComponentModel;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Database;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace Hitsounder.Game.Core;

public partial class ProjectManager : Component
{
    private readonly GameHost host;
    private readonly Storage storage;
    private readonly IResourceStore<byte[]> resources;
    private readonly AudioManager audio;
    private readonly IResourceStore<byte[]> userFiles;
    private readonly DbAccess db;

    public ProjectManager(Storage storage, DbAccess db, GameHost host, IResourceStore<byte[]> resources, AudioManager audio)
    {
        this.audio = audio;
        this.storage = storage;
        this.host = host;
        this.resources = resources;
        this.db = db;

        userFiles = userFiles = new StorageBackedResourceStore(storage.GetStorageForDirectory("projects"));
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        CreateProject();
    }

    public Project CreateProject()
    {
        var skinResources = new NamespacedResourceStore<byte[]>(resources, "Samples/Gameplay");

        var project = new Project(new SkinSampleSource(skinResources, audio));

        return project;
    }
}
