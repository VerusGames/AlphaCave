using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps
{
    public class Tile
    {
        private readonly Index2 Index;

        public TileBaseType BaseType { get; set; }

        public Tile(Index2 index, TileBaseType baseType)
        {
            Index = index;
            BaseType = baseType;
        }
    }
}
