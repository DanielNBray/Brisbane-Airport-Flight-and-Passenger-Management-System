using System;
using System.Collections.Generic;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Provides validation methods for all user inputs.
    /// </summary>
    public static class InputValidator
    {
        private static readonly IdentityValidator _identityValidator = new IdentityValidator();
        private static readonly LoyaltyValidator _loyaltyValidator = new LoyaltyValidator();
        private static readonly StaffValidator _staffValidator = new StaffValidator();
        private static readonly SeatingValidator _seatingValidator = new SeatingValidator();
        private static readonly FlightValidator _flightValidator = new FlightValidator();
        private static readonly UniquenessValidator _uniquenessValidator = new UniquenessValidator();
        /// <summary>
        /// Validates a name input.
        /// </summary>
        /// <param name="name">The name to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidName(string name)
        {
            return _identityValidator.IsValidName(name);
        }

        /// <summary>
        /// Validates an age input.
        /// </summary>
        /// <param name="ageStr">The age string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidAge(string ageStr)
        {
            return _identityValidator.IsValidAge(ageStr);
        }

        /// <summary>
        /// Validates a mobile number input.
        /// </summary>
        /// <param name="mobile">The mobile number to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidMobile(string mobile)
        {
            return _identityValidator.IsValidMobile(mobile);
        }

        /// <summary>
        /// Validates an email input.
        /// </summary>
        /// <param name="email">The email to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidEmail(string email)
        {
            return _identityValidator.IsValidEmail(email);
        }

        /// <summary>
        /// Validates a password input.
        /// </summary>
        /// <param name="password">The password to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidPassword(string password)
        {
            return _identityValidator.IsValidPassword(password);
        }

        /// <summary>
        /// Validates a frequent flyer number input.
        /// </summary>
        /// <param name="ffNumberStr">The frequent flyer number string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidFrequentFlyerNumber(string ffNumberStr)
        {
            return _loyaltyValidator.IsValidFrequentFlyerNumber(ffNumberStr);
        }

        /// <summary>
        /// Validates frequent flyer points input.
        /// </summary>
        /// <param name="pointsStr">The points string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidFrequentFlyerPoints(string pointsStr)
        {
            return _loyaltyValidator.IsValidFrequentFlyerPoints(pointsStr);
        }

        /// <summary>
        /// Validates a staff ID input.
        /// </summary>
        /// <param name="staffIdStr">The staff ID string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidStaffId(string staffIdStr)
        {
            return _staffValidator.IsValidStaffId(staffIdStr);
        }

        /// <summary>
        /// Checks if an email is already in use by existing travellers.
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <param name="existingTravellers">List of existing travellers</param>
        /// <returns>True if email is already in use, false otherwise</returns>
        public static bool IsEmailAlreadyInUse(string email, List<Traveller> existingTravellers)
        {
            return _uniquenessValidator.IsEmailAlreadyInUse(email, existingTravellers);
        }

        /// <summary>
        /// Validates a seat row input.
        /// </summary>
        /// <param name="seatRowStr">The seat row string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidSeatRow(string seatRowStr)
        {
            return _seatingValidator.IsValidSeatRow(seatRowStr);
        }

        /// <summary>
        /// Validates a seat column input.
        /// </summary>
        /// <param name="seatColumn">The seat column to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidSeatColumn(string seatColumn)
        {
            return _seatingValidator.IsValidSeatColumn(seatColumn);
        }

        /// <summary>
        /// Validates a flight ID input.
        /// </summary>
        /// <param name="flightIdStr">The flight ID string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidFlightId(string flightIdStr)
        {
            return _flightValidator.IsValidFlightId(flightIdStr);
        }

        /// <summary>
        /// Validates a plane ID input.
        /// </summary>
        /// <param name="planeIdStr">The plane ID string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidPlaneId(string planeIdStr)
        {
            return _flightValidator.IsValidPlaneId(planeIdStr);
        }
    }


    /// <summary>
    /// Internal validator for identity-related fields.
    /// </summary>
    internal sealed class IdentityValidator
    {
        /// <summary>
        /// Validates a name input.
        /// </summary>
        /// <param name="name">The name to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            // Check if contains at least one letter
            bool hasLetter = false;
            foreach (char character in name)
            {
                if (char.IsLetter(character))
                {
                    hasLetter = true;
                    break;
                }
            }

            if (!hasLetter)
            {
                return false;
            }

            // Check if all characters are valid (letters, spaces, apostrophes, hyphens)
            foreach (char character in name)
            {
                if (!char.IsLetter(character) && character != ' ' && character != '\'' && character != '-')
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates an age input.
        /// </summary>
        /// <param name="ageStr">The age string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidAge(string ageStr)
        {
            if (!int.TryParse(ageStr, out int age))
            {
                return false;
            }

            return age >= ValidationConsts.MIN_AGE && age <= ValidationConsts.MAX_AGE;
        }

        /// <summary>
        /// Validates a mobile number input.
        /// </summary>
        /// <param name="mobile">The mobile number to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return false;
            }

            if (mobile.Length != ValidationConsts.MOBILE_LENGTH)
            {
                return false;
            }

            if (mobile[0] != '0')
            {
                return false;
            }

            foreach (char character in mobile)
            {
                if (!char.IsDigit(character))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates an email input.
        /// </summary>
        /// <param name="email">The email to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            int atCount = 0;
            foreach (char character in email)
            {
                if (character == '@')
                {
                    atCount++;
                }
            }

            if (atCount != 1)
            {
                return false;
            }

            int atIndex = email.IndexOf('@');
            if (atIndex == 0 || atIndex == email.Length - 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a password input.
        /// </summary>
        /// <param name="password">The password to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < ValidationConsts.MIN_PASSWORD_LENGTH)
            {
                return false;
            }

            bool hasNumber = false;
            bool hasLower = false;
            bool hasUpper = false;

            foreach (char character in password)
            {
                if (char.IsDigit(character))
                {
                    hasNumber = true;
                }
                else if (char.IsLower(character))
                {
                    hasLower = true;
                }
                else if (char.IsUpper(character))
                {
                    hasUpper = true;
                }
            }

            return hasNumber && hasLower && hasUpper;
        }
    }

    /// <summary>
    /// Internal validator for loyalty program related fields.
    /// </summary>
    internal sealed class LoyaltyValidator
    {
        /// <summary>
        /// Validates a frequent flyer number input.
        /// </summary>
        /// <param name="ffNumberStr">The frequent flyer number string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidFrequentFlyerNumber(string ffNumberStr)
        {
            if (!int.TryParse(ffNumberStr, out int ffNumber))
            {
                return false;
            }

            return ffNumber >= ValidationConsts.MIN_FREQUENT_FLYER_NUMBER && ffNumber <= ValidationConsts.MAX_FREQUENT_FLYER_NUMBER;
        }

        /// <summary>
        /// Validates frequent flyer points input.
        /// </summary>
        /// <param name="pointsStr">The points string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidFrequentFlyerPoints(string pointsStr)
        {
            if (!int.TryParse(pointsStr, out int points))
            {
                return false;
            }

            return points >= ValidationConsts.MIN_FREQUENT_FLYER_POINTS && points <= ValidationConsts.MAX_FREQUENT_FLYER_POINTS;
        }
    }

    /// <summary>
    /// Internal validator for staff-related fields.
    /// </summary>
    internal sealed class StaffValidator
    {
        /// <summary>
        /// Validates a staff ID input.
        /// </summary>
        /// <param name="staffIdStr">The staff ID string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidStaffId(string staffIdStr)
        {
            if (!int.TryParse(staffIdStr, out int staffId))
            {
                return false;
            }

            return staffId >= ValidationConsts.MIN_STAFF_ID && staffId <= ValidationConsts.MAX_STAFF_ID;
        }
    }

    /// <summary>
    /// Internal validator for seating-related fields.
    /// </summary>
    internal sealed class SeatingValidator
    {
        /// <summary>
        /// Validates a seat row input.
        /// </summary>
        /// <param name="seatRowStr">The seat row string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidSeatRow(string seatRowStr)
        {
            if (!int.TryParse(seatRowStr, out int seatRow))
            {
                return false;
            }
            return seatRow >= ValidationConsts.MIN_SEAT_ROW && seatRow <= ValidationConsts.MAX_SEAT_ROW;
        }

        /// <summary>
        /// Validates a seat column input.
        /// </summary>
        /// <param name="seatColumn">The seat column to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidSeatColumn(string seatColumn)
        {
            if (string.IsNullOrEmpty(seatColumn) || seatColumn.Length != 1)
            {
                return false;
            }
            char column = seatColumn[0];
            return column >= ValidationConsts.MIN_SEAT_COLUMN && column <= ValidationConsts.MAX_SEAT_COLUMN;
        }
    }

    /// <summary>
    /// Internal validator for flight-related fields.
    /// </summary>
    internal sealed class FlightValidator
    {
        /// <summary>
        /// Validates a flight ID input.
        /// </summary>
        /// <param name="flightIdStr">The flight ID string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidFlightId(string flightIdStr)
        {
            if (!int.TryParse(flightIdStr, out int flightId))
            {
                return false;
            }
            return flightId >= ValidationConsts.MIN_FLIGHT_ID && flightId <= ValidationConsts.MAX_FLIGHT_ID;
        }

        /// <summary>
        /// Validates a plane ID input.
        /// </summary>
        /// <param name="planeIdStr">The plane ID string to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidPlaneId(string planeIdStr)
        {
            if (!int.TryParse(planeIdStr, out int planeId))
            {
                return false;
            }
            return planeId >= ValidationConsts.MIN_PLANE_ID && planeId <= ValidationConsts.MAX_PLANE_ID;
        }
    }

    /// <summary>
    /// Internal validator for uniqueness-related checks.
    /// </summary>
    internal sealed class UniquenessValidator
    {
        /// <summary>
        /// Checks if an email is already in use by existing travellers.
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <param name="existingTravellers">List of existing travellers</param>
        /// <returns>True if email is already in use, false otherwise</returns>
        public bool IsEmailAlreadyInUse(string email, List<Traveller> existingTravellers)
        {
            foreach (Traveller traveller in existingTravellers)
            {
                if (traveller.Email.ToLower() == email.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }

}
