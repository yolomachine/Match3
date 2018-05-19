using Microsoft.Xna.Framework;

namespace Match3
{
    public class PulseEffect : Effect
    {
        private float defaultScale;

        public float MinScale;
        public float MaxScale;

        public PulseEffect(float defaultScale, float? speed = null, float? minScale = null, float? maxScale = null) : base()
        {
            Speed = speed ?? 0.01f;
            MinScale = minScale ?? 0.8f;
            MaxScale = maxScale ?? 1.0f;
            this.defaultScale = defaultScale;
        }

        public override void Apply(DrawableComponent component)
        {
            base.Apply(component);
            if (component.Scale.X < MinScale)
            {
                component.Scale = new Vector2(MinScale);
                isReversing = IsPeriodic;
            }
            else if (component.Scale.X > MaxScale)
            {
                component.Scale = new Vector2(MaxScale);
                isReversing = IsPeriodic;
            }
            if (isReversing)
            {
                isReversing = false;
                Speed *= -1;
            }

            component.Scale = new Vector2(component.Scale.X - Speed);
        }

        public override void Restore(DrawableComponent component)
        {
            base.Restore(component);
            component.Scale = new Vector2(defaultScale);
        }
    }
}
