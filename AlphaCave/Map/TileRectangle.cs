using AlphaCave.Core;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Map
{
    public struct TileRectangle
    {
        public static readonly TileRectangle StoneFloor = new TileRectangle(9, 2, "TileSheetDungeon");
        public static readonly TileRectangle DirtFloor = new TileRectangle(9, 7, "TileSheetDungeon");
        public static readonly TileRectangle WallUpLower = new TileRectangle(13, 4, "TileSheetDungeon");
        public static readonly TileRectangle WallUpUpper = new TileRectangle(13, 2, "TileSheetDungeon");
        public static readonly TileRectangle WallTop = new TileRectangle(10, 0, "TileSheetDungeon");
        public static readonly TileRectangle WallRight = new TileRectangle(9, 7, "TileSheetDungeon");



        public Point TileIndex;
        public string SpriteSheetName;

        public const short TileSize = 16;
        public const short TileSpace = 1;

        public Rectangle Rectangle;

        public Texture2D Texture => null;//AssetManager.Instance.Spritesheets[SpriteSheetName];

        private TileRectangle(short x, short y, string spriteSheetName)
        {
            TileIndex = new Point(x, y);
            SpriteSheetName = spriteSheetName;
            Rectangle = new Rectangle(TileIndex.X * (TileSize + TileSpace),
                                        TileIndex.Y * (TileSize + TileSpace),
                                        TileSize, TileSize);
        }
    }
}
