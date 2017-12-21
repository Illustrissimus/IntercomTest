using System;
using System.Configuration;

namespace IntercomConsoleApp
{
    /// <summary>
    /// Class used to read application configuration from the App.config file.
    /// </summary>
    static class ConfigurationReader
    {
        const double DEFAULT_DUBLIN_OFFICE_DEGREE_LONGITUDE = -6.257664d;
        const double DEFAULT_DUBLIN_OFFICE_DEGREE_LATITUDE = 53.339428;

        const double DEFAULT_INVITATION_DISTANCE_KILOMETERS = 100.0d;

        /// <summary>
        /// Customer file attribute key.
        /// </summary>
        private const string CUSTOMER_FILE_SETTINGS_KEY = "customerfile";
        /// <summary>
        /// Default customer file path.
        /// </summary>
        private const string DEFAULT_CUTOMER_FILE_PATH = @"customers.txt";

        /// <summary>
        /// Gets Dublin office degree longitude.
        /// </summary>
        public static double DublinOfficeDegreeLongitude { get; } = DEFAULT_DUBLIN_OFFICE_DEGREE_LONGITUDE;

        /// <summary>
        /// Gets Dublin office degree latitude.
        /// </summary>
        public static double DublinOfficeDegreeLatitude { get; } = DEFAULT_DUBLIN_OFFICE_DEGREE_LATITUDE;

        public static double InvitationDistanceKilometers { get; } = DEFAULT_INVITATION_DISTANCE_KILOMETERS;

        /// <summary>
        /// Gets customer file path.
        /// </summary>
        public static string CustomerFilePath { get; }

        static ConfigurationReader()
        {
            try
            {
                var customerFilePath = ConfigurationManager.AppSettings[CUSTOMER_FILE_SETTINGS_KEY];
                if (string.IsNullOrWhiteSpace(customerFilePath))
                    throw new ConfigurationErrorsException("Customer file path not specified correctly!");
                CustomerFilePath = customerFilePath;
            }
            catch (ConfigurationErrorsException)
            {
                string currentDirectory = Environment.CurrentDirectory;
                if (string.IsNullOrWhiteSpace(currentDirectory))
                    currentDirectory = "./";
                currentDirectory?.Replace('\\', '/');
                if (currentDirectory.EndsWith("/"))
                    currentDirectory += "/";

                Console.WriteLine("Customer file not specified in the configuration file. Defaulting to {0}.", currentDirectory + DEFAULT_CUTOMER_FILE_PATH);
                CustomerFilePath = DEFAULT_CUTOMER_FILE_PATH;
            }
        }
    }
}
