using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// enum for obstacle types, can be extended if has new
    /// </summary>
    public enum ObstacleTypes
    {
        Unknown = 0, //unknow place

        Road = 1, //regular road, no obstacle

        Rock = 2, //rock, robot will be blocked

        Hole = 3, //hole, switch to another location

        Spinner = 4, //spinner, change facing direction
    }
}
