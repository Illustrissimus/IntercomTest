using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using IntercomTest.Readers;

namespace IntercomTestUnitTests
{
    [TestClass]
    public class JsonTextParserTest
    {
        /// <summary>
        /// Test customer JSON text parser.
        /// </summary>
        [TestMethod]
        [Timeout(8000)]
        [Description("Test customer JSON text parser.")]
        public void TestValidText()
        {
            double[] longitudes = { -6.043701, -10.27699, -10.4240951, -7.915833, -7 };
            double[] latitudes = { 52.986375, 51.92893, 51.8856167, 53.74452, 51.999447 };
            int[] userIds = { 12, 1, 2, 20, 31 };
            string[] names = { "Christina McArdle", "Alice Cahill", "Ian McArdle", "Georgina Gallagher", "Jack Dempsey" };

            var builder = new StringBuilder();
            for (int i = 0; i < longitudes.Length; ++i)
            {
                builder
                    .AppendFormat("{{\"latitude\": \"{0}\", \"user_id\": {1}, \"name\": \"{2}\", \"longitude\": \"{3}\"}}",
                    latitudes[i].ToString().Replace(',', '.'), userIds[i], names[i], longitudes[i].ToString().Replace(',', '.'))
                    .AppendLine();
            }

            var jsonReader = new JsonTextParser();
            var customers = jsonReader.ParseText(builder.ToString().TrimEnd());

            Assert.AreEqual(customers.Count, longitudes.Length,
                string.Format("Wrong number of customers read! Expected number of customers: {0}, number of read customers: {1}.",
                longitudes.Length, customers.Count));

            for (int i = 0; i < customers.Count; ++i)
            {
                var customer = customers[i];
                var customerLongitude = customer.Location.DegreeLongitude;
                var customerLatitude = customer.Location.DegreeLatitude;
                Assert.IsTrue(Math.Abs(customerLongitude - longitudes[i]) < 2 * double.Epsilon,
                    string.Format("Wrong customer longitude read! Expected: {0}, got: {1}", longitudes[i], customerLongitude));
                Assert.IsTrue(Math.Abs(customerLatitude - latitudes[i]) < 2 * double.Epsilon,
                    string.Format("Wrong customer latitude read! Expected: {0}, got: {1}", latitudes[i], customerLatitude));
                Assert.AreEqual(customer.UserId, userIds[i],
                    string.Format("Wrong user ID read! Expected {0}, got {1}.", userIds[i], customer.UserId));
                Assert.AreEqual(customer.Name, names[i],
                    string.Format("Wrong customer name read! Expected {0}, got {1}.", names[i], customer.Name));
            }
        }
    }
}
