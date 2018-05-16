using Microsoft.Xna.Framework;

namespace Match3
{
    public static class Settings
    {
        public static readonly int ViewportWidth = 800;
        public static readonly int ViewportHeight = 600;

        public static readonly int RowsCount = 8;
        public static readonly int ColsCount = 8;

        public static readonly Vector2 ScreenCenter = new Vector2(ViewportWidth / 2, ViewportHeight / 2);
    }
}
