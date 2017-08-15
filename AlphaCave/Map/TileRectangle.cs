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
        public readonly static TileRectangle StoneFloor = new TileRectangle(9, 2, "TileSheetDungeon");
        public readonly static TileRectangle DirtFloor = new TileRectangle(9, 7, "TileSheetDungeon");
        public readonly static TileRectangle WallUpLower = new TileRectangle(13, 4, "TileSheetDungeon");
        public readonly static TileRectangle WallUpUpper = new TileRectangle(13, 2, "TileSheetDungeon");
        public readonly static TileRectangle WallTop = new TileRectangle(10, 0, "TileSheetDungeon");
        public readonly static TileRectangle WallRight = new TileRectangle(9, 7, "TileSheetDungeon");



        public Index2 TileIndex;
        public string SpriteSheetName;

        public const short TileSize = 16;
        public const short TileSpace = 1;

        public Rectangle Rectangle;

        public Texture2D Texture => null;//AssetManager.Instance.Spritesheets[SpriteSheetName];

        private TileRectangle(short x, short y, string spriteSheetName)
        {
            TileIndex = new Index2(x, y);
            SpriteSheetName = spriteSheetName;
            Rectangle = new Rectangle(TileIndex.X * (TileSize + TileSpace),
                                        TileIndex.Y * (TileSize + TileSpace),
                                        TileSize, TileSize);
        }
    }
}
