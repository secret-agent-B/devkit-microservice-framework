// -----------------------------------------------------------------------
// <copyright file="ResponseBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS
{
    using System.Collections.Generic;
    using System.Linq;
    using Devkit.Patterns.CQRS.Contracts;
    using Newtonsoft.Json;

    /// <summary>
    /// The output base class.
    /// </summary>
    public class ResponseBase : IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase"/> class.
        /// </summary>
        public ResponseBase()
        {
            this.Exceptions = new Dictionary<string, IEnumerable<string>>();
        }

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>
        /// The exceptions.
        /// </value>
        [JsonIgnore]
        public IDictionary<string, IEnumerable<string>> Exceptions { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsSuccessful
        {
            get
            {
                return !this.Exceptions.Any();
            }
        }
    }
}