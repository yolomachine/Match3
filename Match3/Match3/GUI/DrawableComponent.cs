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

        // TO DO : Add effects such as rotating,
        //         fading, pulsing, etc.

        public MouseState MouseState;
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
        public Color Color;
        public Vector2 Scale;
        public Vector2 Origin;
        public SpriteEffects SpriteEffect;
        public bool IsMouseHovering;
        public bool IsMouseClicked;

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
            IsMouseHovering = false;
            IsMouseClicked = true;
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void LoadContent()
        {
            Origin = new Vector2(Width / 2, Height / 2);
            DestinationRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            RenderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, Width, Height);
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            var previouseMouseState = MouseState;
            MouseState = Mouse.GetState();
            var cursorRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            var destinationRectangle = new Rectangle(
                (int)(DestinationRect.X - Origin.X),
                (int)(DestinationRect.Y - Origin.Y),
                DestinationRect.Width,
                DestinationRect.Height
            );
            IsMouseHovering = cursorRectangle.Intersects(destinationRectangle) ? true : false;
            if (MouseState.LeftButton == ButtonState.Released && previouseMouseState.LeftButton == ButtonState.Pressed)
                Click?.Invoke(this, new EventArgs());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RenderTarget);
        }
    }
}
