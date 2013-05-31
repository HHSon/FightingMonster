using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.ui.Control.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace FightingMonster.Screen
{
    public class AboutScreen : TSScreen
    {
        public AboutScreen(GraphicsDeviceManager graphics)
            : base(graphics)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            if (content == null)
                return;

            BackgroundImage = content.Load<Texture2D>(@"Image/Screen/Background");

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
