using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3
{
    public class GameplayScreen : GameScreen
    {
        public double PreviousTime;
        public double CurrentTime;
        public int TimeLimit;
        public bool IsGameFinished;
        public TextBlock Score;
        public TextBlock TimerText;
        public Texture TimeRectangle;
        public Texture Background;

        public override void LoadContent()
        {
            base.LoadContent();
            Field.Instance.LoadContent();

            TimeLimit = 60;
            CurrentTime = TimeLimit;
            IsGameFinished = false;

            TimeRectangle = new Texture((int)(0.8f * Settings.ViewportWidth), 8, Color.Gold);
            TimeRectangle.Position = new Vector2((int)Settings.ScreenCenter.X, (int)Settings.ViewportHeight - 10);
            TimeRectangle.LoadContent();

            Score = new TextBlock("Fonts/GillSans_32", "SCORE: 0");
            Score.Color = Color.PaleGoldenrod;
            Score.Position = new Vector2(Settings.ScreenCenter.X, 30);
            Score.LoadContent();

            TimerText = new TextBlock("Fonts/GillSans_28", "60");
            TimerText.Position = new Vector2(TimeRectangle.Position.X, TimeRectangle.Position.Y - 20.0f);
            TimerText.LoadContent();

            Background = new Texture("Sprites/Backgrounds/background");
            Background.Scale = new Vector2(0.8f);
            Background.Position = Settings.ScreenCenter;
            Background.LoadContent();

            ScreenObjects.Add(Score);
            ScreenObjects.Add(TimerText);
            ScreenObjects.Add(Background);
            ScreenObjects.Add(TimeRectangle);
        }

        public override void UnloadContent()
        {
            Field.Instance.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsGameFinished)
            {
                ScreenTransitionManager.Instance.MakeTransition(new GameOverScreen(Score));
                return;
            }

            PreviousTime = CurrentTime;
            CurrentTime -= gameTime.ElapsedGameTime.TotalSeconds;

            TimerText.Text = Convert.ToInt32(CurrentTime).ToString();
            TimeRectangle.DestinationRect = new Rectangle(
                new Point((int)TimeRectangle.Position.X, (int)TimeRectangle.Position.Y), 
                new Point((int)((CurrentTime / TimeLimit) * TimeRectangle.Width), TimeRectangle.Height)
            );
            if (CurrentTime <= 0.0f)
                IsGameFinished = true;

            Score.Update(gameTime);
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
            Score.Draw(spriteBatch);
            Field.Instance.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
