using osu.Framework.Graphics;

namespace Hitsounder.Game.Input;

public class EditorDragEvent(Drawable source)
{
    public Drawable Source => source;
}

public class EditorDragEvent<T>(Drawable source, T data) : EditorDragEvent(source)
{
    public T Data => data;
}
