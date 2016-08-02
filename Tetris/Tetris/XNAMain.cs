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
    public class XNAMain : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics; 
        SpriteBatch spriteBatch;
        Timer MusicStart;

        public XNAMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = (int)Values.WindowSize.X;
            graphics.PreferredBackBufferHeight = (int)Values.WindowSize.Y;
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            GameMaster.FillTetrisArray();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Load(Content, GraphicsDevice);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            Assets.Startup.Play(0.4f, 0, 0);
            MusicStart = new Timer(0, Assets.Startup.Duration.Seconds * 60 + 5, (object sender, EventArgs e) => { MediaPlayer.Play(Assets.Music); });
            MusicStart.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            Control.Update();
            MusicStart.Update();
            GameMaster.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(50, 50, 50, 256));
            spriteBatch.Begin();
            GameMaster.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
