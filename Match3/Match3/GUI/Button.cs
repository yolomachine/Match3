using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class Button
    {
        public Texture Texture;
        public TextBlock Caption;

        public Vector2 Position
        {
            set
            {
                if (Texture != null)
                    Texture.Position = value;
                if (Caption != null)
                    Caption.Position = value;
            }
        }

        public Button(TextBlock textBlock, Vector2 position = default(Vector2))
        {
            Caption = textBlock;
            Position = position;
        }

        public Button(Texture texture, Vector2 position = default(Vector2))
        {
            Texture = texture;
            Position = position;
        }

        public Button(Texture texture, TextBlock text, Vector2 position = default(Vector2))
        {
            Texture = texture;
            Caption = text;
            Position = position;
        }

        public void LoadContent()
        {
            Texture?.LoadContent();
            Caption?.LoadContent();
        }

        public void UnloadContent()
        {
            Texture?.UnloadContent();
            Caption?.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Texture?.Update(gameTime);
            Caption?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture?.Draw(spriteBatch);
            Caption?.Draw(spriteBatch);
        }
    }
}
