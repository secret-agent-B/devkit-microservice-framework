// -----------------------------------------------------------------------
// <copyright file="CoinsPhConfiguration.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH
{
    /// <summary>
    /// The CoinsPhConfiguration.
    /// </summary>
    public class CoinsPHConfiguration
    {
        /// <summary>
        /// The coins ph configuration key.
        /// </summary>
        public const string ConfigSection = "CoinsPH";

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the create invoice callback URL.
        /// </summary>
        /// <value>
        /// The create invoice callback URL.
        /// </value>
        public string InvoiceCallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the create invoice URL.
        /// </summary>
        /// <value>
        /// The create invoice URL.
        /// </value>
        public string InvoiceRoute { get; set; }
    }
}