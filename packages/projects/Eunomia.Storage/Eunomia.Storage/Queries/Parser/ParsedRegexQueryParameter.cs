// -----------------------------------------------------------------------
// <copyright file="ParsedRegexQueryParameter.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries.Parser
{
    /// <summary>
    /// A ParsedRegexQueryParameter class.
    /// </summary>
    public record ParsedRegexQueryParameter
    {
        /// <summary>
        /// Gets the column name.
        /// </summary>
        public string ColumnName { get; init; }

        /// <summary>
        /// Gets the regex value.
        /// </summary>
        public string RegexValue { get; init; }

        /// <summary>
        /// Gets the original clause text that we need to replace.
        /// </summary>
        public string LikeClauseText { get; init; }
    }
}