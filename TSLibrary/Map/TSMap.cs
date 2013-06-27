using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace TSLibrary.Map
{
    /// <summary>
    /// Lớp bản đồ được sử dụng để tạo bản đồ cho các màn chơi. Bản đồ
    /// được tạo thành từ một mảng hai chiều các ô nhỏ.
    /// </summary>
    public class TSMap : TSVisibleGameObject
    {
        protected Vector2 _P0;
        protected int _numRows;
        protected int _numColumns;

        protected TSTile[,] _tiles;
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

        public Vector2 P0
        {
            get { return _P0; }
            protected set { _P0 = value; }
        }

        /// <summary>
        /// Số ô theo chiều dọc
        /// </summary>
        public int NumRows
        {
            get { return _numRows; }
            protected set { _numRows = value;}
        }

        /// <summary>
        /// Số ô theo chiều ngang
        /// </summary>
        public int NumColumns
        {
            get { return _numColumns; }
            protected set { _numColumns = value; }
        }


        /// <summary>
        /// Chiều rộng của các ô (tile)
        /// </summary>
        public int TileWidth
        {
            get { return _tileWidth; }
            protected set { _tileWidth = value; }
        }

        /// <summary>
        /// Chiều cao của các ô (tile)
        /// </summary>
        public int TileHeight
        {
            get { return _tileHeight; }
            protected set { _tileHeight = value; }
        }

        #endregion


        protected int numRowsOnScreen;
        protected int numColumnsOnScreen;
        protected Vector2 maxP0;


        public TSMap(int screenWidth, int screenHeight)
        {
            if (screenWidth < 0)
                throw new ArgumentException("Biến screenWidth = " + screenWidth);

            if (screenHeight < 0)
                throw new ArgumentException("Biến screenHeight = " + screenHeight);

            _width = screenWidth;
            _height = screenHeight;
        }

        /// <summary>
        /// Khởi tạo bản đồ
        /// </summary>
        /// <param name="numRows">Số dòng của bản đồ</param>
        /// <param name="numColumns">Số cột của bản đồ</param>
        public TSMap(int numRows, int numColumns, int tileWidth, int tileHeight,
                          int screenWidth, int screenHeight)
        {
            if ((numRows < 0) || (numColumns < 0) || 
                    (tileWidth < 0) || (tileHeight < 0) ||
                    (screenWidth < 0) || (screenHeight < 0))
                throw new ArgumentException("Tất cả các biến phải lớn hơn hoặc bằng 0");

            _numRows = numRows;
            _numColumns = numColumns;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _width = screenWidth;
            _height = screenHeight;

            numRowsOnScreen = (screenHeight / _tileHeight) + 2;   // số dòng vẽ trên màn hình
            numColumnsOnScreen = (screenWidth / _tileWidth) + 2;     // số cột vẽ trên man hình

            maxP0.X = (_numColumns * _tileWidth) - screenWidth;
            maxP0.Y = (_numRows * _tileHeight) - screenHeight;

            if (maxP0.X < 0)
                maxP0.X = 0;

            if (maxP0.Y < 0)
                maxP0.Y = 0;
        }


        public virtual void LoadContent(String mapDefFile, ContentManager content)
        {
            if (mapDefFile == null)
                return;

            if (content != null)
            {
                TSMapHelper mapHelper = new TSMapHelper();
                mapHelper.LoadMapInfo(mapDefFile, content);

                _numRows = mapHelper.NumRows;
                _numColumns = mapHelper.NumColumns;

                _tileWidth = mapHelper.TileWidth;
                _tileHeight = mapHelper.TileHeight;

                _tiles = new TSTile[_numRows, _numColumns];

                for (int row = 0; row < _numRows; row++)
                    for (int col = 0; col < _numColumns; col++)
                    {
                        _tiles[row, col] = new TSTile();
                        _tiles[row, col].Background = mapHelper.TileList[mapHelper.TileMatrix[row, col]];
                        _tiles[row, col].Type = mapHelper.AccessibleMatrix[row, col];
                    }

                InitAdditionalInfo();
            }
        }

        public TSTile GetTile(int row, int col)
        {
            if ((row < 0) || (row >= NumRows))
                return null;

            if ((col < 0) || (col >= NumColumns))
                return null;

            return Tiles[row, col];
        }

        /*public Vector2 GetTileIndex(Vector2 position)
        {
            Vector2 vector = new Vector2();
        }*/

        public bool IsInDrawnArea(Vector2 position)
        {
            if ((position.X < _P0.X) || (position.X > _P0.X + Width))
                return false;

            if ((position.Y < _P0.Y) || (position.Y > _P0.Y + Height))
                return false;

            return true;
        }

        public bool IsInDrawnArea(Vector2 position, int width, int height)
        {
            if ((position.X + width < _P0.X) || (position.X > _P0.X + Width))
                return false;

            if ((position.Y + height < _P0.Y) || (position.Y > _P0.Y + Height))
                return false;

            return true;
        }

        /*public Vector2[] GetPath(Vector2 position, Vector2 destPosition)
        {
            return null;
        }*/

        protected void InitAdditionalInfo()
        {
            numRowsOnScreen = (Height / _tileHeight) + 2;   // số dòng vẽ trên màn hình
            numColumnsOnScreen = (Width / _tileWidth) + 2;     // số cột vẽ trên man hình

            maxP0.X = (_numColumns * _tileWidth) - Width;
            maxP0.Y = (_numRows * _tileHeight) - Height;

            if (maxP0.X < 0)
                maxP0.X = 0;

            if (maxP0.Y < 0)
                maxP0.Y = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled == false)
                return;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == false)
                return;

            int drawColumn = (int)(_P0.X / _tileWidth);
            int drawRow = (int)(_P0.Y / _tileHeight);
            

            if ((drawColumn < 0) || (drawRow < 0))
                return;

            int dx = (int)(_P0.X) % _tileWidth;
            int dy = (int)(_P0.Y) % _tileHeight;

            for (int row = drawRow; row < drawRow + numRowsOnScreen && row < NumRows; row++)
                for (int col = drawColumn; col < drawColumn + numColumnsOnScreen && col < NumColumns; col++)
                    if (Tiles[row, col].Background != null)
                        spriteBatch.Draw(
                            Tiles[row, col].Background,
                            new Rectangle((col - drawColumn) * TileHeight - dx, (row - drawRow) * TileWidth - dy, TileWidth, TileHeight),
                            Color.White);
        }

        public bool ScrollTo(float positionX, float positionY)
        {
            if (positionX < 0)
                _P0.X = 0;
            else if (positionX > maxP0.X)
                _P0.X = maxP0.X;
            else
                _P0.X = positionX;


            if (positionY < 0)
                _P0.Y = 0;
            else if (positionY > maxP0.Y)
                _P0.Y = maxP0.Y;
            else
                _P0.Y = positionY;

            return true;
        }

        public bool ScrollTo(Vector2 point)
        {
            return ScrollTo(point.X, point.Y);
        }

        public bool Move(float dx, float dy)
        {
            return ScrollTo(P0.X + dx, P0.Y + dy);
        }

        public Vector2 GetPositionOnScreen(Vector2 positionOnMap)
        {
            return (positionOnMap - P0);
        }

        public bool IsInScreenArea(Vector2 positionOnMap)
        {
            if ((positionOnMap.X - P0.X < 0) ||
					(positionOnMap.X - P0.X > _width))
                return false;

            if ((positionOnMap.Y - P0.Y < 0) ||
                    (positionOnMap.Y - P0.Y > _height))
                return false;

            return true;
        }
    }
}

