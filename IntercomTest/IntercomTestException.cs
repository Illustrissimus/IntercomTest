using System;

namespace IntercomTest
{
    /// <summary>
    /// Represents an error which ocurred while running the IntercomTest application. It may contain inner exceptions thrown by other parts of the code.
    /// </summary>
    public class IntercomTestException : Exception
    {
        /// <summary>
        /// Gets the custom error message.
        /// </summary>
        public string IntercomTestErrorMessage { get; }

        /// <summary>
        /// Gets a value indicating whether the current exception has an inner exception.
        /// </summary>
        public bool HasInnerException { get; }

        /// <summary>
        /// Creates a new instance of the IntercomTest.IntercomTestException class with the specified error message.
        /// </summary>
        /// <param name="intercomTestErrorMessage">Error messsage.</param>
        public IntercomTestException(string intercomTestErrorMessage) :
            base(intercomTestErrorMessage)
        {
            IntercomTestErrorMessage = IntercomTestErrorMessage;
        }

        /// <summary>
        /// Creates a new instance of the IntercomConsoleApp.IntercomAppException class with the specified error message and inner
        /// exception which caused the error.
        /// </summary>
        /// <param name="intercomTestErrorMessage">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public IntercomTestException(string intercomTestErrorMessage, Exception innerException) :
            base(intercomTestErrorMessage, innerException)
        {
            IntercomTestErrorMessage = intercomTestErrorMessage;
            HasInnerException = true;
        }
    }
}
