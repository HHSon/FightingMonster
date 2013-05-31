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
        private Texture2D _background;
        private int _width;
        private int _height;

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
        /// Chiều rộng của ô
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Chiều cao của ô
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        #endregion


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int pX, int pY)
        {
            if (_background != null)
                spriteBatch.Draw(_background, new Rectangle(pX, pY, _width, _height), Color.White);
        }
    }

    public enum TSTileType : int
    {
        Accessible,
        Unaccessible,
        HPReduction,
        MNReduction
    }
}