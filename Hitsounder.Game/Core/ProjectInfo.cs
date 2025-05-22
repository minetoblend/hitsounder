using System;
using System.Collections.Generic;
using Hitsounder.Game.Core.Samples;
using Microsoft.EntityFrameworkCore;

namespace Hitsounder.Game.Core;

[PrimaryKey(nameof(ProjectId))]
[Index(nameof(Name))]
public class ProjectInfo
{
    public Guid ProjectId { get; set; }

    public required string Name { get; set; }

    public ICollection<SampleCollectionInfo> SampleCollections { get; set; } = new List<SampleCollectionInfo>();
}
