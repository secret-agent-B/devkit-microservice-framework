// -----------------------------------------------------------------------
// <copyright file="PayMayaConfiguration.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.PayMaya.Configurations
{
    using System.Collections.Generic;

    /// <summary>
    /// The PayMayaConfiguration.
    /// </summary>
    public class PayMayaConfiguration
    {
        /// <summary>
        /// The coins ph configuration key.
        /// </summary>
        public const string ConfigSection = "PayMaya";

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string APIKey { get; set; }

        /// <summary>
        /// Gets or sets the API secret.
        /// </summary>
        /// <value>
        /// The API secret.
        /// </value>
        public string APISecret { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the cancel URL.
        /// </summary>
        /// <value>
        /// The cancel URL.
        /// </value>
        public string CancelUrl { get; set; }

        /// <summary>
        /// Gets or sets the faulire URL.
        /// </summary>
        /// <value>
        /// The faulire URL.
        /// </value>
        public string FailureUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sandbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sandbox; otherwise, <c>false</c>.
        /// </value>
        public bool IsSandbox { get; set; }

        /// <summary>
        /// Gets or sets the success URL.
        /// </summary>
        /// <value>
        /// The success URL.
        /// </value>
        public string SuccessUrl { get; set; }
    }
}