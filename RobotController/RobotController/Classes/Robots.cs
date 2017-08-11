using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// Robot base class
    /// </summary>
    public class Robots
    {
        //robot number id
        public int RobotNumber { get; set; }

        //robot type, for multiple types of robot scenario
        public RobotTypes RobotType { get; set; }

        //current facing direction
        public Directions Facing { get; set; }

        //current location coordinate
        public Coordinates Coordinate { get; set; }

        //the map where robot on
        public Map Map { get; set; }
    }

    /// <summary>
    /// Regular Robots Class
    /// </summary>
    public class RegularRobots : Robots
    {
        //robot move
        public void Move(char moveDirection)
        {
            switch (moveDirection)
            {
                case 'L':
                    MoveLeft();
                    break;
                case 'R':
                    MoveRight();
                    break;
                case 'F':
                    MoveForward();
                    break;
                default:
                    break;
            }
        }

        //move left
        public void MoveLeft()
        {
            int mapMaxCoorX = Map.HorizontalSize - 1;
            int mapMaxCoorY = Map.VerticalSize - 1;

            switch (Facing)
            {
                case Directions.N:
                    Facing = Directions.W; //change facing direction
                    if (Coordinate.X > 0)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X - 1, Coordinate.Y];
                        
                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.X--;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.X--;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.N;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.E;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.S;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.S:
                    Facing = Directions.E; //change facing direction
                    if (Coordinate.X < mapMaxCoorX)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X + 1, Coordinate.Y];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.X++;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.X++;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.S;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.W;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.N;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.W:
                    Facing = Directions.S; //change facing direction
                    if (Coordinate.Y > 0)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X, Coordinate.Y - 1];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.Y--;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.Y--;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.W;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.N;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.E;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.E:
                    Facing = Directions.N; //change facing direction
                    if (Coordinate.Y < mapMaxCoorY)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X, Coordinate.Y + 1];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.Y++;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.Y++;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.E;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.S;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.W;
                                }
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //move right
        public void MoveRight()
        {
            int mapMaxCoorX = Map.HorizontalSize - 1;
            int mapMaxCoorY = Map.VerticalSize - 1;

            switch (Facing)
            {
                case Directions.N:
                    Facing = Directions.E;
                    if (Coordinate.X < mapMaxCoorX)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X + 1, Coordinate.Y];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.X++;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.X++;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.S;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.W;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.N;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.S:
                    Facing = Directions.W;
                    if (Coordinate.X > 0)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X - 1, Coordinate.Y];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.X--;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.X--;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.N;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.E;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.S;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.W:
                    Facing = Directions.N;
                    if (Coordinate.Y < mapMaxCoorY)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X, Coordinate.Y + 1];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.Y++;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.Y++;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.E;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.S;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.W;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.E:
                    Facing = Directions.S;
                    if (Coordinate.Y > 0)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X, Coordinate.Y - 1];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.Y--;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.Y--;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.W;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.N;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.E;
                                }
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //move forward
        public void MoveForward()
        {
            int mapMaxCoorX = Map.HorizontalSize - 1;
            int mapMaxCoorY = Map.VerticalSize - 1;

            switch (Facing)
            {
                case Directions.N:
                    if (Coordinate.Y < mapMaxCoorY)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X, Coordinate.Y + 1];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.Y++;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.Y++;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.E;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.S;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.W;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.S:
                    if (Coordinate.Y > 0)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X, Coordinate.Y - 1];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.Y--;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.Y--;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.W;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.N;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.E;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.W:
                    if (Coordinate.X > 0)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X - 1, Coordinate.Y];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.X--;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.X--;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.N;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.E;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.S;
                                }
                                break;
                        }
                    }
                    break;
                case Directions.E:
                    if (Coordinate.X < mapMaxCoorX)
                    {
                        var targetLocationObstacle = Map.Topography[Coordinate.X + 1, Coordinate.Y];

                        //for multiple
                        if (Map.RobotsInMap.Count > 1)
                        {
                            if (CheckIfTargetLocationHasRobot(targetLocationObstacle))
                                return;
                        }

                        switch (targetLocationObstacle.ObstacleType)
                        {
                            case ObstacleTypes.Unknown:
                                break;
                            case ObstacleTypes.Road:
                                Coordinate.X++;
                                break;
                            case ObstacleTypes.Rock:
                                break;
                            case ObstacleTypes.Hole:
                                var hole = (Holes)targetLocationObstacle;
                                Coordinate.X = hole.ConnectedCoordinate.X;
                                Coordinate.Y = hole.ConnectedCoordinate.Y;
                                break;
                            case ObstacleTypes.Spinner:
                                Coordinate.X++;
                                var spinner = (Spinners)targetLocationObstacle;
                                if (spinner.RotateDegrees == 90)
                                {
                                    Facing = Directions.S;
                                }
                                else if (spinner.RotateDegrees == 180)
                                {
                                    Facing = Directions.W;
                                }
                                else if (spinner.RotateDegrees == 270)
                                {
                                    Facing = Directions.N;
                                }
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //check if target location has robot
        public bool CheckIfTargetLocationHasRobot(Obstacles targetObs)
        {
            foreach (var t in Map.RobotsInMap)
            {
                //skip current robot
                if (t.RobotNumber == RobotNumber) continue;

                var targetCoorX = targetObs.Coordinate.X;
                var targetCoorY = targetObs.Coordinate.Y;
                //handle holes
                if (targetObs.ObstacleType == ObstacleTypes.Hole)
                {
                    var hole = (Holes)targetObs;
                    targetCoorX = hole.ConnectedCoordinate.X;
                    targetCoorY = hole.ConnectedCoordinate.Y;
                }

                if (t.Coordinate.X == targetCoorX && t.Coordinate.Y == targetCoorY)
                {
                    return true;
                }
            }

            return false;
        }

    }

    /// <summary>
    /// Jump Robot Class
    /// </summary>
    public class JumpRobots: Robots
    {
        //robot move
        public void Move(char moveDirection)
        {
            switch (moveDirection)
            {
                case 'L':
                    MoveLeft();
                    break;
                case 'R':
                    MoveRight();
                    break;
                case 'F':
                    MoveForward();
                    break;
                default:
                    break;
            }
        }

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }

        public void MoveForward()
        {

        }

    }





}
