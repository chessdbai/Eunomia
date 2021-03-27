// -----------------------------------------------------------------------
// <copyright file="ChessContext.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries
{
    using System.Collections.Concurrent;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A ChessContext class.
    /// </summary>
    public class ChessContext : DbContext
    {
        private static ConcurrentDictionary<string, ChessContext> cachedContexts =
            new ConcurrentDictionary<string, ChessContext>();

        private readonly string databaseName;
        private readonly string tableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessContext" /> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="tableName">Name of the table.</param>
        private ChessContext(string databaseName, string tableName)
        {
            this.databaseName = databaseName;
            this.tableName = tableName;
        }

        /// <summary>
        /// Gets or sets the db set for positions.
        /// </summary>
        public DbSet<PositionRow> Positions { get; set; }

        /// <summary>
        /// Get a cached <see cref="ChessContext"/> object for a database and table name.
        /// </summary>
        /// <param name="dbName">The database name.</param>
        /// <param name="tableName">The table name.</param>
        /// <returns>A <see cref="ChessContext"/> object.</returns>
        public static ChessContext GetContext(string dbName, string tableName)
        {
            string fullName = $"{dbName}.{tableName}";
            if (cachedContexts.TryGetValue(fullName, out var context))
            {
                return context;
            }

            var newContext = new ChessContext(dbName, tableName);
            cachedContexts.TryAdd(fullName, newContext);
            return newContext;
        }

        /// <inheritdoc cref="DbContext"/>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=C:\blogging.db");
        }

        /// <inheritdoc cref="DbContext"/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PositionRow>()
                .HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}