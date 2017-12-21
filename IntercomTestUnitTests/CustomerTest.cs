using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntercomTest;

namespace IntercomTestUnitTests
{
    [TestClass]
    public class CustomerTest
    {
        /// <summary>
        /// Test that IntercomTest.Customer class will be instantiated when all parameters are valid.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [Description("Test that IntercomTest.Customer class will be instantiated when all parameters are valid.")]
        public void TestCustomerValidInstantiation()
        {
            var userId = 0;
            var name = "John Doe";
            var location = GeographicalLocation.FromDegrees(0.0d, 0.0d);

            var customer = new Customer(userId, name, location);

            Assert.AreNotEqual(customer, null, "IntercomTest.Customer class has not been instantiated, even though all parameters are valid!");
            Assert.AreEqual(customer.UserId, userId,
                string.Format("IntercomTest.Customer class instance contains invalid data! Expected user ID: {0}, got {1}", userId, customer.UserId));
            Assert.AreEqual(customer.Name, name.Trim(),
                string.Format("IntercomTest.Customer class instance contains invalid data! Expected name: {0}, got {1}", name, customer.Name));
            Assert.AreEqual(customer.Location, location, "IntercomTest.Customer class instance contains invalid data. Location references do not match.");
        }

        /// <summary>
        /// Test that IntercomTest.Customer class will not be instantiated with customer name being a null reference.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException), "IntercomTest.Customer class was instantiated with customer name being null reference!")]
        [Description("Test that IntercomTest.Customer class will not be instantiated with customer name being a null reference.")]
        public void TestCustomerNameNull()
        {
            var userId = 0;
            var location = GeographicalLocation.FromDegrees(0.0d, 0.0d);
            string name = null;

            var customer = new Customer(userId, name, location);
        }

        /// <summary>
        /// Test that IntercomTest.Customer class will not be instantiated with customer name being an empty string.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentException), "IntercomTest.Customer class was instantiated with customer name being an empty string!", AllowDerivedTypes = false)]
        [Description("Test that IntercomTest.Customer class will not be instantiated with customer name being an empty string.")]
        public void TestCustomerNameEmpty()
        {
            var userId = 0;
            var location = GeographicalLocation.FromDegrees(0.0d, 0.0d);
            string name = string.Empty;

            var customer = new Customer(userId, name, location);
        }

        /// <summary>
        /// Test that IntercomTest.Customer class will not be instantiated with customer name being a whitespace string.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentException), "IntercomTest.Customer class was instantiated with customer name being a whitespace string!", AllowDerivedTypes = false)]
        [Description("Test that IntercomTest.Customer class will not be instantiated with customer name being a whitespace string.")]
        public void TestCustomerNameWhitespace()
        {
            const char whitespaceCharacter = ' ';

            var userId = 0;
            var location = GeographicalLocation.FromDegrees(0.0d, 0.0d);

            var random = new Random(Environment.TickCount);
            var numberOfWhitespaces = random.Next(1, 20);
            var name = new string(whitespaceCharacter, numberOfWhitespaces);

            var customer = new Customer(userId, name, location);
        }

        /// <summary>
        /// Test that IntercomTest.Customer class will not be instantiated with location being a null reference.
        /// </summary>
        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException), "IntercomTest.Customer class was instantiated with location being a null reference!")]
        [Description("Test that IntercomTest.Customer class will not be instantiated with location being a null reference")]
        public void TestLocationNull()
        {
            var userId = 0;
            var name = "John Doe";
            GeographicalLocation location = null;

            var customer = new Customer(userId, name, location);
        }
    }
}
