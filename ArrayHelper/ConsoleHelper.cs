using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayHelper
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Returns a number from the console
        /// </summary>
        /// <param name="label"></param>
        /// <param name="maxAttempts">Maximum no of attempts to enter a number</param>
        /// <param name="defaultValue">Default value if no number was entered</param>
        /// <returns></returns>
        public static int ReadNumberFromConsole(string label, int maxAttempts, int defaultValue)
        {
            label = label ?? "Please write a number";

            int attempts = 0;
            while (attempts < maxAttempts)
            {
                Console.WriteLine(label);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int nr))
                {
                    return nr;
                }

                attempts++;
                Console.WriteLine($"The value is not a number! You have {maxAttempts - attempts} attempts left!");
            }

            return defaultValue;
        }

        /// <summary>
        /// Prints a nxn matrix, given the array
        /// </summary>
        /// <param name="label"></param>
        /// <param name="array">matrix to be printed</param>
        public static void PrintMaze(string label, int[,] array)
        {
            label = label ?? "The maze looks like: ";

            StringBuilder result = new StringBuilder();


            for (int i = 0; i < array.GetLength(0); i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    line.Append($"{array[i, j],10}");
                }

                result.AppendLine(line.ToString());
            }

            Console.WriteLine($"{label}");
            Console.WriteLine(result);
        }
    }
}

