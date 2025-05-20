using System;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Input;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternLayerInsertionDropArea : CompositeDrawable, IDragEventHandler<IHitSoundSample>
{
    public Action<IHitSoundSample>? SampleDropped;

    public PatternLayerInsertionDropArea()
    {
        RelativeSizeAxes = Axes.X;
        Height = 10;
        Margin = new MarginPadding { Vertical = -Height / 2f };
        InternalChild = new Box
        {
            RelativeSizeAxes = Axes.X,
            Height = 1f,
            Alpha = 0f,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
        };
    }

    public bool OnDragEnter(EditorDragEvent<IHitSoundSample> e)
    {
        InternalChild.Alpha = 0.5f;

        return true;
    }

    public void OnDragLeave(EditorDragEvent<IHitSoundSample> e)
    {
        InternalChild.Alpha = 0;
    }

    public bool OnDrop(EditorDragEvent<IHitSoundSample> e)
    {
        SampleDropped?.Invoke(e.Data);
        return true;
    }

    protected override bool OnClick(ClickEvent e) => true;
}
