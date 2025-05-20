using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public partial class DrawableSampleSource(ISampleSource source) : CompositeDrawable
{
    private FillFlowContainer content = null!;

    private readonly BindableBool expanded = new BindableBool(false);

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        AutoSizeAxes = Axes.Y;
        Masking = true;
        AutoSizeDuration = 200;
        AutoSizeEasing = Easing.OutExpo;

        InternalChild = new FillFlowContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
            Direction = FillDirection.Vertical,
            Children =
            [
                new SampleSourceHeader(source, expanded),
                content = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                }
            ]
        };

        foreach (var sample in source.Samples)
        {
            content.Add(new DrawableSampleEntry(sample));
        }
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        expanded.BindValueChanged(expanded =>
        {
            if (expanded.NewValue)
            {
                content.BypassAutoSizeAxes = Axes.None;
                content.FadeIn(100);
            }
            else
            {
                content.BypassAutoSizeAxes = Axes.Both;
                content.FadeOut(100);
            }
        }, true);
        Scheduler.AddDelayed(() => FinishTransforms(true), 1);
    }

    private partial class SampleSourceHeader(ISampleSource source, BindableBool expanded) : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            InternalChildren =
            [
                new GridContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    RowDimensions = [new Dimension(GridSizeMode.AutoSize)],
                    ColumnDimensions = [new Dimension(GridSizeMode.AutoSize), new Dimension()],
                    Padding = new MarginPadding(4),
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new SpriteIcon
                            {
                                Size = new Vector2(14),
                                Icon = FontAwesome.Regular.Folder,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Margin = new MarginPadding { Right = 4 }
                            },
                            new SpriteText
                            {
                                Text = source.Name,
                                Font = new FontUsage(size: 16f),
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.X,
                                Truncate = true
                            }
                        }
                    }
                },
                new HoverHighlight(),
            ];
        }

        protected override bool OnClick(ClickEvent e)
        {
            expanded.Toggle();
            return true;
        }
    }
}
