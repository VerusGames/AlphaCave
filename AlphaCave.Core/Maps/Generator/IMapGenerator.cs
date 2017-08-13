using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps.Generator
{
    public interface IMapGenerator
    {
        Chunk GenerateChunk(int id, Index2 index, Floor floor);
    }
}
