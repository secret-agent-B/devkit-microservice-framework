// -----------------------------------------------------------------------
// <copyright file="UnitTestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System;
    using Devkit.Data;
    using Mongo2Go;
    using Xunit;

    /// <summary>
    /// The base class that helps facilitate unit tests.
    /// </summary>
    /// <typeparam name="T">The type of input to the handler that is being tested.</typeparam>
    /// <seealso cref="TestBase{T}" />
    [Trait("Category", "Unit Test")]
    public abstract class UnitTestBase<T> : TestBase<T>
    {
        /// <summary>
        /// The runner.
        /// </summary>
        private readonly MongoDbRunner _runner;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestBase{T}" /> class.
        /// </summary>
        protected UnitTestBase()
            : base()
        {
            // setup the database.
            this._runner = MongoDbRunner.Start();
            this.Repository = new Repository(new RepositoryOptions
            {
                ConnectionString = this._runner.ConnectionString,
                DatabaseName = Guid.NewGuid().ToString("N")
            });

            // seed the database.
            this.SeedDatabase();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this._runner.Dispose();
        }
    }
}