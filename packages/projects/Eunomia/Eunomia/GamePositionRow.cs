//-----------------------------------------------------------------------
// <copyright file="GamePositionRow.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia
{
    /// <summary>
    /// A record storing data about a single position, the move and game that
    /// brought this position to the board, and the following move.
    /// </summary>
    public class GamePositionRow
    {
        /// <summary>
        /// Gets or sets the unique id of the game.
        /// </summary>
        public string GameUniqueId { get; set; }

        /// <summary>
        /// Gets or sets the ply number of this the game.
        /// </summary>
        public int GamePlyNumber { get; set; }

        /// <summary>
        /// Gets or sets the move number of this move in the game.
        /// </summary>
        public int GameMoveNumber { get; set; }

        /// <summary>
        /// Gets or sets the next move made in SAN notation.
        /// </summary>
        public string NextMoveSan { get; set; }

        /// <summary>
        /// Gets or sets the next move made in FEN notation.
        /// </summary>
        public string NextMoveUci { get; set; }

        /// <summary>
        /// Gets or sets the previous move made in SAN notation.
        /// </summary>
        public string PreviousMoveSan { get; set; }

        /// <summary>
        /// Gets or sets the previous move made in FEN notation.
        /// </summary>
        public string PreviousMoveUci { get; set; }

        /// <summary>
        /// Gets or sets the FEN position after the move is made.
        /// </summary>
        public string FenPosition { get; set; }

        /// <summary>
        /// Gets or sets the eco code.
        /// </summary>
        public string EcoCode { get; set; }
    }
}