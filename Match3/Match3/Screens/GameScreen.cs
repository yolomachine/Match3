﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Match3
{
    public abstract class GameScreen
    {
        protected ContentManager Content;
        public List<DrawableComponent> ScreenObjects;

        ~GameScreen() { }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            ScreenObjects = new List<DrawableComponent>();
        }

        public virtual void UnloadContent()
        {
            foreach (DrawableComponent component in ScreenObjects)
                component.UnloadContent();
            Content.Dispose();
            Content = null;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
