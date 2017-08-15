using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps
{
    [Flags]
    public enum TileFlags : byte
    {
        Visible = 0x01,
        PreVisible,
        Collidable
    }
}
