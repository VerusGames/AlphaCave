using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Core.Maps.Generator
{
    public interface IMapGenerator
    {
        Chunk GenerateChunk(int id, Point index, Floor floor);
    }
}
