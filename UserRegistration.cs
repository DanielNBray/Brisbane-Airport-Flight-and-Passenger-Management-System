using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Interface for user registration operations.
    /// </summary>
    public interface IUserRegistration
    {
        /// <summary>
        /// Registers a new traveller.
        /// </summary>
        /// <param name="registrationData">The registration data for the traveller</param>
        /// <returns>The registered traveller</returns>
        Traveller RegisterTraveller(TravellerRegistrationData registrationData);

        /// <summary>
        /// Registers a new frequent flyer.
        /// </summary>
        /// <param name="registrationData">The registration data for the frequent flyer</param>
        /// <returns>The registered frequent flyer</returns>
        FrequentFlyer RegisterFrequentFlyer(FrequentFlyerRegistrationData registrationData);

        /// <summary>
        /// Registers a new flight manager.
        /// </summary>
        /// <param name="registrationData">The registration data for the flight manager</param>
        /// <returns>The registered flight manager</returns>
        FlightManager RegisterFlightManager(FlightManagerRegistrationData registrationData);

        /// <summary>
        /// Validates that an email is unique across all users.
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <returns>True if email is unique, false otherwise</returns>
        bool IsEmailUnique(string email);
    }

    /// <summary>
    /// Base class for user registration data containing common fields.
    /// </summary>
    public abstract class UserRegistrationData
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Registration data for a traveller.
    /// </summary>
    public class TravellerRegistrationData : UserRegistrationData
    {
        // No additional fields needed for basic traveller
    }

    /// <summary>
    /// Registration data for a frequent flyer.
    /// </summary>
    public class FrequentFlyerRegistrationData : UserRegistrationData
    {
        public int FrequentFlyerNumber { get; set; }
        public int Points { get; set; }
    }

    /// <summary>
    /// Registration data for a flight manager.
    /// </summary>
    public class FlightManagerRegistrationData : UserRegistrationData
    {
        public int StaffId { get; set; }
    }


    /// <summary>
    /// Interface for user registration UI operations.
    /// </summary>
    public interface IUserRegistrationUI
    {
        /// <summary>
        /// Displays the registration menu and gets user selection.
        /// </summary>
        /// <returns>The selected registration type</returns>
        RegistrationType GetRegistrationType();

        /// <summary>
        /// Collects traveller registration data from the user.
        /// </summary>
        /// <param name="existingTravellers">List of existing travellers for email uniqueness check</param>
        /// <returns>The collected registration data</returns>
        TravellerRegistrationData CollectTravellerData(List<Traveller> existingTravellers);

        /// <summary>
        /// Collects frequent flyer registration data from the user.
        /// </summary>
        /// <param name="existingTravellers">List of existing travellers for email uniqueness check</param>
        /// <returns>The collected registration data</returns>
        FrequentFlyerRegistrationData CollectFrequentFlyerData(List<Traveller> existingTravellers);

        /// <summary>
        /// Collects flight manager registration data from the user.
        /// </summary>
        /// <param name="existingTravellers">List of existing travellers for email uniqueness check</param>
        /// <returns>The collected registration data</returns>
        FlightManagerRegistrationData CollectFlightManagerData(List<Traveller> existingTravellers);

        /// <summary>
        /// Displays validation errors to the user.
        /// </summary>
        /// <param name="errors">List of error messages</param>
        void DisplayValidationErrors(List<string> errors);

        /// <summary>
        /// Displays success message for traveller registration.
        /// </summary>
        /// <param name="name">The traveller's name</param>
        void DisplayTravellerRegistrationSuccess(string name);

        /// <summary>
        /// Displays success message for frequent flyer registration.
        /// </summary>
        /// <param name="name">The frequent flyer's name</param>
        void DisplayFrequentFlyerRegistrationSuccess(string name);

        /// <summary>
        /// Displays success message for flight manager registration.
        /// </summary>
        /// <param name="name">The flight manager's name</param>
        void DisplayFlightManagerRegistrationSuccess(string name);
    }

    /// <summary>
    /// Enumeration of registration types.
    /// </summary>
    public enum RegistrationType
    {
        Traveller = 0,
        FrequentFlyer = 1,
        FlightManager = 2
    }

    /// <summary>
    /// Implementation of user registration service.
    /// </summary>
    public class UserRegistrationService : IUserRegistration
    {
        private readonly IUserService userService;

        public UserRegistrationService(IUserService userService)
        {
            this.userService = userService;
        }

        public Traveller RegisterTraveller(TravellerRegistrationData registrationData)
        {
            return userService.RegisterTraveller(
                registrationData.Name,
                registrationData.Age,
                registrationData.Mobile,
                registrationData.Email,
                registrationData.Password
            );
        }

        public FrequentFlyer RegisterFrequentFlyer(FrequentFlyerRegistrationData registrationData)
        {
            return userService.RegisterFrequentFlyer(
                registrationData.Name,
                registrationData.Age,
                registrationData.Mobile,
                registrationData.Email,
                registrationData.Password,
                registrationData.FrequentFlyerNumber,
                registrationData.Points
            );
        }

        public FlightManager RegisterFlightManager(FlightManagerRegistrationData registrationData)
        {
            return userService.RegisterFlightManager(
                registrationData.Name,
                registrationData.Age,
                registrationData.Mobile,
                registrationData.Email,
                registrationData.Password,
                registrationData.StaffId
            );
        }

        public bool IsEmailUnique(string email)
        {
            return !userService.EmailExists(email);
        }
    }


    /// <summary>
    /// Implementation of user registration UI using the dedicated RegistrationMenu.
    /// </summary>
    public class UserRegistrationUI : IUserRegistrationUI
    {
        private readonly RegistrationMenu registrationMenu;

        public UserRegistrationUI(RegistrationMenu registrationMenu)
        {
            this.registrationMenu = registrationMenu;
        }

        public RegistrationType GetRegistrationType()
        {
            const string TRAVELLER_STR = "A standard traveller.";
            const string FREQUENT_FLYER_STR = "A frequent flyer.";
            const string FLIGHT_MANAGER_STR = "A flight manager.";

            List<string> options = new List<string>
            {
                TRAVELLER_STR,
                FREQUENT_FLYER_STR,
                FLIGHT_MANAGER_STR
            };

            int option = RegistrationMenu.DisplayRegisterMenu(options);
            return (RegistrationType)option;
        }

        public TravellerRegistrationData CollectTravellerData(List<Traveller> existingTravellers)
        {
            string name, mobile, email, password;
            int age;

            registrationMenu.RegisterTravellerMenu(out name, out age, out mobile, out email, out password, existingTravellers);

            return new TravellerRegistrationData
            {
                Name = name,
                Age = age,
                Mobile = mobile,
                Email = email,
                Password = password
            };
        }

        public FrequentFlyerRegistrationData CollectFrequentFlyerData(List<Traveller> existingTravellers)
        {
            string name, mobile, email, password;
            int age, ffNumber, ffPoints;

            registrationMenu.RegisterFrequentFlyerMenu(out name, out age, out mobile, out email, out password, out ffNumber, out ffPoints, existingTravellers);

            return new FrequentFlyerRegistrationData
            {
                Name = name,
                Age = age,
                Mobile = mobile,
                Email = email,
                Password = password,
                FrequentFlyerNumber = ffNumber,
                Points = ffPoints
            };
        }

        public FlightManagerRegistrationData CollectFlightManagerData(List<Traveller> existingTravellers)
        {
            string name, mobile, email, password;
            int age, staffId;

            registrationMenu.RegisterFlightManagerMenu(out name, out age, out mobile, out email, out password, out staffId, existingTravellers);

            return new FlightManagerRegistrationData
            {
                Name = name,
                Age = age,
                Mobile = mobile,
                Email = email,
                Password = password,
                StaffId = staffId
            };
        }

        public void DisplayValidationErrors(List<string> errors)
        {
            foreach (string error in errors)
            {
                RegistrationMenu.DisplayError(error);
            }
        }

        public void DisplayTravellerRegistrationSuccess(string name)
        {
            RegistrationMenu.DisplayRegistrationSuccess(name, "traveller");
        }

        public void DisplayFrequentFlyerRegistrationSuccess(string name)
        {
            RegistrationMenu.DisplayRegistrationSuccess(name, "frequent flyer");
        }

        public void DisplayFlightManagerRegistrationSuccess(string name)
        {
            RegistrationMenu.DisplayRegistrationSuccess(name, "flight manager");
        }
    }

    /// <summary>
    /// Facade class that orchestrates the user registration process.
    /// </summary>
    public class UserRegistrationFacade
    {
        private readonly IUserRegistration registrationService;
        private readonly IUserRegistrationUI registrationUI;
        private readonly IUserService userService;

        public UserRegistrationFacade(IUserRegistration registrationService, IUserRegistrationUI registrationUI, IUserService userService)
        {
            this.registrationService = registrationService;
            this.registrationUI = registrationUI;
            this.userService = userService;
        }

        /// <summary>
        /// Handles the complete user registration process.
        /// </summary>
        public void ProcessRegistration()
        {
            try
            {
                RegistrationType registrationType = registrationUI.GetRegistrationType();
                List<Traveller> existingTravellers = new List<Traveller>(userService.GetTravellersSnapshot());

                switch (registrationType)
                {
                    case RegistrationType.Traveller:
                        ProcessTravellerRegistration(existingTravellers);
                        break;
                    case RegistrationType.FrequentFlyer:
                        ProcessFrequentFlyerRegistration(existingTravellers);
                        break;
                    case RegistrationType.FlightManager:
                        ProcessFlightManagerRegistration(existingTravellers);
                        break;
                    default:
                        registrationUI.DisplayValidationErrors(new List<string> { "Invalid registration type selected" });
                        break;
                }
            }
            catch (Exception ex)
            {
                registrationUI.DisplayValidationErrors(new List<string> { ex.Message });
            }
        }

        private void ProcessTravellerRegistration(List<Traveller> existingTravellers)
        {
            var registrationData = registrationUI.CollectTravellerData(existingTravellers);
            var traveller = registrationService.RegisterTraveller(registrationData);
            registrationUI.DisplayTravellerRegistrationSuccess(traveller.Name);
        }

        private void ProcessFrequentFlyerRegistration(List<Traveller> existingTravellers)
        {
            var registrationData = registrationUI.CollectFrequentFlyerData(existingTravellers);
            var frequentFlyer = registrationService.RegisterFrequentFlyer(registrationData);
            registrationUI.DisplayFrequentFlyerRegistrationSuccess(frequentFlyer.Name);
        }

        private void ProcessFlightManagerRegistration(List<Traveller> existingTravellers)
        {
            var registrationData = registrationUI.CollectFlightManagerData(existingTravellers);
            var flightManager = registrationService.RegisterFlightManager(registrationData);
            registrationUI.DisplayFlightManagerRegistrationSuccess(flightManager.Name);
        }
    }
}
