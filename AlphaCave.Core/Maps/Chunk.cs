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
        public Tile GetTile(int x,int y)
        {
            return GetTile(new Index2(x, y));
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

        public void SetVisible(Index2 tileIndex)
        {
            SetFlag(tileIndex, TileFlags.Visible);
            ResetFlag(tileIndex, TileFlags.PreVisible);
            for(int x = tileIndex.X-1; x <= tileIndex.X+1; x++)
            {
                for(int y = tileIndex.Y-1; y <= tileIndex.Y+1; y++)
                {
                    if (x < 0 || x > CHUNKSIZE_X || y < 0 || y > CHUNKSIZE_Y || GetFlags(new Index2(x,y)).HasFlag(TileFlags.Visible))
                        continue;
                    SetFlag(new Index2(x, y), TileFlags.PreVisible);
                }
            }
        }

        public void SetInVisible(Index2 tileIndex)
        {
            ResetFlag(tileIndex, TileFlags.Visible);
            ResetFlag(tileIndex, TileFlags.PreVisible);
            for (int x = tileIndex.X - 1; x <= tileIndex.X + 1; x++)
            {
                for (int y = tileIndex.Y - 1; y <= tileIndex.Y + 1; y++)
                {
                    if (x < 0 || x > CHUNKSIZE_X || y < 0 || y > CHUNKSIZE_Y || !GetFlags(new Index2(x, y)).HasFlag(TileFlags.Visible))
                        continue;

                    SetFlag(new Index2(x, y), TileFlags.PreVisible);
                }
            }
        }

        public bool IsPreVisible(int x, int y)
        {
            return IsPreVisible(new Index2(x, y));
        }

        public bool IsPreVisible(Index2 index)
        {
            return GetFlags(index).HasFlag(TileFlags.PreVisible);
        }

        public bool IsVisible(int x, int y)
        {
            return IsVisible(new Index2(x, y));
        }

        public bool IsVisible(Index2 index)
        {
            return GetFlags(index).HasFlag(TileFlags.Visible);
        }
    }
}
