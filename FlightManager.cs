using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Represents a flight manager at Brisbane Airport with staff privileges.
    /// </summary>
    public class FlightManager : User
    {
        private readonly string staffId;

        /// <summary>
        /// Initializes a new instance of the FlightManager class.
        /// </summary>
        /// <param name="name">The flight manager's name</param>
        /// <param name="age">The flight manager's age</param>
        /// <param name="mobile">The flight manager's mobile number</param>
        /// <param name="email">The flight manager's email address</param>
        /// <param name="password">The flight manager's password</param>
        /// <param name="staffId">The flight manager's staff ID</param>
        public FlightManager(string name, int age, string mobile, string email, string password, string staffId) : base(name, age, mobile, email, password)
        {
            this.staffId = staffId;
        }

        /// <summary>
        /// Gets the flight manager's staff ID.
        /// </summary>
        /// <returns>The flight manager's staff ID</returns>
        public string GetStaffId()
        {
            return this.staffId;
        }

        /// <summary>
        /// Returns a string representation of the flight manager's details.
        /// </summary>
        /// <returns>A formatted string containing flight manager information</returns>
        public override string ToString()
        {
            return $"Your details.\n" +
                $"Name: {Name} \n" +
                $"Age: {Age} \n" +
                $"Mobile phone number: {Mobile} \n" +
                $"Email: {Email}\n" +
                $"Staff ID: {staffId}";
        }
    }
}
