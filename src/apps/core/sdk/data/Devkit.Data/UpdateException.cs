// -----------------------------------------------------------------------
// <copyright file="UpdateException.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data
{
    using System.Diagnostics.CodeAnalysis;
    using MongoDB.Driver;

    /// <summary>
    /// Mongo Db update exception.
    /// </summary>
    /// <seealso cref="MongoException" />
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "MongoException cannot take in 0 args.")]
    public class UpdateException : MongoException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UpdateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UpdateException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}