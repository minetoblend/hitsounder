using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Graphics.Containers;
using Hitsounder.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternLayerHeader(PatternLayer layer) : TimelineLayerHeader, IHasContextMenu
{
    private Drawable dragHandle = null!;
    private Box background = null!;

    [Resolved]
    private PatternTimeline timeline { get; set; } = null!;

    private Drawable sampleIndicator = null!;

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
                            var index = timeline.Layers.IndexOf(layer);

                            if (index >= 0)
                            {
                                timeline.Layers.Insert(index, new PatternLayer
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
                                    Colour = Color4.Black,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                },
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                            new LEDToggle
                            {
                                Current = layer.EnabledBindable,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                            new VolumeKnob
                            {
                                Current = layer.VolumeBindable,
                                StepSize = 0.1f,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                            new SampleSelectButton(layer)
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                        ]
                    },
                ]
            },
            sampleIndicator = new SamplePlaybackIndicator(layer)
        ];
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Scheduler.AddDelayed(() => FinishTransforms(), 1);
    }

    public void OnDragStart()
    {
        background.FadeTo(0.1f, 100);
    }

    public void OnDragEnd()
    {
        background.FadeOut(100);
    }

    public override bool IsDraggableAt(Vector2 screenSpacePos) => dragHandle.Contains(screenSpacePos);

    public MenuItem[]? ContextMenuItems =>
    [
        new MenuItem("Delete", () => timeline.Layers.Remove(layer))
    ];
}
