using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Database;
using osu.Framework.Audio;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace Hitsounder.Game.Core;

public class ProjectManager
{
    private readonly GameHost host;
    private readonly Storage storage;
    private readonly IResourceStore<byte[]> resources;
    private readonly AudioManager audio;
    private readonly IResourceStore<byte[]> userFiles;
    private readonly DbAccess db;

    private readonly DefaultSkinSamples defaultSkinSamples;

    public ProjectManager(Storage storage, DbAccess db, GameHost host, IResourceStore<byte[]> resources, AudioManager audio)
    {
        this.audio = audio;
        this.storage = storage;
        this.host = host;
        this.resources = resources;
        this.db = db;

        userFiles = userFiles = new StorageBackedResourceStore(storage.GetStorageForDirectory("projects"));

        var skinResources = new NamespacedResourceStore<byte[]>(resources, "Skins/Legacy");

        defaultSkinSamples = new DefaultSkinSamples(audio.GetSampleStore(skinResources));
    }

    public Project CreateProject()
    {
        var project = new Project(defaultSkinSamples);

        return project;
    }

    public List<ProjectInfo> GetProjects(CancellationToken cancellationToken = default)
    {
        return db.Context.Projects.ToList();
    }

    public ProjectInfo Create(string name = "Untitled")
    {
        Logger.Log($"Creating new project {name}");

        var project = new ProjectInfo { Name = name, };

        db.Write(ctx => ctx.Projects.Add(project));

        return project;
    }
}
