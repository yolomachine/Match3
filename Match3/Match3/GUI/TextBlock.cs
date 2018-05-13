using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class TextBlock : DrawableComponent
    {
        private SpriteFont font;

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                LoadContent();
            }
        }

        public TextBlock(string path, string text) : base()
        {
            font = Content.Load<SpriteFont>(path);
            this.text = text;
        }

        public override void LoadContent()
        {
            Width = (int)font.MeasureString(text).X;
            Height = (int)font.MeasureString(text).Y;
            
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
            spriteBatch.DrawString(
                font,
                text,
                Position,
                Color,
                Rotation,
                Origin,
                Scale,
                SpriteEffect,
                0.0f
            );
        }
    }
}
