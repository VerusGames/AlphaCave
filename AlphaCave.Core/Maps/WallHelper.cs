using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps
{
    public static class WallHelper
    {
        public static int GetWallType(Index2 index, Chunk chunk)
        {
            int bitMask = 0;

            //bitMask |= (chunk.IsPreVisible(index.X - 1, index.Y - 1) ? 1 << 0 : 0);
            if(index.Y > 0)
                bitMask |= (chunk.IsPreVisible(index.X + 0, index.Y - 1) ? 1 << 1 : 0);
            //bitMask |= (chunk.IsPreVisible(index.X + 1, index.Y - 1) ? 1 << 2 : 0);
            if(index.X > 0)
                bitMask |= (chunk.IsPreVisible(index.X - 1, index.Y + 0) ? 1 << 7 : 0);

            if(index.X < Chunk.CHUNKSIZE_X-1)
                bitMask |= (chunk.IsPreVisible(index.X + 1, index.Y + 0) ? 1 << 3 : 0);
            //bitMask |= (chunk.IsPreVisible(index.X - 1, index.Y + 1) ? 1 << 5 : 0);
            if(index.Y < Chunk.CHUNKSIZE_Y-1)
                bitMask |= (chunk.IsPreVisible(index.X + 0, index.Y + 1) ? 1 << 5 : 0);
            //bitMask |= (chunk.IsPreVisible(index.X + 1, index.Y + 1) ? 1 << 7 : 0);

            return bitMask;
        }

        // Key=Wandtyp, Value=TileIndex
        public static Dictionary<int, Index2> StoneWallLookupTable = new Dictionary<int, Index2>()
        {
            {0, new Index2(9,1) },
            {2, new Index2(8,1) },
            {8, new Index2(9,0) },
            {32, new Index2(8,0) },
            {128, new Index2(11,0) },
            {130, new Index2(13,1) },
            {10, new Index2(12,1) },
            {40, new Index2(12,0) },
            {160, new Index2(13,0) },
            {136, new Index2(10,0) },
            {34, new Index2(11,1) },
            {138, new Index2(15,0) },
            {42, new Index2(14,0)},
            {168, new Index2(15,1) },
            {162, new Index2(14,1) },
            {170, new Index2(10,1)},
        };
    }

    
}
