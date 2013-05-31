using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FightingMonster.ResourceHelper;
using TSLibrary.ui.Control.Screen;
using TSLibrary.ui.Control.Button;
using TSLibrary.ui.Control.Layout;
using TSLibrary.ui.Control;
using TSLibrary.ui.Control.Label;
using TSLibrary.Input;
using Microsoft.Xna.Framework.Input;
using TSLibrary.Map;

namespace FightingMonster.Screen
{
    public class TestScreen : TSScreen
    {
        TSMap map;
        
        public TestScreen(GraphicsDeviceManager graphics)
            : base(graphics)
        {
            
        }


        public override void Initialize()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            map = new TSMap(100, 100, this.Width, this.Height);
            map.TileBackground = content.Load<Texture2D>(@"Image/Map/Untitled");
            map.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {

            map.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            map.Draw(gameTime, spriteBatch);
        }
    }
}