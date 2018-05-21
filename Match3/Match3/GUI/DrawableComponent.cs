using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3
{
    public abstract class DrawableComponent
    {

        // TO DO : Add proper resize function
        //         in order to avoid the loss
        //         of quality while scaling, etc.

        // TO DO : Add a custom font loader
        //         int order to embed custom fonts
        //         as a resource

        public event EventHandler Click;
        public ContentManager Content;
        public RenderTarget2D RenderTarget;
        public Rectangle? SourceRect;
        public Rectangle DestinationRect;

        public int Width;
        public int Height;
        public float Alpha;
        public float Rotation;

        public Vector2 Position;
        public Vector2 Origin;
        public Color Color;
        public SpriteEffects SpriteEffect;
        public bool IsMouseHovering;
        public bool IsMouseClicked;


        private Vector2 scale;
        public Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                UpdateDestinationRect();
            }
        }

        public DrawableComponent()
        {
            Width = 0;
            Height = 0;
            Alpha = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Origin = Vector2.Zero;
            Color = Color.White;
            DestinationRect = Rectangle.Empty;
            SpriteEffect = SpriteEffects.None;
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public DrawableComponent(DrawableComponent other)
        {
            Width = other.Width;
            Height = other.Height;
            Alpha = other.Alpha;
            Rotation = other.Rotation;
            Position = other.Position;
            Scale = other.Scale;
            Origin = other.Origin;
            Color = other.Color;
            SpriteEffect = other.SpriteEffect;
            Content = new ContentManager(other.Content.ServiceProvider, "Content");
        }

        ~DrawableComponent() { }
        
        public virtual void LoadContent()
        {
            UpdateDestinationRect();
            Origin = new Vector2(Width / 2, Height / 2);
            RenderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, Width, Height);
        }

        public virtual void UnloadContent()
        {
            Content.Dispose();
            Content = null;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Position.X != DestinationRect.X || Position.Y != DestinationRect.Y)
                UpdateDestinationRect();

            var cursorTargetRectangle = new Rectangle(
                (int)(DestinationRect.X - Origin.X),
                (int)(DestinationRect.Y - Origin.Y),
                DestinationRect.Width,
                DestinationRect.Height
            );

            IsMouseClicked = false;
            IsMouseHovering = false;
            if (ScreenManager.Instance.Cursor.Rectangle.Intersects(cursorTargetRectangle))
            {
                IsMouseHovering = true;
                if (ScreenManager.Instance.Cursor.PreviousButtonState == ButtonState.Pressed &&
                    ScreenManager.Instance.Cursor.CurrentButtonState == ButtonState.Released)
                {
                    IsMouseClicked = true;
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RenderTarget);
        }

        private void UpdateDestinationRect()
        {
            DestinationRect = new Rectangle((int)Position.X, (int)Position.Y, (int)(Scale.X * Width), (int)(Scale.Y * Height));
        }
    }
}
