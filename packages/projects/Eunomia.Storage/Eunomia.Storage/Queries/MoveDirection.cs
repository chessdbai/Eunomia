// -----------------------------------------------------------------------
// <copyright file="MoveDirection.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries
{
    /// <summary>
    /// An enum used to indicate whether a move filter refers to
    /// the next move or previous move.
    /// </summary>
    public enum MoveDirection
    {
        /// <summary>
        /// Indicates the move filter refers to the next move.
        /// </summary>
        NextMove,

        /// <summary>
        /// Indicates the move filter refers to the previous move.
        /// </summary>
        PreviousMove,
    }
}