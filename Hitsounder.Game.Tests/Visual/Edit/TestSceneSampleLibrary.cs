using Hitsounder.Game.Core;
using Hitsounder.Game.Edit.Samples;
using osu.Framework.Allocation;

namespace Hitsounder.Game.Tests.Visual.Edit;

public partial class TestSceneSampleLibrary : HitsounderTestScene
{
    [Resolved]
    private ProjectManager projectManager { get; set; } = null!;

    private DependencyContainer dependencies;

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
        dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

    [BackgroundDependencyLoader]
    private void load()
    {
        dependencies.Cache(projectManager.CreateProject());

        Add(new SampleLibrary());
    }
}
