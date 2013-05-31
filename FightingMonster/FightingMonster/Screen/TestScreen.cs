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
        private TSMap map;
        private TSButton btnClose;
        
        public TestScreen(GraphicsDeviceManager graphics)
            : base(graphics)
        {
            
        }


        public override void Initialize()
        {
            btnClose = new TSButton("Đóng");
            btnClose.Width = 100;
            btnClose.Height = 30;
            btnClose.MarginRight = 1;
            btnClose.MarginTop = 1;
            btnClose.MouseClick += btnClose_Click;
            this.Add(btnClose);
        }

        public override void LoadContent(ContentManager content)
        {
            ControlHelper.GetInstance().ToStandardButton(btnClose);

            map = new TSMap(100, 100, this.Width, this.Height);
            map.TileBackground = content.Load<Texture2D>(@"Image/Map/grass");
            map.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {

            map.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            map.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}