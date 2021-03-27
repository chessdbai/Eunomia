// -----------------------------------------------------------------------
// <copyright file="GameResult.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage
{
    /// <summary>
    /// A game result enum.
    /// </summary>
    public enum GameResult
    {
        /// <summary>
        /// Win-by-white. 1-0.
        /// </summary>
        WhiteWon,

        /// <summary>
        /// Win-by-black. 0-1.
        /// </summary>
        BlackWon,

        /// <summary>
        /// Draw. 1/2-1/2.
        /// </summary>
        Draw,

        /// <summary>
        /// Unknown or ongoing result. *.
        /// </summary>
        Indeterminate,

        /// <summary>
        /// Both players lost. 0-0.
        /// </summary>
        DoubleForfeit,
    }
}