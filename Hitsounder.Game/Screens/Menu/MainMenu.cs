using System.Collections.Generic;
using System.Linq;
using Hitsounder.Game.Core;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;

namespace Hitsounder.Game.Screens.Menu;

[LongRunningLoad]
public partial class MainMenu : Screen
{
    [Resolved]
    private ProjectManager projectManager { get; set; } = null!;

    private List<ProjectInfo>? projects;

    [BackgroundDependencyLoader]
    private void load()
    {
        projects = projectManager.GetProjects();

        if (!projects.Any())
        {
            projects.Add(projectManager.Create());
        }

        AddInternal(new BasicFileSelector(null, [".osu"])
        {
            RelativeSizeAxes = Axes.Both
        });
    }
}
