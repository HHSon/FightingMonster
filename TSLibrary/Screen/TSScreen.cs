using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.VisibleEntity.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TSLibrary.Screen
{
    public class TSScreen : TSVisibleGameEntity
    {
        protected TSControlManager _controlManager;
        protected Texture2D _backgroundImage;
       

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

        #endregion


        public TSScreen(GraphicsDeviceManager graphics)
        {
            _controlManager = new TSControlManager();

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

            _controlManager.ParentPositionOnScreen = new Vector2(0, 0);
            _controlManager.ParentWidth = Width;
            _controlManager.ParentHeight = Height;
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _controlManager.DrawControls(gameTime, spriteBatch);
        }
    }
}
