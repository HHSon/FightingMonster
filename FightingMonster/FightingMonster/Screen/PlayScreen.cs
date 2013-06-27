using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.ui.Control.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TSLibrary.Map;
using TSLibrary.ui.Control.Layout;
using TSLibrary.ui.Control.MarginType;
using TSLibrary.ui.Control.Button;
using TSLibrary.ui.Control.Label;
using TSLibrary.Input;
using Microsoft.Xna.Framework.Input;
using FightingMonster.ResourceHelper;
using TSLibrary.GameEntity;
using FightingMonster.GameEntity;
using FightingMonster.Character;

namespace FightingMonster.Screen
{
    public class PlayScreen : TSScreen
    {
        protected Warrior mainWarrior;
        protected TSMap _map;
        protected List<TSGameEntity> _gameEntities;

        protected TSButton _btnContinueGame;
        protected TSButton _btnSaveAndExitGame;
        protected TSLayout _pauseGameLayout;


        public PlayScreen(GraphicsDeviceManager graphics)  : base(graphics)
        {
        }

        public override void Initialize()
        {
            // pauseGameLayout
            _pauseGameLayout = new TSLayout();
            _pauseGameLayout.Width = 200;
            _pauseGameLayout.Height = 150;
            _pauseGameLayout.ParentControlManager = this.ControlManager;
            _pauseGameLayout.MarginLeft = TSMarginType.CENTER;
            _pauseGameLayout.MarginTop = TSMarginType.CENTER;
            _pauseGameLayout.Visibled = false;
            _pauseGameLayout.Enabled = false;

            _btnContinueGame = new TSButton("Tiếp tục");
            _btnContinueGame.Width = 150;
            _btnContinueGame.Height = 50;
            _btnContinueGame.MarginTop = 10;
            _btnContinueGame.MarginLeft = TSMarginType.CENTER;
            _btnContinueGame.MouseClick += btnContinueGame_MouseClick;
            _pauseGameLayout.Add(_btnContinueGame);

            _btnSaveAndExitGame = new TSButton("Lưu và thoát game");
            _btnSaveAndExitGame.Width = 150;
            _btnSaveAndExitGame.Height = 50;
            _btnSaveAndExitGame.MarginTop = 70;
            _btnSaveAndExitGame.MarginLeft = TSMarginType.CENTER;
            _btnSaveAndExitGame.MouseClick += btnSaveAndExitGame_MouseClick;
            _pauseGameLayout.Add(_btnSaveAndExitGame);

            // gameEntities
            _gameEntities = new List<TSGameEntity>();
            Random random = new Random();
            int numEntities = 500;//random.Next(900, 2000);

            for (int idx = 0; idx < numEntities; idx++)
                _gameEntities.Add(new Tree());


            // map
            _map = new TSMap(Width, Height);

            // mainWarrior
            mainWarrior = new SilverWarrior();
            mainWarrior.Map = _map;

            base.Initialize();
        }


        public override void LoadContent(ContentManager content)
        {
            // map
            _map.LoadContent(@"Content/Map/lv01.tmx", content);

            // gameEntities
            Random random = new Random();
            int numEntities = _gameEntities.Count;

            for (int idx = 0; idx < numEntities; idx++) 
            {
                _gameEntities[idx].LoadContent(content);
                _gameEntities[idx].PositionX = random.Next(_map.NumColumns * _map.TileWidth);
                _gameEntities[idx].PositionY = random.Next(_map.NumRows * _map.TileHeight);
            }

            // pauseGameLayout
            _pauseGameLayout.BackgroundImage = content.Load<Texture2D>(@"Image/Control/Layout/Background");
            ControlHelper.GetInstance().ToStandardButton(_btnContinueGame);
            ControlHelper.GetInstance().ToStandardButton(_btnSaveAndExitGame);

            // mainWarrior
            mainWarrior.LoadContent(content);

            base.LoadContent(content);
        }


        public override void Update(GameTime gameTime)
        {
            _pauseGameLayout.Update(gameTime);

            if (Enabled == false)
                return;

            if (TSInputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Enabled = false;
                _pauseGameLayout.Enabled = true;
                _pauseGameLayout.Visibled = true;
            }


            _map.Update(gameTime);

            MouseState ms = TSInputHandler.MouseState;
            if (ms.LeftButton == ButtonState.Pressed)
                mainWarrior.MoveTo(ms.X + _map.P0.X, ms.Y + _map.P0.Y);

            mainWarrior.Update(gameTime);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _map.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
            
            foreach (TSGameEntity ge in _gameEntities)
                if (_map.IsInDrawnArea(ge.Position, ge.Width, ge.Height))
                    ge.Draw(ge.Position - _map.P0, gameTime, spriteBatch);


            mainWarrior.Draw(gameTime, spriteBatch);
            _pauseGameLayout.Draw(gameTime, spriteBatch);
        }


        public override void UnloadContent()
        {
            base.UnloadContent();
        }


        public override void Close()
        {
            base.Close();
        }

        #region Event Region

        public void btnContinueGame_MouseClick(object sender, EventArgs e)
        {
            if (_pauseGameLayout.Visibled)
                _pauseGameLayout.Visibled = false;

            if (_pauseGameLayout.Enabled)
                _pauseGameLayout.Enabled = false;

            if (this.Enabled == false)
                this.Enabled = true;
        }

        public void btnSaveAndExitGame_MouseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}