using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Match3
{
    class ScreenManager
    {

        // TO DO : Screen transitions
        //         StartScreen ->
        //         -> GameplayScreen ->
        //         -> GameOverScreen ->
        //         -> StartScreen

        public ContentManager Content { get; private set; }
        public GameScreen CurrentScreen;
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

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
            CurrentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }

        public void Draw()
        {
            SpriteBatch.Begin();
            CurrentScreen.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }
}
