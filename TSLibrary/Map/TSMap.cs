using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TSLibrary.Map
{
    /// <summary>
    /// Lớp bản đồ được sử dụng để tạo bản đồ cho các màn chơi. Bản đồ
    /// được tạo thành từ một mảng hai chiều các ô nhỏ.
    /// </summary>
    public class TSMap : TSVisibleGameEntity
    {
        protected TSTile[,] _tiles;
        private int _numRows;
        protected int _numColumns;
        protected int _currentTileRow;
        protected int _currentTileCol;

        protected Texture2D _tileBackground;
        protected int _tileWidth;
        protected int _tileHeight;

        #region Property Region

        /// <summary>
        /// Mảng 2 chiều các ô tạo thành bản đồ
        /// </summary>
        public TSTile[,] Tiles
        {
            get { return _tiles; }
            protected set { _tiles = value; }
        }

        /// <summary>
        /// Số dòng của mảng 2 chiều các ô (Tiles) tạo nên bản đồ
        /// </summary>
        protected int NumRows
        {
            get { return _numRows; }
            set { _numRows = value; }
        }

        /// <summary>
        /// Số cột của mảng 2 chiều các ô (Tiles) tạo nên bản đồ
        /// </summary>
        protected int NumColumns
        {
            get { return _numColumns; }
            set { _numColumns = value; }
        }

        /// <summary>
        /// Chỉ số dòng của ô hiện tại mà từ đó bản đồ vẽ lên màn hình
        /// </summary>
        public int CurrentTileRow
        {
            get { return _currentTileRow; }
            set { _currentTileRow = value; }
        }

        /// <summary>
        /// Chỉ số cột của ô hiện tại mà từ đó bản đồ vẽ lên màn hình
        /// </summary>
        public int CurrentTileColumn
        {
            get { return _currentTileCol; }
            set { _currentTileCol = value; }
        }

        /// <summary>
        /// Chiều rộng của các ô (tile)
        /// </summary>
        public int TileWidth
        {
            get { return _tileWidth; }
            set { _tileWidth = value; }
        }

        /// <summary>
        /// Chiều cao của các ô (tile)
        /// </summary>
        public int TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; }
        }

        /// <summary>
        /// Hình nền của các ô
        /// </summary>
        public Texture2D TileBackground
        {
            get { return _tileBackground; }
            set { _tileBackground = value; }
        }

        #endregion


        /// <summary>
        /// Khởi tạo bản đồ
        /// </summary>
        /// <param name="numRows">Số dòng của mảng chứa các ô nhỏ</param>
        /// <param name="numCols">Số cột của mảng chứa các ô nhỏ</param>
        /// <param name="screenWidth">Chiều rộng màn hình</param>
        /// <param name="screenHeight">Chiều cao màn hình</param>
        public TSMap(int numRows, int numCols, int screenWidth, int screenHeight)
        {
            if (numRows < 0 || numCols < 0)
                throw new ArgumentOutOfRangeException("Số dòng và cột phải lớn hơn 0");

            _numRows = numRows;
            _numColumns = numCols;
            _width = screenWidth;
            _height = screenHeight;

            
            _tileWidth = 50;
            _tileHeight = 50;

            this.CurrentTileRow = 0;
            this.CurrentTileColumn = 0;

            _tiles = new TSTile[numRows, numCols];

            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numCols; j++)
                {
                    Tiles[i, j] = new TSTile();
                    Tiles[i, j].Width = this.TileWidth;
                    Tiles[i, j].Height = this.TileHeight;
                }
        }

        public void LoadContent(ContentManager content)
        {
            if (this.TileBackground != null)
                for (int i = 0; i < NumRows; i++)
                    for (int j = 0; j < NumColumns; j++)
                    {
                        Tiles[i, j].Background = this.TileBackground;
                    }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int numDrawCol = (this.Width / this.TileWidth) + 1;
            int numDrawRow = (this.Height / this.TileHeight) + 1;

            for (int row = CurrentTileRow; row < (CurrentTileRow + numDrawRow); row++)
                for (int col = CurrentTileColumn; col < (CurrentTileColumn + numDrawCol); col++)
                    Tiles[row, col].Draw(
                        gameTime,
                        spriteBatch,
                        (col - CurrentTileColumn) * TileWidth, 
                        (row - CurrentTileRow) * TileHeight);
        }
    }
}
