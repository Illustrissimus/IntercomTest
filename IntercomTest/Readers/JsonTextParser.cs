using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IntercomTest.Readers
{
    /// <summary>
    /// Reads customer data from a JSON text.
    /// </summary>
    public class JsonTextParser : ICustomerTextParser
    {
        private const char NEWLINE_CHARACTER = '\n';

        private const int LATITUDE_GROUP_INDEX = 1;
        private const int LONGITUDE_GROUP_INDEX = 4;
        private const int USER_ID_GROUP_INDEX = 2;
        private const int NAME_GROUP_INDEX = 3;

        private const string FULL_PATTERN = @"{\s*""latitude"":\s*""(-?[0-9]*\.[0-9]*|-?[0-9]*\.?)"",\s*""user_id"":\s*([0-9]*),\s*""name"":\s*""([A-Za-z\s\.,[0-9]*)"",\s*""longitude"":\s*""(-?[0-9]*\.[0-9]*|-?[0-9]*\.?)""\s*}";

        /// <summary>
        /// Patses the specified JSON text into a list of customers.
        /// </summary>
        /// <param name="text">Text containing a JSON object in each line.</param>
        /// <returns>A list of customers.</returns>
        /// <exception cref="IntercomTestException">Thrown if the specified JSON text has not been properly formatted.</exception>
        public List<Customer> ParseText(string text)
        {
            var customers = new List<Customer>();

            var lines = text.Split(NEWLINE_CHARACTER);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var trimmedLine = line.Trim();
                var match = Regex.Match(trimmedLine, FULL_PATTERN);
                if (!match.Success)
                    throw new IntercomTestException(String.Format("Input has not been properly formatted! Line: {0}.", trimmedLine));

                var customer = ParseJsonObject(match);
                customers.Add(customer);
            }

            return customers;
        }

        /// <summary>
        /// Method used to parse a single JSON object and return customer data.
        /// </summary>
        /// <param name="match">Regex match.</param>
        /// <returns>Customer data.</returns>
        /// <exception cref="IntercomTestException">Thrown if the JSON object is not properly formatted.</exception>
        private Customer ParseJsonObject(Match match)
        {
            if (ReferenceEquals(match, null))
                throw new IntercomTestException("JSON text is in invalid format!");

            var degreeLongitude = ReadDegreeLongitude(match);
            if (!GeographicalLocation.IsDegreeLongitudeValid(degreeLongitude))
                throw new IntercomTestException(String.Format("Invalid geographical longitude read: {0}!", degreeLongitude));

            var degreeLatitude = ReadDegreeLatitude(match);
            if (!GeographicalLocation.IsDegreeLatitudeValid(degreeLatitude))
                throw new IntercomTestException(String.Format("Invalid geographical latitude read: {0}!", degreeLatitude));

            var userId = ReadUserId(match);
            var customerName = ReadCustomerName(match);

            var location = GeographicalLocation.FromDegrees(degreeLongitude, degreeLatitude);
            return new Customer(userId, customerName, location);
        }

        /// <summary>
        /// Reads customer name from the specified JSON object.
        /// </summary>
        /// <param name="match">Regex match.</param>
        /// <returns>Customer name.</returns>
        /// <exception cref="IntercomTestException">Thrown if customer name has not been found.</exception>
        private static string ReadCustomerName(Match match)
        {
            var name = match.Groups[NAME_GROUP_INDEX].Value;
            if (string.IsNullOrWhiteSpace(name))
                throw new IntercomTestException(String.Format("JSON text is in invalid format! Customer name is an empty string: {0}.", match));

            return name.Trim();
        }

        /// <summary>
        /// Reads user ID from the specified JSON object.
        /// </summary>
        /// <param name="match">Regex match.</param>
        /// <returns>User ID.</returns>
        /// <exception cref="IntercomTestException">Thrown if user ID has not been found.</exception>
        private static int ReadUserId(Match match)
        {
            var userIdString = match.Groups[USER_ID_GROUP_INDEX].Value;
            if (string.IsNullOrWhiteSpace(userIdString))
                throw new IntercomTestException(String.Format("JSON text is in invalid format! User ID not found in {0}.", match));
            var parsed = int.TryParse(userIdString, out int userId);
            if (!parsed)
                throw new IntercomTestException(String.Format("JSON text is in invalid format! User ID is not propely formatted: {0}.",
                    userIdString));

            return userId;
        }

        /// <summary>
        /// Reads longitude specified in degrees from the specified JSON object.
        /// </summary>
        /// <param name="match">Regex match.</param>
        /// <returns>Longitude specified in degrees.</returns>
        /// <exception cref="IntercomTestException">Thrown if longitude has not been found.</exception>
        private static double ReadDegreeLongitude(Match match)
        {
            return ReadDegreeDimension(match, "Longitude", LONGITUDE_GROUP_INDEX);
        }

        /// <summary>
        /// Reads latitude specified in degrees from the specified JSON object.
        /// </summary>
        /// <param name="match">Regex match.</param>
        /// <returns>Latitude specified in degrees.</returns>
        /// <exception cref="IntercomTestException">Thrown if latitude has not been found.</exception>
        private static double ReadDegreeLatitude(Match match)
        {
            return ReadDegreeDimension(match, "Latitude", LATITUDE_GROUP_INDEX);
        }

        /// <summary>
        /// Reads a dimension specified in degrees from the specified JSON object.
        /// </summary>
        /// <param name="match">Regex match.</param>
        /// <param name="dimensionName">Dimension name, e.g. longitude or latitude.</param>
        /// <param name="matchGroupIndex">Index of the match group containing the dimension information.</param>
        /// <returns>Dimension specified in degrees.</returns>
        /// <exception cref="IntercomTestException">Thrown if dimension has not been found.</exception>
        private static double ReadDegreeDimension(Match match, string dimensionName, int matchGroupIndex)
        {
            var dimensionString = match.Groups[matchGroupIndex].Value;
            if (string.IsNullOrWhiteSpace(dimensionString))
                throw new IntercomTestException(String.Format("JSON text is in invalid format! {0} not found in {1}.", dimensionName, match));
            var parsed = double.TryParse(dimensionString, NumberStyles.Any, CultureInfo.InvariantCulture, out double degreeDimesnion);
            if (!parsed)
                throw new IntercomTestException(String.Format("JSON text is in invalid format! {0} is not propely formatted: {1}.",
                    dimensionName, dimensionString));

            return degreeDimesnion;
        }
    }
}
