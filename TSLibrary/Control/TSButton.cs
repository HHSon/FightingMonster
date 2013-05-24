using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TSLibrary.Control
{
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

        Rectangle tempSrcRect = new Rectangle();
        Rectangle tempDestRect = new Rectangle();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            tempSrcRect.X = (int)PositionOnScreenX;
            tempSrcRect.Y = (int)PositionOnScreenY;
            tempSrcRect.Width = Width;
            tempSrcRect.Height = Height;

            int widthToDraw;

            if ((ParentControlManager.ParentPositionOnScreen.X + ParentControlManager.ParentWidth) <
                (PositionOnScreenX + Width))
                widthToDraw = (int)(ParentControlManager.ParentPositionOnScreen.X + ParentControlManager.ParentWidth - PositionOnScreenX);
            else
                widthToDraw = Width;

            int heightToDraw;
            if ((ParentControlManager.ParentPositionOnScreen.Y + ParentControlManager.ParentHeight) <
                (PositionOnScreenY + Height))
                heightToDraw = (int)(ParentControlManager.ParentPositionOnScreen.Y + ParentControlManager.ParentHeight - PositionOnScreenY);
            else
                heightToDraw = Height;

            if (widthToDraw < 0 || heightToDraw < 0)
                return;

            tempDestRect.X = (int)PositionOnScreenX;
            tempDestRect.Y = (int)PositionOnScreenY;
            tempDestRect.Width = widthToDraw;
            tempDestRect.Height = heightToDraw;


            if (Visibled)
                if (currentImage != null)
                    spriteBatch.Draw(currentImage, tempDestRect, tempSrcRect, Color.White);




            // draw text
            if (currrentFont != null)
                if (String.IsNullOrEmpty(Text) == false)
                {
                    Vector2 messureString = currrentFont.MeasureString(Text);
                    int x = (int)PositionOnScreenX + (Width / 2 - (int)messureString.X / 2);
                    int y = (int)PositionOnScreenY + (Height / 2 - (int)messureString.Y / 2);

                    spriteBatch.DrawString(
                        currrentFont,
                        Text,
                        new Vector2(x, y),
                        TextColor);
                }
        }

        public override void OnMouseClick(EventArgs e)
        {
            currentImage = _actionPerformedImage;
            base.OnMouseClick(e);
        }

        public override void OnMouseDown(EventArgs e)
        {
            base.OnMouseDown(e);
        }

        public override void OnMouseUp(EventArgs e)
        {
            currentImage = _focusedImage;
            base.OnMouseUp(e);
        }

        public override void OnMouseEnter(EventArgs e)
        {
            currentImage = _focusedImage;
            base.OnMouseEnter(e);
        }

        public override void OnMouseLeave(EventArgs e)
        {
            currentImage = _backgroundImage;
            base.OnMouseLeave(e);
        }
    }
}
