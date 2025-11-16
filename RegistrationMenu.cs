using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Handles registration-related menu operations and user input collection.
    /// </summary>
    public class RegistrationMenu
    {
        /// <summary>
        /// Displays the registration type selection menu and gets user selection.
        /// </summary>
        /// <param name="options">List of registration type options</param>
        /// <returns>Selected option index (0-based)</returns>
        public static int DisplayRegisterMenu(List<string> options)
        {
            int option = GetOption(MenuText.REGISTERMENU_PROMPT, options);
            return option;
        }

        /// <summary>
        /// Prompts the user to register as a traveller.
        /// </summary>
        /// <param name="name">The validated name of the traveller</param>
        /// <param name="age">The validated age of the traveller</param>
        /// <param name="mobile">The validated mobile phone number of the traveller</param>
        /// <param name="email">The validated email address of the traveller</param>
        /// <param name="password">The validated password chosen by the traveller</param>
        /// <param name="existingTravellers">List of existing travellers to check for email uniqueness</param>
        public void RegisterTravellerMenu(out string name, out int age, out string mobile, out string email, out string password, List<Traveller> existingTravellers)
        {
            ConsoleUI.DisplayString(MenuText.REGISTERING_TRAVELLER);

            name = GetValidatedName();
            age = GetValidatedAge();
            mobile = GetValidatedMobile();
            email = GetValidatedEmail(existingTravellers);
            password = GetValidatedPassword();
        }

        /// <summary>
        /// Prompts the user to register as a frequent flyer.
        /// </summary>
        /// <param name="name">The validated name of the frequent flyer</param>
        /// <param name="age">The validated age of the frequent flyer</param>
        /// <param name="mobile">The validated mobile phone number of the frequent flyer</param>
        /// <param name="email">The validated email address of the frequent flyer</param>
        /// <param name="password">The validated password chosen by the frequent flyer</param>
        /// <param name="ffNumber">The validated frequent flyer number</param>
        /// <param name="ffPoints">The validated frequent flyer points</param>
        /// <param name="existingTravellers">List of existing travellers to check for email uniqueness</param>
        public void RegisterFrequentFlyerMenu(out string name, out int age, out string mobile, out string email, out string password, out int ffNumber, out int ffPoints, List<Traveller> existingTravellers)
        {
            ConsoleUI.DisplayString(MenuText.REGISTERING_FREQUENT_FLYER);
            name = GetValidatedName();
            age = GetValidatedAge();
            mobile = GetValidatedMobile();
            email = GetValidatedEmail(existingTravellers);
            password = GetValidatedPassword();
            ffNumber = GetValidatedFrequentFlyerNumber();
            ffPoints = GetValidatedFrequentFlyerPoints();
        }

        /// <summary>
        /// Prompts the user to register as a flight manager.
        /// </summary>
        /// <param name="name">The validated name of the flight manager</param>
        /// <param name="age">The validated age of the flight manager</param>
        /// <param name="mobile">The validated mobile phone number of the flight manager</param>
        /// <param name="email">The validated email address of the flight manager</param>
        /// <param name="password">The validated password chosen by the flight manager</param>
        /// <param name="staffId">The validated staff ID</param>
        /// <param name="existingTravellers">List of existing travellers to check for email uniqueness</param>
        public void RegisterFlightManagerMenu(out string name, out int age, out string mobile, out string email, out string password, out int staffId, List<Traveller> existingTravellers)
        {
            ConsoleUI.DisplayString(MenuText.REGISTERING_FLIGHT_MANAGER);
            name = GetValidatedName();
            age = GetValidatedAge();
            mobile = GetValidatedMobile();
            email = GetValidatedEmail(existingTravellers);
            password = GetValidatedPassword();
            staffId = GetValidatedStaffId();
        }

        /// <summary>
        /// Displays success message for user registration.
        /// </summary>
        /// <param name="name">The user's name</param>
        /// <param name="userType">The type of user that was registered</param>
        public static void DisplayRegistrationSuccess(string name, string userType)
        {
            ConsoleUI.DisplayString($"Congratulations {name}. You have registered as a {userType}.");
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="errorStr">The error message to display</param>
        public static void DisplayError(string errorStr)
        {
            ConsoleUI.DisplayError(errorStr);
        }


        /// <summary>
        /// Gets a validated name from the user.
        /// </summary>
        /// <returns>Valid name</returns>
        private static string GetValidatedName()
        {
            string name;
            do
            {
                ConsoleUI.DisplayString(MenuText.ENTER_NAME);
                name = ConsoleUI.GetString();
                if (!InputValidator.IsValidName(name))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_NAME);
                }
            } while (!InputValidator.IsValidName(name));
            return name;
        }

        /// <summary>
        /// Gets a validated age from the user.
        /// </summary>
        /// <returns>Valid age</returns>
        private static int GetValidatedAge()
        {
            string age_AsString;
            int age;
            do
            {
                ConsoleUI.DisplayString(string.Format(MenuText.ENTER_AGE, ValidationConsts.MIN_AGE, ValidationConsts.MAX_AGE));
                age_AsString = ConsoleUI.GetString();

                if (!int.TryParse(age_AsString, out age))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_VALUE);
                }
                else if (!InputValidator.IsValidAge(age_AsString))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_AGE);
                }
            } while (!InputValidator.IsValidAge(age_AsString));

            return age;
        }

        /// <summary>
        /// Gets a validated mobile number from the user.
        /// </summary>
        /// <returns>Valid mobile number</returns>
        private static string GetValidatedMobile()
        {
            string mobile;
            do
            {
                ConsoleUI.DisplayString(MenuText.ENTER_MOBILE);
                mobile = ConsoleUI.GetString();
                if (!InputValidator.IsValidMobile(mobile))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_MOBILE);
                }
            } while (!InputValidator.IsValidMobile(mobile));
            return mobile;
        }

        /// <summary>
        /// Gets a validated email address from the user.
        /// </summary>
        /// <param name="existingTravellers">List of existing travellers to check for email uniqueness</param>
        /// <returns>Valid email address</returns>
        private static string GetValidatedEmail(List<Traveller> existingTravellers)
        {
            string email;
            do
            {
                ConsoleUI.DisplayString(MenuText.ENTER_EMAIL);
                email = ConsoleUI.GetString();

                if (!InputValidator.IsValidEmail(email))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_EMAIL);
                }
                else if (InputValidator.IsEmailAlreadyInUse(email, existingTravellers))
                {
                    ConsoleUI.DisplayErrorAgain("Email already registered");
                }
            } while (!InputValidator.IsValidEmail(email) || InputValidator.IsEmailAlreadyInUse(email, existingTravellers));

            return email;
        }

        /// <summary>
        /// Gets a validated password from the user.
        /// </summary>
        /// <returns>Valid password</returns>
        private static string GetValidatedPassword()
        {
            string password;
            do
            {
                ConsoleUI.DisplayString(MenuText.ENTER_PASSWORD);
                ConsoleUI.DisplayString(MenuText.PASSWORD_REQUIREMENTS);

                password = ConsoleUI.GetString();

                if (!InputValidator.IsValidPassword(password))
                {
                    ConsoleUI.DisplayErrorAgain(MenuText.INVALID_PASSWORD);
                }
            } while (!InputValidator.IsValidPassword(password));

            return password;
        }

        /// <summary>
        /// Gets a validated frequent flyer number from the user.
        /// </summary>
        /// <returns>Valid frequent flyer number</returns>
        private static int GetValidatedFrequentFlyerNumber()
        {
            return GetValidatedInteger(
                $"Please enter in your frequent flyer number between {ValidationConsts.MIN_FREQUENT_FLYER_NUMBER} and {ValidationConsts.MAX_FREQUENT_FLYER_NUMBER}:",
                InputValidator.IsValidFrequentFlyerNumber,
                "Supplied frequent flyer number is invalid"
            );
        }

        /// <summary>
        /// Gets a validated frequent flyer points from the user.
        /// </summary>
        /// <returns>Valid frequent flyer points</returns>
        private static int GetValidatedFrequentFlyerPoints()
        {
            return GetValidatedInteger(
                $"Please enter in your current frequent flyer points between {ValidationConsts.MIN_FREQUENT_FLYER_POINTS} and {ValidationConsts.MAX_FREQUENT_FLYER_POINTS}:",
                InputValidator.IsValidFrequentFlyerPoints,
                "Supplied current frequent flyer points is invalid"
            );
        }

        /// <summary>
        /// Gets a validated staff ID from the user.
        /// </summary>
        /// <returns>Valid staff ID</returns>
        private static int GetValidatedStaffId()
        {
            return GetValidatedInteger(
                $"Please enter in your staff id between {ValidationConsts.MIN_STAFF_ID} and {ValidationConsts.MAX_STAFF_ID}:",
                InputValidator.IsValidStaffId,
                "Supplied staff id is invalid"
            );
        }

        /// <summary>
        /// Validates integer input with retry loop.
        /// </summary>
        /// <param name="prompt">The prompt message to display</param>
        /// <param name="validator">The validation function</param>
        /// <param name="errorMessage">The error message to display on validation failure</param>
        /// <returns>Valid integer value</returns>
        private static int GetValidatedInteger(string prompt, Func<string, bool> validator, string errorMessage)
        {
            string input;
            int value;
            do
            {
                ConsoleUI.DisplayString(prompt);
                input = ConsoleUI.GetString();
                if (!validator(input))
                {
                    ConsoleUI.DisplayErrorAgain(errorMessage);
                }
            } while (!validator(input));

            value = int.Parse(input);
            return value;
        }



        /// <summary>
        /// Displays a menu and gets user selection.
        /// </summary>
        /// <param name="title">Menu title to display</param>
        /// <param name="options">List of menu options</param>
        /// <returns>Selected option index (0-based) or -1 if no options</returns>
        private static int GetOption(string title, List<string> options)
        {
            if (options.Count <= 0)
            {
                return -1;
            }

            ConsoleUI.DisplayString(title);
            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Count)));
            for (int optionIndex = 0; optionIndex < options.Count; optionIndex++)
            {
                ConsoleUI.DisplayString($"{(optionIndex + 1).ToString().PadLeft(digitsNeeded)}. {options[optionIndex]}");
            }

            int option = ConsoleUI.GetInt(MenuText.ChoicePrompt(options.Count));
            return option - 1;
        }

    }
}
