//-----------------------------------------------------------------------
// <copyright file="DatasetId.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia
{
    /// <summary>
    /// Collection of properties that uniquely identify the set of
    /// data that a game or position game from.
    /// </summary>
    public struct DatasetId
    {
        /// <summary>
        /// Gets or sets the author of the dataset.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the name of the dataset.
        /// </summary>
        public string Name { get; set; }
    }
}