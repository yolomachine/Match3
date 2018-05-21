using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class TextBlock : DrawableComponent
    {
        private SpriteFont font;
        private bool isOutlineDrawn;

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

        public TextBlock(string path, string text, bool isOutlineDrawn = true) : base()
        {
            font = Content.Load<SpriteFont>(path);
            this.text = text;
            this.isOutlineDrawn = isOutlineDrawn;
        }

        public TextBlock(TextBlock other) : base(other)
        {
            font = other.font;
            isOutlineDrawn = other.isOutlineDrawn;
            text = other.text;
        }

        ~TextBlock() { }

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
            if (isOutlineDrawn)
            {
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X + 2f, Position.Y),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X - 2f, Position.Y),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X, Position.Y + 2f),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X, Position.Y - 2f),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X + 1f, Position.Y + 1f),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X + 1f, Position.Y - 1f),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X - 1f, Position.Y + 1f),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
                spriteBatch.DrawString(
                    font,
                    text,
                    new Vector2(Position.X - 1f, Position.Y - 1f),
                    Color.Black * Alpha,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffect,
                    0.0f
                );
            }

            spriteBatch.DrawString(
                font,
                text,
                Position,
                Color * Alpha,
                Rotation,
                Origin,
                Scale,
                SpriteEffect,
                0.0f
            );
        }
    }
}
