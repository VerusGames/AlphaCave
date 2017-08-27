using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Core.Maps
{
    public class Chunk
    {

        private readonly Tile[] Tiles;
        private readonly TileFlags[] Flags;

        public const byte CHUNKSIZE_X = 64;
        public const byte CHUNKSIZE_Y = 64;

        public readonly Point Index;

        public Chunk(Point index)
        {
            Index = index;
            Tiles = new Tile[CHUNKSIZE_X*CHUNKSIZE_Y];
            Flags = new TileFlags[CHUNKSIZE_X * CHUNKSIZE_Y];
        }

        public void SetTile(Point tileIndex, Tile tile)
        {
            Tiles[tileIndex.GetFlatIndex(CHUNKSIZE_Y)] = tile;
        }

        public Tile GetTile(Point tileIndex)
        {
            return Tiles[tileIndex.GetFlatIndex(CHUNKSIZE_Y)];
        }
        public Tile GetTile(int x,int y)
        {
            return GetTile(new Point(x, y));
        }

        public void SetFlag(Point tileIndex, TileFlags flag)
        {
           Flags[tileIndex.GetFlatIndex(CHUNKSIZE_Y)] |= flag;
        }

        public void ResetFlag(Point tileIndex, TileFlags flag)
        {
            Flags[tileIndex.GetFlatIndex(CHUNKSIZE_Y)] &= ~flag;
        }

        public TileFlags GetFlags(Point tileIndex)
        {
            return Flags[tileIndex.GetFlatIndex(CHUNKSIZE_Y)];
        }

        public void SetVisible(Point tileIndex)
        {
            SetFlag(tileIndex, TileFlags.Visible);
            ResetFlag(tileIndex, TileFlags.PreVisible);
            for(int x = tileIndex.X-1; x <= tileIndex.X+1; x++)
            {
                for(int y = tileIndex.Y-1; y <= tileIndex.Y+1; y++)
                {
                    if (x < 0 || x > CHUNKSIZE_X || y < 0 || y > CHUNKSIZE_Y || GetFlags(new Point(x,y)).HasFlag(TileFlags.Visible))
                        continue;
                    SetFlag(new Point(x, y), TileFlags.PreVisible);
                }
            }
        }

        public void SetInVisible(Point tileIndex)
        {
            ResetFlag(tileIndex, TileFlags.Visible);
            ResetFlag(tileIndex, TileFlags.PreVisible);
            for (int x = tileIndex.X - 1; x <= tileIndex.X + 1; x++)
            {
                for (int y = tileIndex.Y - 1; y <= tileIndex.Y + 1; y++)
                {
                    if (x < 0 || x > CHUNKSIZE_X || y < 0 || y > CHUNKSIZE_Y || !GetFlags(new Point(x, y)).HasFlag(TileFlags.Visible))
                        continue;

                    SetFlag(new Point(x, y), TileFlags.PreVisible);
                }
            }
        }

        public bool IsPreVisible(int x, int y)
        {
            return IsPreVisible(new Point(x, y));
        }

        public bool IsPreVisible(Point index)
        {
            return GetFlags(index).HasFlag(TileFlags.PreVisible);
        }

        public bool IsVisible(int x, int y)
        {
            return IsVisible(new Point(x, y));
        }

        public bool IsVisible(Point index)
        {
            return GetFlags(index).HasFlag(TileFlags.Visible);
        }
    }
}
