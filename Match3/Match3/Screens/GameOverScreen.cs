using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3
{
    public class GameOverScreen : GameScreen
    {
        public Button OkButton;
        public TextBlock GameOverText;
        public Texture Background;

        public override void LoadContent()
        {
            base.LoadContent();

            OkButton = new Button(
                new Texture("Sprites/Buttons/button"),
                new TextBlock("Fonts/GillSans_28", "OK"),
                new Vector2(Settings.ScreenCenter.X, Settings.ScreenCenter.Y + 50.0f)
            );
            OkButton.Texture.Click += OkButton_Click;
            OkButton.LoadContent();

            GameOverText = new TextBlock("Fonts/GillSans_48", "Game Over");
            GameOverText.Position = new Vector2(Settings.ScreenCenter.X, Settings.ScreenCenter.Y - 50.0f);
            GameOverText.Color = Color.Gold;
            GameOverText.LoadContent();

            Background = new Texture("Sprites/Backgrounds/background");
            Background.Scale = new Vector2(0.8f);
            Background.Position = Settings.ScreenCenter;
            Background.LoadContent();

            ScreenObjects.Add(Background);
            ScreenObjects.Add(GameOverText);
            ScreenObjects.Add(OkButton.Texture);
            ScreenObjects.Add(OkButton.Caption);
        }

        public override void UnloadContent()
        {
            Background.UnloadContent();
            OkButton.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            OkButton.Caption.Color = OkButton.Texture.IsMouseHovering ? Color.PaleGoldenrod : Color.White;
            OkButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Black);
            Background.Draw(spriteBatch);
            GameOverText.Draw(spriteBatch);
            OkButton.Draw(spriteBatch);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            ScreenTransitionManager.Instance.MakeTransition(new StartScreen());
        }
    }
}
