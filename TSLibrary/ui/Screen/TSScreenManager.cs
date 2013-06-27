using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TSLibrary.ui.Control.Screen;
using TSLibrary.ui.Control.Layout;
using Microsoft.Xna.Framework.Graphics;

namespace TSLibrary.ui.Screen.ScreenManager
{
    public class TSScreenManager : TSInvisibleGameObject
    {
        private List<TSScreen> _screens;
        private Game game;
        

        #region Property Region

        /// <summary>
        /// Lấy danh sách các màn hình hiện tại
        /// </summary>
        public List<TSScreen> Screens
        {
            get { return _screens; }
            set { _screens = value; }
        }

        #endregion


        /// <summary>
        /// Khởi tạo lớp quản lý màn hình. Số lượng màn hình đang quản lý là 0
        /// </summary>
        public TSScreenManager(Game game)
        {
            this.game = game;
            _screens = new List<TSScreen>();
        }

        /// <summary>
        /// Cập nhập màn hình đang hiển thị, những màn hình bên dưới không được cập nhập
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled == false)
                return;

            if (_screens.Count > 0)
                _screens[_screens.Count - 1].Update(gameTime);

            /*for (int idx = 0; (idx < _screens.Count); idx++)
                _screens[idx].Update(gameTime);*/
        }

        /// <summary>
        /// Vẽ màn hình mới nhất (Những màn hình trước đó sẽ không được vẽ lên)
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_screens.Count > 0)
                _screens[_screens.Count - 1].Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Thêm vào một màn hình mới - màn hình này sẽ là màn hình mới nhất và
        /// được hiển thị, tất cả các màn hình trước đó đã thêm vào
        /// bị ẩn đi (đặt biến Visibled bằng false).
        /// </summary>
        /// <param name="screenToAdd">Màn hình cần thêm vào</param>
        public void Add(TSScreen screenToAdd)
        {
            if (screenToAdd != null)
            {
                if (_screens.Count > 0)
                    _screens[_screens.Count - 1].Visibled = false;

                screenToAdd.ScreenManager = this;
                screenToAdd.Visibled = true;

                _screens.Add(screenToAdd);       
            }
        }

        /// <summary>
        /// Đóng một màn hình. Nếu màn hình đang được hiển thị bị đóng
        /// màn hình trước đó sẽ được vẽ lên màn hình.
        /// </summary>
        /// <param name="screenToClose"></param>
        public void Close(TSScreen screenToClose)
        {
            int screenIndex;

            if (screenToClose == null)
                return;

            screenIndex = _screens.IndexOf(screenToClose);

            if ((screenIndex < 0) || (screenIndex >= _screens.Count))
                return;

            if (screenIndex == (_screens.Count - 1))
                if ((_screens.Count - 2) >= 0)
                    _screens[_screens.Count - 2].Visibled = true;


            _screens.RemoveAt(screenIndex);
            screenToClose.Visibled = true;
        }

        public void ExitGame()
        {
            if (game != null)
                game.Exit();
        }
    }
}