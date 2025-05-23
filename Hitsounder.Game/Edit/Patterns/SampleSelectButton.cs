using Hitsounder.Game.Core.Patterns;
using Hitsounder.Game.Core.Samples;
using Hitsounder.Game.Graphics;
using Hitsounder.Game.Input;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Hitsounder.Game.Edit.Patterns;

public partial class SampleSelectButton(PatternLayer layer) : CompositeDrawable, IDragEventHandler<ISampleFile>
{
    private SpriteText name = null!;

    private Bindable<ISampleFile> sample = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Y;
        Width = 110;
        Masking = true;
        CornerRadius = 3;

        InternalChildren =
        [
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4Extensions.FromHex("#22222a")
            },
            (name = new SpriteText
            {
                RelativeSizeAxes = Axes.X,
                Font = new FontUsage("Inter", size: 14, weight: "Regular"),
                Padding = new MarginPadding { Horizontal = 6, Vertical = 4 },
                Colour = Color4Extensions.FromHex("#68cce8"),
                Truncate = true,
                Shadow = true,
            }).WithEffect(new GlowEffect
            {
                Colour = Color4Extensions.FromHex("#68cce8"),
                BlurSigma = new Vector2(10),
                Strength = 3f
            }),
            new HoverHighlight()
        ];

        sample = layer.SampleBindable.GetBoundCopy();
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        sample.BindValueChanged(sample =>
        {
            name.Text = sample.NewValue?.Name ?? "Select sample";
        }, true);
    }

    public bool OnDragEnter(EditorDragEvent<ISampleFile> e)
    {
        this.TransformTo(nameof(BorderThickness), 2f, 100);
        BorderColour = Color4.Gray;

        return true;
    }

    public void OnDragLeave(EditorDragEvent<ISampleFile> e)
    {
        this.TransformTo(nameof(BorderThickness), 0f, 100);
    }

    public bool OnDrop(EditorDragEvent<ISampleFile> e)
    {
        layer.Sample = e.Data;
        return true;
    }

    protected override bool OnClick(ClickEvent e)
    {
        layer.Play();

        return false;
    }
}
