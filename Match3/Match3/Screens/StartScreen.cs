using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Match3
{
    public class StartScreen : GameScreen
    {
        public Button PlayButton;
        public Texture Background;
        public Song Song;

        public override void LoadContent()
        {
            base.LoadContent();

            Song = Content.Load<Song>("Music/start");
            MediaPlayer.Play(Song);

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
            Song = null;
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // I'm not a sound specialist after all
            if (MediaPlayer.State == MediaState.Stopped)
                MediaPlayer.Play(Song, new TimeSpan(147009999));

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
            ScreenTransitionManager.Instance.MakeTransition(new GameplayScreen());
        }
    }
}
