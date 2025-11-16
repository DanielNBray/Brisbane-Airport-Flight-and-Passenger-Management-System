using System;
using System.Collections.Generic;
using System.Linq;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// User store contract for persistence operations.
    /// Interface is colocated with in-memory implementation per project guidance.
    /// </summary>
    public interface IUserStore
    {
        /// <summary>
        /// Gets all users in the store.
        /// </summary>
        /// <returns>Collection of all users</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Gets all travellers in the store.
        /// </summary>
        /// <returns>Collection of all travellers</returns>
        IEnumerable<Traveller> GetTravellers();

        /// <summary>
        /// Gets all frequent flyers in the store.
        /// </summary>
        /// <returns>Collection of all frequent flyers</returns>
        IEnumerable<FrequentFlyer> GetFrequentFlyers();

        /// <summary>
        /// Gets all flight managers in the store.
        /// </summary>
        /// <returns>Collection of all flight managers</returns>
        IEnumerable<FlightManager> GetFlightManagers();

        /// <summary>
        /// Finds a traveller by email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The traveller if found, null otherwise</returns>
        Traveller? FindTravellerByEmail(string email);

        /// <summary>
        /// Finds a frequent flyer by email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The frequent flyer if found, null otherwise</returns>
        FrequentFlyer? FindFrequentFlyerByEmail(string email);

        /// <summary>
        /// Finds a flight manager by email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The flight manager if found, null otherwise</returns>
        FlightManager? FindFlightManagerByEmail(string email);

        /// <summary>
        /// Adds a traveller to the store.
        /// </summary>
        /// <param name="traveller">The traveller to add</param>
        void AddTraveller(Traveller traveller);

        /// <summary>
        /// Adds a frequent flyer to the store.
        /// </summary>
        /// <param name="frequentFlyer">The frequent flyer to add</param>
        void AddFrequentFlyer(FrequentFlyer frequentFlyer);

        /// <summary>
        /// Adds a flight manager to the store.
        /// </summary>
        /// <param name="flightManager">The flight manager to add</param>
        void AddFlightManager(FlightManager flightManager);
    }

    /// <summary>
    /// In-memory implementation of the user store.
    /// </summary>
    public sealed class InMemoryUserStore : IUserStore
    {
        private readonly List<Traveller> travellers = new List<Traveller>();
        private readonly List<FrequentFlyer> frequentFlyers = new List<FrequentFlyer>();
        private readonly List<FlightManager> flightManagers = new List<FlightManager>();

        /// <summary>
        /// Gets all users in the store.
        /// </summary>
        /// <returns>Collection of all users</returns>
        public IEnumerable<User> GetAll()
        {
            foreach (Traveller traveller in travellers) { yield return traveller; }
            foreach (FrequentFlyer frequentFlyer in frequentFlyers) { yield return frequentFlyer; }
            foreach (FlightManager flightManager in flightManagers) { yield return flightManager; }
        }

        /// <summary>
        /// Gets all travellers in the store.
        /// </summary>
        /// <returns>Collection of all travellers</returns>
        public IEnumerable<Traveller> GetTravellers()
        {
            return travellers;
        }

        /// <summary>
        /// Gets all frequent flyers in the store.
        /// </summary>
        /// <returns>Collection of all frequent flyers</returns>
        public IEnumerable<FrequentFlyer> GetFrequentFlyers()
        {
            return frequentFlyers;
        }

        /// <summary>
        /// Gets all flight managers in the store.
        /// </summary>
        /// <returns>Collection of all flight managers</returns>
        public IEnumerable<FlightManager> GetFlightManagers()
        {
            return flightManagers;
        }

        /// <summary>
        /// Finds a traveller by email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The traveller if found, null otherwise</returns>
        public Traveller? FindTravellerByEmail(string email)
        {
            foreach (Traveller traveller in travellers)
            {
                if (string.Equals(traveller.Email, email, StringComparison.OrdinalIgnoreCase))
                {
                    return traveller;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a frequent flyer by email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The frequent flyer if found, null otherwise</returns>
        public FrequentFlyer? FindFrequentFlyerByEmail(string email)
        {
            foreach (FrequentFlyer frequentFlyer in frequentFlyers)
            {
                if (string.Equals(frequentFlyer.Email, email, StringComparison.OrdinalIgnoreCase))
                {
                    return frequentFlyer;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a flight manager by email address.
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The flight manager if found, null otherwise</returns>
        public FlightManager? FindFlightManagerByEmail(string email)
        {
            foreach (FlightManager flightManager in flightManagers)
            {
                if (string.Equals(flightManager.Email, email, StringComparison.OrdinalIgnoreCase))
                {
                    return flightManager;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds a traveller to the store.
        /// </summary>
        /// <param name="traveller">The traveller to add</param>
        public void AddTraveller(Traveller traveller)
        {
            travellers.Add(traveller);
        }

        /// <summary>
        /// Adds a frequent flyer to the store.
        /// </summary>
        /// <param name="frequentFlyer">The frequent flyer to add</param>
        public void AddFrequentFlyer(FrequentFlyer frequentFlyer)
        {
            frequentFlyers.Add(frequentFlyer);
        }

        /// <summary>
        /// Adds a flight manager to the store.
        /// </summary>
        /// <param name="flightManager">The flight manager to add</param>
        public void AddFlightManager(FlightManager flightManager)
        {
            flightManagers.Add(flightManager);
        }
    }
}


