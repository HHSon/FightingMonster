using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TSLibrary.Input
{
    /// <summary>
    /// Lớp chịu trách nhiệm cập nhập trạng thái của chuột và bàn phím
    /// </summary>
    public class TSInputHandler
    {
        /// <summary>
        /// Trạng thái của chuột trong chu kì update trước
        /// </summary>
        public static MouseState LastMouseState;

        /// <summary>
        /// Trạng thái của chuột trong chu kì update hiện tại
        /// </summary>
        public static MouseState MouseState;

        /// <summary>
        /// Trạng thái của bàn phím trong chu kì update trước
        /// </summary>
        public static KeyboardState LastKeyboardState;

        /// <summary>
        /// Trang thái của bàn phím trong chu kì update hiện tại
        /// </summary>
        public static KeyboardState KeyboardState;

        /// <summary>
        /// Hình ảnh con trỏ chuột hiện tại
        /// </summary>
        public static Texture2D CursorImage;


        /// <summary>
        /// Khởi tạo các trạng thái của chuột và bàn phím
        /// </summary>
        public static void Initialize()
        {
            MouseState = Mouse.GetState();
            KeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Cập nhập trạng thái của chuột và bàn phím
        /// </summary>
        public static void Update(GameTime gameTime)
        {
            LastMouseState = MouseState;
            MouseState = Mouse.GetState();

            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Vẽ hình ảnh của con trỏ chuột lên màn hình
        /// </summary>
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (CursorImage != null)
                spriteBatch.Draw(CursorImage, new Vector2(MouseState.X, MouseState.Y), Color.White);
        }
    }
}
