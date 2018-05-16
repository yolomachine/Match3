using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public abstract class Effect
    {
        protected bool isReversing;

        public float Speed;
        public bool IsActive;
        public bool IsPeriodic;

        public Effect()
        {
            IsActive = false;
            isReversing = false;
        }

        public virtual void Apply(DrawableComponent component)
        {
            IsActive = true;
        }

        public virtual void Restore(DrawableComponent component)
        {
            IsActive = false;
            isReversing = false;
        }
    }
}
