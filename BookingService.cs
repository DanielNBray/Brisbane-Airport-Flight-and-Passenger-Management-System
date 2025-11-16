using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Interface for flight booking operations.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Books an arrival flight for a user.
        /// </summary>
        /// <param name="user">The user booking the flight</param>
        /// <param name="flightIndex">Index of the selected flight</param>
        /// <param name="seatRow">The seat row</param>
        /// <param name="seatColumn">The seat column</param>
        /// <returns>True if booking was successful, false otherwise</returns>
        bool BookArrivalFlight(User user, int flightIndex, int seatRow, string seatColumn);

        /// <summary>
        /// Books a departure flight for a user.
        /// </summary>
        /// <param name="user">The user booking the flight</param>
        /// <param name="flightIndex">Index of the selected flight</param>
        /// <param name="seatRow">The seat row</param>
        /// <param name="seatColumn">The seat column</param>
        /// <returns>True if booking was successful, false otherwise</returns>
        bool BookDepartureFlight(User user, int flightIndex, int seatRow, string seatColumn);

        /// <summary>
        /// Gets available arrival flights.
        /// </summary>
        /// <returns>List of available arrival flights</returns>
        IReadOnlyList<FlightRecord> GetAvailableArrivalFlights();

        /// <summary>
        /// Gets available departure flights.
        /// </summary>
        /// <returns>List of available departure flights</returns>
        IReadOnlyList<FlightRecord> GetAvailableDepartureFlights();

        /// <summary>
        /// Validates if a user can book an arrival flight.
        /// </summary>
        /// <param name="user">The user attempting to book</param>
        /// <returns>True if user can book, false otherwise</returns>
        bool CanBookArrivalFlight(User user);

        /// <summary>
        /// Validates if a user can book a departure flight.
        /// </summary>
        /// <param name="user">The user attempting to book</param>
        /// <returns>True if user can book, false otherwise</returns>
        bool CanBookDepartureFlight(User user);

        /// <summary>
        /// Validates flight timing constraints.
        /// </summary>
        /// <param name="user">The user booking the flight</param>
        /// <param name="flight">The flight to book</param>
        /// <param name="isArrival">True if this is an arrival flight, false for departure</param>
        /// <returns>True if timing is valid, false otherwise</returns>
        bool ValidateFlightTiming(User user, FlightRecord flight, bool isArrival);
    }

    /// <summary>
    /// Interface for booking UI operations.
    /// </summary>
    public interface IBookingUI
    {
        /// <summary>
        /// Prompts user to select an arrival flight.
        /// </summary>
        /// <param name="arrivals">List of available arrival flights</param>
        /// <returns>Selected flight index</returns>
        int PromptArrivalFlightSelection(IReadOnlyList<FlightRecord> arrivals);

        /// <summary>
        /// Prompts user to select a departure flight.
        /// </summary>
        /// <param name="departures">List of available departure flights</param>
        /// <returns>Selected flight index</returns>
        int PromptDepartureFlightSelection(IReadOnlyList<FlightRecord> departures);

        /// <summary>
        /// Prompts user for seat row.
        /// </summary>
        /// <returns>Selected seat row</returns>
        int PromptSeatRow();

        /// <summary>
        /// Prompts user for seat column.
        /// </summary>
        /// <returns>Selected seat column</returns>
        string PromptSeatColumn();

        /// <summary>
        /// Displays booking success message.
        /// </summary>
        /// <param name="flightCode">Flight code</param>
        /// <param name="cityName">City name</param>
        /// <param name="when">Flight time</param>
        /// <param name="seat">Seat assignment</param>
        /// <param name="isArrival">True if arrival flight, false for departure</param>
        void DisplayBookingSuccess(string flightCode, string cityName, DateTime when, string seat, bool isArrival);

        /// <summary>
        /// Displays user flight details.
        /// </summary>
        /// <param name="user">The user whose details to display</param>
        void DisplayUserFlightDetails(User user);

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">Error message to display</param>
        void DisplayError(string message);

        /// <summary>
        /// Displays an error message with retry prompt.
        /// </summary>
        /// <param name="message">Error message to display</param>
        void DisplayErrorAgain(string message);
    }

    /// <summary>
    /// Base class for booking strategies.
    /// </summary>
    public abstract class BookingStrategy
    {
        protected readonly IUserService userService;

        protected BookingStrategy(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Handles seat booking with conflict resolution.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="requestedSeat">The originally requested seat</param>
        /// <param name="user">The user booking the seat</param>
        /// <returns>The final seat assignment</returns>
        public abstract string HandleSeatBooking(string flightCode, string requestedSeat, User user);
    }

    /// <summary>
    /// Booking strategy for travellers.
    /// </summary>
    public class TravellerBookingStrategy : BookingStrategy
    {
        public TravellerBookingStrategy(IUserService userService) : base(userService)
        {
        }

        public override string HandleSeatBooking(string flightCode, string requestedSeat, User user)
        {
            // Check if seat is already occupied
            if (!FlightMemory.IsSeatOccupied(flightCode, requestedSeat))
            {
                return requestedSeat; // Seat is available
            }

            // Travellers get the next available seat
            string? nextSeat = FlightMemory.GetNextAvailableSeat(flightCode, requestedSeat);
            if (nextSeat != null)
            {
                return nextSeat;
            }
            else
            {
                // This shouldn't happen in normal operation, but fallback to requested seat
                return requestedSeat;
            }
        }
    }

    /// <summary>
    /// Booking strategy for frequent flyers.
    /// </summary>
    public class FrequentFlyerBookingStrategy : BookingStrategy
    {
        public FrequentFlyerBookingStrategy(IUserService userService) : base(userService)
        {
        }

        public override string HandleSeatBooking(string flightCode, string requestedSeat, User user)
        {
            // Check if seat is already occupied
            if (!FlightMemory.IsSeatOccupied(flightCode, requestedSeat))
            {
                return requestedSeat; // Seat is available
            }

            // Frequent flyers can override any seat - they get priority
            // Find the current occupant and move them to the next available seat
            User? currentOccupant = userService.FindUserBySeat(flightCode, requestedSeat);
            if (currentOccupant != null)
            {
                // First, free the seat from the current occupant
                FlightMemory.FreeSeat(flightCode, requestedSeat);

                // Then find the next available seat for the displaced user
                string? nextSeat = FlightMemory.GetNextAvailableSeat(flightCode, requestedSeat);
                if (nextSeat != null)
                {
                    // Update the displaced user's seat assignment
                    var arrivalFlightCode = currentOccupant.GetArrivalFlightCode();
                    var departureFlightCode = currentOccupant.GetDepartureFlightCode();
                    
                    if (arrivalFlightCode == flightCode)
                    {
                        currentOccupant.UpdateArrivalSeat(nextSeat);
                    }
                    else if (departureFlightCode == flightCode)
                    {
                        currentOccupant.UpdateDepartureSeat(nextSeat);
                    }

                    // Occupy the new seat for the displaced user
                    FlightMemory.OccupySeat(flightCode, nextSeat);
                }
            }
            else
            {
                // Free the seat from the previous occupant and assign to frequent flyer
                FlightMemory.FreeSeat(flightCode, requestedSeat);
            }
            return requestedSeat;
        }
    }

    /// <summary>
    /// Implementation of booking service.
    /// </summary>
    public class BookingService : IBookingService
    {
        private readonly IUserService userService;
        private readonly Dictionary<Type, BookingStrategy> bookingStrategies;

        public BookingService(IUserService userService)
        {
            this.userService = userService;
            this.bookingStrategies = new Dictionary<Type, BookingStrategy>
            {
                { typeof(Traveller), new TravellerBookingStrategy(userService) },
                { typeof(FrequentFlyer), new FrequentFlyerBookingStrategy(userService) }
            };
        }

        public bool BookArrivalFlight(User user, int flightIndex, int seatRow, string seatColumn)
        {
            if (!CanBookArrivalFlight(user))
            {
                return false;
            }

            var arrivals = GetAvailableArrivalFlights();
            if (flightIndex < 0 || flightIndex >= arrivals.Count)
            {
                return false;
            }

            var selectedFlight = arrivals[flightIndex];
            
            if (!ValidateFlightTiming(user, selectedFlight, true))
            {
                return false;
            }

            string requestedSeat = $"{seatRow}:{seatColumn}";
            string finalSeat = GetBookingStrategy(user).HandleSeatBooking(selectedFlight.FlightCode(), requestedSeat, user);

            // If seat is occupied and user is a traveller, booking fails
            if (user is Traveller && finalSeat != requestedSeat)
            {
                return false;
            }

            user.BookArrivalFlight(selectedFlight, finalSeat);
            FlightMemory.OccupySeat(selectedFlight.FlightCode(), finalSeat);
            return true;
        }

        public bool BookDepartureFlight(User user, int flightIndex, int seatRow, string seatColumn)
        {
            if (!CanBookDepartureFlight(user))
            {
                return false;
            }

            var departures = GetAvailableDepartureFlights();
            if (flightIndex < 0 || flightIndex >= departures.Count)
            {
                return false;
            }

            var selectedFlight = departures[flightIndex];
            
            if (!ValidateFlightTiming(user, selectedFlight, false))
            {
                return false;
            }

            string requestedSeat = $"{seatRow}:{seatColumn}";
            string finalSeat = GetBookingStrategy(user).HandleSeatBooking(selectedFlight.FlightCode(), requestedSeat, user);

            // If seat is occupied and user is a traveller, booking fails
            if (user is Traveller && finalSeat != requestedSeat)
            {
                return false;
            }

            user.BookDepartureFlight(selectedFlight, finalSeat);
            FlightMemory.OccupySeat(selectedFlight.FlightCode(), finalSeat);
            return true;
        }

        public IReadOnlyList<FlightRecord> GetAvailableArrivalFlights()
        {
            return FlightMemory.GetArrivals();
        }

        public IReadOnlyList<FlightRecord> GetAvailableDepartureFlights()
        {
            return FlightMemory.GetDepartures();
        }

        public bool CanBookArrivalFlight(User user)
        {
            if (user.HasArrivalFlight())
            {
                return false;
            }

            var arrivals = GetAvailableArrivalFlights();
            return arrivals.Count > 0;
        }

        public bool CanBookDepartureFlight(User user)
        {
            if (user.HasDepartureFlight())
            {
                return false;
            }

            var departures = GetAvailableDepartureFlights();
            return departures.Count > 0;
        }

        public bool ValidateFlightTiming(User user, FlightRecord flight, bool isArrival)
        {
            if (isArrival)
            {
                // Check if user has a departure flight and validate timing
                if (user.HasDepartureFlight())
                {
                    var departureTime = user.GetDepartureFlightTime();
                    if (departureTime.HasValue && flight.When >= departureTime.Value)
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Check if user has an arrival flight and validate timing
                if (user.HasArrivalFlight())
                {
                    var arrivalTime = user.GetArrivalFlightTime();
                    if (arrivalTime.HasValue && flight.When <= arrivalTime.Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private BookingStrategy GetBookingStrategy(User user)
        {
            var userType = user.GetType();
            if (bookingStrategies.TryGetValue(userType, out var strategy))
            {
                return strategy;
            }
            
            // Default to traveller strategy
            return bookingStrategies[typeof(Traveller)];
        }

        /// <summary>
        /// Gets the user service for external access.
        /// </summary>
        /// <returns>The user service instance</returns>
        public IUserService GetUserService()
        {
            return userService;
        }
    }

    /// <summary>
    /// Implementation of booking UI using the dedicated BookingMenu.
    /// </summary>
    public class BookingUI : IBookingUI
    {
        private readonly BookingMenu bookingMenu;

        public BookingUI(BookingMenu bookingMenu)
        {
            this.bookingMenu = bookingMenu;
        }

        public int PromptArrivalFlightSelection(IReadOnlyList<FlightRecord> arrivals)
        {
            return BookingMenu.PromptArrivalFlightSelection(arrivals);
        }

        public int PromptDepartureFlightSelection(IReadOnlyList<FlightRecord> departures)
        {
            return BookingMenu.PromptDepartureFlightSelection(departures);
        }

        public int PromptSeatRow()
        {
            return BookingMenu.PromptSeatRow();
        }

        public string PromptSeatColumn()
        {
            return BookingMenu.PromptSeatColumn();
        }

        public void DisplayBookingSuccess(string flightCode, string cityName, DateTime when, string seat, bool isArrival)
        {
            BookingMenu.DisplayBookingSuccess(flightCode, cityName, when, seat, isArrival);
        }

        public void DisplayUserFlightDetails(User user)
        {
            BookingMenu.DisplayUserFlightDetails(user);
        }

        public void DisplayError(string message)
        {
            BookingMenu.DisplayError(message);
        }

        public void DisplayErrorAgain(string message)
        {
            BookingMenu.DisplayErrorAgain(message);
        }
    }

    /// <summary>
    /// Facade class that orchestrates the flight booking process.
    /// </summary>
    public class BookingFacade
    {
        private readonly IBookingService bookingService;
        private readonly IBookingUI bookingUI;

        public BookingFacade(IBookingService bookingService, IBookingUI bookingUI)
        {
            this.bookingService = bookingService;
            this.bookingUI = bookingUI;
        }

        /// <summary>
        /// Handles the complete arrival flight booking process.
        /// </summary>
        /// <param name="user">The user booking the flight</param>
        /// <returns>True if booking was successful, false otherwise</returns>
        public bool ProcessArrivalFlightBooking(User user)
        {
            if (!bookingService.CanBookArrivalFlight(user))
            {
                if (user.HasArrivalFlight())
                {
                    bookingUI.DisplayError("You already have an arrival flight. You can not book another");
                }
                else
                {
                    bookingUI.DisplayError("There are no arrival flights available.");
                }
                return false;
            }

            var arrivals = bookingService.GetAvailableArrivalFlights();
            int flightIndex;
            FlightRecord selectedFlight;
            
            do
            {
                flightIndex = bookingUI.PromptArrivalFlightSelection(arrivals);
                selectedFlight = arrivals[flightIndex];

                if (!bookingService.ValidateFlightTiming(user, selectedFlight, true))
                {
                    bookingUI.DisplayErrorAgain("The arrival time must be before the departure time");
                    continue;
                }
                break; // Valid arrival time
            } while (true);

            int seatRow;
            string seatColumn;
            string requestedSeat;
            string finalSeat;

            do
            {
                seatRow = bookingUI.PromptSeatRow();
                seatColumn = bookingUI.PromptSeatColumn();
                requestedSeat = $"{seatRow}:{seatColumn}";

                // Handle seat conflicts based on user type
                finalSeat = HandleSeatBooking(selectedFlight.FlightCode(), requestedSeat, user);

                // If seat is occupied and user is a traveller, show error and retry
                if (user is Traveller && finalSeat != requestedSeat)
                {
                    bookingUI.DisplayErrorAgain("Seat is already occupied");
                    continue;
                }

                break; // Valid seat assignment
            } while (true);

            bool success = bookingService.BookArrivalFlight(user, flightIndex, seatRow, seatColumn);
            if (success)
            {
                bookingUI.DisplayBookingSuccess(selectedFlight.FlightCode(), selectedFlight.CityName, selectedFlight.When, finalSeat, true);
            }
            return success;
        }

        /// <summary>
        /// Handles the complete departure flight booking process.
        /// </summary>
        /// <param name="user">The user booking the flight</param>
        /// <returns>True if booking was successful, false otherwise</returns>
        public bool ProcessDepartureFlightBooking(User user)
        {
            if (!bookingService.CanBookDepartureFlight(user))
            {
                if (user.HasDepartureFlight())
                {
                    bookingUI.DisplayError("You already have a departure flight. You can not book another");
                }
                else
                {
                    bookingUI.DisplayError("There are no departure flights available.");
                }
                return false;
            }

            var departures = bookingService.GetAvailableDepartureFlights();
            int flightIndex;
            FlightRecord selectedFlight;
            
            do
            {
                flightIndex = bookingUI.PromptDepartureFlightSelection(departures);
                selectedFlight = departures[flightIndex];

                if (!bookingService.ValidateFlightTiming(user, selectedFlight, false))
                {
                    bookingUI.DisplayErrorAgain("The departure time must be after the arrival time");
                    continue;
                }
                break; // Valid departure time
            } while (true);

            int seatRow;
            string seatColumn;
            string requestedSeat;
            string finalSeat;

            do
            {
                seatRow = bookingUI.PromptSeatRow();
                seatColumn = bookingUI.PromptSeatColumn();
                requestedSeat = $"{seatRow}:{seatColumn}";

                // Handle seat conflicts based on user type
                finalSeat = HandleSeatBooking(selectedFlight.FlightCode(), requestedSeat, user);

                // If seat is occupied and user is a traveller, show error and retry
                if (user is Traveller && finalSeat != requestedSeat)
                {
                    bookingUI.DisplayErrorAgain("Seat is already occupied");
                    continue;
                }

                break; // Valid seat assignment
            } while (true);

            bool success = bookingService.BookDepartureFlight(user, flightIndex, seatRow, seatColumn);
            if (success)
            {
                bookingUI.DisplayBookingSuccess(selectedFlight.FlightCode(), selectedFlight.CityName, selectedFlight.When, finalSeat, false);
            }
            return success;
        }

        /// <summary>
        /// Displays user flight details.
        /// </summary>
        /// <param name="user">The user whose details to display</param>
        public void ShowUserFlightDetails(User user)
        {
            bookingUI.DisplayUserFlightDetails(user);
        }

        /// <summary>
        /// Handles seat booking with conflict resolution based on user type.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="requestedSeat">The originally requested seat</param>
        /// <param name="user">The user booking the seat</param>
        /// <returns>The final seat assignment</returns>
        private string HandleSeatBooking(string flightCode, string requestedSeat, User user)
        {
            // Check if seat is already occupied
            if (!FlightMemory.IsSeatOccupied(flightCode, requestedSeat))
            {
                return requestedSeat; // Seat is available
            }

            // Handle based on user type
            if (user is FrequentFlyer)
            {
                // Frequent flyers can override any seat - they get priority
                // Find the current occupant and move them to the next available seat
                var userService = ((BookingService)bookingService).GetUserService();
                User? currentOccupant = userService.FindUserBySeat(flightCode, requestedSeat);
                if (currentOccupant != null)
                {
                    // First, free the seat from the current occupant
                    FlightMemory.FreeSeat(flightCode, requestedSeat);

                    // Then find the next available seat for the displaced user
                    string? nextSeat = FlightMemory.GetNextAvailableSeat(flightCode, requestedSeat);
                    if (nextSeat != null)
                    {
                        // Update the displaced user's seat assignment
                        var arrivalFlightCode = currentOccupant.GetArrivalFlightCode();
                        var departureFlightCode = currentOccupant.GetDepartureFlightCode();
                        
                        if (arrivalFlightCode == flightCode)
                        {
                            currentOccupant.UpdateArrivalSeat(nextSeat);
                        }
                        else if (departureFlightCode == flightCode)
                        {
                            currentOccupant.UpdateDepartureSeat(nextSeat);
                        }

                        // Occupy the new seat for the displaced user
                        FlightMemory.OccupySeat(flightCode, nextSeat);
                    }
                }
                else
                {
                    // Free the seat from the previous occupant and assign to frequent flyer
                    FlightMemory.FreeSeat(flightCode, requestedSeat);
                }
                return requestedSeat;
            }
            else if (user is Traveller)
            {
                // Travellers get the next available seat
                string? nextSeat = FlightMemory.GetNextAvailableSeat(flightCode, requestedSeat);
                if (nextSeat != null)
                {
                    return nextSeat;
                }
                else
                {
                    // This shouldn't happen in normal operation, but fallback to requested seat
                    return requestedSeat;
                }
            }

            return requestedSeat; // Fallback
        }
    }
}
