using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Base class representing a user in the airport system.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the user's age.
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets the user's mobile phone number.
        /// </summary>
        public string Mobile { get; private set; }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        public string Email { get; private set; }

        private string Password { get; set; }

        /// <summary>
        /// Gets the user's booked arrival flight.
        /// </summary>
        public FlightRecord? BookedArrivalFlight { get; private set; }

        /// <summary>
        /// Gets the user's booked departure flight.
        /// </summary>
        public FlightRecord? BookedDepartureFlight { get; private set; }

        /// <summary>
        /// Gets the user's arrival seat assignment.
        /// </summary>
        public string? ArrivalSeat { get; private set; }

        /// <summary>
        /// Gets the user's departure seat assignment.
        /// </summary>
        public string? DepartureSeat { get; private set; }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        /// <param name="name">The user's name</param>
        /// <param name="age">The user's age</param>
        /// <param name="mobile">The user's mobile phone number</param>
        /// <param name="email">The user's email address</param>
        /// <param name="password">The user's password</param>
        protected User(string name, int age, string mobile, string email, string password)
        {
            this.Name = name;
            this.Age = age;
            this.Mobile = mobile;
            this.Email = email;
            this.Password = password;
        }

        /// <summary>
        /// Sets the user's password.
        /// </summary>
        /// <param name="newPassword">The new password</param>
        internal void SetPassword(string newPassword)
        {
            this.Password = newPassword;
        }

        /// <summary>
        /// Verifies if the provided password matches the user's password
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <returns>True if password matches, false otherwise</returns>
        public bool VerifyPassword(string password)
        {
            return this.Password == password;
        }

        /// <summary>
        /// Books an arrival flight with seat assignment.
        /// </summary>
        public void BookArrivalFlight(FlightRecord flight, string seat)
        {
            BookedArrivalFlight = flight;
            ArrivalSeat = seat;
        }

        /// <summary>
        /// Books a departure flight with seat assignment.
        /// </summary>
        public void BookDepartureFlight(FlightRecord flight, string seat)
        {
            BookedDepartureFlight = flight;
            DepartureSeat = seat;
        }

        /// <summary>
        /// Updates the arrival seat - internal use for seat reassignment
        /// </summary>
        internal void UpdateArrivalSeat(string newSeat)
        {
            ArrivalSeat = newSeat;
        }

        /// <summary>
        /// Updates the departure seat - internal use for seat reassignment
        /// </summary>
        internal void UpdateDepartureSeat(string newSeat)
        {
            DepartureSeat = newSeat;
        }

        /// <summary>
        /// Checks if the user has an arrival flight booked
        /// </summary>
        /// <returns>True if user has an arrival flight, false otherwise</returns>
        public bool HasArrivalFlight()
        {
            return BookedArrivalFlight != null;
        }

        /// <summary>
        /// Checks if the user has a departure flight booked
        /// </summary>
        /// <returns>True if user has a departure flight, false otherwise</returns>
        public bool HasDepartureFlight()
        {
            return BookedDepartureFlight != null;
        }

        /// <summary>
        /// Gets the arrival flight time if booked
        /// </summary>
        /// <returns>Arrival flight time or null if not booked</returns>
        public DateTime? GetArrivalFlightTime()
        {
            return BookedArrivalFlight?.When;
        }

        /// <summary>
        /// Gets the departure flight time if booked
        /// </summary>
        /// <returns>Departure flight time or null if not booked</returns>
        public DateTime? GetDepartureFlightTime()
        {
            return BookedDepartureFlight?.When;
        }

        /// <summary>
        /// Gets the arrival seat if booked
        /// </summary>
        /// <returns>Arrival seat or null if not booked</returns>
        public string? GetArrivalSeat()
        {
            return ArrivalSeat;
        }

        /// <summary>
        /// Gets the departure seat if booked
        /// </summary>
        /// <returns>Departure seat or null if not booked</returns>
        public string? GetDepartureSeat()
        {
            return DepartureSeat;
        }

        /// <summary>
        /// Gets the arrival flight code if booked
        /// </summary>
        /// <returns>Arrival flight code or null if not booked</returns>
        public string? GetArrivalFlightCode()
        {
            return BookedArrivalFlight?.FlightCode();
        }

        /// <summary>
        /// Gets the departure flight code if booked
        /// </summary>
        /// <returns>Departure flight code or null if not booked</returns>
        public string? GetDepartureFlightCode()
        {
            return BookedDepartureFlight?.FlightCode();
        }
    }
}
