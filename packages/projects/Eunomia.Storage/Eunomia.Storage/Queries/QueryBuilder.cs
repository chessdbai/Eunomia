// -----------------------------------------------------------------------
// <copyright file="QueryBuilder.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A QueryBuilder class.
    /// </summary>
    public class QueryBuilder
    {
        private readonly HashSet<MoveFilter> nextMoves = new HashSet<MoveFilter>();
        private readonly HashSet<MoveFilter> prevMoves = new HashSet<MoveFilter>();
        private readonly HashSet<string> fuzzyFens = new HashSet<string>();
        private readonly HashSet<string> ecos = new HashSet<string>();
        private readonly HashSet<GameResult> results = new HashSet<GameResult>();

        private string databaseName;
        private string tableName;
        private int? ratingLowerBound;

        /// <summary>
        /// Set the database to query.
        /// </summary>
        /// <param name="database">The database name.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithDatabase(string database)
        {
            this.databaseName = database;
            return this;
        }

        /// <summary>
        /// Set the table to query.
        /// </summary>
        /// <param name="table">The table name.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithTable(string table)
        {
            this.tableName = table;
            return this;
        }

        /// <summary>
        /// Set the ECO.
        /// </summary>
        /// <param name="ecos">The ECO codes.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithECOs(params string[] ecos)
        {
            foreach (var eco in ecos)
            {
                this.ecos.Add(eco);
            }

            return this;
        }

        /// <summary>
        /// Set the ECO.
        /// </summary>
        /// <param name="eco">The ECO.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithECO(string eco)
        {
            this.ecos.Add(eco);
            return this;
        }

        /// <summary>
        /// Sets the lower bound on the rating.
        /// </summary>
        /// <param name="ratingLowerBound">The lower bound on either rating..</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithRatingLowerBound(int ratingLowerBound)
        {
            this.ratingLowerBound = ratingLowerBound;
            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithResult(GameResult result)
        {
            this.results.Add(result);
            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithResult(string result)
        {
            this.results.Add(result.ToGameResult());
            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithResults(params GameResult[] results)
        {
            foreach (var r in results)
            {
                this.results.Add(r);
            }

            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithResults(params string[] results) =>
            this.WithResults(results.Select(r => r.ToGameResult()).ToArray());

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="fuzzyFen">The fuzzy fen string.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithFuzzyFen(string fuzzyFen)
        {
            this.fuzzyFens.Add(fuzzyFen);
            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="fuzzyFens">The fuzzy FEN strings.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithFuzzyFens(params string[] fuzzyFens)
        {
            foreach (var ff in fuzzyFens)
            {
                this.fuzzyFens.Add(ff);
            }

            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="nextMoves">The result.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithNextMoves(params MoveFilter[] nextMoves)
        {
            foreach (var nm in nextMoves)
            {
                this.nextMoves.Add(nm);
            }

            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="nextMove">The next move.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithNextMove(MoveFilter nextMove)
        {
            this.nextMoves.Add(nextMove);
            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="prevMoves">The previous moves.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithPrevMoves(params MoveFilter[] prevMoves)
        {
            foreach (var pm in prevMoves)
            {
                this.prevMoves.Add(pm);
            }

            return this;
        }

        /// <summary>
        /// Set the result.
        /// </summary>
        /// <param name="prevMove">The next move.</param>
        /// <returns>This query builder.</returns>
        public QueryBuilder WithPrevMoves(MoveFilter prevMove)
        {
            this.prevMoves.Add(prevMove);
            return this;
        }

        /// <summary>
        /// Builds the SQL query.
        /// </summary>
        /// <returns>The SQL query.</returns>
        public string BuildSqlQuery()
        {
            var dbSet = ChessContext.GetContext(this.databaseName, this.tableName);
            var query = dbSet.Positions.AsQueryable();
            if (this.nextMoves.Count > 0)
            {
                var anyNextMoveMatches = this.nextMoves!
                    .Select(nm => nm.ToExpression(MoveDirection.NextMove))
                    .AsOrExpression();
                query = query.Where(anyNextMoveMatches);
            }

            if (this.prevMoves.Count > 0)
            {
                var anyPrevMoveMatches = this.prevMoves!
                    .Select(nm => nm.ToExpression(MoveDirection.PreviousMove))
                    .AsOrExpression();
                query = query.Where(anyPrevMoveMatches);
            }

            if (this.ecos.Count > 0)
            {
                var ecoExpressions = this.ecos!
                    .Select(eco => eco.ToEcoExpression())
                    .AsOrExpression();
                query = query.Where(ecoExpressions);
            }

            if (this.results.Count > 0)
            {
                var resultExpressions = this.results!
                    .Select(result => result.ToResultExpression())
                    .AsOrExpression();
                query = query.Where(resultExpressions);
            }

            if (this.fuzzyFens.Count > 0)
            {
                var fuzzyFenExpressions = this.fuzzyFens!
                    .Select(result => result.ToFuzzyFenExpression())
                    .AsOrExpression();
                query = query.Where(fuzzyFenExpressions);
            }

            if (this.ratingLowerBound != null)
            {
                query = query.Where(row =>
                    row.WhitePlayerElo >= this.ratingLowerBound
                    && row.BlackPlayerElo >= this.ratingLowerBound);
            }

            return query.ToAthenaSql();
        }
    }
}