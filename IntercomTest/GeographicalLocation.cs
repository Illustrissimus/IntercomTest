using System;
using System.Text;

namespace IntercomTest
{
    /// <summary>
    /// Represents a geographical location.
    /// </summary>
    public class GeographicalLocation
    {
        /// <summary>
        /// Mean Earth radius in kilometers.
        /// </summary>
        public const double MEAN_EARTH_RADIUS_KM = 6371.0d;

        /// <summary>
        /// Minimum geographical longitude in degrees.
        /// </summary>
        public const double MIN_DEGREE_LONGITUDE = -180.0d;
        /// <summary>
        /// Maximum geographical longitude in degrees.
        /// </summary>
        public const double MAX_DEGREE_LONGITUDE = 180.0d;
        /// <summary>
        /// Minimum geographical latitude in degrees.
        /// </summary>
        public const double MIN_DEGREE_LATITUDE = -90.0d;
        /// <summary>
        /// Maximum geographical latitude in degrees.
        /// </summary>
        public const double MAX_DEGREE_LATITUDE = 90.0d;

        /// <summary>
        /// Minimum geographical longitude in radians.
        /// </summary>
        public const double MIN_RADIAN_LONGITUDE = - Math.PI;
        /// <summary>
        /// Maximum geographical longitude in radians.
        /// </summary>
        public const double MAX_RADIAN_LONGITUDE = Math.PI;
        /// <summary>
        /// Minimum geographical latitude in radians.
        /// </summary>
        public const double MIN_RADIAN_LATITUDE = -Math.PI / 2.0d;
        /// <summary>
        /// Maximum geographical latitude in radians.
        /// </summary>
        public const double MAX_RADIAN_LATITUDE = Math.PI / 2.0d;

        /// <summary>
        /// Gets longitude in radians.
        /// </summary>
        public double DegreeLongitude { get; }

        /// <summary>
        /// Gets latitude in radians.
        /// </summary>
        public double DegreeLatitude { get; }

        /// <summary>
        /// Gets longitude in radians.
        /// </summary>
        public double RadianLongitude { get; }

        /// <summary>
        /// Gets latitude in radians.
        /// </summary>
        public double RadianLatitude { get; }

        /// <summary>
        /// Creates a new instance of the IntercomTest.Location class with the geographical longitude and latitude specified in degrees.
        /// </summary>
        /// <param name="degreeLongitude">Longitude specified in degrees.</param>
        /// <param name="degreeLatitude">Latitude specified in degrees.</param>
        /// <param name="radianLongitude">Longitude specified in radians.</param>
        /// <param name="radianLatitude">Latitude specified in radians.</param>
        private GeographicalLocation(double degreeLongitude, double degreeLatitude, double radianLongitude, double radianLatitude)
        {
            DegreeLongitude = degreeLongitude;
            DegreeLatitude = degreeLatitude;

            RadianLongitude = radianLongitude;
            RadianLatitude = radianLatitude;
        }

        /// <summary>
        /// Creates a new instance of the IntercomTest.Location class with geographical longitude and latitude specified in degrees.
        /// </summary>
        /// <param name="degreeLongitude">Longitude.</param>
        /// <param name="degreeLatitude">Latitude.</param>
        /// <returns>Geographical location.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if longitude or latitude are out of bounds.</exception>
        public static GeographicalLocation FromDegrees(double degreeLongitude, double degreeLatitude)
        {
            if (!IsDegreeLongitudeValid(degreeLongitude))
                throw new ArgumentOutOfRangeException(nameof(degreeLongitude), CreateDegreeLongitudeOutOfBoundsMessage(degreeLongitude));

            if (!IsDegreeLatitudeValid(degreeLatitude))
                throw new ArgumentOutOfRangeException(nameof(degreeLatitude), CreateDegreeLatitudeOutOfBoundsMessage(degreeLatitude));

            double radianLongitude = Utilty.Calculator.DegreesToRadians(degreeLongitude);
            double radianLatitude = Utilty.Calculator.DegreesToRadians(degreeLatitude);

            return new GeographicalLocation(degreeLongitude, degreeLatitude, radianLongitude, radianLatitude);
        }

        /// <summary>
        /// Creates a new instance of the IntercomTest.Location class with geographical longitude and latitude specified in radians.
        /// </summary>
        /// <param name="radianLongitude">Longitude.</param>
        /// <param name="radianLatitude">Latitude.</param>
        /// <returns>Geographical location.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if longitude or latitude are out of bounds.</exception>
        public static GeographicalLocation FromRadians(double radianLongitude, double radianLatitude)
        {
            if (!IsRadianLongitudeValid(radianLongitude))
                throw new ArgumentOutOfRangeException(nameof(radianLongitude), CreateRadianLongitudeOutOfBoundsMessage(radianLongitude));

            if (!IsRadianLatitudeValid(radianLatitude))
                throw new ArgumentOutOfRangeException(nameof(radianLatitude), CreateRadianLatitudeOutOfBoundsMessage(radianLatitude));

            double degreeLongitude = Utilty.Calculator.RadiansToDegrees(radianLongitude);
            double degreeLatitude = Utilty.Calculator.RadiansToDegrees(radianLongitude);

            return new GeographicalLocation(radianLongitude, degreeLatitude, radianLongitude, radianLatitude);
        }

        /// <summary>
        /// Returns a value indicating whether the specified longitude in degrees is valid, i.e is it between the minumum and maximum values
        /// for geographical longitude in degreees.
        /// </summary>
        /// <param name="longitude">Geographical longitude specified in degrees.</param>
        /// <returns>true if the longitude is valid; otherwise, false.</returns>
        public static bool IsDegreeLongitudeValid(double longitude)
        {
            return (longitude >= MIN_DEGREE_LONGITUDE) && (longitude <= MAX_DEGREE_LONGITUDE);
        }

        /// <summary>
        /// Returns a value indicating whether the specified latitude in degrees is valid, i.e is it between the minumum and maximum values
        /// for geographical latitude in degreees.
        /// </summary>
        /// <param name="latiutude">Geographical latiutude specified in degrees.</param>
        /// <returns>true if the latiutude is valid; otherwise, false.</returns>
        public static bool IsDegreeLatitudeValid(double latiutude)
        {
            return (latiutude >= MIN_DEGREE_LATITUDE) && (latiutude <= MAX_DEGREE_LATITUDE);
        }

        /// <summary>
        /// Returns a value indicating whether the specified longitude in radians is valid, i.e is it between the minumum and maximum values
        /// for geographical longitude in radians.
        /// </summary>
        /// <param name="longitude">Geographical longitude specified in degrees.</param>
        /// <returns>true if the longitude is valid; otherwise, false.</returns>
        public static bool IsRadianLongitudeValid(double longitude)
        {
            return (longitude >= MIN_RADIAN_LONGITUDE) && (longitude <= MAX_RADIAN_LONGITUDE);
        }

        /// <summary>
        /// Returns a value indicating whether the specified latitude in radians is valid, i.e is it between the minumum and maximum values
        /// for geographical latitude in radians.
        /// </summary>
        /// <param name="latiutude">Geographical latiutude specified in degrees.</param>
        /// <returns>true if the latiutude is valid; otherwise, false.</returns>
        public static bool IsRadianLatitudeValid(double latiutude)
        {
            return (latiutude >= MIN_RADIAN_LATITUDE) && (latiutude <= MAX_RADIAN_LATITUDE);
        }

        /// <summary>
        /// Calculates the distance between the specified geographical locations. The formula from https://en.wikipedia.org/wiki/Great-circle_distance
        /// was used for calculations. Mean Earth radius is cca. 6371 kilometers.
        /// </summary>
        /// <param name="firstLocation">First location.</param>
        /// <param name="secondLocation">Second location.</param>
        /// <returns></returns>
        public static double CalculateDistance(GeographicalLocation firstLocation, GeographicalLocation secondLocation)
        {
            return Utilty.Calculator.CalculateGreatCircleDistance(firstLocation.RadianLatitude, firstLocation.RadianLongitude,
                secondLocation.RadianLatitude, secondLocation.RadianLongitude, MEAN_EARTH_RADIUS_KM);
        }

        /// <summary>
        /// Gets the distance from the specified other location, in kilometers.
        /// </summary>
        /// <param name="otherLocation">Other location.</param>
        /// <returns>Distance from the specified location in kilometers.</returns>
        public double DistanceFrom(GeographicalLocation otherLocation)
        {
            return CalculateDistance(this, otherLocation);
        }

        /// <summary>
        /// Creates error message when degree longitude is invalid.
        /// </summary>
        /// <param name="degreeLongitude">Specified longitude.</param>
        /// <returns>Error message</returns>
        private static string CreateDegreeLongitudeOutOfBoundsMessage(double degreeLongitude)
        {
            var builder = new StringBuilder("Degree longitude is out of bounds! ");
            builder.AppendFormat("Minumum value: {0}, maximum value: {1}, got {2}.", MIN_DEGREE_LONGITUDE, MAX_DEGREE_LONGITUDE, degreeLongitude);
            return builder.ToString();
        }

        /// <summary>
        /// Creates error message when degree longitude is invalid.
        /// </summary>
        /// <param name="degreeLatitude">Specified longitude.</param>
        /// <returns>Error message</returns>
        private static string CreateDegreeLatitudeOutOfBoundsMessage(double degreeLatitude)
        {
            var builder = new StringBuilder("Degree latitude is out of bounds! ");
            builder.AppendFormat("Minumum value: {0}, maximum value: {1}, got {2}.", MIN_DEGREE_LATITUDE, MAX_DEGREE_LATITUDE, degreeLatitude);
            return builder.ToString();
        }

        /// <summary>
        /// Creates error message when radian longitude is invalid.
        /// </summary>
        /// <param name="radianLongitude">Specified longitude.</param>
        /// <returns>Error message</returns>
        private static string CreateRadianLongitudeOutOfBoundsMessage(double radianLongitude)
        {
            var builder = new StringBuilder("Radian longitude is out of bounds! ");
            builder.AppendFormat("Minumum value: {0}, maximum value: {1}, got {2}.", MIN_RADIAN_LONGITUDE, MAX_RADIAN_LONGITUDE, radianLongitude);
            return builder.ToString();
        }

        /// <summary>
        /// Creates error message when radian longitude is invalid.
        /// </summary>
        /// <param name="radianLatitude">Specified longitude.</param>
        /// <returns>Error message</returns>
        private static string CreateRadianLatitudeOutOfBoundsMessage(double radianLatitude)
        {
            var builder = new StringBuilder("Radian latitude is out of bounds! ");
            builder.AppendFormat("Minumum value: {0}, maximum value: {1}, got {2}.", MIN_RADIAN_LATITUDE, MAX_RADIAN_LATITUDE, radianLatitude);
            return builder.ToString();
        }
    }
}
