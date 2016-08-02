using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tetris;

namespace Tetris
{
    public static class GameMaster
    {
        public static Point Pos = new Point(0, 0);
        public static int OutLineWidth = 12;
        public static Color OutLineColor = Color.FromNonPremultiplied(100, 100, 256, 256);
        public static int SideThingyWidth = 300;
        public static Tetrimino CurrentTetrimino;
        public static Tetrimino NextTetromino;
        public static Tetrimino OnHold;
        public static Color[,] TetrisArray = new Color[12, 20];
        public static List<Tetrimino> AllTetri = new List<Tetrimino>();
        public static Timer GameTimer = new Timer(0, 30, OnTimerFinished);
        public static int LinesFilledInThisCheck;
        public static int Points;

        public static void GetNewTetrimino()
        {
            if (NextTetromino != null)
            {
                CurrentTetrimino = new Tetrimino(NextTetromino);
            }
            else
            {
                CurrentTetrimino = new Tetrimino(AllTetri[Values.RDM.Next(AllTetri.Count)]);
            }
            NextTetromino = new Tetrimino(AllTetri[Values.RDM.Next(AllTetri.Count)]);
            if (CurrentTetrimino.TouchedGround())
            {
                FillTetrisArray();
            }
            CheckIfLineWasFilled();
        }

        public static void FillTetrisArray()
        {
            for (int ix = 0; ix < TetrisArray.GetLength(0); ix++)
            {
                for (int iy = 0; iy < TetrisArray.GetLength(1); iy++)
                {
                    TetrisArray[ix, iy] = Color.Black;
                }
            }
            GameTimer.Start();

            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'I'));
            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'J'));
            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'L'));
            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'O'));
            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'S'));
            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'T'));
            AllTetri.Add(new Tetrimino(new Point(TetrisArray.GetLength(0) / 2, 0), 'Z'));

            GetNewTetrimino();
            OnHold = null;
            if (Assets.Music != null) { MediaPlayer.Play(Assets.Music); }
            Points = 0;
        }

        static Tetrimino WhereWillTetrominoEndUp(Tetrimino a)
        {
            Tetrimino b = new Tetrimino(a);
            int y = 0;
            while (!b.TouchedGround())
            {
                b.MoveDown();
                y++;
            }
            return b;
        }

        public static void Update()
        {
            if (CurrentTetrimino == null)
            {
                GetNewTetrimino();
            }
            GameTimer.Update();

            if (Control.CurKS.IsKeyDown(Keys.W) && Control.LastKS.IsKeyUp(Keys.W))
            {
                CurrentTetrimino.Rotate();
                Assets.Rotate.Play(0.3f, 0, 0);
                if (CurrentTetrimino.TouchedGround())
                {
                    CurrentTetrimino.TurnSolid();
                    GetNewTetrimino();
                }
            }

            if (Control.CurKS.IsKeyDown(Keys.D) && Control.LastKS.IsKeyUp(Keys.D))
                CurrentTetrimino.MoveRight();

            if (Control.CurKS.IsKeyDown(Keys.A) && Control.LastKS.IsKeyUp(Keys.A))
                CurrentTetrimino.MoveLeft();

            if (Control.CurKS.IsKeyDown(Keys.S) && Control.LastKS.IsKeyUp(Keys.S)) {
                Assets.Drop.Play(0.4f, 0, 0);
                CurrentTetrimino = WhereWillTetrominoEndUp(CurrentTetrimino);
                CurrentTetrimino.TurnSolid();
                GetNewTetrimino(); }

            if (Control.CurKS.IsKeyDown(Keys.Q) && Control.LastKS.IsKeyUp(Keys.Q)) {
                if (OnHold == null)
                {
                    OnHold = new Tetrimino(CurrentTetrimino);
                    GetNewTetrimino();
                }
                else
                {
                    Tetrimino Swap = new Tetrimino(CurrentTetrimino);
                    CurrentTetrimino = new Tetrimino(OnHold);
                    OnHold = new Tetrimino(Swap);
                }
            }

            if (Control.CurKS.IsKeyDown(Keys.X) && Control.LastKS.IsKeyUp(Keys.X))
                FillTetrisArray();
        }

        public static void OnTimerFinished(object sender, EventArgs e)
        {
            Assets.Fall.Play(0.4f, 0.3f, 0);
            CurrentTetrimino.MoveDown();
            if (CurrentTetrimino.TouchedGround())
            {
                CurrentTetrimino.TurnSolid();
                GetNewTetrimino();
            }
            GameTimer.Start();
        }

        public static void CheckIfLineWasFilled()
        {
            for (int iy = 0; iy < TetrisArray.GetLength(1); iy++)
            {
                bool EncounteredAEmpltySpot = false;

                for (int ix = 0; ix < TetrisArray.GetLength(0); ix++)
                {
                    if (TetrisArray[ix, iy] == Color.Black)
                    {
                        EncounteredAEmpltySpot = true;
                    }
                }

                if (EncounteredAEmpltySpot == false)
                {
                    LinesFilledInThisCheck++;
                    for(int iz = iy; iz > 0; iz--)
                    {
                        for (int ix = 0; ix < TetrisArray.GetLength(0); ix++)
                        {
                            TetrisArray[ix, iz] = TetrisArray[ix, iz - 1];
                        }
                    }
                    for (int ix = 0; ix < TetrisArray.GetLength(0); ix++)
                    {
                        TetrisArray[ix, 0] = Color.Black;
                    }
                }
            }
            Points += LinesFilledInThisCheck * LinesFilledInThisCheck * 50;
            if (Assets.LineClear != null && Assets.LineClearDouble != null && Assets.LineClearTriple != null)
            {
                switch (LinesFilledInThisCheck)
                {
                    case 0:
                        break;

                    case 1:
                        Assets.LineClear.Play(0.4f, 0, 0);
                        break;

                    case 2:
                        Assets.LineClearDouble.Play(0.4f, 0, 0);
                        break;

                    default:
                        Assets.LineClearTriple.Play(0.4f, 0, 0);
                        break;
                }
            }
            LinesFilledInThisCheck = 0;
        }

        public static void Draw(SpriteBatch SB)
        {
            SB.Draw(Assets.White, new Rectangle(Pos.X, Pos.Y, OutLineWidth * 2 + TetrisArray.GetLength(0) * Values.gridSize, OutLineWidth * 2 + TetrisArray.GetLength(1) * Values.gridSize), OutLineColor);

            for (int ix = 0; ix < TetrisArray.GetLength(0); ix++)
            {
                for (int iy = 0; iy < TetrisArray.GetLength(1); iy++)
                {
                    SB.Draw(Assets.White, new Rectangle(ix * Values.gridSize + Pos.X + OutLineWidth, iy * Values.gridSize + Pos.Y + OutLineWidth, Values.gridSize, Values.gridSize), TetrisArray[ix, iy]);

                    if (TetrisArray[ix, iy] != Color.Black)
                    {
                        SB.Draw(Assets.Block, new Rectangle(ix * Values.gridSize + Pos.X + OutLineWidth, iy * Values.gridSize + Pos.Y + OutLineWidth, Values.gridSize, Values.gridSize), Color.White);
                    }
                    else
                    {
                        #region BadLookin' Background
                        //if (iy % 2 == 0)
                        //{
                        //    if (ix % 2 == 1)
                        //    {
                        //        SB.Draw(Assets.OutLine, new Rectangle(ix * Values.gridSize, iy * Values.gridSize, Values.gridSize, Values.gridSize), Color.White);
                        //    }
                        //}
                        //else
                        //{
                        //    if (ix % 2 == 0)
                        //    {
                        //        SB.Draw(Assets.OutLine, new Rectangle(ix * Values.gridSize, iy * Values.gridSize, Values.gridSize, Values.gridSize), Color.White);
                        //    }
                        //}
                        #endregion
                    }
                }
            }

            if (CurrentTetrimino != null)
            {
                CurrentTetrimino.Draw(SB);
                WhereWillTetrominoEndUp(CurrentTetrimino).DrawOutlines(SB);
            }

            SB.DrawString(Assets.Font, "Points: " + Points.ToString(), 
                new Vector2(Pos.X + Values.gridSize * TetrisArray.GetLength(0) + OutLineWidth * 2 + SideThingyWidth / 2 - Assets.Font.MeasureString("Points: " + Points.ToString()).X / 2, 
                Pos.Y + 12), Color.White);

            SB.DrawString(Assets.Font, "Next:", 
                new Vector2(Pos.X + Values.gridSize * TetrisArray.GetLength(0) + OutLineWidth * 2 + Values.gridSize * 2 - Assets.Font.MeasureString("Next:").X / 2, 
                Pos.Y + 12 * 2 + Assets.Font.MeasureString("Points: " + Points.ToString()).Y), Color.White);

            NextTetromino.DrawAt(SB, new Point(Pos.X + Values.gridSize * TetrisArray.GetLength(0) + OutLineWidth * 2 + Values.gridSize * 2, 
                Pos.Y + 12 * 2 + (int)Assets.Font.MeasureString("Points: " + Points.ToString()).Y * 2));

            SB.DrawString(Assets.Font, "On Hold:",
                new Vector2(Pos.X + Values.gridSize * TetrisArray.GetLength(0) + OutLineWidth * 2 + Values.gridSize * 2 - Assets.Font.MeasureString("Next:").X / 2,
                Pos.Y + 12 * 3 + Assets.Font.MeasureString("Points: " + Points.ToString()).Y * 2 + Values.gridSize * 3), Color.White);

            if (OnHold != null)
                OnHold.DrawAt(SB, new Point(Pos.X + Values.gridSize * TetrisArray.GetLength(0) + OutLineWidth * 2 + Values.gridSize * 2,
                Pos.Y + 12 * 3 + (int)Assets.Font.MeasureString("Points: " + Points.ToString()).Y * 3 + Values.gridSize * 3));

        }
    }
}
