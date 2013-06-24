using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TSLibrary
{
    /// <summary>
    /// Đối tượng gốc mà mọi đối tượng được vẽ lên màn hình kế thừa
    /// </summary>
    public abstract class TSVisibleGameObject : TSGameObject
    {
        protected bool _visibled;
        protected Vector2 _position;
        protected int _width;
        protected int _height;


        #region Property Region

        /// <summary>
        /// Cho phép đối tượng có được vẽ lên hay không
        /// </summary>
        public virtual bool Visibled
        {
            get { return _visibled; }
            set { _visibled = value; }
        }

        /// <summary>
        /// Vị trí của đối tượng
        /// </summary>
        public virtual Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual float PositionX
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public virtual float PositionY
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        /// <summary>
        /// Chiều rộng của đối tượng
        /// </summary>
        public virtual int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (_width < 0)
                    _width = 0;
            }
        }

        /// <summary>
        /// Chiều cao của đối tượng
        /// </summary>
        public virtual int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (_height < 0)
                    _height = 0;
            }
        }

        #endregion


        public TSVisibleGameObject()
        {
            _visibled = true;
            _width = 100;
            _height = 20;
        }
		
		public virtual void LoadContent(ContentManager content)
		{
		}

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}