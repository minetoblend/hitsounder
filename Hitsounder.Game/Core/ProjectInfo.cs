using System;
using Microsoft.EntityFrameworkCore;

namespace Hitsounder.Game.Core;

[PrimaryKey(nameof(ProjectId))]
[Index(nameof(Name))]
public class ProjectInfo
{
    public Guid ProjectId { get; set; }

    public required string Name { get; set; }
}
