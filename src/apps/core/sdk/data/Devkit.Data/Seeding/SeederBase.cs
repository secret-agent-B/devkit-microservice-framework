// -----------------------------------------------------------------------
// <copyright file="SeederBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Seeding
{
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;

    /// <summary>
    /// The seeder base.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <seealso cref="ISeeder" />
    public abstract class SeederBase<TSource> : ISeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeederBase{TSource}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected SeederBase(IRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public IRepository Repository { get; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public TSource Source { get; protected set; }

        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        public abstract Task Execute();

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public abstract void InitializeSource();
    }
}