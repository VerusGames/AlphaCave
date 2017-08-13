using AlphaCave.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Editor.Objects
{
    public class TextureMap
    {
        public List<TextureLayer> Layers { get; set; } = new List<TextureLayer>();

        public Index2 Size { get; private set; }

        public TextureMap(Index2 size)
        {
            Size = size;
            AddLayer();
        }

        public TextureLayer AddLayer(string name = null)
        {
            if (name == null)
                name = (Layers.Count + 1).ToString();

            var layer = new TextureLayer(Size, name);
            Layers.Add(layer);
            return layer;
        }

        public Bitmap CreateBitmap(Dictionary<string, Bitmap> spriteSheets)
        {
            Bitmap map = new Bitmap(Size.X * 16, Size.Y * 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(map))
            {
                for (int x = 0; x < Size.X; x++)
                {
                    for (int y = 0; y < Size.Y; y++)
                    {
                        for (int l = 0; l < Layers.Count; l++)
                        {
                            var sObject = Layers[l].Sprites[x, y];

                            if (sObject == null || sObject.SpriteSheet == null)
                                continue;

                            var sheet = spriteSheets[sObject.SpriteSheet];

                            graphics.DrawImage(sheet, new Rectangle(x * 16 , y * 16 , 16 , 16 ), new Rectangle(sObject.SpriteID.X * 17, sObject.SpriteID.Y * 17, 16, 16), GraphicsUnit.Pixel);
                        }
                    }
                }
            }

            return map;
        }

    }

    public class TextureLayer
    {
        public SpriteObject[,] Sprites { get; set; }

        public string Name { get; set; }

        public TextureLayer(Index2 size, string name)
        {
            Sprites = new SpriteObject[size.X, size.Y];
            Name = name;
        }


    }

    public class SpriteObject
    {
        public Index2 SpriteID { get; set; }
        public string SpriteSheet { get; set; }

        public SpriteObject(Index2 spriteId, string sheet)
        {
            SpriteID = spriteId;
            SpriteSheet = sheet;
        }
    }
}
