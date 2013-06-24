using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TSLibrary.Map
{
    public class TSTile
    {
        protected Texture2D _background;
        protected TSTileType _type;


        #region Property Region

        /// <summary>
        /// Ảnh nền của ô
        /// </summary>
        public Texture2D Background
        {
            get { return _background; }
            set { _background = value; }
        }

        /// <summary>
        /// Kiểu của ô
        /// </summary>
        public TSTileType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #endregion


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, 
                                int pX, int pY, int width, int height)
        {
            if (_background != null)
                spriteBatch.Draw(
                    _background,
                    new Rectangle(pX, pY, width, height),
                    Color.White);
        }
    }
}