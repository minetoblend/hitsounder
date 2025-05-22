using System;

namespace Hitsounder.Game.Utils;

public class SecondOrderDynamics
{
    public float Previous;
    public float Current;
    public float Velocity;

    private readonly float k1;
    private readonly float k2;
    private readonly float k3;

    public SecondOrderDynamics(float initial, float frequency, float damping, float response)
    {
        k1 = damping / (MathF.PI * frequency);
        k2 = 1 / (2 * MathF.PI * frequency * (2 * MathF.PI * frequency));
        k3 = (response * damping) / (2 * MathF.PI * frequency);

        Previous = initial;
        Current = initial;
    }

    public float Update(double timeElapsed, float target)
    {
        float dt = (float)(timeElapsed / 1000);

        float vel = (target - Previous) / dt;
        Previous = target;

        float k2Stable = MathF.Max(
            k2,
            MathF.Max(
                (dt * dt) / 2 + (dt * k1) / 2,
                dt * k1
            )
        );

        Current += dt * Velocity;
        Velocity += (dt * (target + k3 * vel - Current - k1 * Velocity)) / k2Stable;

        return Current;
    }
}
