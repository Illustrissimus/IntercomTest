using System.Collections.Generic;

namespace IntercomTest
{
    /// <summary>
    /// Reader of customer data.
    /// </summary>
    public interface ICustomerReader
    {
        /// <summary>
        /// Reads customer data from a source and returns a list of customers.
        /// </summary>
        /// <returns>A list of customers</returns>
        /// <exception cref="IntercomTestException">Thrown if customer data could not be read.</exception>
        List<Customer> ReadCustomers();
    }
}
