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


namespace FightingMonter
{
    public class FightingMonsterGame : Microsoft.Xna.Framework.Game
    {
        StartScreen startScreen;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        
        public FightingMonsterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //graphics.IsFullScreen = true;

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

            startScreen = new StartScreen(graphics);
            startScreen.Initialize();
           
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ControlHelper.GetInstance().LoadContent(Content);
            startScreen.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            TSInputHandler.Update(gameTime);  // !: update đầu tiên

            startScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            startScreen.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}