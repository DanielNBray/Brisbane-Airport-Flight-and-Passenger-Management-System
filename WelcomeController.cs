using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Interface for welcome and main menu operations.
    /// </summary>
    public interface IWelcomeService
    {
        /// <summary>
        /// Displays the welcome banner.
        /// </summary>
        void DisplayWelcomeBanner();

        /// <summary>
        /// Displays the main menu and gets user selection.
        /// </summary>
        /// <returns>The selected main menu option</returns>
        MainMenuOption GetMainMenuSelection();

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message to display</param>
        void DisplayMessage(string message);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        void DisplayError(string errorMessage);
    }

    /// <summary>
    /// Interface for welcome UI operations.
    /// </summary>
    public interface IWelcomeUI
    {
        /// <summary>
        /// Displays the welcome banner.
        /// </summary>
        void DisplayWelcomeBanner();

        /// <summary>
        /// Displays the main menu and gets user selection.
        /// </summary>
        /// <returns>The selected main menu option</returns>
        MainMenuOption GetMainMenuSelection();

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message to display</param>
        void DisplayMessage(string message);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        void DisplayError(string errorMessage);
    }

    /// <summary>
    /// Enumeration of main menu options.
    /// </summary>
    public enum MainMenuOption
    {
        Login = 0,
        Register = 1,
        Exit = 2
    }

    /// <summary>
    /// Implementation of welcome service.
    /// </summary>
    public class WelcomeService : IWelcomeService
    {
        private readonly IWelcomeUI welcomeUI;

        public WelcomeService(IWelcomeUI welcomeUI)
        {
            this.welcomeUI = welcomeUI;
        }

        public void DisplayWelcomeBanner()
        {
            welcomeUI.DisplayWelcomeBanner();
        }

        public MainMenuOption GetMainMenuSelection()
        {
            return welcomeUI.GetMainMenuSelection();
        }

        public void DisplayMessage(string message)
        {
            welcomeUI.DisplayMessage(message);
        }

        public void DisplayError(string errorMessage)
        {
            welcomeUI.DisplayError(errorMessage);
        }
    }

    /// <summary>
    /// Implementation of welcome UI using the dedicated WelcomeMenu.
    /// </summary>
    public class WelcomeUI : IWelcomeUI
    {
        private readonly WelcomeMenu welcomeMenu;

        public WelcomeUI(WelcomeMenu welcomeMenu)
        {
            this.welcomeMenu = welcomeMenu;
        }

        public void DisplayWelcomeBanner()
        {
            WelcomeMenu.DisplayWelcomeBanner();
        }

        public MainMenuOption GetMainMenuSelection()
        {
            // Create the display strings and list
            const string LOGIN_STR = "Login as a registered user."; // Menu option 1   
            const string REGISTER_STR = "Register as a new user."; // Menu option 2
            const string EXIT_STR = "Exit."; // Menu option 3

            List<string> options = new List<string>();
            options.Add(LOGIN_STR);
            options.Add(REGISTER_STR);
            options.Add(EXIT_STR);

            int option = WelcomeMenu.DisplayMainMenu(options);
            return (MainMenuOption)option;
        }

        public void DisplayMessage(string message)
        {
            WelcomeMenu.DisplayMessage(message);
        }

        public void DisplayError(string errorMessage)
        {
            WelcomeMenu.DisplayError(errorMessage);
        }
    }

    /// <summary>
    /// Facade class that orchestrates the welcome and main menu process.
    /// </summary>
    public class WelcomeFacade
    {
        private readonly IWelcomeService welcomeService;

        public WelcomeFacade(IWelcomeService welcomeService)
        {
            this.welcomeService = welcomeService;
        }

        /// <summary>
        /// Displays the welcome banner.
        /// </summary>
        public void DisplayWelcomeBanner()
        {
            welcomeService.DisplayWelcomeBanner();
        }

        /// <summary>
        /// Processes the main menu and returns the selected option.
        /// </summary>
        /// <returns>The selected main menu option</returns>
        public MainMenuOption ProcessMainMenu()
        {
            return welcomeService.GetMainMenuSelection();
        }

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message to display</param>
        public void DisplayMessage(string message)
        {
            welcomeService.DisplayMessage(message);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        public void DisplayError(string errorMessage)
        {
            welcomeService.DisplayError(errorMessage);
        }
    }

    /// <summary>
    /// Controller class that handles the welcome main menu orchestration.
    /// Manages the main menu loop, banner display, and routing to appropriate handlers.
    /// </summary>
    public class WelcomeController
    {
        private readonly WelcomeFacade welcomeFacade;
        private readonly Action loginHandler;
        private readonly Action registerHandler;

        /// <summary>
        /// Initializes a new instance of the WelcomeController class.
        /// </summary>
        /// <param name="welcomeFacade">The welcome facade for UI operations</param>
        /// <param name="loginHandler">Action to handle login option selection</param>
        /// <param name="registerHandler">Action to handle register option selection</param>
        public WelcomeController(WelcomeFacade welcomeFacade, Action loginHandler, Action registerHandler)
        {
            this.welcomeFacade = welcomeFacade;
            this.loginHandler = loginHandler;
            this.registerHandler = registerHandler;
        }

        /// <summary>
        /// Runs the welcome main menu loop.
        /// </summary>
        public void Run()
        {
            welcomeFacade.DisplayWelcomeBanner();
            bool keepGoing = true;
            while (keepGoing)
            {
                keepGoing = ProcessMainMenu();
            }
        }

        /// <summary>
        /// Processes the main menu selection and routes to appropriate handlers.
        /// </summary>
        /// <returns>True if the user chooses to continue, false to exit</returns>
        private bool ProcessMainMenu()
        {
            MainMenuOption option = welcomeFacade.ProcessMainMenu();

            switch (option)
            {
                case MainMenuOption.Login:
                    loginHandler();
                    break;
                case MainMenuOption.Register:
                    registerHandler();
                    break;
                case MainMenuOption.Exit:
                    welcomeFacade.DisplayMessage("Thank you. Safe travels.");
                    return false;
                default:
                    welcomeFacade.DisplayError("Wrong main menu choice");
                    break;
            }
            return true;
        }
    }
}
