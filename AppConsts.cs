using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Validation constants for input rules.
    /// </summary>
    public static class ValidationConsts
    {
        public const int MIN_AGE = 0;
        public const int MAX_AGE = 99;
        public const int MOBILE_LENGTH = 10;
        public const int MIN_PASSWORD_LENGTH = 8;

        public const int MIN_FREQUENT_FLYER_NUMBER = 100_000;
        public const int MAX_FREQUENT_FLYER_NUMBER = 999_999;
        public const int MIN_FREQUENT_FLYER_POINTS = 0;
        public const int MAX_FREQUENT_FLYER_POINTS = 1_000_000;

        public const int MIN_STAFF_ID = 1_000;
        public const int MAX_STAFF_ID = 9_000;

        // Flight and plane constraints
        public const int MIN_FLIGHT_ID = 100;
        public const int MAX_FLIGHT_ID = 900;
        public const int MIN_PLANE_ID = 0;
        public const int MAX_PLANE_ID = 9;

        // Seat constraints
        public const int MIN_SEAT_ROW = 1;
        public const int MAX_SEAT_ROW = 10;
        public const char MIN_SEAT_COLUMN = 'A';
        public const char MAX_SEAT_COLUMN = 'D';
    }

    /// <summary>
    /// Airline codes and names.
    /// </summary>
    public static class Airlines
    {
        public const string JETSTAR_CODE = "JST";
        public const string JETSTAR_NAME = "Jetstar";

        public const string QANTAS_CODE = "QFA";
        public const string QANTAS_NAME = "Qantas";

        public const string REGIONAL_EXPRESS_CODE = "RXA";
        public const string REGIONAL_EXPRESS_NAME = "Regional Express";

        public const string VIRGIN_CODE = "VOZ";
        public const string VIRGIN_NAME = "Virgin";

        public const string FLY_PELICAN_CODE = "FRE";
        public const string FLY_PELICAN_NAME = "Fly Pelican";
    }

    /// <summary>
    /// City names and associated points.
    /// </summary>
    public static class Cities
    {
        public const string SYDNEY_NAME = "Sydney";
        public const int SYDNEY_POINTS = 1200;

        public const string MELBOURNE_NAME = "Melbourne";
        public const int MELBOURNE_POINTS = 1750;

        public const string ROCKHAMPTON_NAME = "Rockhampton";
        public const int ROCKHAMPTON_POINTS = 1400;

        public const string ADELAIDE_NAME = "Adelaide";
        public const int ADELAIDE_POINTS = 1950;

        public const string PERTH_NAME = "Perth";
        public const int PERTH_POINTS = 3375;
    }

    /// <summary>
    /// Strings used by menus and prompts.
    /// </summary>
    public static class MenuText
    {
        public const string WELCOME_LINE = "==========================================\n" +
                                             "=  Welcome to Brisbane Domestic Airport  =\n" +
                                             "==========================================";

        public const string MAINMENU_PROMPT = "Please make a choice from the menu below:";
        public const string REGISTERMENU_PROMPT = "Which user type would you like to register?";
        public const string LOGIN_HEADING = "Login Menu.";

        public const string TRAVELLER_MENU_HEADING = "Traveller Menu.";
        public const string FF_MENU_HEADING = "Frequent Flyer Menu.";
        public const string FMANAGER_MENU_HEADING = "Flight Manager Menu.";
        
        // Common prompts
        public static string ChoicePrompt(int max) { return $"Please enter a choice between 1 and {max}:"; }
        
        // Registration messages
        public const string REGISTERING_TRAVELLER = "Registering as a traveller.";
        public const string REGISTERING_FREQUENT_FLYER = "Registering as a frequent flyer.";
        public const string REGISTERING_FLIGHT_MANAGER = "Registering as a flight manager.";
        
        // Success messages
        public const string TRAVELLER_REGISTRATION_SUCCESS = "Congratulations {0}. You have registered as a traveller.";
        public const string FREQUENT_FLYER_REGISTRATION_SUCCESS = "Congratulations {0}. You have registered as a frequent flyer.";
        public const string FLIGHT_MANAGER_REGISTRATION_SUCCESS = "Congratulations {0}. You have registered as a flight manager.";
        
        // Input prompts
        public const string ENTER_NAME = "Please enter in your name:";
        public const string ENTER_AGE = "Please enter in your age between {0} and {1}:";
        public const string ENTER_MOBILE = "Please enter in your mobile number:";
        public const string ENTER_EMAIL = "Please enter in your email:";
        public const string ENTER_PASSWORD = "Please enter in your password:";
        public const string ENTER_CURRENT_PASSWORD = "Please enter your current password.";
        public const string ENTER_NEW_PASSWORD = "Please enter your new password.";
        
        // Flight prompts
        public const string ENTER_AIRLINE = "Please enter the airline:";
        public const string ENTER_ARRIVAL_CITY = "Please enter the arrival city:";
        public const string ENTER_DEPARTING_CITY = "Please enter the departing city:";
        public const string ENTER_FLIGHT_ID = "Please enter in your flight id between {0} and {1}:";
        public const string ENTER_PLANE_ID = "Please enter in your plane id between {0} and {1}:";
        public const string ENTER_ARRIVAL_DATETIME = "Please enter in the arrival date and time in the format HH:mm dd/MM/yyyy:";
        public const string ENTER_DEPARTURE_DATETIME = "Please enter in the departure date and time in the format HH:mm dd/MM/yyyy:";
        public const string ENTER_SEAT_ROW = "Please enter in your seat row between {0} and {1}:";
        public const string ENTER_SEAT_COLUMN = "Please enter in your seat column between {0} and {1}:";
        public const string ENTER_DELAY_MINUTES = "Please enter in your minutes delayed:";
        
        // Error messages
        public const string ERROR_PREFIX = "#####\n# Error - {0}.\n#####";
        public const string ERROR_TRY_AGAIN = "#####\n# Error - {0}.\n# Please try again.\n#####";
        
        // Common error messages
        public const string INVALID_NAME = "Supplied name is invalid";
        public const string INVALID_AGE = "Supplied age is invalid";
        public const string INVALID_MOBILE = "Supplied mobile number is invalid";
        public const string INVALID_EMAIL = "Supplied email is invalid";
        public const string INVALID_PASSWORD = "Supplied password is invalid";
        public const string INVALID_SEAT_ROW = "Supplied seat row is invalid";
        public const string INVALID_SEAT_COLUMN = "Supplied seat column is invalid";
        public const string INVALID_DELAY = "Supplied delay is invalid";
        public const string INVALID_VALUE = "Supplied value is invalid";
        public const string OUT_OF_RANGE = "Supplied value is out of range";
        
        // Booking messages
        public const string ALREADY_HAVE_ARRIVAL = "You already have an arrival flight. You can not book another";
        public const string ALREADY_HAVE_DEPARTURE = "You already have a departure flight. You can not book another";
        public const string NO_ARRIVAL_FLIGHTS = "There are no arrival flights available.";
        public const string NO_DEPARTURE_FLIGHTS = "There are no departure flights available.";
        public const string SEAT_OCCUPIED = "Seat is already occupied";
        public const string ARRIVAL_BEFORE_DEPARTURE = "The arrival time must be before the departure time";
        public const string DEPARTURE_AFTER_ARRIVAL = "The departure time must be after the arrival time";
        
        // Flight management
        public const string PLANE_ALREADY_ASSIGNED_ARRIVAL = "Plane {0} has already been assigned to an arrival flight";
        public const string PLANE_ALREADY_ASSIGNED_DEPARTURE = "Plane {0} has already been assigned to a departure flight";
        public const string FLIGHT_ADDED = "Flight {0} on plane {1} has been added to the system.";
        public const string NO_ARRIVAL_FLIGHTS_TO_DELAY = "The airport does not have any arrival flights.";
        public const string NO_DEPARTURE_FLIGHTS_TO_DELAY = "The airport does not have any departure flights.";
        
        // Password requirements
        public const string PASSWORD_REQUIREMENTS = "Your password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter";
    }
}


