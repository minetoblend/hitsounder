using Hitsounder.Game.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Game.Extensions;
using osuTK;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternTickDisplay : CompositeDrawable
{
    [Resolved]
    private ITimeline timeline { get; set; } = null!;

    private TimelineContent<Box> tickContainer;
    private TimelineContent<SpriteText> timestampContainer;

    public PatternTickDisplay()
    {
        RelativeSizeAxes = Axes.Both;
        InternalChildren =
        [
            tickContainer = new TimelineContent<Box>(),
            timestampContainer = new TimelineContent<SpriteText>(),
        ];
    }

    private readonly double beatLength = 333;

    private readonly int divisor = 4;

    private int tickIndex;
    private int timestampIndex;

    protected override void Update()
    {
        base.Update();

        double padding = timeline.SizeToDuration(100);

        double startTime = timeline.StartTime - padding;
        double endTime = startTime + timeline.SizeToDuration(DrawWidth) + padding * 2;

        tickIndex = 0;
        timestampIndex = 0;

        int beat = 0;

        double lastTimestampTime = double.MinValue;
        double minTimestampDistance = timeline.SizeToDuration(50);

        for (double t = 0; t < timeline.TotalDuration; t += beatLength / divisor)
        {
            if (t < startTime)
            {
                beat++;
                continue;
            }

            float xPos = (float)t;

            if (t > endTime)
                break;

            var line = getNextLine();

            line.X = xPos;

            if (beat % divisor == 0)
            {
                line.Y = 0;
                line.Width = 1;
                line.Alpha = 0.2f;

                if (t - lastTimestampTime > minTimestampDistance)
                {
                    lastTimestampTime = t;

                    var timestamp = getNextTimestamp();
                    timestamp.Text = t.ToEditorFormattedString();
                    timestamp.X = xPos;
                }
            }
            else
            {
                line.Y = 12;
                line.Width = 0.5f;
                line.Alpha = 0.1f;
            }

            beat++;
        }

        while (tickIndex < tickContainer.Children.Count)
            tickContainer.Children[tickIndex++].Expire();

        while (timestampIndex < timestampContainer.Children.Count)
            timestampContainer.Children[timestampIndex++].Expire();
    }

    private Box getNextLine()
    {
        if (tickIndex < tickContainer.Count)
            return tickContainer.Children[tickIndex++];

        tickIndex++;

        var line = new Box
        {
            RelativeSizeAxes = Axes.Y,
            RelativePositionAxes = Axes.X,
            Width = 1,
            Origin = Anchor.TopCentre,
            EdgeSmoothness = new Vector2(0.5f, 0),
            Alpha = 0.5f,
        };

        tickContainer.Add(line);
        return line;
    }

    private SpriteText getNextTimestamp()
    {
        if (timestampIndex < timestampContainer.Count)
            return timestampContainer.Children[timestampIndex++];

        timestampIndex++;

        var text = new SpriteText
        {
            Font = new FontUsage(size: 11),
            RelativePositionAxes = Axes.X,
            Padding = new MarginPadding { Left = 4 }
        };

        timestampContainer.Add(text);
        return text;
    }
}
