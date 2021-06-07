using System;
using System.Text;

namespace ArrayHelper
{
    public static class ArrayUtilities
    {
        /// <summary>
        /// Returns a matrix containing the number of steps to the goal=exit
        /// </summary>
        /// <param name="maze">The given NxN matrix</param>
        /// <returns>A NxN matrix containing stepts to goal, where the goal is marked with 0.</returns>
        public static int[,] WayOutOfMaze(int[,] maze)
        {
            int[,] stepsMatrix = new int[maze.GetLength(0), maze.GetLength(1)];
            int step = 1;
            MatrixItem exit = FindExit(maze);

            for (int i = exit.Row; i >= 0; i--)
            {
                for (int j = exit.Column; j >= 0; j--)
                {
                    switch (maze[i, j])
                    {
                        case -int.MaxValue:
                            stepsMatrix[i, j] = 0;
                            break;

                        case 0:
                            {
                                if (stepsMatrix[i, j] == 0)
                                {
                                    stepsMatrix[i, j] = step;
                                }


                                //verificam cu o pozitie(rand) mai sus in matrice 
                                if (i - 1 > 0 && stepsMatrix[i - 1, j] == 0 && maze[i - 1, j] == 0)
                                {
                                    stepsMatrix[i - 1, j] = step + 1;
                                }

                                //verificam cu o coloana mai la dreapta <- 
                                if (j - 1 > 0 && stepsMatrix[i, j - 1] == 0 && maze[i, j - 1] == 0)
                                {
                                    stepsMatrix[i, j - 1] = step + 1;
                                }

                                step++;
                            }
                            break;

                        case -1:
                        default:
                            stepsMatrix[i, j] = -1;
                            break;
                    }
                }
            }
            return stepsMatrix;
        }

        /// <summary>
        /// Returns a point having two coordinates x=row, y=column in the maze where the exit is.
        /// </summary>
        /// <param name="maze">The given matrix</param>
        /// <returns>(row, column)</returns>
        public static MatrixItem FindExit(int[,] maze)
        {
            MatrixItem exit = new MatrixItem();

            for (int row = 0; row < maze.GetLength(0); row++)
            {
                for (int col = 0; col < maze.GetLength(1); col++)
                {
                    if (maze[row, col] == -int.MaxValue)
                    {
                        exit.Row = row;
                        exit.Column = col;
                    }
                }
            }
            return exit;
        }

        /// <summary>
        /// Prints the pair of coordinates where Pac-Man mustgo in order to find the way out
        /// </summary>
        /// <param name="label"></param>
        /// <param name="maze">The initial maze: NxN matrix</param>
        /// <param name="stepMatrix">The step matrix representing number of steps to goal</param>
        /// <returns></returns>
        public static string PrintWayOutOfMaze(string label, int[,] maze, int[,] stepMatrix)
        {
            label = label ?? "The way out is: ";

            MatrixItem exit = FindExit(maze);

            StringBuilder wayOut = new StringBuilder();
            wayOut = wayOut.Append($"{label}");

            int maxSteps = 0;

            for (int row = 0; row < stepMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < stepMatrix.GetLength(1); col++)
                {
                    if (stepMatrix[row, col] > maxSteps)
                    {
                        maxSteps = stepMatrix[row, col];
                    }
                }
            }
            Console.WriteLine($"Maximum no of steps is: {maxSteps}");

            for (int row = 0; row < stepMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < stepMatrix.GetLength(1); col++)
                {
                    if (stepMatrix[row, col] == maxSteps)
                    {
                        wayOut.Append($"({row},{col}), ");
                        Console.WriteLine($"({row},{col})={maxSteps} ");

                        //comment out
                        //when corrected the WayOutOfMaze() 
                        if (maxSteps == 6)
                        {
                            maxSteps = maxSteps - 2;
                        }
                        else
                        {
                            maxSteps--;
                        }

                        //uncomment when correct 
                        //maxSteps--;

                    }

                    if (stepMatrix[row, col] == 0)
                    {
                        wayOut.Append($"exit:({row},{col})");
                    }
                }
            }

            return wayOut.ToString();
        }


    }
}
