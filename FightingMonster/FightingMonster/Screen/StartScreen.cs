using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.VisibleEntity.Control;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TSLibrary.Screen;
using TSLibrary.Control;
using TSLibrary.Layout;
using FightingMonster.ResourceHelper;

namespace FightingMonters.Screen
{
    public class StartScreen : TSScreen
    {
        private TSButton btnStartGame;
        private TSButton btnIntroduction;
        private TSButton btnSettings;
        private TSButton btnAbout;
        private TSButton btnExitGame;
        private TSLabel  lblMessage;

        private TSLayout menuLayout;

        private int delta = 0;

        private GraphicsDeviceManager graphics;

        public StartScreen(GraphicsDeviceManager graphics)
            : base(graphics)
        {
            this.graphics = graphics;
        }

        public override void Initialize()
        {
            lblMessage = new TSLabel();
            lblMessage.MarginLeft = 50;
            lblMessage.MarginTop = 80;
            lblMessage.Width = 300;
            lblMessage.Height = 60;
            lblMessage.TextColor = Color.White;
            this.ControlManager.Add(lblMessage);


            menuLayout = new TSLayout();
            menuLayout.MarginLeft = -200;
            menuLayout.MarginBottom = 0;
            menuLayout.Width = 180;
            menuLayout.Height = 230;
            this.ControlManager.Add(menuLayout);

            int buttonWidth = 180;
            int buttonHeight = 40;

            btnStartGame = new TSButton("Bắt đầu");
            btnStartGame.Width = buttonWidth;
            btnStartGame.Height = buttonHeight;
            btnStartGame.MouseClick += btnStartGame_Click;
            menuLayout.ControlManager.Add(btnStartGame);

            btnIntroduction = new TSButton("Giới thiệu");
            btnIntroduction.Width = buttonWidth;
            btnIntroduction.Height = buttonHeight;
            btnIntroduction.MouseClick += btnIntroduction_Click;
            menuLayout.ControlManager.Add(btnIntroduction);

            btnSettings = new TSButton("Cài đặt");
            btnSettings.Width = buttonWidth;
            btnSettings.Height = buttonHeight;
            btnSettings.MouseClick += btnSettings_Click;
            menuLayout.ControlManager.Add(btnSettings);

            btnAbout = new TSButton("Về trò chơi");
            btnAbout.Width = buttonWidth;
            btnAbout.Height = buttonHeight;
            btnAbout.MouseClick += btnAbout_Click;
            menuLayout.ControlManager.Add(btnAbout);

            btnExitGame = new TSButton("Thoát");
            btnExitGame.Width = buttonWidth;
            btnExitGame.Height = buttonHeight;
            btnExitGame.MouseClick += btnExit_Click;
            menuLayout.ControlManager.Add(btnExitGame);

            btnStartGame.MarginLeft = 0;
            btnStartGame.MarginTop = 0;

            btnIntroduction.MarginLeft = 0;
            btnIntroduction.MarginTop = 45;  // buttonWidth = 40

            btnSettings.MarginLeft = 0;
            btnSettings.MarginTop =  90;

            btnAbout.MarginLeft = 0;
            btnAbout.MarginTop = 135;

            btnExitGame.MarginLeft = 0;
            btnExitGame.MarginTop = 185;
        }

        public override void LoadContent(ContentManager content)
        {
            if (content == null)
                return;

            _backgroundImage = content.Load<Texture2D>(@"Image/Screen/Background");


            ControlHelper controlResourceHelper = ControlHelper.GetInstance();

            controlResourceHelper.ToNormalButton(btnStartGame);
            controlResourceHelper.ToNormalButton(btnIntroduction);
            controlResourceHelper.ToNormalButton(btnSettings);
            controlResourceHelper.ToNormalButton(btnAbout);
            controlResourceHelper.ToNormalButton(btnExitGame);

            lblMessage.Font = controlResourceHelper.ButtonFont;
        }

        public override void Update(GameTime gameTime)
        {
            delta += 2;
            if (menuLayout.MarginLeft < 30)
                menuLayout.MarginLeft += delta;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_backgroundImage != null)
                spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Width, Height), Color.White);

            base.Draw(gameTime, spriteBatch);
        }


        public void OnSizeChange()
        {
            if (graphics.IsFullScreen)
            {
                Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                Width = graphics.PreferredBackBufferWidth;
                Height = graphics.PreferredBackBufferHeight;
            }

            ControlManager.ParentWidth = Width;
            ControlManager.ParentHeight = Height;
            ControlManager.OnParentSizeChange(null);
        }


        public void btnStartGame_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Nút Bắt đầu được nhấn.";
        }

        public void btnIntroduction_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Nút Giới thiệu được nhấn.";
        }

        public void btnSettings_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Nút Cài đặt được nhấn.";
        }

        public void btnAbout_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Sinh viên thực hiện 1012355 - 1012435";
        }

        public void btnExit_Click(object sender, EventArgs e)
        {
        }
    }
}