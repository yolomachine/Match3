using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3
{
    public abstract class Tile
    {
        private Vector2 targetPosition;

        public Texture Background;
        public Texture Figure;
        public int Row;
        public int Col;
        public bool IsMoving;

        public int Width
        {
            get
            {
                return Background.Width;
            }
        }

        public int Height
        {
            get
            {
                return Background.Height;
            }
        }

        public Vector2 Position
        {
            get
            {
                return Background.Position;
            }
            set
            {
                Background.Position = value;
                Figure.Position = value;
            }
        }

        public Tile(string path)
        {
            Background = new Texture("Sprites/Figures/background");
            Figure = new Texture(path);
        }

        ~Tile() { }

        public virtual void LoadContent()
        {
            IsMoving = false;

            Background.LoadContent();
            Figure.LoadContent();

            ScreenManager.Instance.CurrentScreen.ScreenObjects.Add(Background);
            ScreenManager.Instance.CurrentScreen.ScreenObjects.Add(Figure);
        }

        public virtual void UnloadContent()
        {
            Background.UnloadContent();
            Figure.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            Background.Update(gameTime);
            if (IsMoving)
                MoveTo(targetPosition);
            else if (Background.IsMouseClicked)
            {
                Field.Instance.PreviousSelectedTile = Field.Instance.CurrentSelectedTile;
                Field.Instance.CurrentSelectedTile = this;
            }
            Figure?.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Figure.Draw(spriteBatch);
        }

        public void MoveTo(Vector2 position)
        {
            if (position == Figure.Position)
            {
                IsMoving = false;
                return;
            }

            int dx = 2 * Math.Sign(position.X - Figure.Position.X);
            int dy = 2 * Math.Sign(position.Y - Figure.Position.Y);

            Figure.Position.X += dx;
            Figure.Position.Y += dy;
        }

        public void InitiateMoving(Vector2 position)
        {
            targetPosition = position;
            IsMoving = true;
        }
    }
}
