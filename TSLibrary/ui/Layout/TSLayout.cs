using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TSLibrary.ui.Control;
using TSLibrary.Input;
using TSLibrary.ui.Control.ControlManager;

namespace TSLibrary.ui.Control.Layout
{
    public class TSLayout : TSControl
    {
        protected TSControlManager _controlManager;
        protected EventHandler _positionChange;
        protected EventHandler _sizeChange;

        protected Texture2D _backgroundImage;



        #region Property Region

        public TSControlManager ControlManager
        {
            get { return _controlManager; }
        }

        public override Vector2 Position
        {
            get { return base.Position; }
            set {
                base.Position = value;
                OnPositionChange(null);
            }
        }

        public override float PositionX
        {
            get { return base.PositionX; }
            set {
                base.PositionX = value;
                OnPositionChange(null);
            }
        }

        public override float PositionY
        {
            get { return base.PositionY; }
            set { 
                base.PositionY = value;
                OnPositionChange(null);
            }
        }

        public override int MarginTop
        {
            get { return base.MarginTop; }
            set {
                base.MarginTop = value;
                OnPositionChange(null);
                OnSizeChange(null);
            }
        }

        public override int MarginLeft
        {
            get { return base.MarginLeft; }
            set {
                base.MarginLeft = value;
                OnPositionChange(null);
                OnSizeChange(null);
            }
        }

        public override int MarginRight
        {
            get { return base.MarginRight; }
            set {
                base.MarginRight = value;
                OnPositionChange(null);
                OnSizeChange(null);
            }
        }

        public override int MarginBottom
        {
            get { return base.MarginBottom; }
            set {
                base.MarginBottom = value;
                OnPositionChange(null);
                OnSizeChange(null);
            }
        }

        public override int Width
        {
            get { return base.Width; }
            set {
                base.Width = value;
                OnSizeChange(null);
            }
        }

        public override int Height
        {
            get { return base.Height; }
            set {
                base.Height = value;
                OnSizeChange(null);
            }
        }

        public override TSControlManager ParentControlManager
        {
            get { return base.ParentControlManager; }
            set {
                base.ParentControlManager = value;
                OnPositionChange(null);
                OnSizeChange(null);
            }
        }

        public Texture2D BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        #endregion



        public TSLayout()
        {
            _controlManager = new TSControlManager();
        }

        public void Add(TSControl control)
        {
            ControlManager.Add(control);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled == false)
                return;

            ControlManager.Update(gameTime);
        }

        private Rectangle tempRect = new Rectangle();

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            tempRect.X = (int)PositionOnScreenX;
            tempRect.Y = (int)PositionOnScreenY;

            tempRect.Width = Width;
            tempRect.Height = Height;

            if (Visibled)
                if (BackgroundImage != null)
                    spriteBatch.Draw(BackgroundImage, tempRect, Color.White);


            ControlManager.DrawControls(gameTime, spriteBatch);
        }

        public override void OnParentPositionChange(EventArgs e)
        {
            base.OnParentPositionChange(e);
            ControlManager.ParentWidth = this.Width;
            ControlManager.ParentHeight = this.Height;
            ControlManager.OnParentPositionChange(e);
        }

        public override void OnParentSizeChange(EventArgs e)
        {
            base.OnParentSizeChange(e);
            ControlManager.ParentPositionOnScreen = this.PositionOnScreen;
            ControlManager.OnParentSizeChange(e);
        }

        public void OnPositionChange(EventArgs e)
        {
            ControlManager.ParentPositionOnScreen = PositionOnScreen;
            ControlManager.OnParentPositionChange(null);
        }

        public void OnSizeChange(EventArgs e)
        {
            ControlManager.ParentWidth = Width;
            ControlManager.ParentHeight = Height;
            ControlManager.OnParentSizeChange(null);
        }

        #region Event region


        public override void OnMouseClick(EventArgs e)
        {
            base.OnMouseClick(e);
        }

        public override void OnMouseDown(EventArgs e)
        {
            base.OnMouseDown(e);
        }

        public override void OnMouseUp(EventArgs e)
        {
            base.OnMouseUp(e);
        }

        public override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        public override void OnMouseMove(EventArgs e)
        {
            base.OnMouseMove(e);
        }

        public override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
        }

        #endregion
    }
}
