using System;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Input;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace Hitsounder.Game.Edit.Patterns;

public partial class PatternLayerInsertionDropArea : CompositeDrawable, IDragEventHandler<ISampleFile>
{
    public Action<ISampleFile>? SampleDropped;

    public PatternLayerInsertionDropArea()
    {
        RelativeSizeAxes = Axes.X;
        Height = 10;
        Margin = new MarginPadding { Vertical = -Height / 2f };
        Padding = new MarginPadding { Horizontal = 10 };
        InternalChild = new CircularContainer
        {
            RelativeSizeAxes = Axes.X,
            Height = 2f,
            Alpha = 0f,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
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
        SampleDropped?.Invoke(e.Data);
        return true;
    }

    protected override bool OnClick(ClickEvent e) => true;
}
