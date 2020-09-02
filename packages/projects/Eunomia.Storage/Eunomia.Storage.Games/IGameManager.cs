//-----------------------------------------------------------------------
// <copyright file="IGameManager.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia.Storage.Games
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface declaring methods that a game manager must implement.
    /// </summary>
    public interface IGameManager
    {
        /// <summary>
        /// Save a game.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <returns>An awaitable task.</returns>
        Task SaveGameAsync(IndexedGame game);

        /// <summary>
        /// Save a game.
        /// </summary>
        /// <param name="games">The games to save. Must contain no more than 1000 games.</param>
        /// <returns>An awaitable task.</returns>
        Task SaveGameBatchAsync(List<IndexedGame> games);

        /// <summary>
        /// Fetch a game from the database.
        /// </summary>
        /// <param name="uniqueId">The id of the game to fetch.</param>
        /// <returns>The game with the id.</returns>
        Task<IndexedGame> LoadGameAsync(string uniqueId);
    }
}