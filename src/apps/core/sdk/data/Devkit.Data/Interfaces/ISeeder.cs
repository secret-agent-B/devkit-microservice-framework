// -----------------------------------------------------------------------
// <copyright file="ISeeder.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Interfaces
{
    /// <summary>
    /// The seeder interface.
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        void Execute();
    }
}