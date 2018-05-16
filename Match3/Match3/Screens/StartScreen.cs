using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class StartScreen : GameScreen
    {
        Button PlayButton;
        Texture Background;

        public override void LoadContent()
        {
            base.LoadContent();

            PlayButton = new Button(
                new Texture("Sprites/Buttons/button"), 
                new TextBlock("Fonts/GillSans_28", "PLAY"), 
                Settings.ScreenCenter
            );
            PlayButton.Texture.Click += PlayButton_Click;
            PlayButton.LoadContent();

            Background = new Texture("Sprites/Backgrounds/background");
            Background.Scale = new Vector2(0.8f);
            Background.Position = Settings.ScreenCenter;
            Background.LoadContent();

            ScreenObjects.Add(Background);
            ScreenObjects.Add(PlayButton.Texture);
            ScreenObjects.Add(PlayButton.Caption);
        }

        public override void UnloadContent()
        {
            Background.UnloadContent();
            PlayButton.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PlayButton.Caption.Color = PlayButton.Texture.IsMouseHovering ? Color.PaleGoldenrod : Color.White;
            PlayButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Black);
            Background.Draw(spriteBatch);
            PlayButton.Draw(spriteBatch);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            ScreenTransitionManager.Instance.MakeTransition(new GameOverScreen());
        }
    }
}
