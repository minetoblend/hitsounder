using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osu.Framework.Testing;

namespace Hitsounder.Game.Tests.Visual;

public abstract partial class ScreenTestScene : HitsounderTestScene
{
    protected readonly ScreenStack Stack;

    private readonly Container content;

    protected override Container<Drawable> Content => content;

    protected ScreenTestScene()
    {
        base.Content.AddRange([
            Stack = new ScreenStack
            {
                Name = nameof(ScreenTestScene),
                RelativeSizeAxes = Axes.Both
            },
            content = new Container { RelativeSizeAxes = Axes.Both },
        ]);

        Stack.ScreenPushed += (_, newScreen) => Logger.Log($"{nameof(ScreenTestScene)} screen changed → {newScreen}");
        Stack.ScreenExited += (_, newScreen) => Logger.Log($"{nameof(ScreenTestScene)} screen changed ← {newScreen}");
    }

    protected void LoadScreen(Screen screen) => Stack.Push(screen);

    [SetUpSteps]
    public virtual void SetUpSteps() => addExitAllScreensStep();

    [TearDownSteps]
    public virtual void TearDownSteps()
    {
        if (DebugUtils.IsNUnitRunning)
            addExitAllScreensStep();
    }

    private void addExitAllScreensStep()
    {
        AddUntilStep("exit all screens", () =>
        {
            if (Stack.CurrentScreen == null) return true;

            Stack.Exit();
            return false;
        });
    }
}
