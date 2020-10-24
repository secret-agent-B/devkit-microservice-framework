// -----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Devkit.Data.Interfaces;
    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>
    /// Repository for MongoDb.
    /// </summary>
    public class Repository : IRepository
    {
        /// <summary>
        /// The database.
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="optionsAccessor">The options accessor.</param>
        public Repository(RepositoryOptions optionsAccessor)
        {
            var client = new MongoClient(optionsAccessor?.ConnectionString);
            this._database = client.GetDatabase(optionsAccessor.DatabaseName);
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="item">The item.</param>
        public void Add<T>(T item)
            where T : new()
        {
            this.GetCollection<T>().InsertOne(item);
        }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="items">The items.</param>
        public void Add<T>(IEnumerable<T> items)
            where T : new()
        {
            this.GetCollection<T>().InsertMany(items);
        }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="items">The items.</param>
        public void AddRangeWithAudit<T>(IEnumerable<T> items)
            where T : DocumentBase, new()
        {
            if (items?.Any() ?? false)
            {
                var createdOn = DateTime.Now;

                foreach (var item in items)
                {
                    item.CreatedOn = createdOn;
                }

                this.GetCollection<T>().InsertMany(items);
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="item">The item.</param>
        public void AddWithAudit<T>(T item)
            where T : DocumentBase, new()
        {
            if (item != null)
            {
                item.CreatedOn = DateTime.Now;
                this.GetCollection<T>().InsertOne(item);
            }
        }

        /// <summary>
        /// All instances.
        /// </summary>
        /// <typeparam name="T">The document type.</typeparam>
        /// <returns>A queryable document collection.</returns>
        public List<T> All<T>()
            where T : new()
        {
            return this.GetCollection<T>().AsQueryable().ToList();
        }

        /// <summary>
        /// Collections the exists.
        /// </summary>
        /// <typeparam name="T">The document type.</typeparam>
        /// <returns>True if the collection exist.</returns>
        public bool CollectionExists<T>()
            where T : new()
        {
            var collection = this.GetCollection<T>();
            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);

            return totalCount > 0;
        }

        /// <summary>
        /// Deletes the specified predicate.
        /// </summary>
        /// <typeparam name="T">The document type.</typeparam>
        /// <param name="expression">The predicate.</param>
        public void Delete<T>(Expression<Func<T, bool>> expression)
            where T : new()
        {
            this.GetCollection<T>().DeleteMany(expression);
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="T">Type of document.</typeparam>
        /// <returns>
        /// The MongoCollection of the document type.
        /// </returns>
        public IMongoCollection<T> GetCollection<T>()
        {
            return this._database.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// Wheres the specified expression.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// A queryable collection of documents.
        /// </returns>
        public List<T> GetMany<T>(Expression<Func<T, bool>> expression)
            where T : new()
        {
            return this.GetCollection<T>()
                .AsQueryable()
                .Where(expression)
                .ToList();
        }

        /// <summary>
        /// Wheres the specified expression.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A queryable collection of documents.
        /// </returns>
        public List<T> GetMany<T>(Expression<Func<T, bool>> expression, int page, int pageSize)
            where T : new()
        {
            return this.GetCollection<T>()
                .AsQueryable()
                .Where(expression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Singles the specified expression.
        /// </summary>
        /// <typeparam name="T">The document type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>A single instance of a document.</returns>
        public T GetOneOrDefault<T>(Expression<Func<T, bool>> expression)
            where T : new()
        {
            return this.GetCollection<T>()
                .AsQueryable()
                .Where(expression)
                .SingleOrDefault();
        }

        /// <summary>
        /// Edits the specified record matching the filter.
        /// </summary>
        /// <typeparam name="T">Type of the document that needs to be updated.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="updateFunction">The update function.</param>
        /// <returns>
        /// The updated document.
        /// </returns>
        public T Update<T>(Expression<Func<T, bool>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> updateFunction)
            where T : new()
        {
            var update = updateFunction?.Invoke(Builders<T>.Update);

            var result = this.GetCollection<T>().UpdateOne<T>(filter, update);

            if (result.IsAcknowledged)
            {
                return this.GetOneOrDefault(filter);
            }

            throw new UpdateException($"Update for type {typeof(T)} failed.");
        }

        /// <summary>
        /// Edits the specified filter.
        /// </summary>
        /// <typeparam name="T">Type of the document that needs to be updated.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="updateFunction">The update function.</param>
        /// <returns>
        /// The updated document.
        /// </returns>
        public T UpdateWithAudit<T>(Expression<Func<T, bool>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> updateFunction)
            where T : DocumentBase, new()
        {
            var update = updateFunction?
                .Invoke(Builders<T>.Update)
                .Set(x => x.LastUpdatedOn, DateTime.Now);

            var result = this.GetCollection<T>().UpdateOne<T>(filter, update);

            if (result.IsAcknowledged)
            {
                return this.GetOneOrDefault(filter);
            }

            throw new UpdateException($"Update for type {typeof(T)} failed.");
        }
    }
}