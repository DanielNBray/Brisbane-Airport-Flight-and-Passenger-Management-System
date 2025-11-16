using System;

namespace DanielBray_AirportA2
{
    /// <summary>
    /// Represents a frequent flyer at Brisbane Airport with loyalty program features.
    /// </summary>
    public class FrequentFlyer : Traveller
    {
        private readonly int frequentFlyerNumber;
        private int points;

        /// <summary>
        /// Initializes a new instance of the FrequentFlyer class.
        /// </summary>
        /// <param name="name">The frequent flyer's name</param>
        /// <param name="age">The frequent flyer's age</param>
        /// <param name="mobile">The frequent flyer's mobile number</param>
        /// <param name="email">The frequent flyer's email address</param>
        /// <param name="password">The frequent flyer's password</param>
        /// <param name="frequentFlyerNumber">The frequent flyer's number</param>
        /// <param name="points">The frequent flyer's points</param>
        public FrequentFlyer(string name, int age, string mobile, string email, string password, int frequentFlyerNumber, int points)
            : base(name, age, mobile, email, password)
        {
            this.frequentFlyerNumber = frequentFlyerNumber;
            this.points = points;
        }

        /// <summary>
        /// Gets the frequent flyer's number.
        /// </summary>
        /// <returns>The frequent flyer's number</returns>
        public int GetFrequentFlyerNumber()
        {
            return this.frequentFlyerNumber;
        }

        /// <summary>
        /// Gets the frequent flyer's current points.
        /// </summary>
        /// <returns>The frequent flyer's points</returns>
        public int GetPoints()
        {
            return this.points;
        }

        /// <summary>
        /// Sets the frequent flyer's points.
        /// </summary>
        /// <param name="newPoints">The new points value</param>
        public void SetPoints(int newPoints)
        {
            this.points = newPoints;
        }

        /// <summary>
        /// Calculates points earned from a flight based on distance and airline.
        /// </summary>
        /// <param name="flight">The flight to calculate points for</param>
        /// <returns>Points earned from the flight</returns>
        public int CalculateFlightPoints(FlightRecord flight)
        {
            int basePoints = GetCityBasePoints(flight.CityName);
            int multiplier = GetAirlineMultiplier(flight.AirlineCode);
            return basePoints * multiplier;
        }

        /// <summary>
        /// Gets base points for a city based on distance from Brisbane.
        /// </summary>
        private int GetCityBasePoints(string cityName)
        {
            switch (cityName)
            {
                case Cities.SYDNEY_NAME: return Cities.SYDNEY_POINTS;
                case Cities.MELBOURNE_NAME: return Cities.MELBOURNE_POINTS;
                case Cities.ROCKHAMPTON_NAME: return Cities.ROCKHAMPTON_POINTS;
                case Cities.ADELAIDE_NAME: return Cities.ADELAIDE_POINTS;
                case Cities.PERTH_NAME: return Cities.PERTH_POINTS;
                default: return 1000;
            }
        }

        /// <summary>
        /// Gets airline multiplier for points calculation.
        /// </summary>
        private int GetAirlineMultiplier(string airlineCode)
        {
            switch (airlineCode)
            {
                case Airlines.JETSTAR_CODE: return 1;
                case Airlines.QANTAS_CODE: return 1;
                case Airlines.REGIONAL_EXPRESS_CODE: return 1;
                case Airlines.VIRGIN_CODE: return 1;
                case Airlines.FLY_PELICAN_CODE: return 1;
                default: return 1;
            }
        }


        /// <summary>
        /// Calculates total points from all booked flights.
        /// </summary>
        /// <returns>Total points from all flights</returns>
        public int CalculateTotalFlightPoints()
        {
            int totalPoints = 0;
            
            if (HasArrivalFlight())
            {
                totalPoints += CalculateFlightPoints(BookedArrivalFlight!);
            }
            
            if (HasDepartureFlight())
            {
                totalPoints += CalculateFlightPoints(BookedDepartureFlight!);
            }
            
            return totalPoints;
        }

        /// <summary>
        /// Gets detailed points breakdown for display.
        /// </summary>
        /// <returns>Tuple containing current points, arrival points, and departure points</returns>
        public (int currentPoints, int arrivalPoints, int departurePoints) GetPointsBreakdown()
        {
            int currentPoints = GetPoints();
            int arrivalPoints = 0;
            int departurePoints = 0;

            if (HasArrivalFlight())
            {
                arrivalPoints = CalculateFlightPoints(BookedArrivalFlight!);
            }

            if (HasDepartureFlight())
            {
                departurePoints = CalculateFlightPoints(BookedDepartureFlight!);
            }

            return (currentPoints, arrivalPoints, departurePoints);
        }

        /// <summary>
        /// Returns a string representation of the frequent flyer's details.
        /// </summary>
        /// <returns>A formatted string containing frequent flyer information</returns>
        public override string ToString()
        {
            return $"Your details. \n" +
                $"Name: {Name} \n" +
                $"Age: {Age} \n" +
                $"Mobile phone number: {Mobile} \n" +
                $" Email: {Email} \n" +
                $" Frequent flyer number: {GetFrequentFlyerNumber()} \n" +
                $" Frequent flyer points: {GetPoints():N0}";
        }

    }

}
