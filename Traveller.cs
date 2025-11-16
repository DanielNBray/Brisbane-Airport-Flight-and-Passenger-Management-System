using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Represents a standard traveller at Brisbane Airport.
    /// </summary>
    public class Traveller : User
    {
        /// <summary>
        /// Initializes a new instance of the Traveller class.
        /// </summary>
        /// <param name="name">The traveller's name</param>
        /// <param name="age">The traveller's age</param>
        /// <param name="mobile">The traveller's mobile number</param>
        /// <param name="email">The traveller's email address</param>
        /// <param name="password">The traveller's password</param>
        public Traveller(string name, int age, string mobile, string email, string password) : base(name, age, mobile, email, password)
        {
        }

        /// <summary>
        /// Returns a string representation of the traveller's details.
        /// </summary>
        /// <returns>A formatted string containing traveller information</returns>
        public override string ToString()
        {
            return $"Your details.\n " +
                $"Name: {Name} \n" +
                $"Age: {Age} \n" +
                $"Mobile phone number: {Mobile} \n" +
                $"Email: {Email}";
        }
    }
}



