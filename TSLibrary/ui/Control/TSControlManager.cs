using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TSLibrary.ui.Control;
using TSLibrary.Input;
using TSLibrary.ui.Control.Layout;


namespace TSLibrary.ui.Control.ControlManager
{
    /// <summary>
    /// Chịu trách nhiệm quản lý các control con trong một layout hay một sreen
    /// </summary>
    public class TSControlManager : TSInvisibleGameEntity
    {
        public int ParentWidth { get; set; }
        public int ParentHeight { get; set; }
        public Vector2 ParentPositionOnScreen { get; set; }

        private List<TSControl> _controls;


        protected TSControl lastMouseHoverControl;
        protected TSControl lastMousePressControl;
        protected TSControl focusingControl;

        #region Property Region

        public List<TSControl> Controls
        {
            get { return _controls; }
            protected set { _controls = value; }
        }

        #endregion

        

        public TSControlManager()
        {
            _controls = new List<TSControl>();
        }

        public void Add(TSControl control)
        {
            if (control != null)
            {
                Controls.Add(control);
                control.ParentControlManager = this;

                if (control is TSLayout)
                    ((TSLayout)control).ControlManager.Enabled = false;
            }
        }

        public bool Remove(TSControl control)
        {
            if (control == null)
                return false;

            return Controls.Remove(control);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled == true)
            {
                foreach (TSControl control in Controls)
                    control.Update(gameTime);

                UpdateMouseEvent(gameTime);
                UpdateKeyboardEvent(gameTime);
            }
        }

        protected void UpdateMouseEvent(GameTime gameTime)
        {
            TSControl mouseHoverControl = null;

            if (Enabled == false)
                return;

            mouseHoverControl = FindMouseHoverControl();

            if (mouseHoverControl == null)
                if (lastMouseHoverControl != null)
                {
                    lastMouseHoverControl.OnMouseLeave(null);
                    if (lastMouseHoverControl is TSLayout)
                    {
                        TSLayout mouseHoverLayout = ((TSLayout)lastMouseHoverControl);

                        mouseHoverLayout.ControlManager.Enabled = true;
                        mouseHoverLayout.ControlManager.Update(gameTime);
                        mouseHoverLayout.ControlManager.Enabled = false;
                    }

                    lastMouseHoverControl = null;
                }

            // truyền quyền Update cho layout con
            if ((mouseHoverControl != null) &&
                    (mouseHoverControl is TSLayout))
            {
                TSLayout mouseHoverLayout = ((TSLayout)mouseHoverControl);

                mouseHoverLayout.ControlManager.Enabled = true;
                mouseHoverLayout.ControlManager.Update(gameTime);
                mouseHoverLayout.ControlManager.Enabled = false;

                lastMouseHoverControl = mouseHoverControl;
            }

            // Kiểm tra sự kiện di chuyển của chuột

            if ((mouseHoverControl != null) && 
                    (mouseHoverControl != lastMouseHoverControl))
                mouseHoverControl.OnMouseEnter(null);

            if ((lastMouseHoverControl != null) && 
                    (mouseHoverControl != lastMouseHoverControl))
                lastMouseHoverControl.OnMouseLeave(null);

            if ((mouseHoverControl != null) && 
                    (lastMouseHoverControl != null) &&
                    (lastMouseHoverControl == mouseHoverControl))
                mouseHoverControl.OnMouseMove(null);


            lastMouseHoverControl = mouseHoverControl;


            // Kiểm tra sự kiện nhấn của chuột
            
            if (TSInputHandler.MouseState.LeftButton == ButtonState.Released)
                if (lastMousePressControl != null)
                {
                    lastMousePressControl.OnMouseUp(null);
                    lastMousePressControl = null;
                    return;
                }

            if (mouseHoverControl == null)
                return;


            if (TSInputHandler.LastMouseState.LeftButton == ButtonState.Pressed)
                if (mouseHoverControl == lastMousePressControl)
                {
                    mouseHoverControl.OnMouseDown(null);
                }
                else
                {
                    mouseHoverControl.OnMouseClick(null);
                    lastMousePressControl = mouseHoverControl;

                    if ((focusingControl != null) && (focusingControl == mouseHoverControl))
                    {
                        return;
                    }

                    if (focusingControl != null)
                        focusingControl.OnFocusLeave(null);

                    focusingControl = mouseHoverControl;
                    focusingControl.OnFocusEnter(null);
                }
        }

        protected void UpdateKeyboardEvent(GameTime gameTime)
        {
            if (Enabled == false)
                return;

            if (focusingControl == null)
                return;

            Keys[] pressedKey = TSInputHandler.KeyboardState.GetPressedKeys();
            Keys[] lastPressedKey = TSInputHandler.LastKeyboardState.GetPressedKeys();


            if ((pressedKey == null) && (lastPressedKey == null))
                return;

            if ((pressedKey.Length == 0) && (lastPressedKey.Length == 0))
                return;


            if ((pressedKey == null) || (pressedKey.Length == 0))
                if (lastPressedKey != null && lastPressedKey.Length > 0)
                    focusingControl.OnKeyUp(null);

            if ((pressedKey != null) && (pressedKey.Length > 0))
            {
                if (lastPressedKey != null && lastPressedKey.Length > 0 && pressedKey.Length == lastPressedKey.Length)
                    focusingControl.OnKeyDown(null);
                else
                    focusingControl.OnKeyPress(null);
            }

        }

        protected TSControl FindMouseHoverControl()
        {
            for (int idx = Controls.Count - 1; idx >= 0; idx--)
                if (Controls[idx].isMouseHover(TSInputHandler.MouseState.X, TSInputHandler.MouseState.Y))
                    return Controls[idx];

            return null;  
        }


        public void DrawControls(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (TSControl c in Controls)
                c.Draw(gameTime, spriteBatch);
        }

        public void OnParentPositionChange(EventArgs e)
        {
            foreach (TSControl control in Controls)
                control.OnParentPositionChange(e);
        }

        public void OnParentSizeChange(EventArgs e)
        {
            foreach (TSControl control in Controls)
                control.OnParentSizeChange(e);
        }
    }
}
