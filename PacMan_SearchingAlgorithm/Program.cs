using System;

namespace PacMan_Lab10Assign4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = ArrayHelper.ConsoleHelper.ReadNumberFromConsole("Please enter the size of the maze (Is Hard-coded, so it should be 5): ", 5, 3);

            int[,] maze =
            {
                { -1,               0,  0,   0,     int.MaxValue},
                { -1,               0, -1,  -1,    -1},
                { -1,               0,  0,   0,    -1},
                { -1,              -1, -1,   0,    -1},
                { -1,              -1, -1,   0,    -int.MaxValue}
            };

            ArrayHelper.ConsoleHelper.PrintMaze("The Pac-Man maze looks like: ", maze);
            int[,] wayOutMaze = ArrayHelper.ArrayUtilities.WayOutOfMaze(maze);
            ArrayHelper.ConsoleHelper.PrintMaze("The step matrix looks like: ", wayOutMaze);

            string result = ArrayHelper.ArrayUtilities.PrintWayOutOfMaze("The way out is: ", wayOutMaze);
            Console.WriteLine(result);

        }
    }
}

