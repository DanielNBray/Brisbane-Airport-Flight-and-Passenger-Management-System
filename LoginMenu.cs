using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// UI controller for login operations.
    /// Handles all console I/O and delegates authentication to the domain layer.
    /// Refactored to follow SRP by delegating to specialized components.
    /// </summary>
    public sealed class LoginMenu
    {
        private readonly LoginSystem loginSystem;
        private readonly AirportMenu menu;
        private readonly IUserService userService;
        private readonly BookingFacade bookingFacade;
        
        // New SRP-compliant components
        private readonly LoginCredentialsCollector credentialsCollector;
        private readonly UserSessionCoordinator sessionCoordinator;

        public LoginMenu(LoginSystem loginSystem, AirportMenu menu, IUserService userService, BookingFacade bookingFacade)
        {
            this.loginSystem = loginSystem;
            this.menu = menu;
            this.userService = userService;
            this.bookingFacade = bookingFacade;
            
            // Initialize new components
            this.credentialsCollector = new LoginCredentialsCollector(loginSystem);
            
            // Initialize session handlers
            PasswordChangeHandler passwordChangeHandler = new PasswordChangeHandler(userService);
            FlightOperationsHandler flightOperationsHandler = new FlightOperationsHandler();
            
            TravellerSessionHandler travellerHandler = new TravellerSessionHandler(passwordChangeHandler, bookingFacade);
            FrequentFlyerSessionHandler frequentFlyerHandler = new FrequentFlyerSessionHandler(passwordChangeHandler, bookingFacade);
            FlightManagerSessionHandler flightManagerHandler = new FlightManagerSessionHandler(passwordChangeHandler, flightOperationsHandler);
            
            this.sessionCoordinator = new UserSessionCoordinator(travellerHandler, frequentFlyerHandler, flightManagerHandler);
        }

        /// <summary>
        /// Processes the complete login flow including UI prompts, authentication, and role-specific sessions.
        /// Refactored to delegate to SRP-compliant components while preserving exact behavior.
        /// </summary>
        public void ProcessLogin()
        {
            // Display login heading
            AirportMenu.DisplayLoginHeading();

            // Check if there are any users registered
            if (!loginSystem.HasAnyUsers())
            {
                AirportMenu.DisplayError("There are no people registered");
                return;
            }

            // Collect valid, registered email (delegated to UI component)
            string email = credentialsCollector.CollectValidRegisteredEmail();

            // Get user by email for authentication
            User foundUser = loginSystem.GetUserByEmail(email);

            // Collect valid password and authenticate (delegated to UI component)
            credentialsCollector.CollectAndAuthenticatePassword(foundUser);

            // Successful login - delegate session handling to coordinator
            sessionCoordinator.StartUserSession(foundUser);
        }
    }
}
