using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController;

namespace UnitTests
{
    [TestClass]
    public class RegularRobotMove
    {
        //define map size
        int mapHorizontalSize = 10;
        int mapVerticalSize = 10;
        int countOfObstacleTypes = 5;  //for randomly generate obstacle types
        Map map = new Map();
        RegularRobots robot = new RegularRobots();
        Directions robotBornFacing = new Directions();
        int robotBornCoorX;
        int robotBornCoorY;

        public RegularRobotMove()
        {
            //Manually generate a 10*10 map
            /* map details:(0-Unknown; 1-Road; 2-Rock; 3-Hole; 4-Spinner;)
             0 1 1 2 1 1 3 1 1 1
             1 1 3 1 1 2 1 4 1 1
             1 1 4 4 1 2 1 1 0 1
             3 1 1 1 1 1 4 1 1 1 
             1 1 0 1 1 1 1 2 1 3
             3 2 1 1 0 1 1 1 2 1
             1 0 1 1 2 1 1 1 1 4
             1 1 2 1 1 1 4 1 3 1
             4 2 1 1 1 1 1 1 1 2
            */
            map = new Map()
            {
                CountOfObstacleTypes = mapHorizontalSize,
                HorizontalSize = mapVerticalSize,
                VerticalSize = countOfObstacleTypes
            };

            map.GenerateTopographyManually();

            robot = new RegularRobots()
            {
                RobotNumber = 1,
                RobotType = RobotTypes.Regular,
                //Facing = robotBornFacing,
                //Coordinate = robotBornCoor,
                Map = map
            };

            map.RobotsInMap = new System.Collections.Generic.List<Robots>();
            map.RobotsInMap.Add(robot);
        }


        [TestMethod]
        public void MoveForward_MeetRoads()
        {
            robotBornFacing = Directions.N;
            robotBornCoorX = 4;
            robotBornCoorY = 6;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveForward();
            Assert.AreEqual(robotBornFacing, robot.Facing);  //same facing direction
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //same coordinate X
            Assert.AreEqual(robotBornCoorY + 1, robot.Coordinate.Y); //move to N by 1
        }

        [TestMethod]
        public void MoveForward_MeetUnknownsOrRocksOrMapBorder()
        {
            //meet Unknown
            robotBornFacing = Directions.N;
            robotBornCoorX = 8;
            robotBornCoorY = 6;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveForward();
            Assert.AreEqual(robotBornFacing, robot.Facing);  //same facing direction
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //cannot move, same coordinate Y

            //meet Rock
            robotBornFacing = Directions.N;
            robotBornCoorX = 5;
            robotBornCoorY = 6;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveForward();
            Assert.AreEqual(robotBornFacing, robot.Facing);  //same facing direction
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //cannot move, same coordinate Y

            //meet map border
            robotBornFacing = Directions.N;
            robotBornCoorX = 9;
            robotBornCoorY = 9;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveForward();
            Assert.AreEqual(robotBornFacing, robot.Facing);  //same facing direction
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //cannot move, same coordinate Y
        }

        [TestMethod]
        public void MoveForward_MeetHoles()
        {
            //hole(6,8) connect to (7,9)
            robotBornFacing = Directions.N;
            robotBornCoorX = 6;
            robotBornCoorY = 8;
            var holeConnectedCoorX = 7;
            var holeConnectedCoorY = 9;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveForward();
            Assert.AreEqual(robotBornFacing, robot.Facing);  //same facing direction
            Assert.AreEqual(holeConnectedCoorX, robot.Coordinate.X);  //go to hole connected coor X
            Assert.AreEqual(holeConnectedCoorY, robot.Coordinate.Y);  //go to hole connected coor Y
        }

        [TestMethod]
        public void MoveForward_MeetSpinners()
        {
            //spinner(2,3), rotated degree: 270
            robotBornFacing = Directions.N;
            robotBornCoorX = 2;
            robotBornCoorY = 2;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveForward();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction rotate to W
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //same coordinate X
            Assert.AreEqual(robotBornCoorY + 1, robot.Coordinate.Y);  //move to N by 1
        }


        [TestMethod]
        public void MoveLeft_MeetRoads()
        {
            robotBornFacing = Directions.N;
            robotBornCoorX = 4;
            robotBornCoorY = 6;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveLeft();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction from N to W
            Assert.AreEqual(robotBornCoorX - 1, robot.Coordinate.X);  //move to W by 1
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y
        }

        [TestMethod]
        public void MoveLeft_MeetUnknownsOrRocksOrMapBorder()
        {
            //meet Unknown
            robotBornFacing = Directions.N;
            robotBornCoorX = 2;
            robotBornCoorY = 2;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveLeft();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction from N to W
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y

            //meet Rock
            robotBornFacing = Directions.N;
            robotBornCoorX = 5;
            robotBornCoorY = 2;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveLeft();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction from N to W
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y

            //meet map border
            robotBornFacing = Directions.N;
            robotBornCoorX = 0;
            robotBornCoorY = 7;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveLeft();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction from N to W
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y
        }

        [TestMethod]
        public void MoveLeft_MeetHoles()
        {
            //hole(8,1) connect to (8,4)
            robotBornFacing = Directions.N;
            robotBornCoorX = 9;
            robotBornCoorY = 1;
            var holeConnectedCoorX = 8;
            var holeConnectedCoorY = 4;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveLeft();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction from N to W
            Assert.AreEqual(holeConnectedCoorX, robot.Coordinate.X);  //go to hole connected coor X
            Assert.AreEqual(holeConnectedCoorY, robot.Coordinate.Y);  //go to hole connected coor Y
        }

        [TestMethod]
        public void MoveLeft_MeetSpinners()
        {
            //spinner(7,8), rotated degree: 90
            robotBornFacing = Directions.N;
            robotBornCoorX = 8;
            robotBornCoorY = 8;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveLeft();
            Assert.AreEqual(Directions.N, robot.Facing);  //facing direction rotate to N
            Assert.AreEqual(robotBornCoorX - 1, robot.Coordinate.X);  //move to W by 1 
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y);  //same coordinate Y
        }


        [TestMethod]
        public void MoveRight_MeetRoads()
        {
            robotBornFacing = Directions.N;
            robotBornCoorX = 5;
            robotBornCoorY = 0;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveRight();
            Assert.AreEqual(Directions.E, robot.Facing);  //facing direction from N to E
            Assert.AreEqual(robotBornCoorX + 1, robot.Coordinate.X);  //move to E by 1
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y
        }

        [TestMethod]
        public void MoveRight_MeetUnknownsOrRocksOrMapBorder()
        {
            //meet Unknown
            robotBornFacing = Directions.N;
            robotBornCoorX = 7;
            robotBornCoorY = 7;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveRight();
            Assert.AreEqual(Directions.E, robot.Facing);  //facing direction from N to E
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y

            //meet Rock
            robotBornFacing = Directions.N;
            robotBornCoorX = 4;
            robotBornCoorY = 7;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveRight();
            Assert.AreEqual(Directions.E, robot.Facing);  //facing direction from N to E
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y

            //meet map border
            robotBornFacing = Directions.N;
            robotBornCoorX = 9;
            robotBornCoorY = 3;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveRight();
            Assert.AreEqual(Directions.E, robot.Facing);  //facing direction from N to E
            Assert.AreEqual(robotBornCoorX, robot.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y); //same coordinate Y
        }

        [TestMethod]
        public void MoveRight_MeetHoles()
        {
            //hole(2,8) connect to (4,0)
            robotBornFacing = Directions.N;
            robotBornCoorX = 1;
            robotBornCoorY = 8;
            var holeConnectedCoorX = 4;
            var holeConnectedCoorY = 0;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveRight();
            Assert.AreEqual(Directions.E, robot.Facing);  //facing direction from N to E
            Assert.AreEqual(holeConnectedCoorX, robot.Coordinate.X);  //go to hole connected coor X
            Assert.AreEqual(holeConnectedCoorY, robot.Coordinate.Y);  //go to hole connected coor Y
        }

        [TestMethod]
        public void MoveRight_MeetSpinners()
        {
            //spinner(7,4), rotated degree: 180
            robotBornFacing = Directions.N;
            robotBornCoorX = 6;
            robotBornCoorY = 4;
            robot.Facing = robotBornFacing;
            robot.Coordinate = new Coordinates { X = robotBornCoorX, Y = robotBornCoorY };
            robot.MoveRight();
            Assert.AreEqual(Directions.W, robot.Facing);  //facing direction rotate to W
            Assert.AreEqual(robotBornCoorX + 1, robot.Coordinate.X);  //move to E by 1 
            Assert.AreEqual(robotBornCoorY, robot.Coordinate.Y);  //same coordinate Y
        }

        [TestMethod]
        public void MultiRobots_MoveForward_MeetAnotherRobot()
        {
            var map2 = new Map()
            {
                CountOfObstacleTypes = mapHorizontalSize,
                HorizontalSize = mapVerticalSize,
                VerticalSize = countOfObstacleTypes
            };
            map2.GenerateTopographyManually();
            var bornCoor1 = new Coordinates { X = 3, Y = 5 };
            var robot1 = new RegularRobots()
            {
                RobotNumber = 1,
                RobotType = RobotTypes.Regular,
                Facing = Directions.N,
                Coordinate = bornCoor1,
                Map = map2
            };
            var bornCoor2 = new Coordinates { X = 3, Y = 6 };
            var robot2 = new RegularRobots()
            {
                RobotNumber = 2,
                RobotType = RobotTypes.Regular,
                Facing = Directions.N,
                Coordinate = bornCoor2,
                Map = map2
            };
            map2.RobotsInMap = new System.Collections.Generic.List<Robots>();
            map2.RobotsInMap.Add(robot1);
            map2.RobotsInMap.Add(robot2);

            //robot1 is trying to move forward, but cannot move, robot2 is there
            robot1.MoveForward();
            Assert.AreEqual(bornCoor1.X, robot1.Coordinate.X);  //cannot move, same coordinate X
            Assert.AreEqual(bornCoor1.Y, robot1.Coordinate.Y);  //cannot move, same coordinate Y
        }

        [TestMethod]
        public void MultiRobots_MoveForward_MeetHole_ConnectedLocationHasRobot()
        {
            var map2 = new Map()
            {
                CountOfObstacleTypes = mapHorizontalSize,
                HorizontalSize = mapVerticalSize,
                VerticalSize = countOfObstacleTypes
            };
            map2.GenerateTopographyManually();
            var bornCoor1 = new Coordinates { X = 9, Y = 1 };
            var robot1 = new RegularRobots()
            {
                RobotNumber = 1,
                RobotType = RobotTypes.Regular,
                Facing = Directions.W,
                Coordinate = bornCoor1,
                Map = map2
            };
            var bornCoor2 = new Coordinates { X = 7, Y = 1 };
            var robot2 = new RegularRobots()
            {
                RobotNumber = 2,
                RobotType = RobotTypes.Regular,
                Facing = Directions.E,
                Coordinate = bornCoor2,
                Map = map2
            };
            map2.RobotsInMap = new System.Collections.Generic.List<Robots>();
            map2.RobotsInMap.Add(robot1);
            map2.RobotsInMap.Add(robot2);

            robot1.MoveForward();  //robot1 move to the hole first
            robot2.MoveForward();  //then robot2 move is trying to move to the same hole
            Assert.AreEqual(bornCoor2.X, robot2.Coordinate.X);  //robot2 cannot move, same coordinate X
            Assert.AreEqual(bornCoor2.Y, robot2.Coordinate.Y);  //robot2 cannot move, same coordinate Y
        }


    }
}
