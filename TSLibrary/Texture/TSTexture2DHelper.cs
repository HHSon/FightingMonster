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

            //var graphics = srcImage.GraphicsDevice;

            int numRows = srcImage.Height / tileHeight;
            int numCols = srcImage.Width / tileWidth;


            for (int row = 0; row < numRows; row++)
                for (int col = 0; col < numCols; col++)
                {
                    tempRect = new Rectangle(col * tileWidth, row * tileHeight, tileWidth, tileHeight);
                    //list.Add(cropTexture2D(graphics, srcImage, tempRect));
                     list.Add(Crop(srcImage, tempRect));
                }

            //graphics.SetRenderTarget(null);

            return list;
        }

        public static Texture2D Crop(Texture2D source, Rectangle area)
        {
            // Tham khảo tại: http://stuckinprogramming.blogspot.com/2011/01/crop-texture2d-in-xna.html

            if (source == null)
                return null;

            Texture2D cropped = new Texture2D(source.GraphicsDevice, area.Width, area.Height);
            Color[] data = new Color[source.Width * source.Height];
            Color[] cropData = new Color[cropped.Width * cropped.Height];

            source.GetData<Color>(data);

            int index = 0;

            for (int y = area.Y; y < (area.Y + area.Height); y++)
            {
                for (int x = area.X; x < (area.X + area.Width); x++)
                {
                    cropData[index] = data[x + (y * source.Width)];
                    index++;
                }
            }

            cropped.SetData<Color>(cropData);
            return cropped;
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
