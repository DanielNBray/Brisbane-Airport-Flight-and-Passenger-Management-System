using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Interface for handling user-specific session logic after successful login.
    /// Implements Strategy pattern to eliminate role-based switch statements.
    /// </summary>
    internal interface IUserSessionHandler
    {
        /// <summary>
        /// Handles the complete session for a logged-in user, including welcome message,
        /// menu display, and action processing until logout.
        /// </summary>
        /// <param name="user">The authenticated user</param>
        void HandleSession(User user);
    }

    /// <summary>
    /// Handles session logic for Traveller users after successful login.
    /// RESPONSIBILITY: Traveller-specific menu flow and action routing.
    /// </summary>
    internal sealed class TravellerSessionHandler : IUserSessionHandler
    {
        private readonly PasswordChangeHandler passwordChangeHandler;
        private readonly BookingFacade bookingFacade;

        public TravellerSessionHandler(PasswordChangeHandler passwordChangeHandler, BookingFacade bookingFacade)
        {
            this.passwordChangeHandler = passwordChangeHandler;
            this.bookingFacade = bookingFacade;
        }

        /// <summary>
        /// Handles the complete Traveller session.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void HandleSession(User user)
        {
            Traveller currentTraveller = (Traveller)user;
            
            AirportMenu.DisplayMessage($"Welcome back {currentTraveller.Name}.");

            // Display the traveller menu and accept choices until logout
            List<string> travellerOptions = new List<string>();
            travellerOptions.Add("See my details.");
            travellerOptions.Add("Change password.");
            travellerOptions.Add("Book an arrival flight.");
            travellerOptions.Add("Book a departure flight.");
            travellerOptions.Add("See flight details.");
            travellerOptions.Add("Logout.");

            bool inSession = true;
            while (inSession)
            {
                int selection = AirportMenu.DisplayTravellerMenu(travellerOptions);
                switch (selection)
                {
                    case 0:
                        AirportMenu.DisplayMessage(currentTraveller.ToString());
                        break;
                    case 1:
                        passwordChangeHandler.HandleChangePassword(currentTraveller);
                        break;
                    case 2:
                        bookingFacade.ProcessArrivalFlightBooking(currentTraveller);
                        break;
                    case 3:
                        bookingFacade.ProcessDepartureFlightBooking(currentTraveller);
                        break;
                    case 4:
                        bookingFacade.ShowUserFlightDetails(currentTraveller);
                        break;
                    case 5:
                        inSession = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Handles session logic for FrequentFlyer users after successful login.
    /// RESPONSIBILITY: FrequentFlyer-specific menu flow and action routing.
    /// </summary>
    internal sealed class FrequentFlyerSessionHandler : IUserSessionHandler
    {
        private readonly PasswordChangeHandler passwordChangeHandler;
        private readonly BookingFacade bookingFacade;

        public FrequentFlyerSessionHandler(PasswordChangeHandler passwordChangeHandler, BookingFacade bookingFacade)
        {
            this.passwordChangeHandler = passwordChangeHandler;
            this.bookingFacade = bookingFacade;
        }

        /// <summary>
        /// Handles the complete FrequentFlyer session.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void HandleSession(User user)
        {
            FrequentFlyer currentFrequentFlyer = (FrequentFlyer)user;

            AirportMenu.DisplayMessage($"Welcome back {currentFrequentFlyer.Name}.");
            
            List<string> ffOptions = new List<string>();
            ffOptions.Add("See my details.");
            ffOptions.Add("Change password.");
            ffOptions.Add("Book an arrival flight.");
            ffOptions.Add("Book a departure flight.");
            ffOptions.Add("See flight details.");
            ffOptions.Add("See frequent flyer points.");
            ffOptions.Add("Logout.");

            bool inFFSession = true;
            while (inFFSession)
            {
                int selection = AirportMenu.DisplayFrequentFlyerMenu(ffOptions);
                switch (selection)
                {
                    case 0:
                        AirportMenu.DisplayMessage(currentFrequentFlyer.ToString());
                        break;
                    case 1:
                        passwordChangeHandler.HandleChangePassword(currentFrequentFlyer);
                        break;
                    case 2:
                        bookingFacade.ProcessArrivalFlightBooking(currentFrequentFlyer);
                        break;
                    case 3:
                        bookingFacade.ProcessDepartureFlightBooking(currentFrequentFlyer);
                        break;
                    case 4:
                        bookingFacade.ShowUserFlightDetails(currentFrequentFlyer);
                        break;
                    case 5:
                        AirportMenu.DisplayDetailedFrequentFlyerPoints(currentFrequentFlyer);
                        break;
                    case 6:
                        inFFSession = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Handles session logic for FlightManager users after successful login.
    /// RESPONSIBILITY: FlightManager-specific menu flow and action routing.
    /// </summary>
    internal sealed class FlightManagerSessionHandler : IUserSessionHandler
    {
        private readonly PasswordChangeHandler passwordChangeHandler;
        private readonly FlightOperationsHandler flightOperationsHandler;

        public FlightManagerSessionHandler(PasswordChangeHandler passwordChangeHandler, FlightOperationsHandler flightOperationsHandler)
        {
            this.passwordChangeHandler = passwordChangeHandler;
            this.flightOperationsHandler = flightOperationsHandler;
        }

        /// <summary>
        /// Handles the complete FlightManager session.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        public void HandleSession(User user)
        {
            FlightManager currentFlightManager = (FlightManager)user;

            AirportMenu.DisplayMessage($"Welcome back {currentFlightManager.Name}.");

            List<string> fmOptions = new List<string>();
            fmOptions.Add("See my details.");
            fmOptions.Add("Change password.");
            fmOptions.Add("Create an arrival flight.");
            fmOptions.Add("Create a departure flight.");
            fmOptions.Add("Delay an arrival flight.");
            fmOptions.Add("Delay a departure flight.");
            fmOptions.Add("See the details of all flights.");
            fmOptions.Add("Logout.");

            bool inFMSession = true;
            while (inFMSession)
            {
                int selection = AirportMenu.DisplayFlightManagerMenu(fmOptions);
                switch (selection)
                {
                    case 0:
                        AirportMenu.DisplayMessage(currentFlightManager.ToString());
                        break;
                    case 1:
                        passwordChangeHandler.HandleChangePassword(currentFlightManager);
                        break;
                    case 2:
                        flightOperationsHandler.CreateArrivalFlight();
                        break;
                    case 3:
                        flightOperationsHandler.CreateDepartureFlight();
                        break;
                    case 4:
                        flightOperationsHandler.DelayArrivalFlight();
                        break;
                    case 5:
                        flightOperationsHandler.DelayDepartureFlight();
                        break;
                    case 6:
                        flightOperationsHandler.ShowAllFlights();
                        break;
                    case 7:
                        inFMSession = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Coordinates the routing of authenticated users to their role-specific session handlers.
    /// RESPONSIBILITY: Determining and invoking the appropriate session handler based on user type.
    /// Uses polymorphism to eliminate role-based switch statements.
    /// </summary>
    internal sealed class UserSessionCoordinator
    {
        private readonly TravellerSessionHandler travellerHandler;
        private readonly FrequentFlyerSessionHandler frequentFlyerHandler;
        private readonly FlightManagerSessionHandler flightManagerHandler;

        public UserSessionCoordinator(
            TravellerSessionHandler travellerHandler,
            FrequentFlyerSessionHandler frequentFlyerHandler,
            FlightManagerSessionHandler flightManagerHandler)
        {
            this.travellerHandler = travellerHandler;
            this.frequentFlyerHandler = frequentFlyerHandler;
            this.flightManagerHandler = flightManagerHandler;
        }

        /// <summary>
        /// Routes the authenticated user to their appropriate session handler.
        /// Reproduces exact console output and behavior of original code.
        /// </summary>
        /// <param name="authenticatedUser">The authenticated user</param>
        public void StartUserSession(User authenticatedUser)
        {
            // Determine handler based on user type
            IUserSessionHandler handler = GetHandlerForUser(authenticatedUser);
            
            // Delegate to the appropriate handler
            handler.HandleSession(authenticatedUser);
        }

        /// <summary>
        /// Gets the appropriate session handler for a user based on their type.
        /// Uses type checking to maintain exact behavior match with original code.
        /// </summary>
        private IUserSessionHandler GetHandlerForUser(User user)
        {
            // Check in order: FlightManager, FrequentFlyer, Traveller (base class)
            // This matches the original code's if-else chain
            if (user is FlightManager)
            {
                return flightManagerHandler;
            }
            else if (user is FrequentFlyer)
            {
                return frequentFlyerHandler;
            }
            else
            {
                // Default to Traveller handler for base Traveller class
                return travellerHandler;
            }
        }
    }
}

