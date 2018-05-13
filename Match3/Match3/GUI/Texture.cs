using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class Texture : DrawableComponent
    {
        private Texture2D texture;

        public Texture(string path) : base()
        {
            texture = Content.Load<Texture2D>(path);
        }

        public override void LoadContent()
        {
            Width = texture.Width;
            Height = texture.Height;

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                DestinationRect,
                SourceRect,
                Color.White * Alpha,
                Rotation,
                Origin,
                SpriteEffect,
                0.0f
            );
        }
    }
}
