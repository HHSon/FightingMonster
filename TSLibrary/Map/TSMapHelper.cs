using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using TSLibrary.Texture;


namespace TSLibrary.Map
{
    public class TSMapHelper
    {
        public int NumRows { get; private set; }
        public int NumColumns { get; private set; }
        public int[,] TileMatrix { get; private set; }
        public TSTileType[,] AccessibleMatrix { get; private set; }

        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        

        public List<Texture2D> TileList { get; private set; }
        public ContentManager Content { get; private set; }




        public TSMapHelper()
        {
            TileList = new List<Texture2D>();
            TileList.Add(null);                  // tile đầu tiên bỏ trống
        }


        public void LoadMapInfo(String mapDefFile, ContentManager content)
        {
            this.Content = content;
            XmlDocument document = new XmlDocument();
            document.Load(mapDefFile);


            XmlNodeList mapNodeList = document.GetElementsByTagName("map");

            foreach (XmlNode mapNode in mapNodeList)
            {
                ReadMap(mapNode);
                TileMatrix = new int[NumRows, NumColumns]; // đã có numRows và numColumns
                AccessibleMatrix = new TSTileType[NumRows, NumColumns];

                foreach (XmlNode mapChildNode in mapNode)
                {
                    if (mapChildNode.Name == "tileset")
                    {
                        ReadTileSet(mapChildNode);
                    }
                    else if (mapChildNode.Name == "layer")
                    {
                        ReadLayerNode(mapChildNode);
                    }
                }


                // chỉ nạp 1 đối tượng map duy nhất trong file xml
                break; 
            }
        }

        private void ReadMap(XmlNode mapNode)
        {
            if (mapNode.Name != "map")
                return;

            NumColumns = Int32.Parse(mapNode.Attributes["width"].Value);
            NumRows = Int32.Parse(mapNode.Attributes["height"].Value);
            
            TileWidth = Int32.Parse(mapNode.Attributes["tilewidth"].Value);
            TileHeight = Int32.Parse(mapNode.Attributes["tileheight"].Value);
        }

        private void ReadTileSet(XmlNode tileSetNode)
        {
            if ((tileSetNode == null) ||
                    (tileSetNode.Name != "tileset"))
                return;

            int customTileWidth = Int32.Parse(tileSetNode.Attributes["tilewidth"].Value);
            int customTileHeight = Int32.Parse(tileSetNode.Attributes["tileheight"].Value);

            XmlNode imageNode = tileSetNode.FirstChild;

            if (imageNode == null)
                return;

            String imageSource = "Map/" + imageNode.Attributes["source"].Value;

            // Tìm chỉ số của dấu chấm chỉ kiểu tập tin hình ảnh
            int idx = 0;
            for (idx = imageSource.Length - 1; idx >= 0; idx--)
                if (imageSource[idx] == '.')
                    break;

            imageSource = imageSource.Remove(idx);
            

            List<Texture2D> list = TSTexture2DHelper.CropImageToTiles(
                                        LoadTexture2D(imageSource), 
                                        customTileWidth, 
                                        customTileHeight);

            for (idx = 0; idx < list.Count; idx++)
                this.TileList.Add(list[idx]);
        }

        private Texture2D LoadTexture2D(String assert)
        {
            if (Content != null)
                return Content.Load<Texture2D>(assert);
            else
                return null;
        }

        private void ReadLayerNode(XmlNode layerNode)
        {
            if ((layerNode == null) || (layerNode.Name != "layer"))
                return;

            XmlNode dataNode = layerNode.FirstChild;
            int count = 0;

            if (layerNode.Attributes["name"].Value == "background")
            {
                foreach (XmlNode child in dataNode)
                {
                    TileMatrix[count / NumRows, count % NumRows] =
                        Int32.Parse(child.Attributes["gid"].Value);
                    count++;
                }
            }
            else if (layerNode.Attributes["name"].Value == "accessible")
            {
                foreach (XmlNode child in dataNode)
                {
                    if (child.Attributes["gid"].Value.ToString() != "0")  // có gì đó là không thể đi vào được
                        AccessibleMatrix[count / NumRows, count % NumRows] = TSTileType.Unaccessible;
                    count++;
                }
            }
        }
    }
}