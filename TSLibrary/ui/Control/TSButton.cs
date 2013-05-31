using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TSLibrary.ui.Control.Button
{
    /// <summary>
    /// Lớp đối tượng tạo các button
    /// </summary>
    public class TSButton : TSControl
    {
        protected Texture2D _backgroundImage;
        protected Texture2D _focusedImage;
        protected Texture2D _actionPerformedImage;

        protected String _text;
        protected Color _textColor;

        protected SpriteFont _font;
        protected SpriteFont _focusedFont;
        protected SpriteFont _clickedFont;

        #region Property region

        public Texture2D BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                _backgroundImage = value;
                if (_backgroundImage != null)
                {
                    if (_focusedImage == null)
                        _focusedImage = _backgroundImage;

                    if (_actionPerformedImage == null)
                        _actionPerformedImage = _backgroundImage;

                    if (currentImage == null)
                        currentImage = _backgroundImage;
                }
            }
        }

        public Texture2D FocusedImage
        {
            get { return _focusedImage; }
            set { _focusedImage = value; }
        }

        public Texture2D ActionPerformedImage
        {
            get { return _actionPerformedImage; }
            set { _actionPerformedImage = value; }
        }

        public String Text
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
            set
            {
                _font = value;
                if (_font != null)
                {
                    if (_focusedFont == null)
                        _focusedFont = _font;

                    if (_clickedFont == null)
                        _clickedFont = _font;

                    if (currrentFont == null)
                        currrentFont = _font;
                }
            }
        }

        public SpriteFont FocusedFont
        {
            get { return _focusedFont; }
            set { _focusedFont = value; }
        }

        public SpriteFont ClickedFont
        {
            get { return _clickedFont; }
            set { _clickedFont = value; }
        }

        #endregion

        protected Texture2D currentImage;
        protected SpriteFont currrentFont;


        public TSButton()
        {
        }

        public TSButton(String text)
        {
            _text = text;
            _textColor = Color.Black;
        }

        private Rectangle tempSrcRect = new Rectangle();
        private Rectangle tempDestRect = new Rectangle();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == false)
                return;

            if (needCalculateSizeToDraw == true)
            {
                tempSrcRect.X = (int)PositionOnScreenX;
                tempSrcRect.Y = (int)PositionOnScreenY;
                tempSrcRect.Width = Width;
                tempSrcRect.Height = Height;


                tempDestRect.X = (int)PositionOnScreenX;
                tempDestRect.Y = (int)PositionOnScreenY;
                GetWidthAndHeightToDraw(ref tempDestRect.Width, ref tempDestRect.Height);

                if ((tempDestRect.Width < 0) || (tempDestRect.Height < 0))
                    return;
            }

            
            if (currentImage != null)
                spriteBatch.Draw(currentImage, tempDestRect, tempSrcRect, Color.White);


            // draw text
            if (currrentFont != null)
                if (String.IsNullOrEmpty(Text) == false)
                {
                    Vector2 messureString = currrentFont.MeasureString(Text);
                    int x = (int)PositionOnScreenX + (Width - (int)messureString.X) / 2;
                    int y = (int)PositionOnScreenY + (Height - (int)messureString.Y) / 2;

                    spriteBatch.DrawString(
                        currrentFont,
                        Text,
                        new Vector2(x, y),
                        TextColor);
                }
        }

        public override void OnMouseClick(EventArgs e)
        {
            currrentFont = _clickedFont;
            currentImage = _actionPerformedImage;
            base.OnMouseClick(e);
        }

        public override void OnMouseDown(EventArgs e)
        {
            base.OnMouseDown(e);
        }

        public override void OnMouseUp(EventArgs e)
        {
            currrentFont = _focusedFont;
            currentImage = _focusedImage;
            base.OnMouseUp(e);
        }

        public override void OnMouseEnter(EventArgs e)
        {
            currrentFont = _focusedFont;
            currentImage = _focusedImage;
            base.OnMouseEnter(e);
        }

        public override void OnMouseLeave(EventArgs e)
        {
            currrentFont = _font;
            currentImage = _backgroundImage;
            base.OnMouseLeave(e);
        }
    }
}
