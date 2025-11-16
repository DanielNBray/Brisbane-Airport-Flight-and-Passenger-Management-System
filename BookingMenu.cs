using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Handles booking-related menu operations and user input collection.
    /// </summary>
    public class BookingMenu
    {
        /// <summary>
        /// Prompts user to select an arrival flight from available options.
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
        /// Prompts user to select a departure flight from available options.
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
        /// Prompts user for seat row with validation.
        /// </summary>
        /// <returns>Valid seat row</returns>
        public static int PromptSeatRow()
        {
            int seatRow;
            do
            {
                ConsoleUI.DisplayString(string.Format(MenuText.ENTER_SEAT_ROW, ValidationConsts.MIN_SEAT_ROW, ValidationConsts.MAX_SEAT_ROW));
                string input = ConsoleUI.GetString();

                if (!int.TryParse(input, out seatRow))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_SEAT_ROW);
                    continue;
                }

                if (!InputValidator.IsValidSeatRow(seatRow.ToString()))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_SEAT_ROW);
                    continue;
                }

                break;
            } while (true);

            return seatRow;
        }

        /// <summary>
        /// Prompts user for seat column with validation.
        /// </summary>
        /// <returns>Valid seat column</returns>
        public static string PromptSeatColumn()
        {
            string seatColumn;
            do
            {
                ConsoleUI.DisplayString(string.Format(MenuText.ENTER_SEAT_COLUMN, ValidationConsts.MIN_SEAT_COLUMN, ValidationConsts.MAX_SEAT_COLUMN));
                seatColumn = ConsoleUI.GetString();

                if (!InputValidator.IsValidSeatColumn(seatColumn))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_SEAT_COLUMN);
                    continue;
                }

                break;
            } while (true);

            return seatColumn;
        }

        /// <summary>
        /// Displays booking success message.
        /// </summary>
        /// <param name="flightCode">Flight code</param>
        /// <param name="cityName">City name</param>
        /// <param name="when">Flight time</param>
        /// <param name="seat">Seat assignment</param>
        /// <param name="isArrival">True if arrival flight, false for departure</param>
        public static void DisplayBookingSuccess(string flightCode, string cityName, DateTime when, string seat, bool isArrival)
        {
            string direction = isArrival ? "from" : "to";
            string action = isArrival ? "arriving at" : "departing at";
            ConsoleUI.DisplayString($"Congratulations. You have booked flight {flightCode} {direction} {cityName} {action} {when:HH:mm dd/MM/yyyy} and are seated in {seat}.");
        }

        /// <summary>
        /// Displays user flight details.
        /// </summary>
        /// <param name="user">The user whose details to display</param>
        public static void DisplayUserFlightDetails(User user)
        {
            ConsoleUI.DisplayString($"Showing flight details for {user.Name}:");
            if (user.HasArrivalFlight())
            {
                // Get the current flight information from flight memory to show updated times
                var arrivalFlightCode = user.GetArrivalFlightCode();
                if (arrivalFlightCode != null)
                {
                    FlightRecord? currentFlight = FlightMemory.GetFlightByCode(arrivalFlightCode);
                    if (currentFlight != null)
                    {
                        ConsoleUI.DisplayString($"Arrival Flight: Flight {currentFlight.FlightCode()} from {currentFlight.CityName} arriving at {currentFlight.When:HH:mm dd/MM/yyyy} in seat {user.GetArrivalSeat()}.");
                    }
                    else
                    {
                        // Fallback to original flight if not found in memory
                        ConsoleUI.DisplayString($"Arrival Flight: Flight {arrivalFlightCode} in seat {user.GetArrivalSeat()}.");
                    }
                }
            }
            if (user.HasDepartureFlight())
            {
                // Get the current flight information from flight memory to show updated times
                var departureFlightCode = user.GetDepartureFlightCode();
                if (departureFlightCode != null)
                {
                    FlightRecord? currentFlight = FlightMemory.GetFlightByCode(departureFlightCode);
                    if (currentFlight != null)
                    {
                        ConsoleUI.DisplayString($"Departure Flight: Flight {currentFlight.FlightCode()} to {currentFlight.CityName} departing at {currentFlight.When:HH:mm dd/MM/yyyy} in seat {user.GetDepartureSeat()}.");
                    }
                    else
                    {
                        // Fallback to original flight if not found in memory
                        ConsoleUI.DisplayString($"Departure Flight: Flight {departureFlightCode} in seat {user.GetDepartureSeat()}.");
                    }
                }
            }
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">Error message to display</param>
        public static void DisplayError(string message)
        {
            ConsoleUI.DisplayError(message);
        }

        /// <summary>
        /// Displays an error message with retry prompt.
        /// </summary>
        /// <param name="message">Error message to display</param>
        public static void DisplayErrorAgain(string message)
        {
            ConsoleUI.DisplayErrorAgain(message);
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
