using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps.Generator
{
    public sealed class DebugGenerator : IMapGenerator
    {
        public Chunk GenerateChunk(int id, Index2 index, Floor floor)
        {
            Chunk chunk = new Chunk(index);

            for (short x = 0; x < Chunk.CHUNKSIZE_X; x++)
            {
                for (short y = 0; y < Chunk.CHUNKSIZE_Y; y++)
                {
                    var tileIndex = new Index2(x, y);

                    chunk.SetTile(tileIndex, new Tile(tileIndex,TileBaseType.Grass));

                    if (x < 20 && x > 10 && y < 20 && y > 10)
                        chunk.SetVisible(tileIndex);
                }
            }

            return chunk;
        }
    }
}
