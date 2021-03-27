// -----------------------------------------------------------------------
// <copyright file="PositionRow.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A record storing data about a single position, the move and game that
    /// brought this position to the board, and the following move.
    /// </summary>
    [Table(name: "stgpositions")]
    public class PositionRow
    {
        /// <summary>
        /// Gets or sets the unique id of the game.
        /// </summary>
        [Column("game_id")]
        public string GameUniqueId { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the game.
        /// </summary>
        [Column("dataset")]
        public string Dataset { get; set; }

        /// <summary>
        /// Gets or sets the ply number of this the game.
        /// </summary>
        [Column("game_ply_num")]
        public int GamePlyNumber { get; set; }

        /// <summary>
        /// Gets or sets the move number of this move in the game.
        /// </summary>
        [Column("game_move_num")]
        public int GameMoveNumber { get; set; }

        /// <summary>
        /// Gets or sets the next move made in SAN notation.
        /// </summary>
        [Column("next_move_san")]
        public string NextMoveSan { get; set; }

        /// <summary>
        /// Gets or sets the next move made in FEN notation.
        /// </summary>
        [Column("next_move_uci")]
        public string NextMoveUci { get; set; }

        /// <summary>
        /// Gets or sets the previous move made in SAN notation.
        /// </summary>
        [Column("prev_move_san")]
        public string PreviousMoveSan { get; set; }

        /// <summary>
        /// Gets or sets the previous move made in FEN notation.
        /// </summary>
        [Column("prev_move_uci")]
        public string PreviousMoveUci { get; set; }

        /// <summary>
        /// Gets or sets the FEN position after the move is made.
        /// </summary>
        [Column("fen")]
        public string FenPosition { get; set; }

        /// <summary>
        /// Gets or sets the board state (uncompressed FEN).
        /// </summary>
        [Column("board_state")]
        public string BoardState { get; set; }

        /// <summary>
        /// Gets or sets the game result.
        /// </summary>
        [Column("result")]
        public GameResult GameResult { get; set; }

        /// <summary>
        /// Gets or sets the eco code.
        /// </summary>
        [Column("eco")]
        public string EcoCode { get; set; }

        /// <summary>
        /// Gets or sets the extended ECO code.
        /// </summary>
        [Column("extended_eco")]
        public string ExtendedEco { get; set; }

        /// <summary>
        /// Gets or sets the extended ECO code.
        /// </summary>
        [Column("white_player_name")]
        public string WhitePlayerName { get; set; }

        /// <summary>
        /// Gets or sets the extended ECO code.
        /// </summary>
        [Column("white_player_elo")]
        public int WhitePlayerElo { get; set; }

        /// <summary>
        /// Gets or sets the extended ECO code.
        /// </summary>
        [Column("black_player_name")]
        public string BlackPlayerName { get; set; }

        /// <summary>
        /// Gets or sets the extended ECO code.
        /// </summary>
        [Column("black_player_elo")]
        public int BlackPlayerElo { get; set; }
    }
}