using System;
using System.Numerics;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using Vector2 = osuTK.Vector2;

namespace Hitsounder.Game.UserInterface;

public partial class Knob<T> : CompositeDrawable, IHasCurrentValue<T>, IHasContextMenu
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    private readonly BindableNumberWithCurrent<T> current = new BindableNumberWithCurrent<T>();

    public Bindable<T> Current
    {
        get => current.Current;
        set => current.Current = value;
    }

    private Drawable pointer;

    public Knob()
    {
        Size = new Vector2(20);
        InternalChildren =
        [
            new CircularProgress
            {
                RelativeSizeAxes = Axes.Both,
                Progress = 0.75,
                InnerRadius = 0.1f,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Rotation = -135,
                Alpha = 0.25f,
            },
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(3),
                Children =
                [
                    new CircularContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        Child = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4Extensions.FromHex("#222228"),
                        },
                        EdgeEffect = new EdgeEffectParameters
                        {
                            Offset = new Vector2(0, 2),
                            Radius = 2f,
                            Colour = Color4.Black.Opacity(0.2f),
                            Type = EdgeEffectType.Shadow,
                        }
                    },
                    pointer = new Container
                    {
                        RelativeSizeAxes = Axes.Y,
                        Height = 0.5f,
                        Width = 0.5f,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.BottomCentre,
                        Padding = new MarginPadding { Bottom = 1 },
                        Child = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            EdgeSmoothness = new Vector2(1)
                        }
                    }
                ]
            }
        ];
    }

    private Sample sample = null!;

    [BackgroundDependencyLoader]
    private void load(AudioManager audio)
    {
        sample = audio.Samples.Get("UI/notch-tick");
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        current.BindValueChanged(_ =>
        {
            pointer.RotateTo(-135 + NormalizedValue * 270, 200, Easing.OutExpo);
        }, true);
        pointer.FinishTransforms();
    }

    protected float NormalizedValue
    {
        get
        {
            if (!current.HasDefinedRange)
            {
                throw new InvalidOperationException($"A {nameof(Knob<T>)}'s {nameof(Current)} must have user-defined {nameof(BindableNumber<T>.MinValue)}"
                                                    + $" and {nameof(BindableNumber<T>.MaxValue)} to produce a valid {nameof(NormalizedValue)}.");
            }

            return current.NormalizedValue;
        }
    }

    public T StepSize;

    private double lastPlayback;

    protected override bool OnScroll(ScrollEvent e)
    {
        if (StepSize != default)
        {
            var previous = current.Value;

            current.Value += (StepSize * T.CreateChecked(Math.Sign(-e.ScrollDelta.Y)));

            if (previous != current.Value && Time.Current - lastPlayback > 20)
            {
                sample.Play();
                lastPlayback = Time.Current;
            }

            return true;
        }

        return base.OnScroll(e);
    }

    public MenuItem[]? ContextMenuItems =>
    [
        new MenuItem("Reset", () => current.SetDefault())
    ];
}
