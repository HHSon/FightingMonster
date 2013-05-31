using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TSLibrary.ui.Control.Layout;
using TSLibrary.ui.Control.Button;
using TSLibrary.ui.Control.Label;
using TSLibrary.ui.Control;

namespace FightingMonster.ResourceHelper
{
    /// <summary>
    /// Lớp đối tượng khởi tạo resource (hình ảnh, âm thanh) cho các control (có thể
    /// sử dụng lớp này để tạo resource cho control hoặc không)
    /// </summary>
    public class ControlHelper
    {
        private static ControlHelper _instance;

        // Button
        public Texture2D ButtonBackgroundImage { get; private set; }
        public Texture2D ButtonActionPerformedImage { get; private set; }
        public Texture2D ButtonFocusedImage { get; private set; }
        public SpriteFont ButtonFont { get; private set; }
        public SpriteFont ButtonClickedFont { get; private set; }
        public SpriteFont ButtonFocusedFont { get; private set; }


        // Label
        public SpriteFont LabelFont { get; private set; }
        

        // Progress Bar
        public Texture2D ProgressBarBackgroudImage { get; private set; }
        public Texture2D ProgressBarImage { get; private set; }
        public SpriteFont ProgressBarFont { get; private set; }


        // Layout
        public Texture2D LayoutBackgroundImage { get; private set; }




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
            ButtonBackgroundImage = content.Load<Texture2D>(@"Image/Control/Button/Background");
            ButtonActionPerformedImage = content.Load<Texture2D>(@"Image/Control/Button/ActionPerformed");
            ButtonFocusedImage = content.Load<Texture2D>(@"Image/Control/Button/Focus");

            ButtonFont = content.Load<SpriteFont>(@"Font/Control/StandardFont");
            ButtonClickedFont = content.Load<SpriteFont>(@"Font/Control/Button/ClickedFont");
            ButtonFocusedFont = content.Load<SpriteFont>(@"Font/Control/Button/FocusedFont");


            // Label
            LabelFont = content.Load<SpriteFont>(@"Font/Control/StandardFont");


            // ProgressBar
            ProgressBarBackgroudImage = content.Load<Texture2D>(@"Image/Control/ProgressBar/Background");
            ProgressBarImage = content.Load<Texture2D>(@"Image/Control/ProgressBar/Progress");
            ProgressBarFont = content.Load<SpriteFont>(@"Font/Control/StandardFont");

            // layout
            LayoutBackgroundImage = content.Load<Texture2D>(@"Image/Control/Layout/Background");
        }


        // Button
        public void ToStandardButton(TSButton button)
        {
            if (button != null)
            {
                button.BackgroundImage = ButtonBackgroundImage;
                button.ActionPerformedImage = ButtonActionPerformedImage;
                button.FocusedImage = ButtonFocusedImage;

                button.Font = ButtonFont;
            }
        }

        public void ToStandardTextButton(TSButton button)
        {
            if (button != null)
            {
                button.BackgroundImage = null;
                button.ActionPerformedImage = null;
                button.FocusedImage = null;

                button.Font = ButtonFont;
                button.FocusedFont = ButtonFocusedFont;
                button.ClickedFont = ButtonClickedFont;
            }
        }

        // Label
        public void ToStandardLabel(TSLabel label)
        {
            if (label != null)
            {
                label.Font = LabelFont;
            }
        }

        // ProgressBar
        public void ToStandardProgressBar(TSProgressBar progressBar)
        {
            if (progressBar != null)
            {
                progressBar.BackgroundImage = ProgressBarBackgroudImage;
                progressBar.ProgressImage = ProgressBarImage;
                progressBar.Font = ProgressBarFont;
            }
        }


        // Layout
        public void ToStandardLayout(TSLayout layout)
        {
            if (layout != null)
            {
                layout.BackgroundImage = LayoutBackgroundImage;
            }
        }
    }
}