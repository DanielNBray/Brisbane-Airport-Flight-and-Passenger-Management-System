using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Main controller that orchestrates the airport management system.
    /// </summary>
    public class AirportController
    {
        private readonly IUserService userService;
        private readonly UserRegistrationFacade registrationFacade;
        private readonly BookingFacade bookingFacade;
        private readonly WelcomeController welcomeController;
        private readonly LoginSystem loginSystem;
        private readonly LoginMenu loginMenu;
        private AirportMenu menu;


        /// <summary>
        /// Initializes the airport controller with all required dependencies.
        /// </summary>
        /// <param name="userService">Service for user operations</param>
        public AirportController(IUserService userService)
        {
            this.userService = userService;
            menu = new AirportMenu();
            
            loginSystem = new LoginSystem(userService);
            
            var welcomeMenu = new WelcomeMenu();
            var welcomeUI = new WelcomeUI(welcomeMenu);
            var welcomeService = new WelcomeService(welcomeUI);
            var welcomeFacade = new WelcomeFacade(welcomeService);
            
            welcomeController = new WelcomeController(welcomeFacade, ProcessLogin, ProcessRegister);
            
            var registrationService = new UserRegistrationService(userService);
            var registrationMenu = new RegistrationMenu();
            var registrationUI = new UserRegistrationUI(registrationMenu);
            registrationFacade = new UserRegistrationFacade(registrationService, registrationUI, userService);
            
            var bookingService = new BookingService(userService);
            var bookingMenu = new BookingMenu();
            var bookingUI = new BookingUI(bookingMenu);
            bookingFacade = new BookingFacade(bookingService, bookingUI);
            
            loginMenu = new LoginMenu(loginSystem, menu, userService, bookingFacade);
        }

        /// <summary>
        /// Starts the main application loop.
        /// </summary>
        public void Run()
        {
            welcomeController.Run();
        }

        /// <summary>
        /// Handles user login process.
        /// </summary>
        private void ProcessLogin()
        {
            loginMenu.ProcessLogin();
        }

        /// <summary>
        /// Handles user registration process.
        /// </summary>
        private void ProcessRegister()
        {
            registrationFacade.ProcessRegistration();
        }


    }
}

