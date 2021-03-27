//-----------------------------------------------------------------------
// <copyright file="GamePositionRow.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia
{
    using System;

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
        /// Gets or sets the unique id of the game.
        /// </summary>
        public string Dataset { get; set; }

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
        /// Gets or sets the board state (uncompressed FEN).
        /// </summary>
        public string BoardState { get; set; }

        /// <summary>
        /// Gets or sets the game result.
        /// </summary>
        public string GameResult { get; set; }

        /// <summary>
        /// Gets or sets the eco code.
        /// </summary>
        public string EcoCode { get; set; }

        /// <summary>
        /// Gets or sets the extended ECO code.
        /// </summary>
        public string ExtendedEco { get; set; }

        /// <summary>
        /// Returns a simple equality check.
        /// </summary>
        /// <param name="obj">The other row to compare to.</param>
        /// <returns>A value indicating whether the given row object is the same as the present object.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((GamePositionRow)obj);
        }

        /// <summary>
        /// Returns the has code.
        /// </summary>
        /// <returns>The has code of this object.</returns>
        public override int GetHashCode()
        {
            var hashCode = default(HashCode);
            hashCode.Add(this.GameUniqueId);
            hashCode.Add(this.GamePlyNumber);
            hashCode.Add(this.GameMoveNumber);
            hashCode.Add(this.NextMoveSan);
            hashCode.Add(this.NextMoveUci);
            hashCode.Add(this.PreviousMoveSan);
            hashCode.Add(this.PreviousMoveUci);
            hashCode.Add(this.GameResult);
            hashCode.Add(this.FenPosition);
            hashCode.Add(this.EcoCode);
            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Returns a simple equality check.
        /// </summary>
        /// <param name="other">The other row to compare to.</param>
        /// <returns>A value indicating whether the given row object is the same as the present object.</returns>
        protected bool Equals(GamePositionRow other)
        {
            return this.GameUniqueId == other.GameUniqueId &&
                   this.GamePlyNumber == other.GamePlyNumber &&
                   this.GameMoveNumber == other.GameMoveNumber &&
                   this.NextMoveSan == other.NextMoveSan &&
                   this.NextMoveUci == other.NextMoveUci &&
                   this.PreviousMoveSan == other.PreviousMoveSan &&
                   this.PreviousMoveUci == other.PreviousMoveUci &&
                   this.GameResult == other.GameResult &&
                   this.FenPosition == other.FenPosition &&
                   this.EcoCode == other.EcoCode;
        }
    }
}