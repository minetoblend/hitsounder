using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK.Graphics;

namespace Hitsounder.Game.Graphics;

public partial class PhysicalPanel : CompositeDrawable
{
    public ColourInfo BackgroundColour
    {
        get => backgroundColour;
        set
        {
            backgroundColour = value;
            if (IsLoaded)
                background.Colour = value;
        }
    }

    public ColourInfo SpecularColour
    {
        get => specularColour;
        set
        {
            specularColour = value;
            if (IsLoaded)
                specular.Colour = value;
        }
    }

    private NineSliceSprite background = null!;
    private NineSliceSprite specular = null!;

    private ColourInfo backgroundColour = Color4.White;
    private ColourInfo specularColour = Color4.White;

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        InternalChildren =
        [
            background = new NineSliceSprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("UI/panel"),
                TextureInset = new MarginPadding(6),
                Colour = backgroundColour,
            },
            specular = new NineSliceSprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("UI/panel-specular"),
                TextureInset = new MarginPadding(6),
                Colour = specularColour,
                Blending = BlendingParameters.Additive
            },
            new Grain { Alpha = 0.6f, }
        ];
    }

    public new MarginPadding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }
}
