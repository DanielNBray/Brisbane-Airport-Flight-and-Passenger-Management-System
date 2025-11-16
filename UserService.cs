using System;
using System.Linq;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// User service contract for domain operations.
    /// Interface is colocated with the concrete UserService per guidance.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Checks if an email address is already registered.
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <returns>True if email exists, false otherwise</returns>
        bool EmailExists(string email);

        /// <summary>
        /// Gets a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The user if found, null otherwise</returns>
        User? GetUserByEmail(string email);

        /// <summary>
        /// Authenticates a user with the provided password.
        /// </summary>
        /// <param name="user">The user to authenticate</param>
        /// <param name="password">The password to verify</param>
        /// <returns>True if authentication successful, false otherwise</returns>
        bool Authenticate(User user, string password);

        /// <summary>
        /// Registers a new traveller.
        /// </summary>
        /// <param name="name">The traveller's name</param>
        /// <param name="age">The traveller's age</param>
        /// <param name="mobile">The traveller's mobile number</param>
        /// <param name="email">The traveller's email address</param>
        /// <param name="password">The traveller's password</param>
        /// <returns>The registered traveller</returns>
        Traveller RegisterTraveller(string name, int age, string mobile, string email, string password);

        /// <summary>
        /// Registers a new frequent flyer.
        /// </summary>
        /// <param name="name">The frequent flyer's name</param>
        /// <param name="age">The frequent flyer's age</param>
        /// <param name="mobile">The frequent flyer's mobile number</param>
        /// <param name="email">The frequent flyer's email address</param>
        /// <param name="password">The frequent flyer's password</param>
        /// <param name="ffNumber">The frequent flyer's number</param>
        /// <param name="points">The frequent flyer's points</param>
        /// <returns>The registered frequent flyer</returns>
        FrequentFlyer RegisterFrequentFlyer(string name, int age, string mobile, string email, string password, int ffNumber, int points);

        /// <summary>
        /// Registers a new flight manager.
        /// </summary>
        /// <param name="name">The flight manager's name</param>
        /// <param name="age">The flight manager's age</param>
        /// <param name="mobile">The flight manager's mobile number</param>
        /// <param name="email">The flight manager's email address</param>
        /// <param name="password">The flight manager's password</param>
        /// <param name="staffId">The flight manager's staff ID</param>
        /// <returns>The registered flight manager</returns>
        FlightManager RegisterFlightManager(string name, int age, string mobile, string email, string password, int staffId);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="user">The user whose password to change</param>
        /// <param name="newPassword">The new password</param>
        void ChangePassword(User user, string newPassword);

        /// <summary>
        /// Checks if there are any users in the system.
        /// </summary>
        /// <returns>True if there are users, false otherwise</returns>
        bool HasAnyUsers();

        /// <summary>
        /// Gets a snapshot of all travellers.
        /// </summary>
        /// <returns>Collection of all travellers</returns>
        System.Collections.Generic.IEnumerable<Traveller> GetTravellersSnapshot();

        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        /// <returns>Collection of all users</returns>
        System.Collections.Generic.IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Finds the user who is currently occupying a specific seat on a flight.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="seat">The seat to check (format: "row:column")</param>
        /// <returns>The user occupying the seat, or null if not found</returns>
        User? FindUserBySeat(string flightCode, string seat);
    }

    /// <summary>
    /// Application-layer user service orchestrating validation and repository operations.
    /// </summary>
    public sealed class UserService : IUserService
    {
        private readonly IUserStore userStore;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="userStore">The user store for persistence operations</param>
        public UserService(IUserStore userStore)
        {
            this.userStore = userStore;
        }

        /// <summary>
        /// Checks if an email address is already registered.
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <returns>True if email exists, false otherwise</returns>
        public bool EmailExists(string email)
        {
            return userStore.GetAll().Any(user => string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The user if found, null otherwise</returns>
        public User? GetUserByEmail(string email)
        {
            FlightManager flightManager = userStore.FindFlightManagerByEmail(email);
            if (flightManager != null)
            {
                return flightManager;
            }
            FrequentFlyer frequentFlyer = userStore.FindFrequentFlyerByEmail(email);
            if (frequentFlyer != null)
            {
                return frequentFlyer;
            }
            Traveller traveller = userStore.FindTravellerByEmail(email);
            return traveller;
        }

        /// <summary>
        /// Authenticates a user with the provided password.
        /// </summary>
        /// <param name="user">The user to authenticate</param>
        /// <param name="password">The password to verify</param>
        /// <returns>True if authentication successful, false otherwise</returns>
        public bool Authenticate(User user, string password)
        {
            if (user == null)
            {
                return false;
            }
            return user.VerifyPassword(password);
        }

        /// <summary>
        /// Registers a new traveller.
        /// </summary>
        /// <param name="name">The traveller's name</param>
        /// <param name="age">The traveller's age</param>
        /// <param name="mobile">The traveller's mobile number</param>
        /// <param name="email">The traveller's email address</param>
        /// <param name="password">The traveller's password</param>
        /// <returns>The registered traveller</returns>
        public Traveller RegisterTraveller(string name, int age, string mobile, string email, string password)
        {
            // Assume caller (controller/UI) has validated inputs and uniqueness
            Traveller traveller = new Traveller(name, age, mobile, email, password);
            userStore.AddTraveller(traveller);
            return traveller;
        }

        /// <summary>
        /// Registers a new frequent flyer.
        /// </summary>
        /// <param name="name">The frequent flyer's name</param>
        /// <param name="age">The frequent flyer's age</param>
        /// <param name="mobile">The frequent flyer's mobile number</param>
        /// <param name="email">The frequent flyer's email address</param>
        /// <param name="password">The frequent flyer's password</param>
        /// <param name="ffNumber">The frequent flyer's number</param>
        /// <param name="points">The frequent flyer's points</param>
        /// <returns>The registered frequent flyer</returns>
        public FrequentFlyer RegisterFrequentFlyer(string name, int age, string mobile, string email, string password, int ffNumber, int points)
        {
            // Assume caller (controller/UI) has validated inputs and uniqueness
            FrequentFlyer frequentFlyer = new FrequentFlyer(name, age, mobile, email, password, ffNumber, points);
            userStore.AddFrequentFlyer(frequentFlyer);
            return frequentFlyer;
        }

        /// <summary>
        /// Registers a new flight manager.
        /// </summary>
        /// <param name="name">The flight manager's name</param>
        /// <param name="age">The flight manager's age</param>
        /// <param name="mobile">The flight manager's mobile number</param>
        /// <param name="email">The flight manager's email address</param>
        /// <param name="password">The flight manager's password</param>
        /// <param name="staffId">The flight manager's staff ID</param>
        /// <returns>The registered flight manager</returns>
        public FlightManager RegisterFlightManager(string name, int age, string mobile, string email, string password, int staffId)
        {
            FlightManager flightManager = new FlightManager(name, age, mobile, email, password, staffId.ToString());
            userStore.AddFlightManager(flightManager);
            return flightManager;
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="user">The user whose password to change</param>
        /// <param name="newPassword">The new password</param>
        public void ChangePassword(User user, string newPassword)
        {
            // Assume caller validated password format
            user.SetPassword(newPassword);
        }

        /// <summary>
        /// Checks if there are any users in the system.
        /// </summary>
        /// <returns>True if there are users, false otherwise</returns>
        public bool HasAnyUsers()
        {
            return userStore.GetAll().Any();
        }

        /// <summary>
        /// Gets a snapshot of all travellers.
        /// </summary>
        /// <returns>Collection of all travellers</returns>
        public System.Collections.Generic.IEnumerable<Traveller> GetTravellersSnapshot()
        {
            return userStore.GetTravellers();
        }

        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        /// <returns>Collection of all users</returns>
        public System.Collections.Generic.IEnumerable<User> GetAllUsers()
        {
            return userStore.GetAll();
        }

        /// <summary>
        /// Finds the user who is currently occupying a specific seat on a flight.
        /// </summary>
        /// <param name="flightCode">The flight code</param>
        /// <param name="seat">The seat to check (format: "row:column")</param>
        /// <returns>The user occupying the seat, or null if not found</returns>
        public User? FindUserBySeat(string flightCode, string seat)
        {
            foreach (User user in userStore.GetAll())
            {
                // Check arrival flight
                if (user.BookedArrivalFlight != null &&
                    user.BookedArrivalFlight.FlightCode() == flightCode &&
                    user.ArrivalSeat == seat)
                {
                    return user;
                }

                // Check departure flight
                if (user.BookedDepartureFlight != null &&
                    user.BookedDepartureFlight.FlightCode() == flightCode &&
                    user.DepartureSeat == seat)
                {
                    return user;
                }
            }
            return null;
        }
    }
}


