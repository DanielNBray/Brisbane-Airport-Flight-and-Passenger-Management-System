using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// General flight UI prompts and menus usable by any role.
    /// Contains shared flight-related prompts for arrivals, departures, viewing flights, etc.
    /// </summary>
    public static class FlightGeneralMenu
    {
        /// <summary>
        /// Prompts the user to select an airline from the available options.
        /// </summary>
        /// <returns>Selected airline index (0-based)</returns>
        public static int PromptAirline()
        {
            ConsoleUI.DisplayString(MenuText.ENTER_AIRLINE);
            ConsoleUI.DisplayString("1. Jetstar");
            ConsoleUI.DisplayString("2. Qantas");
            ConsoleUI.DisplayString("3. Regional Express");
            ConsoleUI.DisplayString("4. Virgin");
            ConsoleUI.DisplayString("5. Fly Pelican");
            int option = ConsoleUI.GetInt(MenuText.ChoicePrompt(5));
            return option - 1; // 0-based
        }

        /// <summary>
        /// Prompts the user to select an arrival city from the available options.
        /// </summary>
        /// <returns>Selected city index (0-based)</returns>
        public static int PromptArrivalCity()
        {
            ConsoleUI.DisplayString(MenuText.ENTER_ARRIVAL_CITY);
            DisplayCityOptions();
            int option = ConsoleUI.GetInt(MenuText.ChoicePrompt(5));
            return option - 1;
        }

        /// <summary>
        /// Prompts the user to select a departing city from the available options.
        /// </summary>
        /// <returns>Selected city index (0-based)</returns>
        public static int PromptDepartingCity()
        {
            ConsoleUI.DisplayString(MenuText.ENTER_DEPARTING_CITY);
            DisplayCityOptions();
            int option = ConsoleUI.GetInt(MenuText.ChoicePrompt(5));
            return option - 1;
        }

        /// <summary>
        /// Displays the available city options to the user.
        /// </summary>
        private static void DisplayCityOptions()
        {
            ConsoleUI.DisplayString("1. Sydney");
            ConsoleUI.DisplayString("2. Melbourne");
            ConsoleUI.DisplayString("3. Rockhampton");
            ConsoleUI.DisplayString("4. Adelaide");
            ConsoleUI.DisplayString("5. Perth");
        }

        /// <summary>
        /// Prompts the user to enter a flight ID within the valid range.
        /// </summary>
        /// <returns>Entered flight ID</returns>
        public static int PromptFlightId()
        {
            ConsoleUI.DisplayString(string.Format(MenuText.ENTER_FLIGHT_ID, ValidationConsts.MIN_FLIGHT_ID, ValidationConsts.MAX_FLIGHT_ID));
            return ConsoleUI.GetInt();
        }

        /// <summary>
        /// Prompts the user to enter a plane ID within the valid range.
        /// </summary>
        /// <returns>Entered plane ID</returns>
        public static int PromptPlaneId()
        {
            ConsoleUI.DisplayString(string.Format(MenuText.ENTER_PLANE_ID, ValidationConsts.MIN_PLANE_ID, ValidationConsts.MAX_PLANE_ID));
            return ConsoleUI.GetInt();
        }

        /// <summary>
        /// Prompts the user to enter an arrival date and time in the specified format.
        /// </summary>
        /// <returns>Parsed arrival DateTime</returns>
        public static DateTime PromptArrivalDateTime()
        {
            ConsoleUI.DisplayString(MenuText.ENTER_ARRIVAL_DATETIME);
            string input = ConsoleUI.GetString();
            DateTime dt = DateTime.ParseExact(input, "HH:mm dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return dt;
        }

        /// <summary>
        /// Prompts the user to enter a departure date and time in the specified format.
        /// </summary>
        /// <returns>Parsed departure DateTime</returns>
        public static DateTime PromptDepartureDateTime()
        {
            ConsoleUI.DisplayString(MenuText.ENTER_DEPARTURE_DATETIME);
            string input = ConsoleUI.GetString();
            DateTime dt = DateTime.ParseExact(input, "HH:mm dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return dt;
        }

        /// <summary>
        /// Displays a confirmation message when a flight has been added to the system.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="planeCode">The plane code</param>
        public static void DisplayFlightAdded(string flightCode, string planeCode)
        {
            ConsoleUI.DisplayString(string.Format(MenuText.FLIGHT_ADDED, flightCode, planeCode));
        }

        /// <summary>
        /// Displays all flights (arrivals and departures) in a formatted list.
        /// </summary>
        /// <param name="arrivals">List of arrival flights</param>
        /// <param name="departures">List of departure flights</param>
        public static void DisplayAllFlights(IReadOnlyList<FlightRecord> arrivals, IReadOnlyList<FlightRecord> departures)
        {
            ConsoleUI.DisplayString();
            ConsoleUI.DisplayString("Arrival Flights:");
            if (arrivals.Count == 0)
            {
                ConsoleUI.DisplayString("There are no arrival flights.");
            }
            else
            {
                List<FlightRecord> sortedArrivals = new List<FlightRecord>(arrivals);
                sortedArrivals.Sort(new FlightWhenComparer());
                foreach (FlightRecord arrivalFlight in sortedArrivals)
                {
                    ConsoleUI.DisplayString($"Flight {arrivalFlight.FlightCode()} operated by {arrivalFlight.AirlineName} arriving at {arrivalFlight.When:HH:mm dd/MM/yyyy} from {arrivalFlight.CityName} on plane {arrivalFlight.PlaneCode()}.");
                }
            }
            ConsoleUI.DisplayString("Departure Flights:");
            if (departures.Count == 0)
            {
                ConsoleUI.DisplayString("There are no departure flights.");
            }
            else
            {
                List<FlightRecord> sortedDepartures = new List<FlightRecord>(departures);
                sortedDepartures.Sort(new FlightWhenComparer());
                foreach (FlightRecord departureFlight in sortedDepartures)
                {
                    ConsoleUI.DisplayString($"Flight {departureFlight.FlightCode()} operated by {departureFlight.AirlineName} departing at {departureFlight.When:HH:mm dd/MM/yyyy} to {departureFlight.CityName} on plane {departureFlight.PlaneCode()}.");
                }
            }
        }

        /// <summary>
        /// Prompts the user to enter delay minutes for a flight.
        /// </summary>
        /// <returns>Delay minutes as integer</returns>
        public static int PromptDelayMinutes()
        {
            int delayMinutes;
            do
            {
                ConsoleUI.DisplayString("Please enter in your minutes delayed:");
                string input = ConsoleUI.GetString();

                if (!int.TryParse(input, out delayMinutes))
                {
                    ConsoleUI.DisplayErrorAgain("Supplied delay is invalid");
                    continue;
                }

                if (delayMinutes < 0)
                {
                    ConsoleUI.DisplayErrorAgain("Delay must be a positive number");
                    continue;
                }

                break; // Valid input received
            } while (true);

            return delayMinutes;
        }
    }
}
