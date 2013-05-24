using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.Control;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FightingMonster.ResourceHelper
{
    public class ControlHelper
    {
        private static ControlHelper _instance;

        // Button
        private Texture2D buttonBackgroundImage;
        private Texture2D buttonActionPerformedImage;
        private Texture2D buttonFocusedImage;
        private SpriteFont buttonFont;
        private SpriteFont buttonClickedFont;
        private SpriteFont buttonFocusedFont;

        public SpriteFont ButtonFont
        {
            get { return buttonFont; }
        }

        private ControlHelper()
        {
            // Singleton
        }

        public static ControlHelper GetInstance()
        {
            if (_instance == null)
                _instance = new ControlHelper();

            return _instance;
        }

        public void LoadContent(ContentManager content)
        {
            if (content == null)
                return;

            // button
            buttonBackgroundImage = content.Load<Texture2D>(@"Image/Control/Button/Background");
            buttonActionPerformedImage = content.Load<Texture2D>(@"Image/Control/Button/ActionPerformed");
            buttonFocusedImage = content.Load<Texture2D>(@"Image/Control/Button/Focus");

            buttonFont = content.Load<SpriteFont>(@"Font/Control/Button/NormalFont");
        }

        public void ToNormalButton(TSButton button)
        {
            if (button == null)
                return;

            button.BackgroundImage = buttonBackgroundImage;
            button.ActionPerformedImage = buttonActionPerformedImage;
            button.FocusedImage = buttonFocusedImage;

            button.Font = buttonFont;
        }

        public void ToTextButton(TSButton button)
        {
            if (button == null)
                return;

            button.BackgroundImage = null;
            button.ActionPerformedImage = null;
            button.FocusedImage = null;

            button.Font = buttonFont;
            button.FocusedFont = buttonFocusedFont;
            button.ClickedFont = buttonClickedFont;
        }
    }
}