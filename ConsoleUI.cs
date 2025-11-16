using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Handles console input and output operations.
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// Displays a blank line to the console.
        /// </summary>
        public static void DisplayString()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a message to the console.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayString(string msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Displays an object's string representation to the console.
        /// </summary>
        /// <param name="obj">The object to display</param>
        public static void DisplayString(object obj)
        {
            Console.WriteLine(obj.ToString());
        }


        /// <summary>
        /// Displays an error message to the console.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayError(string msg)
        {
            Console.WriteLine(string.Format(MenuText.ERROR_PREFIX, msg));
        }

        /// <summary>
        /// Displays an error message to the console and asks the user to try again.
        /// </summary>
        /// <param name="msg">The message to display</param>
        public static void DisplayErrorAgain(string msg)
        {
            Console.WriteLine(string.Format(MenuText.ERROR_TRY_AGAIN, msg));
        }

        /// <summary>
        /// Reads a string from console input.
        /// </summary>
        /// <returns>The user's input as a string</returns>
        public static string GetString()
        {
            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Reads an integer from console input.
        /// </summary>
        /// <returns>The user's input as an integer</returns>
        public static int GetInt()
        {
            string input = Console.ReadLine();
            int i_input = int.Parse(input);
            return i_input;
        }

        /// <summary>
        /// Displays a message and reads an integer from console input.
        /// </summary>
        /// <param name="msg">The message to display</param>
        /// <returns>The user's input as an integer</returns>      
        public static int GetInt(string msg)
        {
            Console.WriteLine($"{msg}");
            string input = Console.ReadLine();
            int i_input = int.Parse(input);
            return i_input;
        }

        /// <summary>
        /// Reads a double from console input.
        /// </summary>
        /// <returns>The user's input as a double</returns>
        public static double GetDouble()
        {
            string input = Console.ReadLine();
            double d_input = Double.Parse(input);
            return d_input;
        }

        /// <summary>
        /// Reads a boolean from console input.
        /// </summary>
        /// <returns>The user's input as a boolean</returns>
        public static bool GetBool()
        {
            string input = Console.ReadLine();
            bool b_input = Boolean.Parse(input);
            return b_input;
        }

    }
}
