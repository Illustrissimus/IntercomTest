using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace IntercomTest.Readers
{
    class JsonTextFileReader : ICustomerReader
    {
        /// <summary>
        /// Customer file path.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Json text parser.
        /// </summary>
        private readonly ICustomerTextParser _customerTextParser;

        /// <summary>
        /// Creates a new instance of the IntercomConsoleApp.Readers.JsonTextFileReader class with the specified file path.
        /// </summary>
        /// <param name="path">Customer file path</param>
        /// <param name="customerTextParser">Customer text parser.</param>
        /// <exception cref="ArgumentException">Path is a zero-length string.</exception>
        /// <exception cref="ArgumentNullException">Path or customer reader is null</exception>
        /// <exception cref="FileNotFoundException">The file specified in path was not found.</exception>
        public JsonTextFileReader(string path, ICustomerTextParser customerTextParser)
        {
            if (ReferenceEquals(path, null))
                throw new ArgumentNullException(nameof(path), "Customer file path cannot be null!");

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Customer file path cannot be an empty string!", nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException("Customer file not found!", path);

            _path = path;

            if (ReferenceEquals(customerTextParser, null))
                throw new ArgumentNullException(nameof(customerTextParser), "Customer text parser cannot be null!");
            _customerTextParser = customerTextParser;
        }

        /// <summary>
        /// Reads the customer list from a JSON text file.
        /// </summary>
        /// <returns>A list of customers.</returns>
        /// <exception cref="IntercomTestException">Thrown if customer data could not be read.</exception>
        /// <exception cref="UnauthorizedAccessException">The user does not have the required permission.</exception>
        /// <exception cref="ArgumentException">path is a zero-length string, contains only white space, or contains one or more invalid characters as defined by InvalidPathChars.</exception>
        /// <exception cref="PathTooLongException">The specified file name exceeds the system-defined maximum length.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="NotSupportedException">path is in an invalid format.</exception>
        /// <exception cref="SecurityException">Thrown if the user does not have the permission to accss the file.</exception>
        public List<Customer> ReadCustomers()
        {
            try
            {
                var text = File.ReadAllText(_path)?.Trim();

                if (string.IsNullOrWhiteSpace(text))
                    throw new IntercomTestException("Customer file is empty!");

                return _customerTextParser.ParseText(text);
            }
            catch (ArgumentException ae)
            {
                throw new IntercomTestException("Customer file path is invalid!", ae);
            }
            catch (PathTooLongException ptle)
            {
                throw new IntercomTestException(String.Format("Customer file path is too long! Length: {0}.", _path.Length), ptle);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw new IntercomTestException("Customer file is read-only or the operation is not supported or the specified path is a " +
                    "directory or the user does not have a permmission to access the file!", uae);
            }
            catch (NotSupportedException nse)
            {
                throw new IntercomTestException("Customer file path is invalid format!", nse);
            }
            catch (SecurityException se)
            {
                throw new IntercomTestException("The current user does not have a permission to access the customer file.", se);
            }
            catch (IntercomTestException ite)
            {
                throw ite;
            }
            catch (Exception e)
            {
                throw new IntercomTestException("An unknown error has ocurred!", e);
            }
        }
    }
}
