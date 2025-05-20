using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternLayerHeader : RearrangeableListItem<PatternLayer>
{
    private Drawable dragHandle = null!;
    private Box background = null!;

    [Resolved]
    private PatternLayerContainer layerContainer { get; set; } = null!;

    public PatternLayerHeader(PatternLayer item)
        : base(item)
    {
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        AutoSizeAxes = Axes.Y;

        InternalChildren =
        [
            background = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Alpha = 0,
            },
            new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Children =
                [
                    new PatternLayerInsertionDropArea
                    {
                        SampleDropped = sample =>
                        {
                            var index = layerContainer.Items.IndexOf(Model);

                            if (index >= 0)
                            {
                                layerContainer.Items.Insert(index, new PatternLayer
                                {
                                    Sample = sample
                                });
                            }
                        }
                    },
                    new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Horizontal,
                        Padding = new MarginPadding(4),
                        Spacing = new Vector2(4),
                        Children =
                        [
                            dragHandle = new Container
                            {
                                Size = new Vector2(20),
                                Margin = new MarginPadding { Horizontal = -5 },
                                Masking = true,
                                Child = new SpriteIcon
                                {
                                    Size = new Vector2(12),
                                    Icon = FontAwesome.Solid.GripLinesVertical,
                                    Alpha = 0.25f,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                },
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                            new LEDToggle
                            {
                                Current = Model.EnabledBindable,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Padding = new MarginPadding(4)
                            },
                            new VolumeKnob
                            {
                                Current = Model.VolumeBindable,
                                StepSize = 0.1f,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                            new SampleSelectButton(Model)
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                        ]
                    },
                ]
            }
        ];
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() => FinishTransforms(), 1);
    }

    protected override bool OnDragStart(DragStartEvent e)
    {
        if (!base.OnDragStart(e))
            return false;

        background.FadeTo(0.1f, 100);

        return true;
    }

    protected override void OnDragEnd(DragEndEvent e)
    {
        base.OnDragEnd(e);

        background.FadeOut(100);
    }

    protected override bool IsDraggableAt(Vector2 screenSpacePos) => dragHandle.Contains(screenSpacePos);
}
