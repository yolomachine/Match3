using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public class Pearl : Tile
    {
        public Pearl() : base("Sprites/Figures/pearl")
        {
        }

        public Pearl(Pearl tile) : base(tile)
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
