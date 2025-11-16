using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Prints Menus and Text only
    /// </summary>
    public class AirportMenu
    {

        /// <summary>
        /// Shows the Login menu heading.
        /// </summary>
        public static void DisplayLoginHeading()
        {
            ConsoleUI.DisplayString(MenuText.LOGIN_HEADING);
        }

        /// <summary>
        /// Displays the Traveller menu and returns a choice (1..6).
        /// </summary>
        /// <param name="options">Traveller menu options</param>
        /// <returns>Selected index (0-based)</returns>
        public static int DisplayTravellerMenu(List<string> options)
        {
            ConsoleUI.DisplayString(MenuText.TRAVELLER_MENU_HEADING);
            int option = GetOption(MenuText.MAINMENU_PROMPT, options);
            return option;
        }

        /// <summary>
        /// Displays the Frequent Flyer menu and returns a choice (1..7).
        /// </summary>
        /// <param name="options">Frequent flyer menu options</param>
        /// <returns>Selected index (0-based)</returns>
        public static int DisplayFrequentFlyerMenu(List<string> options)
        {
            ConsoleUI.DisplayString(MenuText.FF_MENU_HEADING);
            int option = GetOption(MenuText.MAINMENU_PROMPT, options);
            return option;
        }

        /// <summary>
        /// Displays the Flight Manager menu and returns a choice (1..8).
        /// </summary>
        /// <param name="options">Flight Manager menu options</param>
        /// <returns>Selected index (0-based)</returns>
        public static int DisplayFlightManagerMenu(List<string> options)
        {
            return FlightManagerMenu.DisplayFlightManagerMenu(options);
        }

        /// <summary>
        /// Prompts user to select an airline.
        /// </summary>
        /// <returns>Selected airline index</returns>
        public static int PromptAirline()
        {
            return FlightGeneralMenu.PromptAirline();
        }

        /// <summary>
        /// Prompts user to select an arrival city.
        /// </summary>
        /// <returns>Selected arrival city index</returns>
        public static int PromptArrivalCity()
        {
            return FlightGeneralMenu.PromptArrivalCity();
        }

        /// <summary>
        /// Prompts user to select a departing city.
        /// </summary>
        /// <returns>Selected departing city index</returns>
        public static int PromptDepartingCity()
        {
            return FlightGeneralMenu.PromptDepartingCity();
        }

        /// <summary>
        /// Prompts user to enter a flight ID.
        /// </summary>
        /// <returns>Selected flight ID</returns>
        public static int PromptFlightId()
        {
            return FlightGeneralMenu.PromptFlightId();
        }

        /// <summary>
        /// Prompts user to enter a plane ID.
        /// </summary>
        /// <returns>Selected plane ID</returns>
        public static int PromptPlaneId()
        {
            return FlightGeneralMenu.PromptPlaneId();
        }

        /// <summary>
        /// Prompts user to enter arrival date and time.
        /// </summary>
        /// <returns>Arrival date and time</returns>
        public static DateTime PromptArrivalDateTime()
        {
            return FlightGeneralMenu.PromptArrivalDateTime();
        }

        /// <summary>
        /// Prompts user to enter departure date and time.
        /// </summary>
        /// <returns>Departure date and time</returns>
        public static DateTime PromptDepartureDateTime()
        {
            return FlightGeneralMenu.PromptDepartureDateTime();
        }

        /// <summary>
        /// Displays flight added confirmation message.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="planeCode">The plane code</param>
        public static void DisplayFlightAdded(string flightCode, string planeCode)
        {
            FlightGeneralMenu.DisplayFlightAdded(flightCode, planeCode);
        }

        /// <summary>
        /// Displays all available flights.
        /// </summary>
        /// <param name="arrivals">List of arrival flights</param>
        /// <param name="departures">List of departure flights</param>
        public static void DisplayAllFlights(IReadOnlyList<FlightRecord> arrivals, IReadOnlyList<FlightRecord> departures)
        {
            FlightGeneralMenu.DisplayAllFlights(arrivals, departures);
        }

        /// <summary>
        /// Prompts user to select an arrival flight.
        /// </summary>
        /// <param name="arrivals">List of available arrival flights</param>
        /// <returns>Selected arrival flight index</returns>
        public static int PromptArrivalFlightSelection(IReadOnlyList<FlightRecord> arrivals)
        {
            return FlightManagerMenu.PromptArrivalFlightSelection(arrivals);
        }

        /// <summary>
        /// Prompts user to select a departure flight.
        /// </summary>
        /// <param name="departures">List of available departure flights</param>
        /// <returns>Selected departure flight index</returns>
        public static int PromptDepartureFlightSelection(IReadOnlyList<FlightRecord> departures)
        {
            return FlightManagerMenu.PromptDepartureFlightSelection(departures);
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
        public static int GetOption(string title, List<string> options)
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
        public static int GetValidatedOption(string title, List<string> options)
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

        /// <summary>
        /// Displays a string to the UI.
        /// </summary>
        /// <param name="str">The string to display.</param>
        public static void DisplayMessage(string str)
        {
            ConsoleUI.DisplayString(str);
        }

        /// <summary>
        /// Displays an error to the UI.
        /// </summary>
        /// <param name="errorStr">The string.</param>
        public static void DisplayError(string errorStr)
        {
            ConsoleUI.DisplayError(errorStr);
        }

        /// <summary>
        /// Displays just the frequent flyer points.
        /// </summary>
        /// <param name="ffPoints">Points value.</param>
        public static void DisplayFrequentFlyerPoints(int ffPoints)
        {
            ConsoleUI.DisplayString($"Frequent flyer points: {ffPoints.ToString("N0")}");
        }

        /// <summary>
        /// Displays detailed frequent flyer points breakdown including flight points.
        /// </summary>
        /// <param name="frequentFlyer">The frequent flyer to display points for</param>
        public static void DisplayDetailedFrequentFlyerPoints(FrequentFlyer frequentFlyer)
        {
            var (currentPoints, arrivalPoints, departurePoints) = frequentFlyer.GetPointsBreakdown();
            
            ConsoleUI.DisplayString($"Your current points are: {currentPoints.ToString("N0")}.");

            if (arrivalPoints > 0)
            {
                ConsoleUI.DisplayString($"Your points from your arrival flight will be : {arrivalPoints.ToString("N0")}.");
            }

            if (departurePoints > 0)
            {
                ConsoleUI.DisplayString($"Your points from your departure flight will be: {departurePoints.ToString("N0")}.");
            }

            if (arrivalPoints > 0 || departurePoints > 0)
            {
                int newTotal = currentPoints + arrivalPoints + departurePoints;
                string flightText = (arrivalPoints > 0 && departurePoints > 0) ? "flights" : "flight";
                ConsoleUI.DisplayString($"After completing your {flightText} your new points will be: {newTotal.ToString("N0")}.");
            }
        }

        /// <summary>
        /// Prompts the user to enter their current password.
        /// </summary>
        /// <returns>The current password entered by the user</returns>
        public static string PromptCurrentPassword()
        {
            ConsoleUI.DisplayString("Please enter your current password.");
            return ConsoleUI.GetString();
        }

        /// <summary>
        /// Prompts the user to enter a new password with validation.
        /// </summary>
        /// <returns>The validated new password</returns>
        public static string PromptNewPassword()
        {
            string new_password;
            string actual_password;
            do
            {
                ConsoleUI.DisplayString("Please enter your new password.");
                new_password = ConsoleUI.GetString();

                if (!InputValidator.IsValidPassword(new_password))
                {
                    ConsoleUI.DisplayErrorAgain("Supplied password is invalid");
                }
            } while (!InputValidator.IsValidPassword(new_password));
            actual_password = new_password;
            return actual_password;
        }

        /// <summary>
        /// Prompts the user to enter delay minutes for a flight.
        /// </summary>
        /// <returns>Delay minutes as integer</returns>
        public static int PromptDelayMinutes()
        {
            return FlightGeneralMenu.PromptDelayMinutes();
        }
    }
}