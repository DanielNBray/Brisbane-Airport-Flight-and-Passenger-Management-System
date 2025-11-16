using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Handles welcome and main menu operations.
    /// </summary>
    public class WelcomeMenu
    {
        /// <summary>
        /// Displays the welcome banner.
        /// </summary>
        public static void DisplayWelcomeBanner()
        {
            ConsoleUI.DisplayString(MenuText.WELCOME_LINE);
        }

        /// <summary>
        /// Displays the main menu and gets user selection.
        /// </summary>
        /// <param name="options">List of menu options</param>
        /// <returns>Selected option index (0-based)</returns>
        public static int DisplayMainMenu(List<string> options)
        {
            int option = GetOption(MenuText.MAINMENU_PROMPT, options);
            return option;
        }

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message to display</param>
        public static void DisplayMessage(string message)
        {
            ConsoleUI.DisplayString(message);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="errorStr">The error message</param>
        public static void DisplayError(string errorStr)
        {
            ConsoleUI.DisplayError(errorStr);
        }


        /// <summary>
        /// Displays a menu and gets user selection.
        /// </summary>
        /// <param name="title">Menu title to display</param>
        /// <param name="options">List of menu options</param>
        /// <returns>Selected option index (0-based) or -1 if no options</returns>
        private static int GetOption(string title, List<string> options)
        {
            if (options.Count <= 0)
            {
                return -1;
            }

            ConsoleUI.DisplayString(title);
            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Count)));
            for (int optionIndex = 0; optionIndex < options.Count; optionIndex++)
            {
                ConsoleUI.DisplayString($"{(optionIndex + 1).ToString().PadLeft(digitsNeeded)}. {options[optionIndex]}");
            }

            int option = ConsoleUI.GetInt(MenuText.ChoicePrompt(options.Count));
            return option - 1;
        }

    }
}
