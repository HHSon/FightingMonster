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
using FightingMonters.Screen;
using TSLibrary.Input;
using FightingMonster.ResourceHelper;
using FightingMonster.Screen;
using TSLibrary.ui.Screen.ScreenManager;
using TSLibrary.ui.Control.Screen;



namespace FightingMonter
{
    public class FightingMonsterGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TestScreen screen;
        TSScreenManager screenManager;

        public FightingMonsterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            if (graphics.IsFullScreen == true)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }

            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            TSInputHandler.Initialize();

            screen = new TestScreen(graphics);


            screenManager = new TSScreenManager();
            screenManager.Add(screen);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            TSInputHandler.Update(gameTime);
            screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            screenManager.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}