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

namespace Tetris
{
    public class Tetrimino
    {
        public Point Pos;
        public Point[] Cubes;
        public Color Col;
        public int Height;

        // Base Constructor
        public Tetrimino(Point Pos, Color Col, Point Qube1, Point Qube2, Point Qube3, Point Qube4, int Heigth)
        {
            this.Pos = Pos;
            Cubes = new Point[4];
            Cubes[0] = Qube1;
            Cubes[1] = Qube2;
            Cubes[2] = Qube3;
            Cubes[3] = Qube4;
            this.Col = Col;
            this.Height = Heigth;
        }
        
        // Charakter Constructor
        public Tetrimino(Point Pos, char a)
        {
            this.Pos = Pos;

            switch(a)
            {
                case 'I':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(-1, 0);
                    Cubes[2] = new Point(-2, 0);
                    Cubes[3] = new Point(1, 0);
                    Height = 1;
                    Col = Color.LightBlue;
                    break;

                case 'T':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(0, 1);
                    Cubes[2] = new Point(-1, 0);
                    Cubes[3] = new Point(1, 0);
                    Height = 1;
                    Col = Color.Purple;
                    break;

                case 'L':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(1, 1);
                    Cubes[2] = new Point(-1, 0);
                    Cubes[3] = new Point(1, 0);
                    Height = 1;
                    Col = Color.Orange;
                    break;

                case 'J':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(-1, 1);
                    Cubes[2] = new Point(-1, 0);
                    Cubes[3] = new Point(1, 0);
                    Height = 1;
                    Col = Color.Blue;
                    break;

                case 'O':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(0, 1);
                    Cubes[2] = new Point(-1, 0);
                    Cubes[3] = new Point(-1, 1);
                    Height = 1;
                    Col = Color.Yellow;
                    break;

                case 'S':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(1, 0);
                    Cubes[2] = new Point(0, 1);
                    Cubes[3] = new Point(-1, 1);
                    Height = 1;
                    Col = Color.Green;
                    break;

                case 'Z':
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(-1, 0);
                    Cubes[2] = new Point(0, 1);
                    Cubes[3] = new Point(1, 1);
                    Height = 1;
                    Col = Color.Red;
                    break;

                default:
                    Cubes = new Point[4];
                    Cubes[0] = new Point(0, 0);
                    Cubes[1] = new Point(-1, 0);
                    Cubes[2] = new Point(-2, 0);
                    Cubes[3] = new Point(1, 0);
                    Height = 1;
                    Col = Color.LightBlue;
                    break;
            }
        }

        // Copy-Constructor
        public Tetrimino(Tetrimino a)
        {
            Pos.X = a.Pos.X;
            Pos.Y = a.Pos.Y;
            Col = a.Col;
            Height = a.Height;
            Cubes = new Point[4];
            for (int i = 0; i < a.Cubes.Length; i++)
            {
                Cubes[i].X = a.Cubes[i].X;
                Cubes[i].Y = a.Cubes[i].Y;
            }
        }


        // GetDistances
        int GetLeftWidth()
        {
            int x = 0;
            for (int i = 0; i < Cubes.Length; i++)
            {
                if (x > Cubes[i].X)
                    x = Cubes[i].X;
            }

            return x;
        }

        int GetRightWidth()
        {
            int x = 0;
            for (int i = 0; i < Cubes.Length; i++)
            {
                if (x < Cubes[i].X)
                    x = Cubes[i].X;
            }

            return x;
        }

        int GetAboveHeigth()
        {
            int y = 0;
            for (int i = 0; i < Cubes.Length; i++)
            {
                if (y > Cubes[i].Y)
                    y = Cubes[i].Y;
            }

            return y;
        }

        int GetUnderneathHeigth()
        {
            int y = 0;
            for (int i = 0; i < Cubes.Length; i++)
            {
                if (y < Cubes[i].Y)
                    y = Cubes[i].Y;
            }

            return y;
        }


        // Checks
        public void CheckIfOutOfBounds()
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                if (Cubes[i].X + Pos.X < 0)
                {
                    Pos.X++;
                }

                if (Cubes[i].X + Pos.X > GameMaster.TetrisArray.GetLength(0) - 1)
                {
                    Pos.X--;
                }

                if (Cubes[i].Y + Pos.Y < 0)
                {
                    Pos.Y++;
                }
            }
        }

        public bool TouchedGround()
        {
            CheckIfOutOfBounds();
            try
            {
                if (Pos.Y + GetUnderneathHeigth() < GameMaster.TetrisArray.GetLength(1) - 1)
                {
                    for (int i = 0; i < Cubes.Length; i++)
                    {
                        if (GameMaster.TetrisArray[Pos.X + Cubes[i].X, Pos.Y + Cubes[i].Y + 1] != Color.Black)
                            return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }

            return false;
        }

        public bool CanGoRight()
        {
            CheckIfOutOfBounds();
            try
            {
                if (Pos.X + GetRightWidth() < GameMaster.TetrisArray.GetLength(0) - 1)
                {
                    for (int i = 0; i < Cubes.Length; i++)
                    {
                        if (GameMaster.TetrisArray[Pos.X + Cubes[i].X + 1, Pos.Y + Cubes[i].Y] != Color.Black)
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }

            return true;
        }

        public bool CanGoLeft()
        {
            CheckIfOutOfBounds();
            try
            {
                if (Pos.X - GetLeftWidth() > 0)
                {
                    for (int i = 0; i < Cubes.Length; i++)
                    {
                        if (GameMaster.TetrisArray[Pos.X + Cubes[i].X - 1, Pos.Y + Cubes[i].Y] != Color.Black)
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }

            return true;
        }


        // Movement
        public void Rotate()
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                int Swap = Cubes[i].X;
                Cubes[i].X = Cubes[i].Y;
                Cubes[i].Y = -Swap;
            }
            CheckIfOutOfBounds();
        }

        public void MoveDown()
        {
            Pos.Y++;
        }

        public void MoveRight()
        {
            if (CanGoRight())
                Pos.X++;
        }

        public void MoveLeft()
        {
            if (CanGoLeft())
                Pos.X--;
        }


        // Michellicious
        public void TurnSolid()
        {
            Assets.Solid.Play(0.3f, 0, 0);
            for (int i = 0; i < Cubes.Length; i++)
            {
                try
                {
                    GameMaster.TetrisArray[Pos.X + Cubes[i].X, Pos.Y + Cubes[i].Y] = Col;
                }
                catch { }
            }
        }

        public void Draw(SpriteBatch SB)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                SB.Draw(Assets.White, new Rectangle((Pos.X + Cubes[i].X) * Values.gridSize + GameMaster.Pos.X + GameMaster.OutLineWidth, (Pos.Y + Cubes[i].Y) * Values.gridSize + GameMaster.Pos.Y + GameMaster.OutLineWidth, Values.gridSize, Values.gridSize), Col);
                SB.Draw(Assets.Block, new Rectangle((Pos.X + Cubes[i].X) * Values.gridSize + GameMaster.Pos.X + GameMaster.OutLineWidth, (Pos.Y + Cubes[i].Y) * Values.gridSize + GameMaster.Pos.Y + GameMaster.OutLineWidth, Values.gridSize, Values.gridSize), Color.White);
            }
        }

        public void DrawAt(SpriteBatch SB, Point Pos)
        {
            int y = 0;
            for (int i = 0; i < Cubes.Length; i++)
            {
                if (Cubes[i].Y < y)
                    y = Cubes[i].Y;
            }
            Pos.Y -= y * Values.gridSize;

            for (int i = 0; i < Cubes.Length; i++)
            {
                SB.Draw(Assets.White, new Rectangle(Pos.X + Cubes[i].X * Values.gridSize, Pos.Y + Cubes[i].Y * Values.gridSize, Values.gridSize, Values.gridSize), Col);
                SB.Draw(Assets.Block, new Rectangle(Pos.X + Cubes[i].X * Values.gridSize, Pos.Y + Cubes[i].Y * Values.gridSize, Values.gridSize, Values.gridSize), Color.White);
            }
        }

        public void DrawOutlines(SpriteBatch SB)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                SB.Draw(Assets.OutLine, new Rectangle((Pos.X + Cubes[i].X) * Values.gridSize + GameMaster.Pos.X + GameMaster.OutLineWidth, (Pos.Y + Cubes[i].Y) * Values.gridSize + GameMaster.Pos.Y + GameMaster.OutLineWidth, Values.gridSize, Values.gridSize), Col);
                SB.Draw(Assets.Block, new Rectangle((Pos.X + Cubes[i].X) * Values.gridSize + GameMaster.Pos.X + GameMaster.OutLineWidth, (Pos.Y + Cubes[i].Y) * Values.gridSize + GameMaster.Pos.Y + GameMaster.OutLineWidth, Values.gridSize, Values.gridSize), Color.White);
            }
        }
    }
}
