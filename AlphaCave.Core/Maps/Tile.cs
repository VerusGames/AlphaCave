using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Core.Maps
{
    public class Tile
    {
        private readonly Point Index;

        public TileBaseType BaseType { get; set; }

        public Tile(Point index, TileBaseType baseType)
        {
            Index = index;
            BaseType = baseType;
        }
    }
}
