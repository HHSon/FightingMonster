using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TSLibrary.Control;
using TSLibrary.Input;
using TSLibrary.Layout;

namespace TSLibrary.VisibleEntity.Control
{
    public class TSControlManager : TSInvisibleGameEntity
    {
        public int ParentWidth { get; set; }
        public int ParentHeight { get; set; }
        public Vector2 ParentPositionOnScreen { get; set; }

        private List<TSControl> _controls;
        private bool _updateEnabled;


        protected TSControl lastMouseHoverControl;
        protected TSControl lastMousePressControl;


        #region Property Region

        public List<TSControl> Controls
        {
            get { return _controls; }
            protected set { _controls = value; }
        }

        public bool UpdateEnabled
        {
            get { return _updateEnabled; }
            set { _updateEnabled = value; }

        }

        #endregion

        

        public TSControlManager()
        {
            _controls = new List<TSControl>();
            _updateEnabled = true;
        }

        public void Add(TSControl control)
        {
            if (control != null)
            {
                Controls.Add(control);
                control.ParentControlManager = this;

                if (control is TSLayout)
                    ((TSLayout)control).ControlManager.UpdateEnabled = false;
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
            foreach (TSControl control in Controls)
                control.Update(gameTime);

            UpdateMouseEvent(gameTime);
        }

        protected void UpdateMouseEvent(GameTime gameTime)
        {
            TSControl mouseHoverControl = null;

            if (UpdateEnabled == false)
                return;

            mouseHoverControl = FindMouseHoverControl();

            if (mouseHoverControl == null)
                if (lastMouseHoverControl != null)
                {
                    lastMouseHoverControl.OnMouseLeave(null);
                    if (lastMouseHoverControl is TSLayout)
                    {
                        TSLayout mouseHoverLayout = ((TSLayout)lastMouseHoverControl);

                        mouseHoverLayout.ControlManager.UpdateEnabled = true;
                        mouseHoverLayout.ControlManager.Update(gameTime);
                        mouseHoverLayout.ControlManager.UpdateEnabled = false;
                    }

                    lastMouseHoverControl = null;
                }

            // truyền quyền Update cho layout con
            if ((mouseHoverControl != null) &&
                    (mouseHoverControl is TSLayout))
            {
                TSLayout mouseHoverLayout = ((TSLayout)mouseHoverControl);

                mouseHoverLayout.ControlManager.UpdateEnabled = true;
                mouseHoverLayout.ControlManager.Update(gameTime);
                mouseHoverLayout.ControlManager.UpdateEnabled = false;

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
