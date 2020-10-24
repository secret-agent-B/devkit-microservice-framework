// -----------------------------------------------------------------------
// <copyright file="EnumerationException.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.Exceptions
{
    using System;

    /// <summary>
    /// The EnumerationException is an exception that is thrown for class enumeration issues.
    /// </summary>
    public class EnumerationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public EnumerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerationException"/> class.
        /// </summary>
        public EnumerationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EnumerationException(string message)
            : base(message)
        {
        }
    }
}