using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TSLibrary.ui.Control
{
    /// <summary>
    /// Lớp đối tượng để hiện thị một hình ảnh lên màn hình
    /// </summary>
    public class TSPictureBox : TSControl
    {
        protected Texture2D _image;


        #region Property Region

        public Texture2D Image
        {
            get { return _image; }
            set { _image = value; }
        }

        #endregion


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Image != null)
            {
                spriteBatch.Draw(Image, new Rectangle((int)PositionOnScreenX, (int)PositionOnScreenY, Width, Height), Color.White);
            }
        }
    }
}
