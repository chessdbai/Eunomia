//-----------------------------------------------------------------------
// <copyright file="IndexedGame.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A searchable game that contains all the PGN tags.
    /// </summary>
    public class IndexedGame
    {
        /// <summary>
        /// Gets or sets the unique identifier for the game.
        /// </summary>
        public GameId Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the dataset.
        /// </summary>
        public DatasetId Dataset { get; set; }

        /// <summary>
        /// Gets or sets the timestamp that this game was imported.
        /// </summary>
        public DateTime ImportTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the player with the white pieces.
        /// </summary>
        public string White { get; set; }

        /// <summary>
        /// Gets or sets the name of the player with the black pieces.
        /// </summary>
        public string Black { get; set; }

        /// <summary>
        /// Gets or sets the name of the event the game was played at.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Gets or sets the location the game was played at.
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the location the game was played at.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the round of the tournament that the game was played.
        /// </summary>
        public string Round { get; set; }

        /// <summary>
        /// Gets or sets the result of the game.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the tag dictionary for tags other than those contained in the seven-tag-roster.
        /// </summary>
        public Dictionary<string, string> OtherTags { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the PGN text.
        /// </summary>
        public string GamePgn { get; set; }
    }
}