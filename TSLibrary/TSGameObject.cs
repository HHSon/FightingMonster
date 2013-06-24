using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TSLibrary
{
    /// <summary>
    /// Đối tượng gốc mà các đối tượng khác kế thừa
    /// </summary>
    public abstract class TSGameObject
    {
        protected bool _enabled = true;

        /// <summary>
        /// Cho phép đối tượng có được update hay không
        /// </summary>
        public virtual bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /// <summary>
        /// Khởi tạo các dữ liệu trong lớp
        /// </summary>
        public virtual void Initialize() 
        {
        }

        /// <summary>
        /// Cập nhập đối tượng
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
