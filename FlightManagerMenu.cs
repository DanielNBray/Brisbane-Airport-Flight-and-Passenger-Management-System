using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Flight Manager-specific UI prompts and menus.
    /// Contains manager-only flight management options and flows.
    /// </summary>
    public static class FlightManagerMenu
    {
        /// <summary>
        /// Displays the Flight Manager menu and returns a choice (1..8).
        /// </summary>
        /// <param name="options">Flight Manager menu options</param>
        /// <returns>Selected index (0-based)</returns>
        public static int DisplayFlightManagerMenu(List<string> options)
        {
            ConsoleUI.DisplayString(MenuText.FMANAGER_MENU_HEADING);
            int option = GetOption(MenuText.MAINMENU_PROMPT, options);
            return option;
        }

        /// <summary>
        /// Prompts the user to select an arrival flight from the available options.
        /// </summary>
        /// <param name="arrivals">List of available arrival flights</param>
        /// <returns>Selected flight index (0-based)</returns>
        public static int PromptArrivalFlightSelection(IReadOnlyList<FlightRecord> arrivals)
        {
            ConsoleUI.DisplayString("Please enter the arrival flight:");
            List<string> options = new List<string>();
            foreach (FlightRecord flight in arrivals)
            {
                options.Add($"Flight {flight.FlightCode()} operated by {flight.AirlineName} arriving at {flight.When:HH:mm dd/MM/yyyy} from {flight.CityName} on plane {flight.PlaneCode()}.");
            }
            int option = GetValidatedOption("", options);
            return option;
        }

        /// <summary>
        /// Prompts the user to select a departure flight from the available options.
        /// </summary>
        /// <param name="departures">List of available departure flights</param>
        /// <returns>Selected flight index (0-based)</returns>
        public static int PromptDepartureFlightSelection(IReadOnlyList<FlightRecord> departures)
        {
            ConsoleUI.DisplayString("Please enter the departure flight:");
            List<string> options = new List<string>();
            foreach (FlightRecord flight in departures)
            {
                options.Add($"Flight {flight.FlightCode()} operated by {flight.AirlineName} departing at {flight.When:HH:mm dd/MM/yyyy} to {flight.CityName} on plane {flight.PlaneCode()}.");
            }
            int option = GetValidatedOption("", options);
            return option;
        }

        /// <summary>
        /// Displays a menu, with the options numbered from 1 to options.Length,
        /// the gets a validated integer in the range 1..options.Length. 
        /// Subtracts 1, then returns the result. If the supplied list of options 
        /// is empty, returns an error value (-1).
        /// </summary>
        /// <param name="title">A heading to display before the menu is listed.</param>
        /// <param name="options">The list of objects to be displayed.</param>
        /// <returns>Return value is either -1 (if no options are provided) or a 
        /// value in 0 .. (options.Length-1).</returns>
        private static int GetOption(string title, List<string> options)
        {
            // Defensive error checking - There should be at least 1 option, but we need double check.
            if (options.Count <= 0)
            {
                return -1;
            }

            // Setting up some formatting so the menu looks nice
            ConsoleUI.DisplayString(title);
            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Count)));
            for (int optionIndex = 0; optionIndex < options.Count; optionIndex++)
            {
                ConsoleUI.DisplayString($"{(optionIndex + 1).ToString().PadLeft(digitsNeeded)}. {options[optionIndex]}");
            }

            // Highlighting the importance of diversity. The upper limit will depend of the number of elements passed.
            int option = ConsoleUI.GetInt(MenuText.ChoicePrompt(options.Count));
            // need to subtract 1 to align because programers count from zero 
            return option - 1;
        }

        /// <summary>
        /// Gets a validated option from the user with proper error handling and retry loops.
        /// </summary>
        /// <param name="title">Title to display</param>
        /// <param name="options">List of options to choose from</param>
        /// <returns>Selected index (0-based)</returns>
        private static int GetValidatedOption(string title, List<string> options)
        {
            if (options.Count <= 0)
            {
                return -1;
            }

            // Display the options
            ConsoleUI.DisplayString(title);
            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Count)));
            for (int optionIndex = 0; optionIndex < options.Count; optionIndex++)
            {
                ConsoleUI.DisplayString($"{(optionIndex + 1).ToString().PadLeft(digitsNeeded)}. {options[optionIndex]}");
            }

            // Get validated input with retry loop
            int option;
            do
            {
                ConsoleUI.DisplayString(MenuText.ChoicePrompt(options.Count));
                string input = ConsoleUI.GetString();

                if (!int.TryParse(input, out option))
                {
                    ConsoleUI.DisplayErrorAgain("Supplied value is out of range");
                    continue;
                }

                if (option < 1 || option > options.Count)
                {
                    ConsoleUI.DisplayErrorAgain("Supplied value is out of range");
                    continue;
                }

                break; // Valid input received
            } while (true);

            return option - 1; // Convert to 0-based index
        }
    }
}
