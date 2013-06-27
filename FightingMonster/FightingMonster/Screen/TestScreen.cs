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
using System.Diagnostics;
using TSLibrary.GameEntity;
using FightingMonster.Character;

namespace FightingMonster.Screen
{
    public class TestScreen : TSScreen
    {
        private TSMap map;
        private TSButton btnClose;
        private BlueWarrior blue;
        
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

            map = new TSMap(this.Width, this.Height);

            blue = new BlueWarrior();
            blue.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            ControlHelper.GetInstance().ToStandardButton(btnClose);

            
            map.LoadContent("Content/Map/lv01.tmx", content);
            blue.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = TSInputHandler.MouseState;

            int d = 10;
            if (ms.X < d)
                map.Move(-5, 0);
            else if ((this.Width - d) <= ms.X && ms.X < this.Width)
                map.Move(5, 0);
            else if (ms.Y < d)
                map.Move(0, -5);
            else if ((this.Height - d) <= ms.Y && ms.Y < this.Height)
                map.Move(0, 5);

            if (ms.LeftButton == ButtonState.Pressed)
                blue.MoveTo(ms.X, ms.Y);

            map.Update(gameTime);
            blue.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            map.Draw(gameTime, spriteBatch);
            blue.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}