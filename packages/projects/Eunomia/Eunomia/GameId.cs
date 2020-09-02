//-----------------------------------------------------------------------
// <copyright file="GameId.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia
{
    /// <summary>
    /// Properties that uniquely identify a game (or help to
    /// deduplicate a game).
    /// </summary>
    public struct GameId
    {
        /// <summary>
        /// Gets or sets the canonicalized identifier for the game's main line.
        /// </summary>
        public string CanonicalizedId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for this game.
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the public identifier.
        /// </summary>
        public string PublicId { get; set; }
    }
}