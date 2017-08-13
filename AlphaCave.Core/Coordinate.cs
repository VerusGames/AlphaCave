using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core
{
    /// <summary>
    /// Stellt eine komplexe Koordinate in der Welt dar
    /// </summary>
    public struct Coordinate
    {
        /// <summary>
        /// Blockposition in einer Ebene
        /// </summary>
        public Index2 FloorPosition;

        /// <summary>
        /// Ebene
        /// </summary>
        public int Floor;

        /// <summary>
        /// Position im Block
        /// </summary>
        public Point BlockPoint;
    }
}
