using osu.Framework.Graphics;

namespace Hitsounder.Game.DragDrop;

public interface IDraggable
{
    public Drawable GetDragRepresentation();

    public object GetDragData();
}
