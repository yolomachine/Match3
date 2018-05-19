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

        ~Effect() { }

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
