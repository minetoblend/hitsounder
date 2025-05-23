﻿using osu.Framework.Graphics;
using osuTK;

namespace Hitsounder.Game.Input;

public interface IDragRepresentation : IDrawable
{
    public void OnDragStart(Vector2 position);

    public void OnDragEnd(bool dropped);

    public void UpdatePosition(Vector2 position);
}
