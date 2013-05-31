using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TSLibrary;
using Microsoft.Xna.Framework;

namespace FightingMonster.Sprite
{
    /// <summary>
    /// Lớp chứa danh sách các texture chạy tự động được sử dụng cho các animation sprite
    /// </summary>
    public class TSTexture2DArray : TSInvisibleGameEntity
    {
        private int _iTexture;
        private List<Texture2D> _textures;
        private int _delay;
        private bool _animation;



        #region Property Region

        /// <summary>
        /// Số lượng texture
        /// </summary>
        public int nTextures
        {
            get { return _textures.Count; }
        }

        /// <summary>
        /// Chỉ số của texture hiện tại. Trả về -1 nếu số lượng texture là 0
        /// </summary>
        public int iTexture
        {
            get { return _iTexture; }
            set {
                if (value < 0 || value >= _textures.Count)
                    throw new ArgumentException("Chỉ số iTexture không nằm trong vùng hợp lệ");
                _iTexture = value;
            }
        }

        /// <summary>
        /// Texture hiện tại. Trả về null nếu số lượng texture là 0
        /// </summary>
        public Texture2D CurrentTexture
        {
            get {
                if (_iTexture == -1)
                    return null;
                else
                    return _textures[_iTexture];
            }
        }

        /// <summary>
        /// Danh sách texture
        /// </summary>
        public List<Texture2D> Textures
        {
            get { return _textures; }
            set { _textures = value; }
        }

        /// <summary>
        /// Độ dừng tính theo số chu kì update của game. Mặc định Delay = 1
        /// </summary>
        public int Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        #endregion

        /// <summary>
        /// Cho phép chuyển liên tục qua các texture để tạo animation sprite. Mặc định
        /// Animation = false.
        /// </summary>
        public bool Animation
        {
            get { return _animation; }
            set { _animation = value; }
        }

        public TSTexture2DArray()
            : this(10)
        {
        }

        public TSTexture2DArray(int nTextures)
        {
            _textures = new List<Texture2D>(nTextures);
            _iTexture = -1;
            _delay = 1;
            _animation = false;
        }

        public bool Add(Texture2D texture)
        {
            if (texture != null)
            {
                _textures.Add(texture);
                return true;
            }
            else
                return false;
        }

        public bool Remove(Texture2D texture)
        {
            if (texture != null)
                return _textures.Remove(texture);
            else
                return false;
        }

        public override void Update(GameTime gameTime)
        {
            ///ToDo:
            base.Update(gameTime);
        }
    }
}
