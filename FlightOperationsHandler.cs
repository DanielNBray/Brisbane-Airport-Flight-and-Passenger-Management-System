using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Handles flight operations for Flight Managers.
    /// RESPONSIBILITY: Orchestrating flight creation, delay, and viewing operations.
    /// Contains NO console I/O directly - delegates to AirportMenu and ConsoleUI.
    /// </summary>
    internal sealed class FlightOperationsHandler
    {
        /// <summary>
        /// Creates a new arrival flight by prompting for details and adding to system.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void CreateArrivalFlight()
        {
            int airlineIdx = AirportMenu.PromptAirline();
            string airlineCode, airlineName;
            GetAirlineFromIndex(airlineIdx, out airlineCode, out airlineName);

            int cityIdx = AirportMenu.PromptDepartingCity();
            string cityName = GetCityFromIndex(cityIdx);

            int flightId = AirportMenu.PromptFlightId();
            int planeId = AirportMenu.PromptPlaneId();
            DateTime when = AirportMenu.PromptArrivalDateTime();

            FlightRecord rec = new FlightRecord(airlineCode, airlineName, cityName, flightId, planeId, when, true);
            string planeCode = rec.PlaneCode();

            if (FlightMemory.IsPlaneAlreadyAssigned(planeCode))
            {
                ConsoleUI.DisplayError($"Plane {planeCode} has already been assigned to an arrival flight");
                return;
            }

            FlightMemory.AddArrival(rec);
            AirportMenu.DisplayFlightAdded(rec.FlightCode(), planeCode);
        }

        /// <summary>
        /// Creates a new departure flight by prompting for details and adding to system.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void CreateDepartureFlight()
        {
            int airlineIdx = AirportMenu.PromptAirline();
            string airlineCode, airlineName;
            GetAirlineFromIndex(airlineIdx, out airlineCode, out airlineName);

            int cityIdx = AirportMenu.PromptArrivalCity();
            string cityName = GetCityFromIndex(cityIdx);

            int flightId = AirportMenu.PromptFlightId();
            int planeId = AirportMenu.PromptPlaneId();
            DateTime when = AirportMenu.PromptDepartureDateTime();

            FlightRecord rec = new FlightRecord(airlineCode, airlineName, cityName, flightId, planeId, when, false);
            string planeCode = rec.PlaneCode();

            if (FlightMemory.IsPlaneAlreadyAssigned(planeCode))
            {
                ConsoleUI.DisplayError($"Plane {planeCode} has already been assigned to a departure flight");
                return;
            }

            FlightMemory.AddDeparture(rec);
            AirportMenu.DisplayFlightAdded(rec.FlightCode(), planeCode);
        }

        /// <summary>
        /// Displays all flights (arrivals and departures).
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void ShowAllFlights()
        {
            AirportMenu.DisplayAllFlights(FlightMemory.GetArrivals(), FlightMemory.GetDepartures());
        }

        /// <summary>
        /// Handles delaying an arrival flight.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void DelayArrivalFlight()
        {
            IReadOnlyList<FlightRecord> arrivals = FlightMemory.GetArrivals();
            if (arrivals.Count == 0)
            {
                ConsoleUI.DisplayString("The airport does not have any arrival flights.");
                return;
            }

            int flightIndex = AirportMenu.PromptArrivalFlightSelection(arrivals);
            FlightRecord selectedFlight = arrivals[flightIndex];

            int delayMinutes = AirportMenu.PromptDelayMinutes();
            FlightMemory.DelayFlight(selectedFlight.FlightCode(), delayMinutes);
        }

        /// <summary>
        /// Handles delaying a departure flight.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void DelayDepartureFlight()
        {
            IReadOnlyList<FlightRecord> departures = FlightMemory.GetDepartures();
            if (departures.Count == 0)
            {
                ConsoleUI.DisplayString("The airport does not have any departure flights.");
                return;
            }

            int flightIndex = AirportMenu.PromptDepartureFlightSelection(departures);
            FlightRecord selectedFlight = departures[flightIndex];

            int delayMinutes = AirportMenu.PromptDelayMinutes();
            FlightMemory.DelayFlight(selectedFlight.FlightCode(), delayMinutes);
        }

        /// <summary>
        /// Maps airline index to code and name.
        /// </summary>
        private static void GetAirlineFromIndex(int idx, out string code, out string name)
        {
            switch (idx)
            {
                case 0: code = Airlines.JETSTAR_CODE; name = Airlines.JETSTAR_NAME; return;
                case 1: code = Airlines.QANTAS_CODE; name = Airlines.QANTAS_NAME; return;
                case 2: code = Airlines.REGIONAL_EXPRESS_CODE; name = Airlines.REGIONAL_EXPRESS_NAME; return;
                case 3: code = Airlines.VIRGIN_CODE; name = Airlines.VIRGIN_NAME; return;
                case 4: code = Airlines.FLY_PELICAN_CODE; name = Airlines.FLY_PELICAN_NAME; return;
                default: code = Airlines.JETSTAR_CODE; name = Airlines.JETSTAR_NAME; return;
            }
        }

        /// <summary>
        /// Maps city index to city name.
        /// </summary>
        private static string GetCityFromIndex(int idx)
        {
            switch (idx)
            {
                case 0: return Cities.SYDNEY_NAME;
                case 1: return Cities.MELBOURNE_NAME;
                case 2: return Cities.ROCKHAMPTON_NAME;
                case 3: return Cities.ADELAIDE_NAME;
                case 4: return Cities.PERTH_NAME;
                default: return Cities.SYDNEY_NAME;
            }
        }
    }
}

