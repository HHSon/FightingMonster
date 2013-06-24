using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TSLibrary.ui.Control.ControlManager;
using TSLibrary.ui.Control.MarginType;


namespace TSLibrary.ui.Control
{
    /// <summary>
    /// Lớp control gốc
    /// </summary>
    public abstract class TSControl : TSVisibleGameObject
    {
        protected Vector2 _positionOnScreen;

        protected int _marginLeft;
        protected int _marginRight;
        protected int _marginTop;
        protected int _marginBottom;

        protected TSControlManager _parentControlManager;

        protected EventHandler _mouseEnter;
        protected EventHandler _mouseMove;
        protected EventHandler _mouseLeave;

        protected EventHandler _mouseClick;
        protected EventHandler _mouseDown;
        protected EventHandler _mouseUp;

        protected EventHandler _parentPositionChange;
        protected EventHandler _parentSizeChange;

        protected EventHandler _keyPress;
        protected EventHandler _keyDown;
        protected EventHandler _keyUp;

        protected EventHandler _focusEnter;
        protected EventHandler _focusLeave;


        #region Property Region

        public virtual int MarginLeft
        {
            get { return _marginLeft; }
            set
            {
                _marginLeft = value;
                CalculatePositionXAndWidth();
            }
        }

        public virtual int MarginRight
        {
            get { return _marginRight; }
            set
            {
                _marginRight = value;
                CalculatePositionXAndWidth();
            }
        }

        public virtual int MarginTop
        {
            get { return _marginTop; }
            set
            {
                _marginTop = value;
                CalculatePositionYAndHeight();
            }
        }

        public virtual int MarginBottom
        {
            get { return _marginBottom; }
            set
            {
                _marginBottom = value;
                CalculatePositionYAndHeight();
            }
        }

        public override Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                CalculatePositionAndSize();
            }
        }

        public override float PositionX
        {
            get { return _position.X; }
            set
            {
                _position.X = value;
                CalculatePositionXAndWidth();
            }
        }

        public override float PositionY
        {
            get { return _position.Y; }
            set
            {
                _position.Y = value;
                CalculatePositionYAndHeight();
            }
        }

        public virtual float PositionOnScreenX
        {
            get { return _positionOnScreen.X; }
            protected set { _positionOnScreen.X = value; }
        }

        public virtual float PositionOnScreenY
        {
            get { return _positionOnScreen.Y; }
            protected set { _positionOnScreen.Y = value; }
        }

        public virtual Vector2 PositionOnScreen
        {
            get { return _positionOnScreen; }
            protected set { _positionOnScreen = value; }
        }

        public override int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (_width < 0)
                    _width = 0;
                CalculatePositionXAndWidth();
            }
        }

        public override int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (_height < 0)
                    _height = 0;
                CalculatePositionYAndHeight();
            }
        }

        public virtual TSControlManager ParentControlManager
        {
            get { return _parentControlManager; }
            set
            {
                _parentControlManager = value;
                CalculatePositionAndSize();
            }
        }


        public EventHandler MouseClick
        {
            get { return _mouseClick; }
            set { _mouseClick = value; }
        }

        public EventHandler MouseDown
        {
            get { return _mouseDown; }
            set { _mouseDown = value; }
        }

        public EventHandler MouseUp
        {
            get { return _mouseUp; }
            set { _mouseUp = value; }
        }

        public EventHandler MouseEnter
        {
            get { return _mouseEnter; }
            set { _mouseEnter = value; }
        }

        public EventHandler MouseMove
        {
            get { return _mouseMove; }
            set { _mouseMove = value; }
        }

        public EventHandler MouseLeave
        {
            get { return _mouseLeave; }
            set { _mouseLeave = value; }
        }

        public EventHandler KeyPress
        {
            get { return _keyPress; }
            set { _keyPress = value; }
        }

        public EventHandler KeyDown
        {
            get { return _keyDown; }
            set { _keyDown = value; }
        }

        public EventHandler KeyUp
        {
            get { return _keyUp; }
            set { _keyUp = value; }
        }


        public EventHandler FocusEnter
        {
            get { return _focusEnter; }
            set { _focusEnter = value; }
        }

        public EventHandler FocusLeave
        {
            get { return _focusLeave; }
            set { _focusLeave = value; }
        }
        #endregion

        protected bool needCalculateSizeToDraw;
        
        public TSControl()
        {

            _marginTop = TSMarginType.NONE;
            _marginBottom = TSMarginType.NONE;
            _marginLeft = TSMarginType.NONE;
            _marginRight = TSMarginType.NONE;

            CalculatePositionAndSize(); // canh chỉnh lề
        }

        public virtual bool isMouseHover(int x, int y)
        {
            if ((x < PositionOnScreenX) ||
                    (x > (PositionOnScreenX + Width)) ||
                    (x > (ParentControlManager.ParentPositionOnScreen.X + ParentControlManager.ParentWidth)))
                return false;

            if ((y < PositionOnScreenY) ||
                    (y > (PositionOnScreenY + Height)) ||
                    (y > ParentControlManager.ParentPositionOnScreen.Y + ParentControlManager.ParentHeight))
                return false;

            return true;
        }

        public virtual void OnMouseEnter(EventArgs e)
        {
            if (_mouseEnter != null)
                _mouseEnter(this, e);
        }

        public virtual void OnMouseMove(EventArgs e)
        {
            if (_mouseMove != null)
                _mouseMove(this, e);
        }

        public virtual void OnMouseLeave(EventArgs e)
        {
            if (_mouseLeave != null)
                _mouseLeave(this, e);
        }

        public virtual void OnMouseClick(EventArgs e)
        {
            if (_mouseClick != null)
                _mouseClick(this, e);
        }

        public virtual void OnMouseDown(EventArgs e)
        {
            if (_mouseDown != null)
                _mouseDown(this, e);
        }

        public virtual void OnMouseUp(EventArgs e)
        {
            if (_mouseUp != null)
                _mouseUp(this, e);
        }

        public virtual void OnKeyPress(EventArgs e)
        {
            if (_keyPress != null)
                _keyPress(this, e);
        }

        public virtual void OnKeyDown(EventArgs e)
        {
            if (_keyDown != null)
                _keyDown(this, e);
        }

        public virtual void OnKeyUp(EventArgs e)
        {
            if (_keyUp != null)
                _keyUp(this, e);
        }

        public virtual void OnFocusEnter(EventArgs e)
        {
            if (_focusEnter != null)
                _focusEnter(this, e);
        }

        public virtual void OnFocusLeave(EventArgs e)
        {
            if (_focusLeave != null)
                _focusLeave(this, e);
        }

        protected void GetWidthAndHeightToDraw(ref int widthToDraw, ref int heightToDraw)
        {
            if (ParentControlManager == null)
            {
                widthToDraw = Width;
                heightToDraw = Height;
                return;
            }

            if ((ParentControlManager.ParentPositionOnScreen.X + ParentControlManager.ParentWidth) <
                (PositionOnScreenX + Width))
                widthToDraw = (int)(ParentControlManager.ParentPositionOnScreen.X + ParentControlManager.ParentWidth - PositionOnScreenX);
            else
                widthToDraw = Width;

            if ((ParentControlManager.ParentPositionOnScreen.Y + ParentControlManager.ParentHeight) <
                (PositionOnScreenY + Height))
                heightToDraw = (int)(ParentControlManager.ParentPositionOnScreen.Y + ParentControlManager.ParentHeight - PositionOnScreenY);
            else
                heightToDraw = Height;
        }

        public virtual void OnParentPositionChange(EventArgs e)
        {
            if (_parentPositionChange != null)
                _parentPositionChange(this, e);

            CalculatePositionAndSize();
        }

        public virtual void OnParentSizeChange(EventArgs e)
        {
            if (_parentSizeChange != null)
                _parentSizeChange(this, e);

            CalculatePositionAndSize();
        }

        protected void CalculatePositionAndSize()
        {
            CalculatePositionXAndWidth();
            CalculatePositionYAndHeight();
            needCalculateSizeToDraw = true;
        }

        protected void CalculatePositionXAndWidth()
        {
            if ((_marginLeft == TSMarginType.CENTER) ||
                (_marginRight == TSMarginType.CENTER))
            {
                if (_parentControlManager != null)
                    _position.X = (_parentControlManager.ParentWidth / 2) - (_width / 2);

                if (_parentControlManager != null)
                    _positionOnScreen.X = _position.X + _parentControlManager.ParentPositionOnScreen.X;
                else
                    _positionOnScreen.X = _position.X;

                return;
            }

            if (_marginLeft != TSMarginType.NONE)
                _position.X = _marginLeft;


            if (_marginRight != TSMarginType.NONE)
            {
                if (_parentControlManager != null)
                {
                    if (_marginLeft == TSMarginType.NONE)
                        _position.X = _parentControlManager.ParentWidth - _marginRight - _width;
                    else
                        _width = (_parentControlManager.ParentWidth - _marginRight) - (int)(_position.X);
                }
            }

            if (_parentControlManager != null)
                _positionOnScreen.X = _position.X + _parentControlManager.ParentPositionOnScreen.X;
            else
                _positionOnScreen.X = _position.X;
        }

        protected void CalculatePositionYAndHeight()
        {
            if ((_marginTop == TSMarginType.CENTER) ||
                (_marginBottom == TSMarginType.CENTER))
            {
                if (_parentControlManager != null)
                    _position.Y = _parentControlManager.ParentHeight / 2 - _height / 2;

                if (_parentControlManager != null)
                    _positionOnScreen.Y = _position.Y + _parentControlManager.ParentPositionOnScreen.Y;
                else
                    _positionOnScreen.Y = _position.Y;

                return;
            }

            if (_marginTop != TSMarginType.NONE)
                _position.Y = _marginTop;


            if (_marginBottom != TSMarginType.NONE)
            {
                if (_parentControlManager != null)
                {
                    if (_marginTop == TSMarginType.NONE)
                        _position.Y = _parentControlManager.ParentHeight - _marginBottom - _height;
                    else
                        _height = (_parentControlManager.ParentHeight - _marginBottom) - (int)(_position.Y);
                }
            }

            if (_parentControlManager != null)
                _positionOnScreen.Y = _position.Y + _parentControlManager.ParentPositionOnScreen.Y;
            else
                _positionOnScreen.Y = _position.Y;
        }
    }
}
