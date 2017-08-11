using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// enum for robot types
    /// </summary>
    public enum RobotTypes
    {
        Regular, //regular robot
        Jump,    //can jump over Rocks or jump over another robot
        Bomb,    //can destroy Rocks
        Fat,     //too fat, cannot fall into Holes
        Giant,   //very big, take 2 continuously locations
        Fast,    //move 2 steps at one time
        Diagonal,//diagonally move, do not change facing direction
    }
}
