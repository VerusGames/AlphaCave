using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core
{
    /// <summary>
    /// Integerposition
    /// </summary>
    public struct Index2
    {
        public short X;
        public short Y;

        public Index2(short x, short y)
        {
            X = x;
            Y = y;
        }
        
        //TODO: Throw exception if too big!
        public Index2(int x, int y)
        {
            X = (short)x;
            Y = (short)y;
        }

        public override int GetHashCode()
        {
            return X << 16 & Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Index2 index)
            {
                return index.X == X && index.Y == Y;
            }

            return false;
        }

        public int GetFlatIndex(int ySize)
        {
            return Y * ySize + X;
        }
    }
}
