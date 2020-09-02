//-----------------------------------------------------------------------
// <copyright file="DynamoGameManager.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia.Storage.Games
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2.DocumentModel;
    using Eunomia;

    /// <summary>
    /// A <see cref="IGameManager"/> that saves games to
    /// DynamoDB for quick system lookup by the game identifier.
    /// </summary>
    public class DynamoGameManager : IGameManager
    {
        private readonly Table table;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamoGameManager"/> class.
        /// </summary>
        /// <param name="table">The DynamoDB table object.</param>
        public DynamoGameManager(Table table)
        {
            this.table = table;
        }

        /// <inheritdoc cref="IGameManager"/>
        public async Task SaveGameAsync(IndexedGame game)
        {
            var doc = game.ToDocument();
            await this.table.PutItemAsync(doc);
        }

        /// <inheritdoc cref="IGameManager"/>
        public async Task SaveGameBatchAsync(List<IndexedGame> games)
        {
            var tasks = games
                .Partition(25)
                .Select(partition => this.SaveSingleBatchAsync(partition.ToList()));
            await Task.WhenAll(tasks);
        }

        /// <inheritdoc cref="IGameManager"/>
        public async Task<IndexedGame> LoadGameAsync(string uniqueId)
        {
            var gameDoc = await this.table.GetItemAsync(uniqueId);
            return gameDoc.ToGame();
        }

        private async Task SaveSingleBatchAsync(List<IndexedGame> smallBatch)
        {
            var write = this.table.CreateBatchWrite();
            foreach (var i in smallBatch)
            {
                write.AddDocumentToPut(i.ToDocument());
            }

            await write.ExecuteAsync();
        }
    }
}