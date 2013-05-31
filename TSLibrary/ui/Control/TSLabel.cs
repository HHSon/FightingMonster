using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSLibrary.ui.Control.Label
{
    /// <summary>
    /// Đối tượng tạo layout
    /// </summary>
    public class TSLabel : TSControl
    {
        private string _text;
        private Color _textColor;
        private SpriteFont _font;


        #region Property Region

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        #endregion



        public TSLabel()
        {
        }

        public TSLabel(string text)
        {
            _text = text;
            _textColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if ((Text != null) && (Font != null))
                spriteBatch.DrawString(Font, Text, PositionOnScreen, TextColor);

            base.Draw(gameTime, spriteBatch);
        }
    }
}