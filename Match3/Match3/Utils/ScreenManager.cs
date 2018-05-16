using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Match3
{
    public class ScreenManager
    {
        public ContentManager Content { get; private set; }
        public GameScreen CurrentScreen;
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        public Cursor Cursor;

        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        public ScreenManager()
        {
            CurrentScreen = new StartScreen();
        }

        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
            Cursor = new Cursor("Sprites/Cursors/cursor");
            Cursor.Scale = new Vector2(0.8f);
            Cursor.LoadContent();
            Cursor.Origin = new Vector2(8.0f, 2.0f);
            CurrentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            Cursor.UnloadContent();
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Cursor.Update(gameTime);
            if (ScreenTransitionManager.Instance.IsActive)
                ScreenTransitionManager.Instance.Update(gameTime);
            else
                CurrentScreen.Update(gameTime);
        }

        public void Draw()
        {
            SpriteBatch.Begin();
            CurrentScreen.Draw(SpriteBatch);
            Cursor.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }
}
