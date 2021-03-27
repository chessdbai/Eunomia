// -----------------------------------------------------------------------
// <copyright file="MoveFilter.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage
{
    /// <summary>
    /// A MoveFilter class.
    /// </summary>
    public record MoveFilter
    {
        /// <summary>
        /// Gets or sets the move text.
        /// </summary>
        public string MoveText { get; set; }

        /// <summary>
        /// Gets or sets the move format (UCI/SAN).
        /// </summary>
        public MoveFormat MoveFormat { get; set; }
    }
}