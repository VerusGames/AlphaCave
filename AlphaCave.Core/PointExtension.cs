using System;
using engenious;

namespace AlphaCave.Core
{
    public static class PointExtension
    {
        public static int GetFlatIndex(this Point point,int ySize)
        {
            if(point.X > short.MaxValue)
                throw new NotSupportedException("Point X to Big for GetFlatIndex");
            
            if(point.Y > short.MaxValue)
                throw new NotSupportedException("Point Y to Big for GetFlatIndex");

            return point.Y * ySize + point.X;
        }
    }
}