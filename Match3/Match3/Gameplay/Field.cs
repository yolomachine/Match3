using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

// Attention
// This part of project contains extremely huge
// Amount of (consecutive)? loops, conditional statements 
// And heavy combat crutches.

namespace Match3
{
    public enum FieldStates {
        Idle,
        Swapping, SwappingBack,
        TilesMoving, TilesStoppedMoving,
        RemovingTiles, FillingTiles, TilesFading
    };

    public class Field
    {
        private int score;
        private int? xOffset;
        private int? yOffset;
        private Random Random = new Random();
        private readonly List<Tile> TileClasses;

        public FieldStates MainState;
        public FieldStates AdditionalState;

        public PulseEffect ClickEffect;
        public FadeEffect RemoveEffect;
        public Tile PreviousSelectedTile;
        public Tile CurrentSelectedTile;

        public List<Tile> FadingTiles;
        public List<Tile> MovingTiles;
        public Tile[,] TileArray;
        public Tile[,] TileArrayDeepCopy
        {
            get
            {
                Tile[,] copy = new Tile[Settings.ColsCount, Settings.RowsCount];
                for (var i = 0; i < Settings.ColsCount; ++i)
                    for (var j = 0; j < Settings.RowsCount; ++j)
                        copy[i, j] = TileArray[i, j].Copy;
                return copy;
            }
        }

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
            TileClasses = new List<Tile> {
                new Ruby(),
                new Pearl(),
                new Emerald(),
                new Diamond(),
                new Mountain()
            };

            ClickEffect = new PulseEffect(1.0f, 0.015f, 0.8f, 1.0f);
            ClickEffect.IsPeriodic = true;

            RemoveEffect = new FadeEffect(1.0f, 0.1f);
        }

        ~Field() { }

        public void LoadContent()
        {
            score = 0;
            MainState = FieldStates.Idle;
            AdditionalState = FieldStates.Idle;
            MovingTiles = new List<Tile>();
            FadingTiles = new List<Tile>();

            do
            {
                Generate();
            } while (!MovesAvailable());
        }

        public void UnloadContent()
        {
            TileArray = null;
        }

        public void Update(GameTime gameTime)
        {
            // Attention: a handful of brain cells has been sacrificed to Satan
            // In order to make fancy swapping animation work properly;
            // It's time to kick switches and chew bubblegum... and I'm all outta gum

            switch (MainState)
            {
                case FieldStates.RemovingTiles:
                    switch(AdditionalState)
                    {
                        case FieldStates.TilesFading:
                            if (FadingTiles.Last().Figure.Alpha != 0.0f)
                                foreach (Tile tile in FadingTiles)
                                    RemoveEffect.Apply(tile.Figure);
                            else
                                AdditionalState = FieldStates.RemovingTiles;
                            break;

                        case FieldStates.RemovingTiles:
                            RemoveBlocks();
                            break;

                        case FieldStates.TilesStoppedMoving:
                            FillTiles();
                            if (FindBlocks().Count > 0)
                            {
                                BlocksFadeout();
                                AdditionalState = FieldStates.TilesFading;
                            }
                            else if (MovesAvailable())
                                MainState = AdditionalState = FieldStates.Idle;
                            else
                                ((GameplayScreen)ScreenManager.Instance.CurrentScreen).IsGameFinished = true;
                            break;
                    }
                    break;

                case FieldStates.Swapping:
                    switch (AdditionalState)
                    {
                        case FieldStates.SwappingBack:
                            MainState = AdditionalState = FieldStates.Idle;
                            CurrentSelectedTile = PreviousSelectedTile = null;
                            break;

                        case FieldStates.TilesStoppedMoving:
                            if (FindBlocks().Count == 0)
                            {
                                CurrentSelectedTile.InitiateMovement(PreviousSelectedTile.Position);
                                PreviousSelectedTile.InitiateMovement(CurrentSelectedTile.Position);
                                SwapTiles(ref CurrentSelectedTile, ref PreviousSelectedTile);
                                AdditionalState = FieldStates.SwappingBack;
                            }
                            else
                            {
                                BlocksFadeout();
                                MainState = FieldStates.RemovingTiles;
                                CurrentSelectedTile = PreviousSelectedTile = null;
                            }
                            break;
                    }
                    break;

                case FieldStates.Idle:
                    if (PreviousSelectedTile != null)
                    {
                        ClickEffect.Restore(PreviousSelectedTile.Figure);
                        if (IsAdjacent(CurrentSelectedTile, PreviousSelectedTile))
                        {
                            CurrentSelectedTile.InitiateMovement(PreviousSelectedTile.Position);
                            PreviousSelectedTile.InitiateMovement(CurrentSelectedTile.Position);
                            SwapTiles(ref CurrentSelectedTile, ref PreviousSelectedTile);
                            MainState = FieldStates.Swapping;
                            AdditionalState = FieldStates.TilesMoving;
                            break;
                        }
                        else
                        {
                            if (CurrentSelectedTile == PreviousSelectedTile)
                                CurrentSelectedTile = null;
                            PreviousSelectedTile = null;
                        }
                    }

                    if (CurrentSelectedTile != null)
                        ClickEffect.Apply(CurrentSelectedTile.Figure);
                    break;
            }
            
            // So many field states, so little time :)

            foreach (Tile tile in TileArray)
                tile.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in TileArray)
                tile.Background?.Draw(spriteBatch);
            foreach (Tile tile in TileArray)
                tile.Figure?.Draw(spriteBatch);
        }

        private Tile RandomTile()
        {
            Type[] classes = TileClasses.Select(x => x.GetType()).ToArray();
            return Activator.CreateInstance(classes[Random.Next(0, classes.Length)]) as Tile;
        }

        private void Generate()
        {
            TileArray = new Tile[Settings.ColsCount, Settings.RowsCount];
            for (int i = 0; i < Settings.ColsCount; ++i)
                for (int j = 0; j < Settings.RowsCount; ++j)
                {
                    Tile tile = RandomTile();
                    tile.LoadContent();
                    xOffset = xOffset ?? (Settings.ViewportWidth - (Settings.ColsCount - 1) * tile.Width) / 2;
                    yOffset = yOffset ?? (Settings.ViewportHeight - (Settings.RowsCount - 1) * tile.Height) / 2;
                    tile.Position = new Vector2((float)xOffset + i * tile.Width, (float)yOffset + j * tile.Height);
                    tile.Col = i;
                    tile.Row = j;
                    TileArray[i, j] = tile;
                }

            List<List<Tile>> blocks;
            while ((blocks = FindBlocks()).Count > 0)
                foreach (List<Tile> block in blocks)
                    foreach(Tile tile in block)
                        Replace(ref TileArray[tile.Col, tile.Row], RandomTile());
        }

        private void Replace(ref Tile oldTile, Tile newTile)
        {
            int row = oldTile.Row;
            int col= oldTile.Col;
            Vector2 position = oldTile.Position;

            oldTile = newTile;
            oldTile.Row = row;
            oldTile.Col = col;
            oldTile.Position = position;
            oldTile.LoadContent();

        }

        private bool MovesAvailable()
        {
            for (int i = 0; i < Settings.ColsCount; ++i)
                for (int j = 0; j < Settings.RowsCount - 1; ++j)
                {
                    List<List<Tile>> blocks;
                    SwapTiles(ref TileArray[i, j], ref TileArray[i, j + 1]);
                    blocks = FindBlocks();
                    SwapTiles(ref TileArray[i, j], ref TileArray[i, j + 1]);
                    if (blocks.Count > 0)
                        return true;
                }

            for (int j = 0; j < Settings.RowsCount; ++j)
                for (int i = 0; j < Settings.ColsCount - 1; ++i)
                {
                    List<List<Tile>> blocks;
                    SwapTiles(ref TileArray[i, j], ref TileArray[i + 1, j]);
                    blocks = FindBlocks();
                    SwapTiles(ref TileArray[i, j], ref TileArray[i + 1, j]);
                    if (blocks.Count > 0)
                        return true;
                }

            return false;
        }

        private void FillTiles()
        {
            for (int i = 0; i < Settings.ColsCount; ++i)
                for (int j = 0; j < Settings.RowsCount; ++j)
                    if (TileArray[i, j].Figure == null)
                        Replace(ref TileArray[i, j], RandomTile());
        }

        private void BlocksFadeout()
        {
            AdditionalState = FieldStates.TilesFading;

            List<List<Tile>> blocks = FindBlocks();
            foreach (List<Tile> block in blocks)
                foreach (Tile tile in block)
                {
                    ++score;
                    FadingTiles.Add(tile);
                }
        }

        private Tile FindFirstAbove(int col, int row)
        {
            for (int i = row - 1; i >= 0; --i)
                if (TileArray[col, i].Figure != null)
                    return TileArray[col, i].Copy;

            return null;
        }

        private void RemoveBlocks()
        {
            List<List<Tile>> blocks = FindBlocks();
            foreach (List<Tile> block in blocks)
                foreach (Tile tile in block)
                {
                    if (FadingTiles.Count > 0)
                        FadingTiles.Remove(tile);
                    TileArray[tile.Col, tile.Row].Figure = null;
                }

            ((GameplayScreen)ScreenManager.Instance.CurrentScreen).CurrentTime += 1;
            if (((GameplayScreen)ScreenManager.Instance.CurrentScreen).CurrentTime > ((GameplayScreen)ScreenManager.Instance.CurrentScreen).TimeLimit)
                ((GameplayScreen)ScreenManager.Instance.CurrentScreen).CurrentTime = ((GameplayScreen)ScreenManager.Instance.CurrentScreen).TimeLimit;

            ((GameplayScreen)ScreenManager.Instance.CurrentScreen).Score.Text = "SCORE: " + (10 * score).ToString();
            AdditionalState = FieldStates.TilesStoppedMoving;

            for (int i = Settings.ColsCount - 1; i >= 0; --i)
                for (int j = Settings.RowsCount - 1; j >= 0; -- j)
                    if (TileArray[i, j].Figure == null)
                    {
                        Tile firstAbove = FindFirstAbove(i, j);
                        if (firstAbove != null)
                        {
                            AdditionalState = FieldStates.TilesMoving;
                            TileArray[firstAbove.Col, firstAbove.Row].Figure = null;
                            firstAbove.InitiateMovement(TileArray[i, j].Position, 4);
                            firstAbove.Background.Position = TileArray[i, j].Background.Position;
                            firstAbove.Col = i;
                            firstAbove.Row = j;
                            TileArray[i, j] = firstAbove;
                        }
                    }
        }

        private List<List<Tile>> FindBlocks()
        {
            List<List<Tile>> blocks = new List<List<Tile>>();

            // Vertical
            for (int i = 0; i < Settings.ColsCount; ++i)
            {
                int blockLength = 1;
                for (int j = 0; j < Settings.RowsCount; ++j)
                    if (j < Settings.RowsCount - 1 && TileArray[i, j].GetType() == TileArray[i, j + 1].GetType())
                        ++blockLength;
                    else
                        CheckBlock(ref blocks, ref blockLength, j, i, true);
            }

            // Horizontal
            for (int j = 0; j < Settings.RowsCount; ++j)
            {
                int blockLength = 1;
                for (int i = 0; i < Settings.ColsCount; ++i)
                    if (i < Settings.ColsCount - 1 && TileArray[i, j].GetType() == TileArray[i + 1, j].GetType())
                        ++blockLength;
                    else
                        CheckBlock(ref blocks, ref blockLength, j, i, false);
            }

            return blocks;
        }

        private void CheckBlock(ref List<List<Tile>> blocks, ref int blockLength, int row, int col, bool isVertical)
        {
            if (blockLength >= 3)
            {
                List<Tile> block = new List<Tile>();
                for (int i = 0; i < blockLength; ++i)
                    block.Add(TileArray[col - (!isVertical ? i : 0), row - (isVertical ? i : 0)]);
                blocks.Add(block);
            }

            blockLength = 1;

        }

        private bool IsAdjacent(Tile first, Tile second)
        {
            return
                (first.Position.X == second.Position.X &&
                Math.Abs(first.Position.Y - second.Position.Y) == first.Height) ||
                (first.Position.Y == second.Position.Y &&
                Math.Abs(first.Position.X - second.Position.X) == first.Width);

        }

        private void SwapTiles(ref Tile first, ref Tile second)
        {
            Swap(ref TileArray[first.Col, first.Row], ref TileArray[second.Col, second.Row]);
            Swap(ref first.Background.Position, ref second.Background.Position);
            Swap(ref first.Col, ref second.Col);
            Swap(ref first.Row, ref second.Row);
        }

        private void Swap<T>(ref T first, ref T second)
        {
            T temp;
            temp = first;
            first = second;
            second = temp;
        }
    }
}
