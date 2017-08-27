using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Core.Structures
{
    public abstract class Structure
    {
        public Point Size { get; set; }
        public int Height { get; set; }
    }
}
