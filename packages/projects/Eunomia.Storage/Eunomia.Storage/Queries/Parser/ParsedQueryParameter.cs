// -----------------------------------------------------------------------
// <copyright file="ParsedQueryParameter.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries.Parser
{
    /// <summary>
    /// A ParsedQueryParameter class.
    /// </summary>
    public record ParsedQueryParameter
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the value of the parameter.
        /// </summary>
        public string Value { get; init; }
    }
}