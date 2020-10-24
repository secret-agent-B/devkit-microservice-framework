// -----------------------------------------------------------------------
// <copyright file="InstanceFaker.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System.Collections.Generic;
    using Bogus;
    using Devkit.Test.Interfaces;

    /// <summary>
    /// The EntityFaker is creates test instances of a specified type.
    /// </summary>
    /// <typeparam name="T">Type of object to fake.</typeparam>
    /// <seealso cref="IFaker{T}" />
    public abstract class FakerBase<T> : IFaker<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakerBase{T}"/> class.
        /// </summary>
        protected FakerBase()
        {
            this.Faker = new Faker<T>();
        }

        /// <summary>
        /// Gets the faker.
        /// </summary>
        /// <value>
        /// The faker.
        /// </value>
        protected Faker<T> Faker { get; }

        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public abstract T Generate();

        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>
        /// A list of T.
        /// </returns>
        public List<T> Generate(int count)
        {
            var fakes = new List<T>();

            for (int i = 0; i < count; i++)
            {
                fakes.Add(this.Generate());
            }

            return fakes;
        }
    }
}