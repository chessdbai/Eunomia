//-----------------------------------------------------------------------
// <copyright file="IGameFlattener.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia.BulkImporter
{
    using System.Collections.Generic;
    using Aletheia.Pgn.Model;

    /// <summary>
    /// An interface declaring methods required to be a game flattener.
    /// </summary>
    public interface IGameFlattener
    {
        /// <summary>
        /// Flatten a game into its indexed form and a list of the positions.
        /// </summary>
        /// <param name="pgnGame">The parsed PGN game object to flatten.</param>
        /// <param name="pgnText">The original text of the PGN Game.</param>
        /// <param name="datasetId">The dataset for this import job.</param>
        /// <returns>A 2-tuple with the indexed game and a list of positions.</returns>
        (IndexedGame, List<GamePositionRow>) FlattenPgnGame(
            PgnGame pgnGame,
            string pgnText,
            DatasetId datasetId);
    }
}