using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3
{
    public class GameplayScreen : GameScreen
    {
        private bool isGameFinished;
        private double previousTime;
        private double currentTime;
        private int timeLimit;

        public TextBlock TimerText;
        public Texture TimeRectangle;
        public Texture Background;

        public override void LoadContent()
        {
            base.LoadContent();
            Field.Instance.LoadContent();

            timeLimit = 60;
            currentTime = timeLimit;
            isGameFinished = false;

            TimeRectangle = new Texture((int)(0.8f * Settings.ViewportWidth), 8, Color.Gold);
            TimeRectangle.Position = new Vector2((int)Settings.ScreenCenter.X, (int)Settings.ViewportHeight - 10);
            TimeRectangle.LoadContent();

            TimerText = new TextBlock("Fonts/GillSans_28", "60");
            TimerText.Position = new Vector2(TimeRectangle.Position.X, TimeRectangle.Position.Y - 20.0f);
            TimerText.LoadContent();

            Background = new Texture("Sprites/Backgrounds/background");
            Background.Scale = new Vector2(0.8f);
            Background.Position = Settings.ScreenCenter;
            Background.LoadContent();

            ScreenObjects.Add(TimerText);
            ScreenObjects.Add(Background);
            ScreenObjects.Add(TimeRectangle);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (isGameFinished)
            {
                ScreenTransitionManager.Instance.MakeTransition(new GameOverScreen());
                return;
            }

            previousTime = currentTime;
            currentTime -= gameTime.ElapsedGameTime.TotalSeconds;

            if ((int)previousTime != (int)currentTime)
                TimerText.Text = Convert.ToInt32(currentTime).ToString();
            TimeRectangle.DestinationRect = new Rectangle(
                new Point((int)(((60.0f - gameTime.ElapsedGameTime.Seconds) / 60.0f) * TimeRectangle.Position.X), (int)TimeRectangle.Position.Y), 
                new Point((int)((currentTime / timeLimit) * TimeRectangle.Width), TimeRectangle.Height)
            );
            if (currentTime <= 0.0f)
                isGameFinished = true;

            Field.Instance.Update(gameTime);
            TimerText.Update(gameTime);
            TimeRectangle.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Black);
            Background.Draw(spriteBatch);
            TimerText.Draw(spriteBatch);
            TimeRectangle.Draw(spriteBatch);
            Field.Instance.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
