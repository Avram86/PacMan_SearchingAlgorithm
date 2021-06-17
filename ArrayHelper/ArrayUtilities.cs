using System;
using System.Text;

namespace ArrayHelper
{
    public static class ArrayUtilities
    {
        /// <summary>
        /// Returns a matrix containing the number of steps to the goal=exit
        /// STEPS TOWARDS EXIT MATRIX
        /// </summary>
        /// <param name="maze">The given NxN matrix</param>
        /// <returns>A NxN matrix containing stepts to goal, where the goal is marked with 0.</returns>
        //public static int[,] WayOutOfMaze(int[,] maze)
        //{
        //    int[,] stepsMatrix = new int[maze.GetLength(0), maze.GetLength(1)];
        //    int step = 1;
        //    MatrixItem exit = FindExit(maze);

        //    for (int i = exit.Row; i >= 0; i--)
        //    {
        //        for (int j = exit.Column; j >= 0; j--)
        //        {
        //            switch (maze[i, j])
        //            {
        //                //the EXIT
        //                case -int.MaxValue:
        //                    stepsMatrix[i, j] = 0;
        //                    break;

        //                //FREE CELLS
        //                case 0:
        //                    {
        //                        if (stepsMatrix[i, j] == 0)
        //                        {
        //                            stepsMatrix[i, j] = step;
        //                        }


        //                        //verificam cu o pozitie(rand) mai sus(pt ca "i"-ul scade in for) in matrice 
        //                        if (i - 1 > 0 && stepsMatrix[i - 1, j] == 0 && maze[i - 1, j] == 0)
        //                        {
        //                            stepsMatrix[i - 1, j] = step + 1;
        //                        }

        //                        //verificam cu o coloana mai la dreapta <- (pt ca "j"-ul scade in for)
        //                        if (j - 1 > 0 && stepsMatrix[i, j - 1] == 0 && maze[i, j - 1] == 0)
        //                        {
        //                            stepsMatrix[i, j - 1] = step + 1;
        //                        }

        //                        step++;
        //                    }
        //                    break;

        //                //WALL
        //                case -1:
        //                default:
        //                    stepsMatrix[i, j] = -1;
        //                    break;
        //            }
        //        }
        //    }
        //    return stepsMatrix;
        //}

        public static int[,] WayOutOfMaze(int[,] maze)
        {
            int[,] stepsMatrix = new int[maze.GetLength(0), maze.GetLength(1)];
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    stepsMatrix[i, j] = -1;
                }
            }

            bool pacManFound = false;
            int currentWeight = 0;
            MatrixItem exit = FindExit(maze);
            MatrixItem pacMan = FindPacMan(maze);
            stepsMatrix[exit.Row, exit.Column] = currentWeight;
            while (!pacManFound)
            {
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        switch (maze[i, j]) /* We're on an free cell (not wall) */
                        {
                            //FREE CELLS
                            case 0:
                            //PAC-MAN CELL
                            case int.MaxValue:
                                {
                                    /*        TOP  
                                    *  LEFT   X   RIGHT
                                    *      BOTTOM      
                                    */
                                    bool isCloseNeighbourTop = (i - 1 >= 0) /* Top neighbour is not outside of the map */
                                                               && stepsMatrix[i - 1, j] == currentWeight /* Top neighbour was previously closer to the goal */
                                                               && stepsMatrix[i, j] == -1; /* Not yet marked cell */
                                    if (isCloseNeighbourTop)
                                    {
                                        stepsMatrix[i, j] = currentWeight + 1;
                                    }

                                    bool isCloseNeighbourLeft = (j - 1 >= 0)
                                                                && stepsMatrix[i, j - 1] == currentWeight
                                                                && stepsMatrix[i, j] == -1;
                                    if (isCloseNeighbourLeft)
                                    {
                                        stepsMatrix[i, j] = currentWeight + 1;
                                    }

                                    bool isCloseNeighbourBottom = (i + 1 < maze.GetLength(0))
                                                                  && stepsMatrix[i + 1, j] == currentWeight
                                                                  && stepsMatrix[i, j] == -1;
                                    if (isCloseNeighbourBottom)
                                    {
                                        stepsMatrix[i, j] = currentWeight + 1;
                                    }

                                    bool isCloseNeighbourRight = (j + 1 < maze.GetLength(1))
                                                                 && stepsMatrix[i, j + 1] == currentWeight
                                                                 && stepsMatrix[i, j] == -1;
                                    if (isCloseNeighbourRight)
                                    {
                                        stepsMatrix[i, j] = currentWeight + 1;
                                    }
                                }
                                break;
                        }
                    }
                }

                currentWeight++;
                if (stepsMatrix[pacMan.Row, pacMan.Column] >= 0)
                {
                    pacManFound = true;
                }

                // Uncomment to see intermediary steps:
                // ConsoleHelper.PrintMaze($"At step {currentWeight} steps matrix looks like: ", stepsMatrix);
            }

            return stepsMatrix;
        }

        public static MatrixItem FindPacMan(int[,] maze)
        {
            MatrixItem pacMan = new MatrixItem();
            for (int row = 0; row < maze.GetLength(0); row++)
            {
                for (int col = 0; col < maze.GetLength(1); col++)
                {
                    if (maze[row, col] == int.MaxValue)
                    {
                        pacMan.Row = row;
                        pacMan.Column = col;
                        // no reason to continue iterating
                        return pacMan;
                    }
                }
            }

            return pacMan;
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
        /// <param name="stepMatrix">The step matrix representing number of steps to goal</param>
        /// <returns></returns>
        public static string PrintWayOutOfMaze(string label, int[,] stepMatrix)
        {
            label = label ?? "The way out is: ";
            StringBuilder wayOut = new StringBuilder();
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

            wayOut.AppendLine($"Maximum no of steps is: {maxSteps}");
            wayOut.Append($"{label}");
            while (maxSteps >= 0)
            {
                for (int row = 0; row < stepMatrix.GetLength(0); row++)
                {
                    for (int col = 0; col < stepMatrix.GetLength(1); col++)
                    {
                        if (stepMatrix[row, col] == maxSteps)
                        {
                            if (maxSteps == 0)
                            {
                                wayOut.Append($"exit: ");
                            }

                            wayOut.Append($"({row},{col})");
                            if (maxSteps > 0)
                            {
                                wayOut.Append(", ");
                            }

                            maxSteps--;
                        }
                    }
                }
            }

            return wayOut.ToString();
        }
    }



}

