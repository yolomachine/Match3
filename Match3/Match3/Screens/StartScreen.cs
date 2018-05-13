using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class StartScreen : GameScreen
    {
        Button PlayButton;

        public override void LoadContent()
        {
            base.LoadContent();
            PlayButton = new Button(
                new Texture("Sprites/Buttons/play"), 
                new TextBlock("Fonts/GillSans_32", "PLAY"), 
                Settings.PlayButtonPosition
            );
            PlayButton.Texture.Scale = new Vector2(0.5f);
            PlayButton.Texture.Click += PlayButton_Click;
            PlayButton.LoadContent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            PlayButton.Caption.Text = "WOW";
        }

        public override void UnloadContent()
        {
            PlayButton.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PlayButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Instance.GraphicsDevice.Clear(Color.PapayaWhip);
            PlayButton.Caption.Color = PlayButton.Texture.IsMouseHovering ? Color.Gold : Color.White;
            PlayButton.Draw(spriteBatch);
        }


    }
}
