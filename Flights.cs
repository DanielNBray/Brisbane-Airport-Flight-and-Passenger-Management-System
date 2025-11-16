using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Compares flight records by their scheduled time for sorting.
    /// </summary>
    public sealed class FlightWhenComparer : System.Collections.Generic.IComparer<FlightRecord>
    {
        public int Compare(FlightRecord firstFlight, FlightRecord secondFlight)
        {
            if (ReferenceEquals(firstFlight, secondFlight))
            {
                return 0;
            }
            if (firstFlight == null)
            {
                return -1;
            }
            if (secondFlight == null)
            {
                return 1;
            }
            return firstFlight.When.CompareTo(secondFlight.When);
        }
    }

    /// <summary>
    /// Represents a single flight record with all its details.
    /// </summary>
    public sealed class FlightRecord
    {
        public string AirlineCode { get; }
        public string AirlineName { get; }
        public string CityName { get; }
        public int FlightId { get; }
        public int PlaneId { get; }
        public DateTime When { get; }
        public bool IsArrival { get; }

        /// <summary>
        /// Creates a new flight record with all required details.
        /// </summary>
        public FlightRecord(string airlineCode, string airlineName, string cityName, int flightId, int planeId, DateTime when, bool isArrival)
        {
            AirlineCode = airlineCode;
            AirlineName = airlineName;
            CityName = cityName;
            FlightId = flightId;
            PlaneId = planeId;
            When = when;
            IsArrival = isArrival;
        }

        /// <summary>
        /// Gets the plane code in format: {AirlineCode}{PlaneId}{A|D}
        /// </summary>
        public string PlaneCode()
        {
            char planeTypeSuffix = IsArrival ? 'A' : 'D';
            return $"{AirlineCode}{PlaneId}{planeTypeSuffix}";
        }

        /// <summary>
        /// Gets the flight code in format: {AirlineCode}{FlightId}
        /// </summary>
        public string FlightCode()
        {
            return $"{AirlineCode}{FlightId}";
        }
    }

    /// <summary>
    /// Simple in-memory storage for flight records.
    /// Keeps arrivals and departures in separate lists.
    /// </summary>
    public static class FlightMemory
    {
        private static readonly List<FlightRecord> arrivalFlights = new List<FlightRecord>();
        private static readonly List<FlightRecord> departureFlights = new List<FlightRecord>();

        // Seat management - tracks occupied seats per flight
        private static readonly Dictionary<string, HashSet<string>> occupiedSeats = new Dictionary<string, HashSet<string>>();

        /// <summary>
        /// Checks if a plane is already assigned to any flight.
        /// </summary>
        public static bool IsPlaneAlreadyAssigned(string planeCode)
        {
            foreach (FlightRecord arrival in arrivalFlights)
            {
                if (arrival.PlaneCode() == planeCode)
                {
                    return true;
                }
            }
            foreach (FlightRecord departure in departureFlights)
            {
                if (departure.PlaneCode() == planeCode)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a new arrival flight to the system.
        /// </summary>
        public static void AddArrival(FlightRecord flight)
        {
            arrivalFlights.Add(flight);
        }

        /// <summary>
        /// Adds a new departure flight to the system.
        /// </summary>
        public static void AddDeparture(FlightRecord flight)
        {
            departureFlights.Add(flight);
        }

        /// <summary>
        /// Gets all arrival flights.
        /// </summary>
        public static IReadOnlyList<FlightRecord> GetArrivals()
        {
            return arrivalFlights;
        }

        /// <summary>
        /// Gets all departure flights.
        /// </summary>
        public static IReadOnlyList<FlightRecord> GetDepartures()
        {
            return departureFlights;
        }

        /// <summary>
        /// Gets a flight by its flight code from either arrivals or departures.
        /// </summary>
        /// <param name="flightCode">The flight code to search for</param>
        /// <returns>The flight record if found, null otherwise</returns>
        public static FlightRecord? GetFlightByCode(string flightCode)
        {
            // Search in arrivals first
            foreach (FlightRecord flight in arrivalFlights)
            {
                if (flight.FlightCode() == flightCode)
                {
                    return flight;
                }
            }

            // Search in departures
            foreach (FlightRecord flight in departureFlights)
            {
                if (flight.FlightCode() == flightCode)
                {
                    return flight;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if a seat is occupied on a specific flight.
        /// </summary>
        /// <param name="flightCode">The flight code to check</param>
        /// <param name="seat">The seat to check (format: "row:column")</param>
        /// <returns>True if seat is occupied, false otherwise</returns>
        public static bool IsSeatOccupied(string flightCode, string seat)
        {
            if (!occupiedSeats.ContainsKey(flightCode))
            {
                return false;
            }
            return occupiedSeats[flightCode].Contains(seat);
        }

        /// <summary>
        /// Occupies a seat on a specific flight.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="seat">The seat to occupy (format: "row:column")</param>
        public static void OccupySeat(string flightCode, string seat)
        {
            if (!occupiedSeats.ContainsKey(flightCode))
            {
                occupiedSeats[flightCode] = new HashSet<string>();
            }
            occupiedSeats[flightCode].Add(seat);
        }

        /// <summary>
        /// Frees a seat on a specific flight.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="seat">The seat to free (format: "row:column")</param>
        public static void FreeSeat(string flightCode, string seat)
        {
            if (occupiedSeats.ContainsKey(flightCode))
            {
                occupiedSeats[flightCode].Remove(seat);
            }
        }

        /// <summary>
        /// Gets the next available seat on a flight, starting from the requested seat.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="requestedSeat">The originally requested seat</param>
        /// <returns>The next available seat, or null if none available</returns>
        public static string? GetNextAvailableSeat(string flightCode, string requestedSeat)
        {
            if (!occupiedSeats.ContainsKey(flightCode))
            {
                return requestedSeat;
            }

            // Parse the requested seat
            if (!TryParseSeat(requestedSeat, out int row, out char column))
            {
                return null;
            }

            // Try the next column in the same row first
            if (column < ValidationConsts.MAX_SEAT_COLUMN)
            {
                char nextCol = (char)(column + 1);
                string seat = $"{row}:{nextCol}";
                if (!occupiedSeats[flightCode].Contains(seat))
                {
                    return seat;
                }
            }

            // Try other rows starting from the next row, first column
            for (int rowIndex = row + 1; rowIndex <= ValidationConsts.MAX_SEAT_ROW; rowIndex++)
            {
                string seat = $"{rowIndex}:{ValidationConsts.MIN_SEAT_COLUMN}";
                if (!occupiedSeats[flightCode].Contains(seat))
                {
                    return seat;
                }
            }

            // Try remaining rows from 1 to the current row, first column
            for (int rowIndex = ValidationConsts.MIN_SEAT_ROW; rowIndex < row; rowIndex++)
            {
                string seat = $"{rowIndex}:{ValidationConsts.MIN_SEAT_COLUMN}";
                if (!occupiedSeats[flightCode].Contains(seat))
                {
                    return seat;
                }
            }

            // If no seats available in first column, try all remaining seats
            for (int rowIndex = ValidationConsts.MIN_SEAT_ROW; rowIndex <= ValidationConsts.MAX_SEAT_ROW; rowIndex++)
            {
                for (char columnIndex = ValidationConsts.MIN_SEAT_COLUMN; columnIndex <= ValidationConsts.MAX_SEAT_COLUMN; columnIndex++)
                {
                    string seat = $"{rowIndex}:{columnIndex}";
                    if (!occupiedSeats[flightCode].Contains(seat))
                    {
                        return seat;
                    }
                }
            }

            return null; // No seats available
        }

        /// <summary>
        /// Parses a seat string into row and column components.
        /// </summary>
        /// <param name="seat">Seat string in format "row:column"</param>
        /// <param name="row">Output row number</param>
        /// <param name="column">Output column character</param>
        /// <returns>True if parsing successful, false otherwise</returns>
        private static bool TryParseSeat(string seat, out int row, out char column)
        {
            row = 0;
            column = 'A';

            if (string.IsNullOrEmpty(seat) || !seat.Contains(':'))
            {
                return false;
            }

            string[] parts = seat.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            if (!int.TryParse(parts[0], out row))
            {
                return false;
            }

            if (parts[1].Length != 1)
            {
                return false;
            }

            column = parts[1][0];
            return true;
        }

        /// <summary>
        /// Delays a flight by the specified number of minutes.
        /// Also automatically delays related flights using the same aircraft.
        /// </summary>
        /// <param name="flightCode">The flight code to delay</param>
        /// <param name="delayMinutes">Number of minutes to delay</param>
        public static void DelayFlight(string flightCode, int delayMinutes)
        {
            // Find and delay the flight in arrivals
            foreach (FlightRecord flight in arrivalFlights)
            {
                if (flight.FlightCode() == flightCode)
                {
                    // Create a new flight record with delayed time
                    FlightRecord delayedFlight = new FlightRecord(
                        flight.AirlineCode,
                        flight.AirlineName,
                        flight.CityName,
                        flight.FlightId,
                        flight.PlaneId,
                        flight.When.AddMinutes(delayMinutes),
                        flight.IsArrival
                    );

                    // Replace the old flight with the delayed one
                    int index = arrivalFlights.IndexOf(flight);
                    arrivalFlights[index] = delayedFlight;

                    // Auto-delay related departure flights using the same aircraft
                    DelayRelatedFlights(flight.AirlineCode, flight.PlaneId, delayMinutes, false);
                    return;
                }
            }

            // Find and delay the flight in departures
            foreach (FlightRecord flight in departureFlights)
            {
                if (flight.FlightCode() == flightCode)
                {
                    // Create a new flight record with delayed time
                    FlightRecord delayedFlight = new FlightRecord(
                        flight.AirlineCode,
                        flight.AirlineName,
                        flight.CityName,
                        flight.FlightId,
                        flight.PlaneId,
                        flight.When.AddMinutes(delayMinutes),
                        flight.IsArrival
                    );

                    // Replace the old flight with the delayed one
                    int index = departureFlights.IndexOf(flight);
                    departureFlights[index] = delayedFlight;

                    // Auto-delay related arrival flights using the same aircraft
                    DelayRelatedFlights(flight.AirlineCode, flight.PlaneId, delayMinutes, true);
                    return;
                }
            }
        }

        /// <summary>
        /// Delays related flights using the same aircraft (same airline code and plane ID).
        /// </summary>
        /// <param name="airlineCode">The airline code</param>
        /// <param name="planeId">The plane ID</param>
        /// <param name="delayMinutes">Number of minutes to delay</param>
        /// <param name="delayArrivals">True to delay arrivals, false to delay departures</param>
        private static void DelayRelatedFlights(string airlineCode, int planeId, int delayMinutes, bool delayArrivals)
        {
            if (delayArrivals)
            {
                // Find and delay related arrival flights using the same aircraft
                for (int flightIndex = 0; flightIndex < arrivalFlights.Count; flightIndex++)
                {
                    FlightRecord flight = arrivalFlights[flightIndex];
                    if (flight.AirlineCode == airlineCode && flight.PlaneId == planeId)
                    {
                        FlightRecord delayedFlight = new FlightRecord(
                            flight.AirlineCode,
                            flight.AirlineName,
                            flight.CityName,
                            flight.FlightId,
                            flight.PlaneId,
                            flight.When.AddMinutes(delayMinutes),
                            flight.IsArrival
                        );
                        arrivalFlights[flightIndex] = delayedFlight;
                    }
                }
            }
            else
            {
                // Find and delay related departure flights using the same aircraft
                for (int flightIndex = 0; flightIndex < departureFlights.Count; flightIndex++)
                {
                    FlightRecord flight = departureFlights[flightIndex];
                    if (flight.AirlineCode == airlineCode && flight.PlaneId == planeId)
                    {
                        FlightRecord delayedFlight = new FlightRecord(
                            flight.AirlineCode,
                            flight.AirlineName,
                            flight.CityName,
                            flight.FlightId,
                            flight.PlaneId,
                            flight.When.AddMinutes(delayMinutes),
                            flight.IsArrival
                        );
                        departureFlights[flightIndex] = delayedFlight;
                    }
                }
            }
        }
    }
}


