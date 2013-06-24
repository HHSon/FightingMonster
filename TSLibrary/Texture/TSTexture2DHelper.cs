using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TSLibrary.Texture
{
    public class TSTexture2DHelper
    {
        public static List<Texture2D> CropImageToTiles(Texture2D srcImage, int tileWidth, int tileHeight)
        {
            if ((srcImage == null) || (tileWidth < 0) || (tileHeight < 0))
                return null;

            List<Texture2D> list = new List<Texture2D>();
            Rectangle tempRect;

            var graphics = srcImage.GraphicsDevice;

            int numRows = srcImage.Height / tileHeight;
            int dy = srcImage.Height % tileHeight;
            if (dy > 0)
                numRows = numRows + 1;

            int numCols = srcImage.Width / tileWidth;
            int dx = srcImage.Width % tileWidth;
            if (dx > 0)
                numCols = numCols + 1;


            for (int row = 0; row < numRows; row++)
                for (int col = 0; col < numCols; col++)
                {
                    tempRect = new Rectangle(col * tileWidth, row * tileHeight, tileWidth, tileHeight);
                    list.Add(cropTexture2D(graphics, srcImage, tempRect));
                }

            graphics.SetRenderTarget(null);

            return list;
        }

        public static Texture2D cropTexture2D(GraphicsDevice graphics, Texture2D image, Rectangle source)
        {
            // Tham khảo từ: http://stackoverflow.com/questions/8331494/crop-texture2d-spritesheet

            if (image == null || graphics == null)
                return null;

            
            var ret = new RenderTarget2D(graphics, source.Width, source.Height);
            var spriteBatch = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret);
            graphics.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin();
            spriteBatch.Draw(image, Vector2.Zero, source, Color.White);
            spriteBatch.End();

            
            return (Texture2D)ret;
        }
    }
}
