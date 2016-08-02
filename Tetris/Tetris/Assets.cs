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
    public static class Assets
    {
        public static SpriteFont Font;

        public static Song Music;

        public static Texture2D Block;
        public static Texture2D OutLine; 
        public static Texture2D White;

        public static SoundEffect LineClear;
        public static SoundEffect LineClearDouble;
        public static SoundEffect LineClearTriple;
        public static SoundEffect Rotate;
        public static SoundEffect Solid;
        public static SoundEffect Startup;
        public static SoundEffect Fall;
        public static SoundEffect Drop;

        public static void Load(ContentManager Content, GraphicsDevice GD)
        {
            Fall = Content.Load<SoundEffect>("Fall");
            Drop = Content.Load<SoundEffect>("Drop");
            LineClear = Content.Load<SoundEffect>("LineClear");
            LineClearDouble = Content.Load<SoundEffect>("LineClearDouble");
            LineClearTriple = Content.Load<SoundEffect>("LineClearTriple");
            Rotate = Content.Load<SoundEffect>("Rotate");
            Solid = Content.Load<SoundEffect>("Solid");
            Startup = Content.Load<SoundEffect>("Startup");
            Font = Content.Load<SpriteFont>("Font");
            Block = Content.Load<Texture2D>("Block");
            OutLine = Content.Load<Texture2D>("Outline");
            Music = Content.Load<Song>("Music");
            White = new Texture2D(GD, 1, 1);
            Color[] Col = new Color[1];
            Col[0] = Color.White;
            White.SetData<Color>(Col);
        }
    }
}
