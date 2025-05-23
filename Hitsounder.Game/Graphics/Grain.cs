using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK.Graphics;

namespace Hitsounder.Game.Graphics;

public partial class Grain : Sprite
{
    public Grain()
    {
        RelativeSizeAxes = Axes.Both;
        TextureRectangle = new RectangleF(0, 0, 1024, 1024);
        TextureRelativeSizeAxes = Axes.None;
        Colour = Color4.Black;
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        Texture = textures.Get("UI/grain", WrapMode.Repeat, WrapMode.Repeat);
    }
}
