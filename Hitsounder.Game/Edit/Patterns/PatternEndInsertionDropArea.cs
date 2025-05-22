using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Graphics.Containers;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternEndInsertionDropArea : TimelineLayer<PatternLayer>
{
    public PatternEndInsertionDropArea()
        : base(new DummyLayer(), 30)
    {
    }

    private class DummyLayer : PatternLayer;

    protected override TimelineLayerHeader CreateHeader() => new DropHeader();

    protected override Drawable CreateTimelineContent() => Empty();

    private partial class DropHeader : TimelineLayerHeader, IDragEventHandler<ISampleFile>
    {
        [Resolved]
        private PatternTimeline timeline { get; set; } = null!;

        public DropHeader()
        {
            Padding = new MarginPadding { Horizontal = 10 };
            InternalChild = new CircularContainer
            {
                RelativeSizeAxes = Axes.X,
                Height = 2f,
                Alpha = 0f,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Masking = true,
                Child = new Box { RelativeSizeAxes = Axes.Both, }
            };
        }

        public bool OnDragEnter(EditorDragEvent<ISampleFile> e)
        {
            InternalChild.Alpha = 0.5f;
            return true;
        }

        public void OnDragLeave(EditorDragEvent<ISampleFile> e)
        {
            InternalChild.Alpha = 0;
        }

        public bool OnDrop(EditorDragEvent<ISampleFile> e)
        {
            timeline.Layers.Add(new PatternLayer
            {
                Sample = e.Data
            });
            return false;
        }

        public override bool HandlePositionalInput => true;
    }
}
