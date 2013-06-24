using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TSLibrary.ui.Control.ControlManager;
using TSLibrary.ui.Screen.ScreenManager;

namespace TSLibrary.ui.Control.Screen
{
    /// <summary>
    /// TSScreen là đối tượng vẽ lên toàn bộ màn hình vật lý
    /// </summary>
    public abstract class TSScreen : TSVisibleGameObject
    {
        protected TSControlManager _controlManager;
        protected Texture2D _backgroundImage;
        protected TSScreenManager _screenManager;
        
        #region Property region

        public TSControlManager ControlManager
        {
            get { return _controlManager; }
            protected set { _controlManager = value; }
        }

        public Texture2D BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        public TSScreenManager ScreenManager
        {
            get { return _screenManager; }
            set { _screenManager = value; }
        }

        #endregion

        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;




        public TSScreen(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            _controlManager = new TSControlManager();

            if (graphics.IsFullScreen)
            {
                _width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                _width = graphics.PreferredBackBufferWidth;
                _height = graphics.PreferredBackBufferHeight;
            }

            _controlManager.ParentPositionOnScreen = new Vector2(0, 0);
            _controlManager.ParentWidth = _width;
            _controlManager.ParentHeight = _height;
        }

        public virtual void LoadContent(ContentManager content)
        {
            _content = content;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled == false)
                return;

            this.ControlManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == true)
            {
                if (_backgroundImage != null)
                    spriteBatch.Draw(_backgroundImage, new Rectangle(0, 0, Width, Height), Color.White);
                this.ControlManager.DrawControls(gameTime, spriteBatch);
            }
        }

        public virtual void Add(TSControl control)
        {
            this.ControlManager.Add(control);
        }

        public virtual void Close()
        {
            if (ScreenManager != null)
                ScreenManager.Close(this);
        }
    }
}