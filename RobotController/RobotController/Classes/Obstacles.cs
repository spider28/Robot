using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// Obstacles class
    /// We can add new obstacle type if needed
    /// </summary>
    public class Obstacles
    {
        //obstacle type
        public virtual ObstacleTypes ObstacleType { get; set; }

        //coordinate of obstacle
        public Coordinates Coordinate { get; set; }
    }

    /// <summary>
    /// Roads class, inherited from Obstacles class
    /// </summary>
    public class Roads : Obstacles
    {
        public override ObstacleTypes ObstacleType
        {
            get
            {
                return ObstacleTypes.Road;
            }
        }
    }

    /// <summary>
    /// Rocks class, inherited from Obstacles class
    /// </summary>
    public class Rocks : Obstacles
    {
        public override ObstacleTypes ObstacleType
        {
            get
            {
                return ObstacleTypes.Rock;
            }
        }
    }

    /// <summary>
    /// Holes class, inherited from Obstacles class
    /// </summary>
    public class Holes : Obstacles
    {
        public override ObstacleTypes ObstacleType
        {
            get
            {
                return ObstacleTypes.Hole;
            }
        }

        //connected coordinate of the hole
        public Coordinates ConnectedCoordinate { get; set; }
    }

    /// <summary>
    /// Spinnners class, inherited from Obstacles class
    /// </summary>
    public class Spinners : Obstacles
    {
        public override ObstacleTypes ObstacleType
        {
            get
            {
                return ObstacleTypes.Spinner;
            }
        }

        //spinner rotate degrees, value is times of 90
        public int RotateDegrees { get; set; }
    }





}
