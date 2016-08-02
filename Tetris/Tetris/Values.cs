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
    public static class Values
    {
        public static Random RDM = new Random();
        public static Vector2 WindowSize = new Vector2(GameMaster.TetrisArray.GetLength(0) * gridSize + GameMaster.OutLineWidth * 2 + GameMaster.SideThingyWidth, 
            GameMaster.TetrisArray.GetLength(1) * gridSize + GameMaster.OutLineWidth * 2);
        public const int gridSize = 32;
    }
}
