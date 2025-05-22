using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleBrowserDirectory(SampleTree samples) : SampleBrowserItem
{
    private readonly Bindable<bool> expanded = new Bindable<bool>();

    private FillFlowContainer childFlow = null!;

    private FillFlowContainer content = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        AutoSizeAxes = Axes.Y;

        InternalChildren =
        [
            content = new FillFlowContainer
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

        foreach (var entry in samples.Children.Values)
        {
            switch (entry)
            {
                case SampleTreeSample sample:
                    childFlow.Insert(1, new SampleBrowserSample(sample.Sample));
                    break;

                case SampleTree childTree:
                    childFlow.Insert(0, new SampleBrowserDirectory(childTree));
                    break;
            }
        }
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        expanded.BindValueChanged(expanded =>
        {
            childFlow.BypassAutoSizeAxes = expanded.NewValue ? Axes.None : Axes.Both;
        }, true);

        Scheduler.AddDelayed(() => InternalChild.FinishTransforms(), 1);
    }
}
