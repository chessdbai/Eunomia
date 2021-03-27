// -----------------------------------------------------------------------
// <copyright file="ParsedQuery.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries.Parser
{
    using System.Collections.Generic;

    /// <summary>
    /// A ParsedQuery class.
    /// </summary>
    public record ParsedQuery
    {
        /// <summary>
        /// Gets the query portion.
        /// </summary>
        public string Query { get; init; }

        /// <summary>
        /// Gets the parameters found in the parsed query.
        /// </summary>
        public List<ParsedQueryParameter> Parameters { get; init; }

        /// <summary>
        /// Gets the LIKE clauses in the parsed query.
        /// </summary>
        public List<ParsedRegexQueryParameter> LikeClauses { get; init; }
    }
}