using Microsoft.Xna.Framework;
using System;

namespace Match3
{
    public class ScreenTransitionManager
    {
        private bool transitionFinished;
        private float targetAlpha;
        private GameScreen newScreen;
        private Effect transitionEffect;

        public bool IsActive;

        private static ScreenTransitionManager instance;
        public static ScreenTransitionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenTransitionManager();
                return instance;
            }
        }

        public ScreenTransitionManager()
        {
            transitionEffect = new FadeEffect(1.0f);
        }

        public void MakeTransition(GameScreen screen)
        {
            if (IsActive)
                return;
            targetAlpha = 0.0f;
            newScreen = screen;
            IsActive = true;
            transitionFinished = false;
            transitionEffect.Speed *= Math.Sign(transitionEffect.Speed);
        }

        public void Update(GameTime gameTime)
        {
            transitionFinished = ScreenManager.Instance.CurrentScreen.ScreenObjects[0].Alpha == targetAlpha;
            foreach (DrawableComponent component in ScreenManager.Instance.CurrentScreen.ScreenObjects)
            {
                transitionEffect.Apply(component);
                component.Update(gameTime);
                transitionFinished = transitionFinished && (component.Alpha == targetAlpha);
            }

            if (transitionFinished)
            {
                if (!Convert.ToBoolean(targetAlpha))
                {
                    ScreenManager.Instance.CurrentScreen.UnloadContent();
                    ScreenManager.Instance.CurrentScreen = newScreen;
                    ScreenManager.Instance.CurrentScreen.LoadContent();
                    foreach (DrawableComponent component in ScreenManager.Instance.CurrentScreen.ScreenObjects)
                        component.Alpha = 0.0f;
                    targetAlpha = 1.0f;
                    transitionEffect.Speed *= -1;
                    transitionFinished = false;
                }
                else
                    IsActive = false;
            }   
        }
    }
}
