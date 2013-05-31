using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FightingMonster.ResourceHelper;
using TSLibrary.ui.Control.Screen;
using TSLibrary.ui.Control.Button;
using TSLibrary.ui.Control.Label;
using TSLibrary.ui.Control.Layout;
using FightingMonster.Screen;
using TSLibrary.ui.Control.MarginType;

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

        public StartScreen(GraphicsDeviceManager graphics) 
            : base(graphics)
        {
        }

        public override void Initialize()
        {
            lblMessage = new TSLabel();
            lblMessage.Width = 300;
            lblMessage.Height = 60;
            lblMessage.MarginLeft = 50;
            lblMessage.MarginTop = 80;
            lblMessage.TextColor = Color.White;
            this.Add(lblMessage);


            menuLayout = new TSLayout();
            menuLayout.MarginLeft = TSMarginType.CENTER; //-200;
            menuLayout.MarginBottom = TSMarginType.CENTER; //= 0;
            menuLayout.Width = 180;
            menuLayout.Height = 230;
            this.Add(menuLayout);


            btnStartGame = new TSButton("Bắt đầu");
            btnStartGame.Width = 180;
            btnStartGame.Height = 40;
            btnStartGame.MarginLeft = 0;
            btnStartGame.MarginTop = 0;
            btnStartGame.MouseClick += btnStartGame_Click;
            menuLayout.Add(btnStartGame);


            btnIntroduction = new TSButton("Giới thiệu");
            btnIntroduction.Width = 180;
            btnIntroduction.Height = 40;
            btnIntroduction.MarginLeft = 0;
            btnIntroduction.MarginTop = 45;
            btnIntroduction.MouseClick += btnIntroduction_Click;
            menuLayout.Add(btnIntroduction);


            btnSettings = new TSButton("Cài đặt");
            btnSettings.Width = 180;
            btnSettings.Height = 40;
            btnSettings.MarginLeft = 0;
            btnSettings.MarginTop = 90;
            btnSettings.MouseClick += btnSettings_Click;
            menuLayout.Add(btnSettings);


            btnAbout = new TSButton("Về trò chơi");
            btnAbout.Width = 180;
            btnAbout.Height = 40;
            btnAbout.MarginLeft = 0;
            btnAbout.MarginTop = 135;
            btnAbout.MouseClick += btnAbout_Click;
            menuLayout.Add(btnAbout);


            btnExitGame = new TSButton("Thoát");
            btnExitGame.Width = 180;
            btnExitGame.Height = 40;
            btnExitGame.MarginLeft = 0;
            btnExitGame.MarginTop = 180;
            btnExitGame.MouseClick += btnExitGame_Click;
            menuLayout.Add(btnExitGame);
        }

        public override void LoadContent(ContentManager content)
        {
            if (content == null)
                return;

            _backgroundImage = content.Load<Texture2D>(@"Image/Screen/trine_puzzle_video_game");

            ControlHelper controlResourceHelper = ControlHelper.GetInstance();

            controlResourceHelper.ToStandardButton(btnStartGame);
            controlResourceHelper.ToStandardButton(btnIntroduction);
            controlResourceHelper.ToStandardButton(btnSettings);
            controlResourceHelper.ToStandardButton(btnAbout);
            controlResourceHelper.ToStandardButton(btnExitGame);

            lblMessage.Font = controlResourceHelper.ButtonFont;

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled == false)
                return;

            /*delta += 2;
            if (menuLayout.MarginLeft < 30)
            {
                menuLayout.MarginLeft += delta;
            }*/

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == false)
                return;

            base.Draw(gameTime, spriteBatch);
        }

        public void btnExitGame_Click(object sender, EventArgs e)
        {
            this.ScreenManager.ExitGame();
        }

        public void btnStartGame_Click(object sender, EventArgs e)
        {
            if (sender == null)
                lblMessage.Text = "{null} Nút Bắt đầu được nhấn.";
            else
                lblMessage.Text = "{" + sender.ToString() + "}\nNút Bắt đầu được nhấn.";


            TestScreen testScreen = new TestScreen(_graphics);
            testScreen.Initialize();
            testScreen.LoadContent(_content);
            this.ScreenManager.Add(testScreen);
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
    }
}