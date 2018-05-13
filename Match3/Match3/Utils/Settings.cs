using Microsoft.Xna.Framework;

namespace Match3
{
    public static class Settings
    {
        public static readonly int ViewportWidth = 1024;
        public static readonly int ViewportHeight = 768;

        public static readonly int RowsCount = 8;
        public static readonly int ColsCount = 8;

        public static readonly Vector2 PlayButtonPosition = new Vector2(ViewportWidth / 2, ViewportHeight / 2);
    }
}
