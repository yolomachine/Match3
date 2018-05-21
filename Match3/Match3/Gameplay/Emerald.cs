using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class Emerald : Tile
    {
        public Emerald() : base("Sprites/Figures/emerald")
        {
        }

        public Emerald(Emerald tile) : base(tile)
        {
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
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
