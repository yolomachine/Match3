using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3
{
    public class Cursor : Texture
    {
        public MouseState CurrentMouseState;
        public MouseState PreviousMouseState;
        public ButtonState CurrentButtonState;
        public ButtonState PreviousButtonState;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 1, 1);
            }
        }


        public Cursor(string path) : base(path)
        {
            CurrentMouseState = PreviousMouseState = Mouse.GetState();
            PreviousButtonState = CurrentButtonState = CurrentMouseState.LeftButton;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PreviousMouseState = CurrentMouseState;
            PreviousButtonState = PreviousMouseState.LeftButton;
            CurrentMouseState = Mouse.GetState();
            CurrentButtonState = CurrentMouseState.LeftButton;

            Position.X = CurrentMouseState.X;
            Position.Y = CurrentMouseState.Y;

            if (CurrentButtonState == ButtonState.Pressed)
                Rotation = -0.1f;
            else
                Rotation = 0.0f;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
