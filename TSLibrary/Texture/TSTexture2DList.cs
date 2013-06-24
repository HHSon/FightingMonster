using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

/*Tìm kiếm những kĩ thuật đã sử dụng ít nhất 2 lần trong project này*/

namespace TSLibrary.Texture
{
    /// <summary>
    /// Lớp chứa danh sách các texture2D. Lớp này có thể dùng để chứa các texture2D trong
    /// các lớp sprite hoặc animation sprite
    /// </summary>
    public class TSTexture2DList : TSInvisibleGameObject
    {
        protected int _iTexture;
        protected List<Texture2D> _textures;
        protected bool _isAnimated;
        protected int _delay;


        #region Property Region

        /// <summary>
        /// Chỉ số của texture hiện tại. Trả về -1 nếu số lượng texture bằng 0
        /// </summary>
        public int iTexture
        {
            get { return _iTexture; }
            set { _iTexture = value; }
        }

        /// <summary>
        /// Texture hiện tại (có chỉ số là iTexture). Trả về null nếu số lượng texture bằng 0
        /// </summary>
        public Texture2D CurrentTexture
        {
            get {
                if ((0 <= _iTexture) && (_iTexture < _textures.Count))
                    return _textures[_iTexture];
                else
                    return null;
            }
        }

        /// <summary>
        /// Danh sách các texture. Danh sách này luôn luôn khác null
        /// </summary>
        public List<Texture2D> Textures
        {
            get { return _textures; }
            protected set {
                if (value != null)
                    _textures = value;
                else
                    _textures.Clear();
            }
        }

        /// <summary>
        /// Nếu IsAnimated bằng true thì sau mỗi delay lần chu kì update
        /// hình ảnh hiện tại sẽ chuyển sang hình ảnh kế tiếp trong danh sách,
        /// lặp lại phần tử đầu tiên nếu đến cuối danh sách
        /// </summary>
        public bool IsAnimated
        {
            get { return _isAnimated; }
            set { _isAnimated = value; }
        }

        /// <summary>
        /// Độ dừng khi chuyển các texture. Tính theo số chu kì update của lớp
        /// nếu delay = 10 thì sau 10 chu kì texture mới chuyển 1 lần
        /// </summary>
        public int delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        #endregion


        protected int delayCount;


        /// <summary>
        /// Tạo một TSTexture2DList mới
        /// </summary>
        public TSTexture2DList()
        {
            _textures = new List<Texture2D>();
            _iTexture = 0;
            _isAnimated = false;
            _delay = 5;
            delayCount = 0;
        }

        /// <summary>
        /// Khởi tạo một đối tượng TSTexture2DList với số texture lưu trong srcTexture. Chương
        /// trình sẽ cắt srcTexture thành một danh sách gồm (numRows x numColumns) texture con
        /// bên trong
        /// </summary>
        public TSTexture2DList(Texture2D srcTexture, int numRows, int numColumns) : this()
        {
            
			InitFromTexture(srcTexture, numRows, numColumns);
        }
		
		protected void CheckNumRowsAndColumns(int numRows, int numColumns) 
		{
			if (numRows < 0)
                throw new ArgumentException("Biến numRows phải lớn hơn 0");

            if (numColumns < 0)
                throw new ArgumentException("Biến numColumns phải lớn hơn 0");
		}
		
		protected void InitFromTexture(Texture2D srcTexture, int numRows, int numColumns)
		{
			CheckNumRowsAndColumns(numRows, numColumns);
			
			int subTextureWidth = srcTexture.Width / numColumns;
			int subTextureHeight = srcTexture.Height / numRows;
			
			_textures = TSTexture2DHelper.CropImageToTiles(srcTexture, subTextureWidth, subTextureHeight);
		}
		
		///<summary>
		/// Nạp các texture từ file trong content
		///</summary>
		public void LoadContent(String filePath, int numRows, int numColumns, ContentManager content)
		{
			Texture2D srcTexture = content.Load<Texture2D>(filePath);
			InitFromTexture(srcTexture, numRows, numColumns);
		}

        /// <summary>
        /// Thêm một texture
        /// </summary>
        public void add(Texture2D texture)
        {
            _textures.Add(texture);
        }

        /// <summary>
        /// Lấy ra texture có chỉ số textureIdx nằm trong danh sách Textures. Trả về
        /// null nếu chỉ số không hợp lệ
        /// </summary>
        public Texture2D getTexture(int textureIdx)
        {
            if ((0 <= textureIdx) && (textureIdx < _textures.Count))
                return _textures[textureIdx];
            else
                return null;
        }
		
		///<summary>
		/// Cho phép các texture chuyển sau mỗi delay chu kì update
		///</summary>
		public void Animate(int delay)
		{
			if (_isAnimated == false)
				_isAnimated = true;
				
			_delay = delay;
		}

        public override void Update(GameTime gameTime)
        {
			if (_enabled == false)
				return;
				
            if (_isAnimated)
            {
                delayCount++;
                if (delayCount >= _delay)
                    delayCount = 0;

                _iTexture++;
                if (_iTexture >= _textures.Count)
                    _iTexture = 0;
            }
        }
    }
}
