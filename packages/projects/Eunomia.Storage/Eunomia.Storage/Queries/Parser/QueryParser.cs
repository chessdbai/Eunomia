// -----------------------------------------------------------------------
// <copyright file="QueryParser.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries.Parser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A QueryParser class.
    /// </summary>
    public static class QueryParser
    {
        private const string LikeClausePatternText = "\"[a-z]+\".\"[a-z0-9-_]+\" LIKE @__[a-zA-Z0-9_]+";
        private static readonly Regex LikeClausePattern = new Regex(LikeClausePatternText);

        /// <summary>
        /// Parses a LINQ SQL query, separating the parameter declaration
        /// from the SQL body.
        /// </summary>
        /// <param name="query">The LINQ T-SQL expression.</param>
        /// <returns>The broken-down query.</returns>
        public static ParsedQuery ParseQuery(string query)
        {
            using var reader = new StringReader(query);
            var parameters = new List<ParsedQueryParameter>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null || !line.StartsWith(".param"))
                {
                    break;
                }

                var lineParts = line!
                    .Split(' ')
                    .Skip(1)
                    .ToList();
                string commandName = lineParts[0];
                if (commandName == "set")
                {
                    var paramName = lineParts[1];
                    var paramValue =
                        string.Join(' ', lineParts.Skip(2));
                    parameters.Add(new ParsedQueryParameter()
                    {
                        Name = paramName,
                        Value = paramValue,
                    });
                }
            }

            string queryBody = reader.ReadToEnd();
            var likeMatches = LikeClausePattern.Matches(queryBody)
                .Cast<Match>()
                .ToList();
            var likeClauses = likeMatches.Select(m =>
                {
                    var clauseParts = Regex.Split(m.Value, " LIKE ");
                    return new ParsedRegexQueryParameter()
                    {
                        LikeClauseText = m.Value,
                        ColumnName = clauseParts[0],
                        RegexValue = clauseParts[1],
                    };
                })
                .ToList();

            return new ParsedQuery()
            {
                Query = queryBody,
                Parameters = parameters,
                LikeClauses = likeClauses,
            };
        }
    }
}