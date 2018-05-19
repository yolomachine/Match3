using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3
{
    public class Field
    {
        private Random Random = new Random();
        private readonly List<Tile> TileClasses;

        public PulseEffect ClickEffect;
        public Tile PreviousSelectedTile;
        public Tile CurrentSelectedTile;
        public TextBlock Score;
        public Tile[,] TileArray;

        private static Field instance;
        public static Field Instance
        {
            get
            {
                if (instance == null)
                    instance = new Field();
                return instance;
            }
        }

        public Field()
        {
            TileClasses = new List<Tile>{ new Mountain(), new Emerald(), new Ruby(), new Diamond(), new Pearl() };

            ClickEffect = new PulseEffect(1.0f, 0.015f, 0.8f, 1.0f);
            ClickEffect.IsPeriodic = true;
        }

        ~Field() { }

        public void LoadContent()
        {
            Generate();

            Score = new TextBlock("Fonts/GillSans_28", "Score: 0");
            Score.Position = new Vector2(Settings.ScreenCenter.X, 30);
            Score.LoadContent();
        }

        public void UnloadContent()
        {
            Score.UnloadContent();
            foreach (Tile tile in TileArray)
                tile.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Score.Update(gameTime);
            if (PreviousSelectedTile != null)
            {
                ClickEffect.Restore(PreviousSelectedTile.Figure);
                if (IsAdjacent(CurrentSelectedTile, PreviousSelectedTile))
                {
                    SwapTiles(ref CurrentSelectedTile, ref PreviousSelectedTile);
                    CurrentSelectedTile = null;
                }
                PreviousSelectedTile = null;
            }
            if (CurrentSelectedTile != null)
                ClickEffect.Apply(CurrentSelectedTile.Figure);
            foreach (Tile tile in TileArray)
                tile.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Score.Draw(spriteBatch);
            foreach (Tile tile in TileArray)
                tile.Background.Draw(spriteBatch);
            foreach (Tile tile in TileArray)
                tile.Figure.Draw(spriteBatch);
        }

        public Tile RandomTile()
        {
            Type[] classes = TileClasses.Select(x => x.GetType()).ToArray();
            return Activator.CreateInstance(classes[Random.Next(0, classes.Length)]) as Tile;
        }

        public void Generate()
        {
            TileArray = new Tile[Settings.ColsCount, Settings.RowsCount];
            for (int i = 0; i < Settings.ColsCount; ++i)
                for (int j = 0; j < Settings.RowsCount; ++j)
                {
                    Tile tile = RandomTile();
                    tile.LoadContent();
                    int xOffset = (Settings.ViewportWidth - (Settings.ColsCount - 1) * tile.Width) / 2;
                    int yOffset = (Settings.ViewportHeight - (Settings.RowsCount - 1) * tile.Height) / 2;
                    tile.Position = new Vector2(xOffset + i * tile.Width, yOffset + j * tile.Height);
                    tile.Col = i;
                    tile.Row = j;
                    TileArray[i, j] = tile;
                }
        }

        public bool IsAdjacent(Tile first, Tile second)
        {
            return
                (first.Position.X == second.Position.X &&
                Math.Abs(first.Position.Y - second.Position.Y) == first.Height) ||
                (first.Position.Y == second.Position.Y &&
                Math.Abs(first.Position.X - second.Position.X) == first.Width);

        }

        public void SwapTiles(ref Tile first, ref Tile second)
        {
            first.InitiateMoving(second.Position);
            second.InitiateMoving(first.Position);

            Tile tile = first;
            TileArray[first.Col, first.Row] = TileArray[second.Col, second.Row];
            TileArray[second.Col, second.Row] = tile;

            Vector2 position = first.Background.Position;
            first.Background.Position = second.Background.Position;
            second.Background.Position = position;

            int row = first.Row;
            first.Row = second.Row;
            second.Row = row;

            int col = first.Col;
            first.Col = second.Col;
            second.Col = col;
        }
    }
}
