using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntercomTest;
using IntercomTest.Utilty;

using static System.Math;

namespace IntercomTestUnitTests
{
    [TestClass]
    public class GeographicalLocationTest
    {
        /// <summary>
        /// Tolerated error when calculating the distance.
        /// </summary>
        private const double DISTANCE_EPSILON = 0.05d; // 50 meter error is tolerated.

        private static readonly Random rng = new Random(Environment.TickCount);

        #region ValueValidation
        /// <summary>
        /// Tests whether valid geographical longitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether valid geographical longitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.")]
        public void TestValidDegreeLongitude()
        {
            double[] degreeLongitudes = { -180.0d, 180.0d, 0.0d, 90.0d, -19.0d, -45.0d, 45.0d, 23.0d, 47.0d, -123.4343d, 159.0d, -20.43d };

            foreach (var degreeLongitude in degreeLongitudes)
                Assert.IsTrue(GeographicalLocation.IsDegreeLongitudeValid(degreeLongitude),
                    String.Format("Degree longitude {0} declared invalid, even though it is valid!", degreeLongitude));
        }

        /// <summary>
        /// Tests whether invalid geographical longitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether invalid geographical longitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.")]
        public void TestInvalidDegreeLongitude()
        {
            double[] degreeLongitudes = { -181.0d, 181.0d, 190.0d, -190.0d, 270.0, -270.0d, 360.0d, 360.0d, 200.0d, -300.0d };

            foreach (var degreeLongitude in degreeLongitudes)
                Assert.IsFalse(GeographicalLocation.IsDegreeLongitudeValid(degreeLongitude),
                    String.Format("Degree longitude {0} declared valid, even though it is invalid!", degreeLongitude));
        }

        /// <summary>
        /// Tests whether valid geographical latitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether valid geographical latitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.")]
        public void TestValidDegreeLatitude()
        {
            double[] degreeLatitudes = { -90.0d, 00.0d, 0.0d, -19.0d, -45.0d, 45.0d, 23.0d, 47.0d, -20.43d, 60.0d, -60.0d, 34.23d, -86.0d };

            foreach (var degreeLatitude in degreeLatitudes)
                Assert.IsTrue(GeographicalLocation.IsDegreeLatitudeValid(degreeLatitude),
                    String.Format("Degree latitude {0} declared invalid, even though it is valid!", degreeLatitude));
        }

        /// <summary>
        /// Tests whether invalid geographical latitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether invalid geographical latitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.")]
        public void TestInvalidDegreeLatitude()
        {
            double[] degreeLatitudes = { -91.0d, 91.0d, 190.0d, -190.0d, 270.0, -270.0d, 360.0d, 360.0d, 200.0d, -300.0d };

            foreach (var degreeLatitude in degreeLatitudes)
                Assert.IsFalse(GeographicalLocation.IsDegreeLatitudeValid(degreeLatitude),
                    String.Format("Degree latitude {0} declared valid, even though it is invalid!", degreeLatitude));
        }

        /// <summary>
        /// Tests whether valid geographical longitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether valid geographical longitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.")]
        public void TestValidRadianLongitude()
        {
            double[] radianLongitudes = { -PI, PI, 0.0d, PI / 2, -PI / 2, -PI / 4, PI / 4, PI * 0.23, PI * 0.753, -PI * 0.3421, -PI * 0.6831, PI * 0.89123 };

            foreach (var radianLongitude in radianLongitudes)
                Assert.IsTrue(GeographicalLocation.IsRadianLongitudeValid(radianLongitude),
                    String.Format("Radian longitude {0} declared invalid, even though it is valid!", radianLongitude));
        }

        /// <summary>
        /// Tests whether invalid geographical longitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether invalid geographical longitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.")]
        public void TestInvalidRadianLongitude()
        {
            double[] radianLongitudes = { -PI - 0.001, PI + 0.001, -PI - 1, PI + 1, 3 * PI / 2, -3 * PI / 2, 2* PI , -2 * PI, PI * 1.231, -PI * 1.231 };

            foreach (var radianLongitude in radianLongitudes)
                Assert.IsFalse(GeographicalLocation.IsRadianLongitudeValid(radianLongitude),
                    String.Format("Radian longitude {0} declared valid, even though it is invalid!", radianLongitude));
        }

        /// <summary>
        /// Tests whether valid geographical latitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether valid geographical latitudes will actually be classified as valid by the IntercomTest.GeographicalLocation class.")]
        public void TestValidRadianLatitude()
        {
            double[] radianLatitudes = { -PI / 2, 00.0d, PI / 2, -PI * 0.32, PI / 4, -PI / 4, -PI * 0.426, PI * 0.001, PI * 0.499, -PI * 0.499, -PI / 3, PI / 3 };

            foreach (var radianLatitude in radianLatitudes)
                Assert.IsTrue(GeographicalLocation.IsRadianLatitudeValid(radianLatitude),
                    String.Format("Radian latitude {0} declared invalid, even though it is valid!", radianLatitude));
        }

        /// <summary>
        /// Tests whether invalid geographical latitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Tests whether invalid geographical latitudes will actually be classified as invalid by the IntercomTest.GeographicalLocation class.")]
        public void TestInvalidRadianLatitude()
        {
            double[] radianLatitudes = { -PI - 0.001, PI + 0.001, PI * 0.52, -PI * 0.52, 3 * PI, -3 * PI, 2 * PI, -2 * PI, -3.232 * PI, 4.52 * PI };

            foreach (var radianLatitude in radianLatitudes)
                Assert.IsFalse(GeographicalLocation.IsRadianLatitudeValid(radianLatitude),
                    String.Format("Radian latitude {0} declared valid, even though it is invalid!", radianLatitude));
        }
        #endregion

        #region InstantiationFromDegrees
        /// <summary>
        /// Tests whether an instance of the IntercomTest.Location class will be correctly created when valid degree location has been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests whether an instance of the IntercomTest.Location class will be correctly created when valid degree location has been specified.")]
        public void TestFromValidDegrees()
        {
            const int runs = 10;

            for (int i = 0; i < runs; ++i)
            {
                var degreeLongitude = GenerateValidValue(GeographicalLocation.MAX_DEGREE_LONGITUDE);
                var degreeLatitude = GenerateValidValue(GeographicalLocation.MAX_DEGREE_LATITUDE);

                var radianLongitude = Calculator.DegreesToRadians(degreeLongitude);
                var radianLatitude = Calculator.DegreesToRadians(degreeLatitude);

                var location = GeographicalLocation.FromDegrees(degreeLongitude, degreeLatitude);
                Assert.IsTrue(location.DegreeLongitude == degreeLongitude,
                    String.Format("Location degree longitude is not equal to the expected degree longitude! Expected: {0}, got: {1}.",
                    degreeLongitude, location.DegreeLongitude));
                Assert.IsTrue(location.DegreeLatitude == degreeLatitude,
                    String.Format("Location degree latitude is not equal to the expected degree latitude! Expected: {0}, got: {1}.",
                    degreeLatitude, location.DegreeLatitude));
                Assert.IsTrue(location.RadianLongitude == radianLongitude,
                    String.Format("Location radian longitude is not equal to the expected radian longitude! Expected: {0}, got: {1}.",
                    radianLongitude, location.RadianLongitude));
                Assert.IsTrue(location.RadianLatitude == radianLatitude,
                    String.Format("Location radian latitude is not equal to the expected radian latitude! Expected: {0}, got: {1}.",
                    radianLatitude, location.RadianLatitude));
            }
        }

        /// <summary>
        /// Tests that an instance of the IntercomTest.Location will not be created when invalid degree values have been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests that an instance of the IntercomTest.Location will not be created when invalid degree values have been specified.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "IntercomTest.GeographicalLocation class was instantiated with invalid parameters!", AllowDerivedTypes = true)]
        public void TestFromInvalidDegrees()
        {
            var degreeLongitude = GenerateInvalidValue(GeographicalLocation.MAX_DEGREE_LONGITUDE);
            var degreeLatitude = GenerateInvalidValue(GeographicalLocation.MAX_DEGREE_LATITUDE);

            var location = GeographicalLocation.FromDegrees(degreeLongitude, degreeLatitude);
        }

        /// <summary>
        /// Tests that an instance of the IntercomTest.Location will not be created when invalid degree longitude has been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests that an instance of the IntercomTest.Location will not be created when invalid degree longitude has been specified.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "IntercomTest.GeographicalLocation class was instantiated with invalid degree longitude!", AllowDerivedTypes = true)]
        public void TestFromInvalidDegreeLongitude()
        {
            var degreeLongitude = GenerateInvalidValue(GeographicalLocation.MAX_DEGREE_LONGITUDE);
            var degreeLatitude = GenerateValidValue(GeographicalLocation.MAX_DEGREE_LATITUDE);

            var location = GeographicalLocation.FromDegrees(degreeLongitude, degreeLatitude);
        }

        /// <summary>
        /// Tests that an instance of the IntercomTest.Location will not be created when invalid degree latitude has been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests that an instance of the IntercomTest.Location will not be created when invalid degree latitude has been specified.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "IntercomTest.GeographicalLocation class was instantiated with invalid degree latitude!", AllowDerivedTypes = true)]
        public void TestFromInvalidDegreeLatitude()
        {
            var degreeLongitude = GenerateValidValue(GeographicalLocation.MAX_DEGREE_LONGITUDE);
            var degreeLatitude = GenerateInvalidValue(GeographicalLocation.MAX_DEGREE_LATITUDE);

            var location = GeographicalLocation.FromDegrees(degreeLongitude, degreeLatitude);
        }
        #endregion

        #region InstantiationFromRadians
        /// <summary>
        /// Tests whether an instance of the IntercomTest.Location class will be correctly created when valid radian location has been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests whether an instance of the IntercomTest.Location class will be correctly created when valid radian location has been specified.")]
        public void TestFromValidRadians()
        {
            const int runs = 10;

            for (int i = 0; i < runs; ++i)
            {
                var radianLongitude = GenerateValidValue(GeographicalLocation.MAX_RADIAN_LONGITUDE);
                var radianLatitude = GenerateValidValue(GeographicalLocation.MAX_RADIAN_LATITUDE);

                var degreeLongitude = Calculator.DegreesToRadians(radianLongitude);
                var degreeLatitude = Calculator.DegreesToRadians(radianLatitude);

                var location = GeographicalLocation.FromRadians(radianLongitude, radianLatitude);
                Assert.IsTrue(location.RadianLongitude == radianLongitude,
                    String.Format("Location radian longitude is not equal to the expected radian longitude! Expected: {0}, got: {1}.",
                    radianLongitude, location.RadianLongitude));
                Assert.IsTrue(location.RadianLatitude == radianLatitude,
                    String.Format("Location radian latitude is not equal to the expected radian latitude! Expected: {0}, got: {1}.",
                    radianLatitude, location.RadianLatitude));
                Assert.IsTrue(location.RadianLongitude == radianLongitude,
                    String.Format("Location radian longitude is not equal to the expected radian longitude! Expected: {0}, got: {1}.",
                    radianLongitude, location.RadianLongitude));
                Assert.IsTrue(location.RadianLatitude == radianLatitude,
                    String.Format("Location radian latitude is not equal to the expected radian latitude! Expected: {0}, got: {1}.",
                    radianLatitude, location.RadianLatitude));
            }
        }

        /// <summary>
        /// Tests that an instance of the IntercomTest.Location will not be created when invalid radian values have been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests that an instance of the IntercomTest.Location will not be created when invalid radian values have been specified.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "IntercomTest.GeographicalLocation class was instantiated with invalid parameters!", AllowDerivedTypes = true)]
        public void TestFromInvalidRadians()
        {
            var radianLongitude = GenerateInvalidValue(GeographicalLocation.MAX_RADIAN_LONGITUDE);
            var radianLatitude = GenerateInvalidValue(GeographicalLocation.MAX_RADIAN_LATITUDE);

            var location = GeographicalLocation.FromRadians(radianLongitude, radianLatitude);
        }

        /// <summary>
        /// Tests that an instance of the IntercomTest.Location will not be created when invalid radian longitude has been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests that an instance of the IntercomTest.Location will not be created when invalid radian longitude has been specified.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "IntercomTest.GeographicalLocation class was instantiated with invalid radian longitude!", AllowDerivedTypes = true)]
        public void TestFromInvalidRadianLongitude()
        {
            var radianLongitude = GenerateInvalidValue(GeographicalLocation.MAX_RADIAN_LONGITUDE);
            var radianLatitude = GenerateValidValue(GeographicalLocation.MAX_RADIAN_LATITUDE);

            var location = GeographicalLocation.FromRadians(radianLongitude, radianLatitude);
        }

        /// <summary>
        /// Tests that an instance of the IntercomTest.Location will not be created when invalid radian latitude has been specified.
        /// </summary>
        [TestMethod]
        [Timeout(2000)]
        [Description("Tests that an instance of the IntercomTest.Location will not be created when invalid radian latitude has been specified.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "IntercomTest.GeographicalLocation class was instantiated with invalid radian latitude!", AllowDerivedTypes = true)]
        public void TestFromInvalidRadianLatitude()
        {
            var radianLongitude = GenerateValidValue(GeographicalLocation.MAX_RADIAN_LONGITUDE);
            var radianLatitude = GenerateInvalidValue(GeographicalLocation.MAX_RADIAN_LATITUDE);

            var location = GeographicalLocation.FromRadians(radianLongitude, radianLatitude);
        }
        #endregion

        #region DistanceCalculations
        /// <summary>
        /// Test distance calculations are correct.
        /// </summary>
        [TestMethod]
        [Timeout(4000)]
        [Description("Test distance calculations are correct.")]
        public void TestDistance()
        {
            var fromDegreeLongitude = -6.257664d;
            var fromDegreeLatitude = 53.339428d;
            double[] toDegreeLongitudes = { -6.043701d, -10.27699d, -10.4240951d };
            double[] toDegreeLatitudes = { 52.986375d, 51.92893d, 51.8856167d };
            double[] expectedDistances = { 41.77d, 313.24d, 324.36d };

            var fromLocation = GeographicalLocation.FromDegrees(fromDegreeLongitude, fromDegreeLatitude);

            for (int i = 0; i < toDegreeLongitudes.Length; ++i)
            {
                var toLocation = GeographicalLocation.FromDegrees(toDegreeLongitudes[i], toDegreeLatitudes[i]);

                var distance = fromLocation.DistanceFrom(toLocation);
                Assert.IsTrue(distance - expectedDistances[i] < DISTANCE_EPSILON,
                    string.Format("Wrong distance calculated! Expected: {0}, got: {1}.", expectedDistances[i], distance));
            }
        }

        /// <summary>
        /// Test static method distance calculations are correct.
        /// </summary>
        [TestMethod]
        [Timeout(4000)]
        [Description("Test static method distance calculations are correct.")]
        public void TestStaticCalculateDistance()
        {
            var fromDegreeLongitude = -6.257664d;
            var fromDegreeLatitude = 53.339428d;
            double[] toDegreeLongitudes = { -6.043701d, -10.27699d, -10.4240951d };
            double[] toDegreeLatitudes = { 52.986375d, 51.92893d, 51.8856167d };
            double[] expectedDistances = { 41.77d, 313.24d, 324.36d };

            var fromLocation = GeographicalLocation.FromDegrees(fromDegreeLongitude, fromDegreeLatitude);

            for (int i = 0; i < toDegreeLongitudes.Length; ++i)
            {
                var toLocation = GeographicalLocation.FromDegrees(toDegreeLongitudes[i], toDegreeLatitudes[i]);

                var distance = GeographicalLocation.CalculateDistance(fromLocation, toLocation);
                Assert.IsTrue(distance - expectedDistances[i] < DISTANCE_EPSILON,
                    string.Format("Wrong distance calculated! Expected: {0}, got: {1}.", expectedDistances[i], distance));
            }
        }
        #endregion

        #region CommonMethods
        /// <summary>
        /// Generates a random value from [-maxValue, maxValue] interval.
        /// </summary>
        /// <param name="maxValue">Interval max value.</param>
        /// <returns>A value from the interval</returns>
        private static double GenerateValidValue(double maxValue)
        {
            return (rng.NextDouble() * 2 - 1) * maxValue;
        }

        /// <summary>
        /// Generates a random value outside of [-maxValue, maxValue] interval.
        /// </summary>
        /// <param name="maxValue">Interval max value.</param>
        /// <returns>A value from the interval</returns>
        private static double GenerateInvalidValue(double maxValue)
        {
            var value = GenerateValidValue(maxValue);
            value += value > 0 ? maxValue : -maxValue;
            return value;
        }

        #endregion
    }
}
