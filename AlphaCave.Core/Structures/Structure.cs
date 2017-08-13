using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Structures
{
    public abstract class Structure
    {
        public Index2 Size { get; set; }
        public int Height { get; set; }
    }
}
