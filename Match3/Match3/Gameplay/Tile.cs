using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3
{
    public abstract class Tile
    {
        private int movementSpeed;
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

        public Tile Copy
        {
            get
            {
                return Activator.CreateInstance(GetType(), this) as Tile;
            }
        }

        public Tile(string path)
        {
            Background = new Texture("Sprites/Figures/background");
            Figure = new Texture(path);
        }

        public Tile(Tile other)
        {
            Background = new Texture(other.Background);
            Figure = new Texture(other.Figure);
            Row = other.Row;
            Col = other.Col;
            IsMoving = other.IsMoving;
            targetPosition = other.targetPosition;

            LoadContent();
        }

        ~Tile() { }

        public virtual void LoadContent()
        {
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
            else if (Background.IsMouseClicked && 
                Field.Instance.MainState == FieldStates.Idle)
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
                Field.Instance.MovingTiles.Remove(this);
                if (Field.Instance.MovingTiles.Count == 0)
                    Field.Instance.AdditionalState = FieldStates.TilesStoppedMoving;
                IsMoving = false;
                return;
            }

            int dx = movementSpeed * Math.Sign(position.X - Figure.Position.X);
            int dy = movementSpeed * Math.Sign(position.Y - Figure.Position.Y);

            Figure.Position.X += dx;
            Figure.Position.Y += dy;
        }

        public void InitiateMovement(Vector2 position, int speed = 2)
        {
            Field.Instance.MovingTiles.Add(this);
            targetPosition = position;
            movementSpeed = speed;
            IsMoving = true;
        }
    }
}
