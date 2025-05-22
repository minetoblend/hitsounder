using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Graphics;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public partial class SampleBrowserSample(ISampleFile sample) : SampleBrowserItem
{
    [Resolved]
    private DragOperationHandler dragHandler { get; set; } = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        AddRangeInternal([
            new Container
            {
                Padding = IndentPadding,
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Child = new GridContainer
                {
                    Padding = new MarginPadding(4),
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    RowDimensions = [new Dimension(GridSizeMode.AutoSize)],
                    ColumnDimensions = [new Dimension(GridSizeMode.AutoSize), new Dimension()],
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new SpriteIcon
                            {
                                Size = new Vector2(14),
                                Icon = FontAwesome.Regular.FileAudio,
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Margin = new MarginPadding { Right = 4 },
                                Colour = ThemeColours.ForSampleSet(sample.DefaultSampleSet)
                            },
                            new SpriteText
                            {
                                Text = sample.Name,
                                Font = new FontUsage(size: 16f),
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.X,
                                Truncate = true,
                            }
                        }
                    },
                },
            },
            new HoverHighlight(),
        ]);

        if (!sample.Available)
            Alpha = 0.5f;
    }

    public override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
    {
        return sample.Available && base.ReceivePositionalInputAt(screenSpacePos);
    }

    protected override bool OnDragStart(DragStartEvent e)
    {
        dragHandler.BeginDragOperation(this, sample, new DraggedSample(sample));
        return true;
    }

    protected override bool OnClick(ClickEvent e)
    {
        if (sample.Sample != null)
        {
            sample.Sample.Play();
            var box = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Alpha = 0.25f,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
            AddInternal(box);

            box.FadeOut(300, Easing.Out);
        }

        return base.OnClick(e);
    }
}
