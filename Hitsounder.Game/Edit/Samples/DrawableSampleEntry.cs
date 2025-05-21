using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Graphics;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace Hitsounder.Game.Edit.Samples;

public partial class DrawableSampleEntry(IHitSoundSample sample) : CompositeDrawable
{
    [Resolved]
    private DragOperationHandler dragHandler { get; set; } = null!;

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
                Padding = new MarginPadding(4) { Left = 14 },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new SpriteIcon
                        {
                            Size = new Vector2(12),
                            Icon = FontAwesome.Regular.FileAudio,
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Margin = new MarginPadding { Right = 4 }
                        },
                        new SpriteText
                        {
                            Text = sample.Name,
                            Font = new FontUsage(size: 14f),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            RelativeSizeAxes = Axes.X,
                            Truncate = true
                        }
                    }
                },
            },
            new HoverHighlight(),
        ];
    }

    private SampleChannel? currentPlayback;

    protected override bool OnClick(ClickEvent e)
    {
        if (currentPlayback is { Playing: true })
            currentPlayback.Stop();

        currentPlayback = sample.Sample.Play();

        return true;
    }

    protected override bool OnDragStart(DragStartEvent e)
    {
        dragHandler.BeginDragOperation(this, sample, new DraggedSample(sample));
        return true;
    }

    public object GetDragData() => sample;
}
