using System;

namespace IntercomTest.Utilty
{
    /// <summary>
    /// Contains mathematical functions used for location services.
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// A cricle has 360 degrees.
        /// </summary>
        public const double CIRCLE_DEGREES = 360;
        /// <summary>
        /// A circle has 2 * PI radians.
        /// </summary>
        public const double CIRCLE_RADIANS = 2 * Math.PI;

        /// <summary>
        /// Converts an angle specified in degrees to radians.
        /// </summary>
        /// <param name="degrees">Degrees.</param>
        /// <returns>Radian equivalent.</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        /// <summary>
        /// Converts an angle specified in radians to degrees.
        /// </summary>
        /// <param name="degrees">Radians.</param>
        /// <returns>Degree equivalent.</returns>
        public static double RadiansToDegrees(double radians)
        {
            return (radians * 180) / Math.PI;
        }

        /// <summary>
        /// Calculates great circle distance between two points. The points are specified by their latitute (theta) and longitude (lambda)
        /// in radians. The formula from https://en.wikipedia.org/wiki/Great-circle_distance was used for calculations.
        /// </summary>
        /// <param name="theta1">Latitude of the first point in radians.</param>
        /// <param name="lambda1">Longitude of the first point in radians.</param>
        /// <param name="theta2">Latitude of the second point in radinas.</param>
        /// <param name="lambda2">Longitude of the second point in radians.</param>
        /// <param name="radius">Circle radius.</param>
        /// <returns>Great-circle distance of the two points.</returns>
        public static double CalculateGreatCircleDistance(double theta1, double lambda1, double theta2, double lambda2, double radius)
        {
            // The formula from https://en.wikipedia.org/wiki/Great-circle_distance was used for calculations.
            double sinTheta1 = Math.Sin(theta1);
            double sinTheta2 = Math.Sin(theta2);

            double cosTheta1 = Math.Cos(theta1);
            double cosTheta2 = Math.Cos(theta2);

            var deltaLambda = Math.Abs(lambda1 - lambda2);
            var cosDeltaLambda = Math.Cos(deltaLambda);
            double centralAngle = Math.Acos(sinTheta1 * sinTheta2 + cosTheta1 * cosTheta2 * cosDeltaLambda);

            return radius * centralAngle;
        }
    }
}
