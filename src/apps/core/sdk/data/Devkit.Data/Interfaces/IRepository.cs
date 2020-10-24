// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MongoDB.Driver;

    /// <summary>
    /// The repository contract.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="item">The item.</param>
        void Add<T>(T item)
            where T : new();

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="items">The items.</param>
        void Add<T>(IEnumerable<T> items)
            where T : new();

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="items">The items.</param>
        void AddRangeWithAudit<T>(IEnumerable<T> items)
            where T : DocumentBase, new();

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="item">The item.</param>
        void AddWithAudit<T>(T item)
            where T : DocumentBase, new();

        /// <summary>
        /// All instances.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <returns>A queryable collection of documents.</returns>
        List<T> All<T>()
            where T : new();

        /// <summary>
        /// Collections the exists.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <returns>True if the collection exists.</returns>
        bool CollectionExists<T>()
            where T : new();

        /// <summary>
        /// Deletes the specified expression.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="expression">The expression.</param>
        void Delete<T>(Expression<Func<T, bool>> expression)
            where T : new();

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <returns>The MongoCollection of the document type.</returns>
        IMongoCollection<T> GetCollection<T>();

        /// <summary>
        /// Gets a collection using the specified expression as filter.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// A list of T.
        /// </returns>
        List<T> GetMany<T>(Expression<Func<T, bool>> expression)
            where T : new();

        /// <summary>
        /// Gets a paged collection using the specified expression as filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A list of T.</returns>
        List<T> GetMany<T>(Expression<Func<T, bool>> expression, int page, int pageSize)
            where T : new();

        /// <summary>
        /// Singles the specified expression.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>A document.</returns>
        T GetOneOrDefault<T>(Expression<Func<T, bool>> expression)
            where T : new();

        /// <summary>
        /// Edits the specified record matching the filter.
        /// </summary>
        /// <typeparam name="T">Type of the document that needs to be updated.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="updateFunction">The update function.</param>
        /// <returns>
        /// The updated document.
        /// </returns>
        T Update<T>(Expression<Func<T, bool>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> updateFunction)
            where T : new();

        /// <summary>
        /// Edits the specified record matching the filter.
        /// </summary>
        /// <typeparam name="T">Type of the document that needs to be updated.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="updateFunction">The update function.</param>
        /// <returns>
        /// The updated document.
        /// </returns>
        T UpdateWithAudit<T>(Expression<Func<T, bool>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> updateFunction)
            where T : DocumentBase, new();
    }
}