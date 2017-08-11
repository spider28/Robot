using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    /// <summary>
    /// Map class
    /// Assuming map is in 1st quadrant, all X and Y coordinates are 0 or positive number
    /// No different between "all positive number" and "both negative and positive number"
    /// </summary>
    public class Map
    {
        //count of obstacle types
        public int CountOfObstacleTypes { get; set; }

        //coordinate X range
        public int HorizontalSize { get; set; }

        //coordinate Y range
        public int VerticalSize { get; set; }

        //for multiple robots scenario
        public List<Robots> RobotsInMap { get; set; }

        //map details with roads and obstacles
        public Obstacles[,] Topography { get; set; }

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
        public void GenerateTopographyManually()
        {
            HorizontalSize = 10;
            VerticalSize = 10;
            Topography = new Obstacles[HorizontalSize, VerticalSize];

            for (int j = 0; j < VerticalSize; j++)
            {
                for (int i = 0; i < HorizontalSize; i++)
                {
                    Topography[i, j] = new Roads
                    {
                        Coordinate = new Coordinates { X = i, Y = j }
                    };
                }
            }

            //5 Unknowns
            //(0,9), (8,7), (2,5), (4,4), (1,2)
            var unknownsCoors = new List<int[]>()
            {
                new int[]{0,9},
                new int[]{8,7},
                new int[]{2,5},
                new int[]{4,4},
                new int[]{1,2},
            };
            foreach (var t in unknownsCoors)
            {
                Topography[t[0], t[1]] = new Obstacles
                {
                    ObstacleType = ObstacleTypes.Unknown,
                    Coordinate = new Coordinates { X = t[0], Y = t[1] }
                };
            }

            //10 Rocks
            //(3,9), (5,8), (5,7), (7,5), (1,4), (8,3), (4,2), (2,1), (1,0), (9,0)
            var rocksCoors = new List<int[]>()
            {
                new int[]{3,9},
                new int[]{5,8},
                new int[]{5,7},
                new int[]{7,5},
                new int[]{1,4},
                new int[]{8,3},
                new int[]{4,2},
                new int[]{2,1},
                new int[]{1,0},
                new int[]{9,0},
            };
            foreach (var t in rocksCoors)
            {
                Topography[t[0], t[1]] = new Rocks
                {
                    Coordinate = new Coordinates { X = t[0], Y = t[1] }
                };
            }

            //6 Holes
            //(6,9)-->(7,9)
            //(2,8)-->(4,0)
            //(0,6)-->(3,2) 
            //(9,5)-->(6,3) 
            //(0,4)-->(5,5) 
            //(8,1)-->(8,4)
            var holesCoors = new List<int[]>()
            {
                new int[]{6,9,7,9},
                new int[]{2,8,4,0},
                new int[]{0,6,3,2},
                new int[]{9,5,6,3},
                new int[]{0,4,5,5},
                new int[]{8,1,8,4},
            };
            foreach (var t in holesCoors)
            {
                Topography[t[0], t[1]] = new Holes
                {
                    Coordinate = new Coordinates { X = t[0], Y = t[1] },
                    ConnectedCoordinate = new Coordinates
                    {
                        X = t[2],
                        Y = t[3]
                    }
                };
            }

            //10 Spinners
            //(7,8)90, (2,7)270, (3,7)0, (6,6)180, (7,4)180, (2,3)270, (4,3)90, (9,2)0, (6,1)90, (0,0)180
            var spinnersCoors = new List<int[]>()
            {
                new int[]{7,8,90},
                new int[]{2,7,270},
                new int[]{3,7,0},
                new int[]{6,6,180},
                new int[]{7,4,180},
                new int[]{2,3,270},
                new int[]{4,3,90},
                new int[]{9,2,0},
                new int[]{6,1,90},
                new int[]{0,0,180},
            };
            foreach (var t in spinnersCoors)
            {
                Topography[t[0], t[1]] = new Spinners
                {
                    Coordinate = new Coordinates { X = t[0], Y = t[1] },
                    RotateDegrees = t[2]
                };
            }
        }


        //Randomly generate a map, one third are Roads
        public void GenerateTopographyRandomly()
        {
            var rd = new Random(Guid.NewGuid().GetHashCode());
            Topography = new Obstacles[HorizontalSize, VerticalSize];

            for (int j = 0; j < VerticalSize; j++)
            {
                for (int i = 0; i < HorizontalSize; i++)
                {
                    if (rd.Next(0, 3) == 0)
                    {
                        //one third obstacles are Roads
                        Topography[i, j] = new Roads
                        {
                            Coordinate = new Coordinates { X = i, Y = j }
                        };
                    }
                    else
                    {
                        var type = (ObstacleTypes)rd.Next(0, CountOfObstacleTypes);
                        switch (type)
                        {
                            //generate unknown obstacle
                            case ObstacleTypes.Unknown:
                                Topography[i, j] = new Obstacles
                                {
                                    ObstacleType = ObstacleTypes.Unknown,
                                    Coordinate = new Coordinates { X = i, Y = j }
                                };
                                break;
                            //generate road
                            case ObstacleTypes.Road:
                                Topography[i, j] = new Roads
                                {
                                    Coordinate = new Coordinates { X = i, Y = j }
                                };
                                break;
                            //generate rock
                            case ObstacleTypes.Rock:
                                Topography[i, j] = new Rocks
                                {
                                    Coordinate = new Coordinates { X = i, Y = j }
                                };
                                break;
                            //generate hole
                            case ObstacleTypes.Hole:
                                var rdCoorX = rd.Next(0, HorizontalSize);
                                var rdCoorY = rd.Next(0, VerticalSize);
                                Topography[i, j] = new Holes
                                {
                                    Coordinate = new Coordinates { X = i, Y = j },
                                    ConnectedCoordinate = new Coordinates
                                    {
                                        X = rdCoorX,
                                        Y = rdCoorY
                                    }
                                };
                                break;
                            //generate spinner
                            case ObstacleTypes.Spinner:
                                Topography[i, j] = new Spinners
                                {
                                    Coordinate = new Coordinates { X = i, Y = j },
                                    //rotate degree maybe 0/90/180/270, randomly set
                                    RotateDegrees = 90 * (rd.Next(0, 4))
                                };
                                break;
                            //default case
                            default:
                                Topography[i, j] = new Obstacles
                                {
                                    ObstacleType = ObstacleTypes.Unknown,
                                    Coordinate = new Coordinates { X = i, Y = j }
                                };
                                break;
                        }
                    }
                }
            }
        }

    }

}
