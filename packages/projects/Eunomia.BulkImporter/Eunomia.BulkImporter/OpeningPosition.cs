//-----------------------------------------------------------------------
// <copyright file="OpeningPosition.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia.BulkImporter
{
    /// <summary>
    /// An opening position, with a name, FEN string, and ECO code.
    /// </summary>
    public class OpeningPosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningPosition"/> class.
        /// </summary>
        public OpeningPosition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningPosition"/> class.
        /// </summary>
        /// <param name="eco">The ECO code of the opening position.</param>
        /// <param name="name">The name of this opening.</param>
        /// <param name="fen">The FEN string.</param>
        public OpeningPosition(string eco, string name, string fen)
        {
            this.ECO = eco;
            this.Name = name;
            this.Fen = fen;
        }

        /// <summary>
        /// Gets or sets the ECO code of the opening position.
        /// </summary>
        public string ECO { get; set; }

        /// <summary>
        /// Gets or sets the position of this opening.
        /// </summary>
        public string Fen { get; set; }

        /// <summary>
        /// Gets or sets the name of this opening.
        /// </summary>
        public string Name { get; set; }
    }
}