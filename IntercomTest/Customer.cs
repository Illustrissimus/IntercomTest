using System;

namespace IntercomTest
{
    /// <summary>
    /// Stores customer data.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets customer user ID.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Gets customer name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets customer geographical location.
        /// </summary>
        public GeographicalLocation Location { get; }

        /// <summary>
        /// Creates a new instance of the IntercomTest class with the specified parameters.
        /// </summary>
        /// <param name="userId">Customer user ID.</param>
        /// <param name="name">Customer name.</param>
        /// <param name="location">Customer location.</param>
        /// <exception cref="ArgumentNullException">Thrown if customer name or location is null.</exception>
        /// <exception cref="ArgumentException">Thrown if customer name is an empty string.</exception>
        public Customer(int userId, string name, GeographicalLocation location)
        {
            UserId = userId;

            if (ReferenceEquals(name, null))
                throw new ArgumentNullException(nameof(name), CreateNameNullMessage());

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(CreateNameEmptyMessage(), nameof(name));

            Name = name.Trim();

            if (ReferenceEquals(location, null))
                throw new ArgumentNullException(nameof(location), CreateLocationNullMessage());

            Location = location;
        }

        /// <summary>
        /// Creates an error message when customer name is null.
        /// </summary>
        /// <returns>Erro message.</returns>
        private static string CreateNameNullMessage()
        {
            return "Customer name cannot be null!";
        }

        /// <summary>
        /// Creates an error message when customer name is an empty string.
        /// </summary>
        /// <returns>Erro message.</returns>
        private static string CreateNameEmptyMessage()
        {
            return "Customer name cannot be empty!";
        }

        /// <summary>
        /// Creates an error message when customer location has not been specified, i.e. is null.
        /// </summary>
        /// <returns>Error message.</returns>
        private static string CreateLocationNullMessage()
        {
            return "Customer location cannot be null!";
        }
    }
}
