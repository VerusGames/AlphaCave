using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Core.Maps
{
    public enum WallType : int
    {
        Solo=0,
        Down=32,
        Left=128,
        Up=2,
        Right=8,
        RightDown=40,
        LeftDown=160,
        LeftUp=130,
        RightUp=10,
        HorizontalMiddle=136,
        VerticalMiddle=34,
        LeftUpRight=138,
        UpRightDown=42,
        LeftDownRight=168,
        LeftUpDown=162,
        LeftUpRightDown=170,
    }
    
    public static class  WallTypeHelper
    {
        public static WallType GetWallType(Point index, Chunk chunk)
        {
            int bitMask = 0;

            //bitMask |= (chunk.IsPreVisible(index.X - 1, index.Y - 1) ? 1 << 0 : 0);
            if(index.Y > 0)
                bitMask |= (chunk.IsPreVisible(index.X + 0, index.Y - 1) ? 1 << 1 : 0);
            //bitMask |= (chunk.IsPreVisible(index.X + 1, index.Y - 1) ? 1 << 2 : 0);
            if(index.X > 0)
                bitMask |= (chunk.IsPreVisible(index.X - 1, index.Y + 0) ? 1 << 7 : 0);

            if(index.X < Chunk.CHUNKSIZE_X-1)
                bitMask |= (chunk.IsPreVisible(index.X + 1, index.Y + 0) ? 1 << 3 : 0);
            //bitMask |= (chunk.IsPreVisible(index.X - 1, index.Y + 1) ? 1 << 5 : 0);
            if(index.Y < Chunk.CHUNKSIZE_Y-1)
                bitMask |= (chunk.IsPreVisible(index.X + 0, index.Y + 1) ? 1 << 5 : 0);
            //bitMask |= (chunk.IsPreVisible(index.X + 1, index.Y + 1) ? 1 << 7 : 0);

            return (WallType)bitMask;
        }

        // Key=Wandtyp, Value=TileIndex
        public static Dictionary<WallType, Point[]> StoneWallLookupTable = new Dictionary<WallType, Point[]>()
        {
            {WallType.Solo, new[] {new Point(9,1),new Point(14,2),new Point(14,4)} },
            {WallType.Down, new[] {new Point(8,0)} },
            {WallType.Left, new[] {new Point(11,0), new Point(15,2),new Point(15,4)} },
            {WallType.Up, new[] {new Point(8,1),new Point(14,2),new Point(14,4)} },
            {WallType.Right, new[] {new Point(9,0), new Point(12,2),new Point(12,4)} },
            {WallType.RightDown, new[] {new Point(12,0)} },
            {WallType.LeftDown, new[] { new Point(13,0)} },
            {WallType.LeftUp, new[] { new Point(13,1), new Point(15,2),new Point(15,4) }},
            {WallType.RightUp, new[] { new Point(12,1), new Point(12,2),new Point(12,4)} },
            {WallType.HorizontalMiddle, new[] { new Point(10,0), new Point(13,2),new Point(9,4) }},
            {WallType.VerticalMiddle, new[] { new Point(11,1)} },
            {WallType.LeftUpRight, new[] { new Point(15,0),new Point(13,2),new Point(9,4) }},
            {WallType.UpRightDown, new[] { new Point(14,0)}},
            {WallType.LeftDownRight, new[] { new Point(15,1)} },
            {WallType.LeftUpDown,new[] {new Point(14,1) }},
            {WallType.LeftUpRightDown, new[] {new Point(10,1)}},
        };
    }
}
