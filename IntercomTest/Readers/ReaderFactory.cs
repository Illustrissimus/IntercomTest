namespace IntercomTest.Readers
{
    /// <summary>
    /// Customer reader factory.
    /// </summary>
    public static class ReaderFactory
    {
        /// <summary>
        /// Creates a JSON text file reader.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <returns>JSON text file reader.</returns>
        /// <exception cref="ArgumentException">Path is a zero-length string.</exception>
        /// <exception cref="ArgumentNullException">Path or customer reader is null</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in path was not found.</exception>
        public static ICustomerReader CreateJsonTextReader(string path)
        {
            var jsonParser = new JsonTextParser();
            return new JsonTextFileReader(path, jsonParser);
        }
    }
}
