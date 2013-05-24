using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TSLibrary.Input
{
    public class TSInputHandler
    {
        public static MouseState LastMouseState;
        public static MouseState MouseState;

        public static KeyboardState LastKeyboardState;
        public static KeyboardState KeyboardState;

        public static Texture2D CursorImage;


        public static void Initialize()
        {
            MouseState = Mouse.GetState();
            KeyboardState = Keyboard.GetState();
        }

        public static void Update(GameTime gameTime)
        {
            LastMouseState = MouseState;
            MouseState = Mouse.GetState();

            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (CursorImage != null)
                spriteBatch.Draw(CursorImage, new Vector2(MouseState.X, MouseState.Y), Color.White);
        }
    }
}
