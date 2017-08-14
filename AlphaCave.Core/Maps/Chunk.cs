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
        private readonly TileFlags[] Flags;

        public const byte CHUNKSIZE_X = 64;
        public const byte CHUNKSIZE_Y = 64;

        public readonly Index2 Index;

        public Chunk(Index2 index)
        {
            Index = index;
            Tiles = new Tile[CHUNKSIZE_X*CHUNKSIZE_Y];
            Flags = new TileFlags[CHUNKSIZE_X * CHUNKSIZE_Y];
        }

        public void SetTile(Index2 tileIndex, Tile tile)
        {
            Tiles[tileIndex.GetFlatIndex(CHUNKSIZE_Y)] = tile;
        }

        public Tile GetTile(Index2 tileIndex)
        {
            return Tiles[tileIndex.GetFlatIndex(CHUNKSIZE_Y)];
        }

        public void SetFlag(Index2 tileIndex, TileFlags flag)
        {
           Flags[tileIndex.GetFlatIndex(CHUNKSIZE_Y)] |= flag;
        }

        public void ResetFlag(Index2 tileIndex, TileFlags flag)
        {
            Flags[tileIndex.GetFlatIndex(CHUNKSIZE_Y)] &= ~flag;
        }

        public TileFlags GetFlags(Index2 tileIndex)
        {
            return Flags[tileIndex.GetFlatIndex(CHUNKSIZE_Y)];
        }
    }
}
