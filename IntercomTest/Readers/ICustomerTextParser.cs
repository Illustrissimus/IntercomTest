using System.Collections.Generic;

namespace IntercomTest.Readers
{
    /// <summary>
    /// Reads customer data from a text.
    /// </summary>
    interface ICustomerTextParser
    {
        /// <summary>
        /// Parse the specified text and extract customer data.
        /// </summary>
        /// <param name="text">text containing the customer data.</param>
        /// <returns>A list of customers.</returns>
        /// <exception cref="IntercomTestException">Thrown if reading of the data failed.</exception>
        List<Customer> ParseText(string text);
    }
}
