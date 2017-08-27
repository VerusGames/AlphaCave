using AlphaCave.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPoint = engenious.Point;

namespace AlphaCave.Editor.Objects
{
    public class TextureMap
    {
        public List<TextureLayer> Layers { get; set; } = new List<TextureLayer>();

        public EPoint Size { get; private set; }

        public TextureMap(EPoint size, bool addStartingLayer = true)
        {
            Size = size;

            if (addStartingLayer)
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

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Size.X);
            bw.Write(Size.Y);
            bw.Write(Layers.Count);
            for (int i = 0; i < Layers.Count; i++)
                Layers[i].Serialize(bw);
        }

        public static TextureMap Deserialize(BinaryReader br)
        {
            var sizeX = br.ReadInt16();
            var sizeY = br.ReadInt16();
            var layerCount = br.ReadInt32();
            var texMap = new TextureMap(new EPoint(sizeX, sizeY), false);
            for (int i = 0; i < layerCount; i++)
                texMap.Layers.Add(TextureLayer.Deserialize(br));
            return texMap;
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

        public TextureLayer(EPoint size, string name)
        {
            Sprites = new SpriteObject[size.X, size.Y];
            for (int x = 0; x < Sprites.GetLength(0); x++)
            {
                for (int y = 0; y < Sprites.GetLength(1); y++)
                    Sprites[x, y] = new SpriteObject(new EPoint(0, 0), "");
            }
            Name = name;
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Name);
            bw.Write(Sprites.GetLength(0));
            bw.Write(Sprites.GetLength(1));
            for(int x = 0; x < Sprites.GetLength(0); x++)
            {
                for (int y = 0; y < Sprites.GetLength(1); y++)
                    Sprites[x, y].Serialize(bw);
            }
        }

        public static TextureLayer Deserialize(BinaryReader br)
        {
            var name = br.ReadString();
            var sizeX = br.ReadInt32();
            var sizeY = br.ReadInt32();

            var tl = new TextureLayer(new EPoint(sizeX, sizeY), name);

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                    tl.Sprites[x, y] = SpriteObject.Deserialize(br);
            }

            return tl;
        }

    }

    public class SpriteObject
    {
        public EPoint SpriteID { get; set; }
        public string SpriteSheet { get; set; }

        public SpriteObject(EPoint spriteId, string sheet)
        {
            SpriteID = spriteId;
            SpriteSheet = sheet;
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(SpriteID.X);
            bw.Write(SpriteID.Y);

            if (SpriteSheet == null)
                SpriteSheet = "";

            bw.Write(SpriteSheet);
        }

        public static SpriteObject Deserialize(BinaryReader br)
        {
            var x = br.ReadInt16();
            var y = br.ReadInt16();
            var spritesheet = br.ReadString();

            if (spritesheet == "")
                spritesheet = null;

            return new SpriteObject(new EPoint(x, y), spritesheet);
        }
    }
}
