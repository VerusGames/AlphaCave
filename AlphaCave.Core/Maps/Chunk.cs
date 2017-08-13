using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps
{
    public class Chunk
    {

        private readonly Tile[] Tiles;

        public const byte CHUNKSIZE_X = 64;
        public const byte CHUNKSIZE_Y = 64;

        public readonly Index2 Index;

        public Chunk(Index2 index)
        {
            Index = index;
            Tiles = new Tile[CHUNKSIZE_X*CHUNKSIZE_Y];
        }

        public void SetTile(Index2 tileIndex, Tile tile)
        {
            var localIndex = tileIndex.X * tileIndex.Y + tileIndex.X;

            Tiles[localIndex] = tile;
        }

        public Tile GetTile(Index2 tileIndex)
        {
            var localIndex = tileIndex.X * tileIndex.Y + tileIndex.X;

            return Tiles[localIndex];
        }
    }
}
