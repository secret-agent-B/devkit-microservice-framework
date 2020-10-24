// -----------------------------------------------------------------------
// <copyright file="ValidatorExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.Extensions
{
    using System;
    using FluentValidation;

    /// <summary>
    /// The ValidatorExtensions extends the IRuleBuilderInitial capabilities.
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Valids the base64 image.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilderOptions">The rule builder options.</param>
        /// <returns>
        /// An instance of a rule builder options.
        /// </returns>
        public static IRuleBuilderOptions<T, string> ValidBase64Image<T>(this IRuleBuilderOptions<T, string> ruleBuilderOptions)
        {
            ruleBuilderOptions.Must(photo =>
                {
                    try
                    {
                        const string base64Key = "base64,";

                        if (photo.Contains(base64Key))
                        {
                            photo = photo.Substring(photo.IndexOf(base64Key) + base64Key.Length);
                        }

                        byte[] bytes = Convert.FromBase64String(photo);

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage("Item photo is not a valid base 64 image");

            return ruleBuilderOptions;
        }
    }
}