using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    class Program
    {
        static void Main(string[] args)
        {
            //define map size
            int mapHorizontalSize = 10;
            int mapVerticalSize = 10;
            int countOfObstacleTypes = 5;  //for randomly generate obstacle types

            //create a map
            var map1 = new Map()
            {
                CountOfObstacleTypes = countOfObstacleTypes,
                HorizontalSize = mapHorizontalSize,
                VerticalSize = mapVerticalSize
            };
            //generate a map
            map1.GenerateTopographyManually();
            //map.GenerateTopographyRandomly();

            //print map from top to bottom
            Console.WriteLine("map size: horizontal-" + mapHorizontalSize + " vertical-" + mapVerticalSize);
            Console.WriteLine("map topography:(0-Unknown; 1-Road; 2-Rock; 3-Hole; 4-Spinner;)");
            for (int j = map1.Topography.GetLength(1) - 1; j >= 0; j--)
            {
                for (int i = 0; i < map1.Topography.GetLength(0); i++)
                {
                    Console.Write((int)map1.Topography[i, j].ObstacleType + " ");
                }

                Console.WriteLine();
            }

            //initialize robots infomation as below:
            //randomly generate robot born location
            var rd = new Random(Guid.NewGuid().GetHashCode());

            var commands1 = "LFFFRFFLFFFFRLFFFR";
            var commands2 = "LLFFFRLRLFFLLRRLFLFFRLLF";

            //create a regular robot1
            var bornCoor1 = new Coordinates
                {
                    X = rd.Next(0, mapHorizontalSize),
                    Y = rd.Next(0, mapVerticalSize)
                };
            var regularRobot1 = new RegularRobots()
            {
                RobotNumber = 1,
                RobotType = RobotTypes.Regular,
                Facing = Directions.W,
                Coordinate = bornCoor1,
                Map = map1
            };
            map1.RobotsInMap = new List<Robots>();
            map1.RobotsInMap.Add(regularRobot1);

            ////set robots born location to a "Road"
            //map1.Topography[bornCoor1.X, bornCoor1.Y] = new Roads
            //{
            //    Coordinate = new Coordinates { X = bornCoor1.X, Y = bornCoor1.Y }
            //};

            Console.WriteLine();
            Console.WriteLine("For Single Robot Scenario:");
            Console.WriteLine("Robot born location: (" + bornCoor1.X + "," + bornCoor1.Y + ")");
            Console.WriteLine("Robot born facing: " + regularRobot1.Facing);
            Console.WriteLine("commands1: " + commands1);
            Console.WriteLine("robot path1:");

            //print robot traversal path
            var robotPath = RobotPath(regularRobot1, commands1);
            //print path
            foreach (var t in robotPath)
            {
                Console.Write("(" + t.X + "," + t.Y + ")-->");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("continue moving...");
            Console.WriteLine("commands2: " + commands2);
            Console.WriteLine("robot path2:");
            //print robot traversal path
            var robotPath2 = RobotPath(regularRobot1, commands2);
            //print path
            foreach (var t in robotPath2)
            {
                Console.Write("(" + t.X + "," + t.Y + ")-->");
            }


            //create a map
            var map2 = new Map()
            {
                CountOfObstacleTypes = countOfObstacleTypes,
                HorizontalSize = mapHorizontalSize,
                VerticalSize = mapVerticalSize
            };
            //generate a map
            map2.GenerateTopographyManually();
            //map.GenerateTopographyRandomly();

            //create a regular robot2
            var bornCoor2 = new Coordinates
            {
                X = rd.Next(0, mapHorizontalSize),
                Y = rd.Next(0, mapVerticalSize)
            };
            var regularRobot2 = new RegularRobots()
            {
                RobotNumber = 2,
                RobotType = RobotTypes.Regular,
                Facing = Directions.E,
                Coordinate = bornCoor2,
                Map = map2
            };
            //create a regular robot3
            var bornCoor3 = new Coordinates
            {
                X = rd.Next(0, mapHorizontalSize),
                Y = rd.Next(0, mapVerticalSize)
            };
            var regularRobot3 = new RegularRobots()
            {
                RobotNumber = 3,
                RobotType = RobotTypes.Regular,
                Facing = Directions.N,
                Coordinate = bornCoor3,
                Map = map2
            };
            //set robots list in map
            map2.RobotsInMap = new List<Robots>();
            map2.RobotsInMap.Add(regularRobot2);
            map2.RobotsInMap.Add(regularRobot3);

            ////set robots born location to a "Road"
            //map2.Topography[bornCoor2.X, bornCoor2.Y] = new Roads
            //{
            //    Coordinate = new Coordinates { X = bornCoor2.X, Y = bornCoor2.Y }
            //};
            //map2.Topography[bornCoor3.X, bornCoor3.Y] = new Roads
            //{
            //    Coordinate = new Coordinates { X = bornCoor3.X, Y = bornCoor3.Y }
            //};

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("For multiple Robots Scenario: robots move one by one");
            Console.WriteLine("Robot2 born location: (" + bornCoor2.X + "," + bornCoor2.Y + ")");
            Console.WriteLine("Robot2 born facing: " + regularRobot2.Facing);
            Console.WriteLine("Robot2 commands: " + commands1);
            Console.WriteLine("Robot3 born location: (" + bornCoor3.X + "," + bornCoor3.Y + ")");
            Console.WriteLine("Robot3 born facing: " + regularRobot3.Facing);
            Console.WriteLine("Robot3 commands: " + commands2);
            var robotsList = new List<Robots> { regularRobot2, regularRobot3 };
            var commandsList = new List<string> { commands1, commands2 };
            var paths = RobotPaths(robotsList, commandsList);
            Console.WriteLine("Regular robot2 path:");
            foreach (var t in paths[0])
            {
                Console.Write("(" + t.X + "," + t.Y + ")-->");
            }
            Console.WriteLine();   
            Console.WriteLine("Regular robot3 path:");
            foreach (var t in paths[1])
            {
                Console.Write("(" + t.X + "," + t.Y + ")-->");
            }


            Console.ReadLine();
        }

        /// <summary>
        /// Get robot traversal path from given commands
        /// </summary>
        /// <param name="robot">input robot</param>
        /// <param name="command">Movement commands</param>
        /// <returns>Robot traversal path</returns>
        public static List<Coordinates> RobotPath(Robots robot, string commands)
        {
            Map map = robot.Map;
            int mapHoriSize = map.HorizontalSize;
            int mapVertiSize = map.VerticalSize;
            var path = new List<Coordinates>();
            //robot born location is a Road, add born coordinates into path
            path.Add(new Coordinates { X = robot.Coordinate.X, Y = robot.Coordinate.Y }); 

            //corner cases
            //invalid map
            if (mapHoriSize == 0 || mapVertiSize == 0)
                return path;
            //robot born location is out of map
            if (robot.Coordinate.X < 0 || robot.Coordinate.X >= mapHoriSize || robot.Coordinate.Y < 0 || robot.Coordinate.Y >= mapVertiSize)
                return path;
            //no commands
            if (commands.Length == 0)
                return path;

            //regular Robot case
            var regularRobot = (RegularRobots)robot;
            foreach (var t in commands)
            {
                regularRobot.Move(t);
                var coorAfterMove = new Coordinates
                {
                    X = regularRobot.Coordinate.X,
                    Y = regularRobot.Coordinate.Y
                };

                path.Add(coorAfterMove);
            }

            return path;
        }

        /// <summary>
        /// Get robots traversal paths list from given commands
        /// </summary>
        /// <param name="robots">input robots</param>
        /// <param name="commands">Movement commands for each robot</param>
        /// <returns>Robots traversal paths list</returns>
        public static List<List<Coordinates>> RobotPaths(List<Robots> robots, List<string> commands)
        {
            Map map = robots[0].Map;
            int mapHoriSize = map.HorizontalSize;
            int mapVertiSize = map.VerticalSize;
            var paths = new List<List<Coordinates>>();

            //put each born location into paths list
            foreach (var t in robots)
            {
                var coorList = new List<Coordinates>();
                coorList.Add(new Coordinates { X = t.Coordinate.X, Y = t.Coordinate.Y });
                paths.Add(coorList);
            }

            int maxLen = commands.Max(x => x.Length);
            for (int i = 0; i < maxLen;i++ )
            {
                for (int j = 0; j < commands.Count; j++)
                {
                    var cmd = commands[j];
                    if (i < cmd.Length)
                    {
                        var regularRobot = (RegularRobots)robots[j];
                        regularRobot.Move(cmd[i]);
                        var coorAfterMove = new Coordinates
                        {
                            X = regularRobot.Coordinate.X,
                            Y = regularRobot.Coordinate.Y
                        };

                        paths[j].Add(coorAfterMove);
                    }
                }
            }

            return paths;
        }



    }
}
