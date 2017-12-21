using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntercomTest.Utilty;

using static System.Math;

namespace IntercomTestUnitTests
{
    [TestClass]
    public class CalculatorTest
    {
        /// <summary>
        /// Tolerated error when comparing double precision floating point numbers.
        /// </summary>
        private const double EPSILON = 0.00000001d;

        /// <summary>
        /// Test the conversion of degrees to radians.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Test the conversion of degrees to radians.")]
        public void TestDegreesToRadians()
        {
            double[] degreeAngles = { 0.0d, 90.0d, 180.0d, 270.0d, 360.0d, -90.0d, -180.0d, 45.0d, 135.0d, 86.2d, 35.1d, 146.93d, 11.23d };
            double[] expectedRadianAngles = { 0.0d, PI / 2, PI, 3 *PI / 2, 2 * PI, -PI / 2, - PI,  PI / 4, 3 * PI / 4,
                1.504473815219112, 0.6126105674500096, 2.564412269955268, 0.1960004749989632 };

            for (int i = 0; i < degreeAngles.Length; ++i)
            {
                double degreeAngle = degreeAngles[i];
                double result = Calculator.DegreesToRadians(degreeAngle);
                double expectedRadianAngle = expectedRadianAngles[i];
                double distance = Abs(result - expectedRadianAngle);
                Assert.IsTrue(distance < EPSILON, CreateDegreeToRadianErrorMessage(expectedRadianAngle, result));
            }
        }

        /// <summary>
        /// Test the conversion of radians to degrees.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Test for converting radians to degrees.")]
        public void TestRadiansToDegrees()
        {
            double[] radianAngles = { 0.0d, PI / 2, PI, 3 *PI / 2, 2 * PI, -PI / 2, - PI,  PI / 4, 3 * PI / 4,
                1.504473815219112, 0.6126105674500096, 2.564412269955268, 0.1960004749989632 };
            double[] expectedDegreeAngles = { 0.0d, 90.0d, 180.0d, 270.0d, 360.0d, -90.0d, -180.0d, 45.0d, 135.0d, 86.2d, 35.1d, 146.93d, 11.23d };

            for (int i = 0; i < radianAngles.Length; ++i)
            {
                double radianAngle = radianAngles[i];
                double result = Calculator.RadiansToDegrees(radianAngle);
                double expectedDegreeAngle = expectedDegreeAngles[i];
                double distance = Abs(result - expectedDegreeAngle);
                Assert.IsTrue(distance < EPSILON, CreateRadianToDegreeErrorMessage(expectedDegreeAngle, result));
            }
        }

        /// <summary>
        /// Test great-circle distance calculations.
        /// </summary>
        [TestMethod]
        [Timeout(4000)]
        [Description("Test great-circle distance calculations.")]
        public void TestGreatCircleDistanceCalculation()
        {
            const double toleratedError = 0.03;

            double[] theta1 = { -PI/3, 0, PI / 4 };
            double[] theta2 = { PI/6, 0, -PI / 4 };

            double[] lambda1 = { -PI/4, 0, PI / 4 };
            double[] lambda2 = { PI/8, 0, -PI / 4 };

            double[] radius = { 5000.0d, 6000.0d, 7000.0d };

            double[] expectedDistances = { 9206.962, 0, 14660.766 };

            for (int i = 0; i < theta1.Length; ++i)
            {
                double distance = Calculator.CalculateGreatCircleDistance(theta1[i], lambda1[i], theta2[i], lambda2[i], radius[i]);
                Assert.IsTrue(Abs(distance - expectedDistances[i]) <= toleratedError,
                    string.Format("Wrong distance calculated! Expected {0} +- {1}, got {2}.", expectedDistances[i], toleratedError, distance));
            }
        }

        /// <summary>
        /// Creates an error message when degree to radian conversion gives an unexpected result.
        /// </summary>
        /// <param name="expected">Expected value.</param>
        /// <param name="result">Result.</param>
        /// <returns>Error message.</returns>
        private static string CreateDegreeToRadianErrorMessage(double expected, double result)
        {
            var builder = new StringBuilder("Unexpected result of conversion from degrees to radians! ");
            builder.AppendFormat("Expected: {0}, got: {1}.", expected, result);
            return builder.ToString();
        }

        /// <summary>
        /// Creates an error message when radian to degree conversion gives an unexpected result.
        /// </summary>
        /// <param name="expected">Expected value.</param>
        /// <param name="result">Result.</param>
        /// <returns>Error message.</returns>
        private static string CreateRadianToDegreeErrorMessage(double expected, double result)
        {
            var builder = new StringBuilder("Unexpected result of conversion from radians to degrees! ");
            builder.AppendFormat("Expected: {0}, got: {1}.", expected, result);
            return builder.ToString();
        }
    }
}
