// -----------------------------------------------------------------------
// <copyright file="RequestException.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;

    /// <summary>
    /// The request exception.
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AppException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public AppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException" /> class.
        /// </summary>
        internal AppException()
        {
            this.Errors = new Dictionary<string, IEnumerable<string>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException" /> class.
        /// </summary>
        /// <param name="failures">The failures.</param>
        internal AppException(IReadOnlyCollection<ValidationFailure> failures)
        {
            this.Errors = new Dictionary<string, IEnumerable<string>>();

            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures.Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .Distinct()
                    .ToArray();

                this.Errors.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// Gets the failures.
        /// </summary>
        /// <value>
        /// The failures.
        /// </value>
        public IDictionary<string, IEnumerable<string>> Errors { get; }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="errorMessage">The error message.</param>
        public void Add(string key, string errorMessage)
        {
            this.AddRange(key, new[] { errorMessage });
        }

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="errorMessages">The error messages.</param>
        public void AddRange(string key, IEnumerable<string> errorMessages)
        {
            if (this.Errors.ContainsKey(key))
            {
                var errorsTemp = this.Errors[key].ToList();
                var enumerable = errorMessages as string[] ?? errorMessages.ToArray();
                errorsTemp.AddRange(enumerable);

                this.Errors[key] = errorsTemp.Distinct().ToList();
            }
            else
            {
                this.Errors.Add(key, errorMessages.Distinct().ToList());
            }
        }
    }
}