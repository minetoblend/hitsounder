using Hitsounder.Game.Core;
using Hitsounder.Game.Edit;
using NUnit.Framework;
using osu.Framework.Allocation;

namespace Hitsounder.Game.Tests.Visual.Edit;

public partial class TestSceneHitSoundEditor : ScreenTestScene
{
    [Resolved]
    private ProjectManager projectManager { get; set; }

    [Test]
    public void TestHitSoundEditor()
    {
        AddStep("Load editor", () => LoadScreen(new HitSoundEditor(projectManager.CreateProject())));
    }
}
