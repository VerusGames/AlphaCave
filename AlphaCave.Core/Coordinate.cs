using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public Point FloorPosition;

        /// <summary>
        /// Ebene
        /// </summary>
        public int Floor;

        /// <summary>
        /// Position im Block
        /// </summary>
        public Vector2 BlockPoint;


        public Coordinate(Point floorPosition, int floor, Vector2 blockPoint)
        {
            FloorPosition = floorPosition;
            Floor = floor;
            BlockPoint = blockPoint;
        }

        public Vector3 GetPositionVector()
        {
            return new Vector3(FloorPosition.X+BlockPoint.X,FloorPosition.Y+BlockPoint.Y,FloorPosition.Y+BlockPoint.Y);
        }
        
        public static Coordinate operator +(Coordinate main, Point position)
        {
            return new Coordinate(main.FloorPosition + position,main.Floor,main.BlockPoint);
        }
        
        public static Coordinate operator +(Coordinate main, Vector2 position)
        {
            return new Coordinate(main.FloorPosition,main.Floor,main.BlockPoint +position);
        }
    }
}
