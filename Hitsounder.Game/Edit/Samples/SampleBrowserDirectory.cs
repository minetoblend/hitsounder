using Hitsounder.Game.Core.Samples;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleBrowserDirectory(ISampleDirectory samples) : SampleBrowserItem
{
    private readonly Bindable<bool> expanded = new Bindable<bool>();

    private FillFlowContainer childFlow = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        AutoSizeAxes = Axes.Y;

        InternalChildren =
        [
            new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Masking = true,
                AutoSizeDuration = 100,
                AutoSizeEasing = Easing.OutExpo,
                Children =
                [
                    new SampleBrowserGroupHeader(samples.Name, expanded, IndentPadding),
                    childFlow = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                    }
                ]
            }
        ];
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        expanded.BindValueChanged(expanded =>
        {
            if (expanded.NewValue)
                ensureChildren();

            childFlow.BypassAutoSizeAxes = expanded.NewValue ? Axes.None : Axes.Both;
        }, true);

        Scheduler.AddDelayed(() => InternalChild.FinishTransforms(), 1);
    }

    private void ensureChildren()
    {
        if (childFlow.Count > 0)
            return;

        foreach (var entry in samples.Children)
        {
            switch (entry)
            {
                case ISampleFile sample:
                    childFlow.Insert(1, new SampleBrowserSample(sample));
                    break;

                case ISampleDirectory childDirectory:
                    childFlow.Insert(0, new SampleBrowserDirectory(childDirectory));
                    break;
            }
        }
    }
}
