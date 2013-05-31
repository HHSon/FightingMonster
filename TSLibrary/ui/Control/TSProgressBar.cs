using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSLibrary.ui.Control
{
    /// <summary>
    /// Lớp đối tượng để tạo một thanh progressBar
    /// </summary>
    public class TSProgressBar : TSControl
    {
        int _value;
        int _max;
        int _min;

        protected string _text;
        protected SpriteFont _font;
        protected Color _textColor;

        protected Texture2D _backgoundImage;
        protected Texture2D _progressImage;


        #region Property Region

        public int Value
        {
            get { return _value; }
            set { 
                _value = value;
                if (_value > _max)
                    _value = _max;

                if (_value < _min)
                    _value = _min;
            }
        }

        public int Max
        {
            get { return _max; }
            set { 
                _max = value;
                needCalculateSizeToDraw = true;
            }
        }

        public int Min
        {
            get { return _min; }
            set { 
                _min = value;
                needCalculateSizeToDraw = true;
            }
        }

        public string Text
        {
            get { return _text; }
            set { 
                _text = value;
                needCalculateSizeToDraw = true;
            }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        public SpriteFont Font
        {
            get { return _font; }
            set { 
                _font = value;
                needCalculateSizeToDraw = true;
            }
        }

        public Texture2D BackgroundImage
        {
            get { return _backgoundImage; }
            set { _backgoundImage = value; }
        }

        public Texture2D ProgressImage
        {
            get { return _progressImage; }
            set { _progressImage = value; }
        }

        #endregion



        public TSProgressBar()
        {
            _min = 0;
            _value = 0;
            _max = 100;
            _textColor = Color.White;
        }

        public TSProgressBar(string text) 
            : this()
        {
            _text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == false)
                return;

            // Vẽ nền
            Rectangle tempRect = new Rectangle();

            tempRect.X = (int)(PositionOnScreenX);
            tempRect.Y = (int)(PositionOnScreenY);
            tempRect.Width = Width;
            tempRect.Height = Height;

            if (BackgroundImage != null)
                spriteBatch.Draw(BackgroundImage, tempRect, Color.White);

            // Vẽ thanh progress bên trong
            if (Max != Min)
                tempRect.Width = (int)((float)(Value - Min) / (float)(Max - Min) * (float)Width);
            else
                tempRect.Width = 0;


            if (ProgressImage != null)
                spriteBatch.Draw(ProgressImage, tempRect, Color.White);

            // Vẽ chuỗi vào giữa
            if ((Text != null) && (Font != null))
            {
                Vector2 textSize = Font.MeasureString(Text);
                spriteBatch.DrawString(
                                Font, 
                                Text, new Vector2(PositionOnScreenX + (Width - textSize.X) / 2, PositionOnScreenY + (Height - textSize.Y) / 2), 
                                TextColor);
            }

        }
    }
}
