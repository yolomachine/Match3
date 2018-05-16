using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class FadeEffect : Effect
    {
        private float defaultAlpha;

        public float MinAlpha;
        public float MaxAlpha;

        public FadeEffect(float defaultAlpha, float? speed = null, float? minAlpha = null, float? maxAlpha = null) : base()
        {
            Speed = speed ?? 0.03f;
            MinAlpha = minAlpha ?? 0.0f;
            MaxAlpha = maxAlpha ?? 1.0f;
            this.defaultAlpha = defaultAlpha;
        }

        public override void Apply(DrawableComponent component)
        {
            base.Apply(component);
            component.Alpha -= Speed;

            if (component.Alpha < MinAlpha)
            {
                component.Alpha = MinAlpha;
                isReversing = IsPeriodic;
            }
            else if (component.Alpha > MaxAlpha)
            {
                component.Alpha = MaxAlpha;
                isReversing = IsPeriodic;
            }
            if (isReversing)
            {
                isReversing = false;
                Speed *= -1;
            }
        }

        public override void Restore(DrawableComponent component)
        {
            base.Restore(component);
            component.Alpha = defaultAlpha;
        }
    }
}
