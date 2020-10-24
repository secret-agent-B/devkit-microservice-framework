// -----------------------------------------------------------------------
// <copyright file="TestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System;
    using System.Collections.Generic;
    using Bogus;
    using Devkit.Data.Interfaces;

    /// <summary>
    /// The test base class.
    /// </summary>
    /// <typeparam name="TRequest">The type of object to build.</typeparam>
    public abstract class TestBase<TRequest> : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase{T}" /> class.
        /// </summary>
        protected TestBase()
        {
            this.Faker = new Faker();
        }

        /// <summary>
        /// Gets the faker.
        /// </summary>
        /// <value>
        /// The faker.
        /// </value>
        protected Faker Faker { get; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        protected IRepository Repository { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>An instance of T.</returns>
        protected virtual TRequest Build()
        {
            return Activator.CreateInstance<TRequest>();
        }

        /// <summary>
        /// Builds the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>A collection of T.</returns>
        protected IEnumerable<TRequest> Build(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return this.Build();
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // do nothing by default...
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected virtual void SeedDatabase()
        {
            // do nothing by default...
        }
    }
}