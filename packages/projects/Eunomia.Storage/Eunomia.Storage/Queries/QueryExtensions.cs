// -----------------------------------------------------------------------
// <copyright file="QueryExtensions.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Eunomia.Storage.Queries.Parser;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A QueryExtensions class.
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Converts a <see cref="MoveFilter"/> object to a LINQ expression.
        /// </summary>
        /// <param name="filter">The move filter object.</param>
        /// <param name="direction">The direction of the move filter.</param>
        /// <returns>The LINQ expression.</returns>
        /// <exception cref="ArgumentException">When an invalid MoveFormat is present inside the MoveFilter object.</exception>
        public static Expression<Func<PositionRow, bool>> ToExpression(
            this MoveFilter filter,
            MoveDirection direction)
        {
            Expression<Func<PositionRow, bool>> expression;
            if (direction == MoveDirection.NextMove)
            {
                if (filter.MoveFormat == MoveFormat.SAN)
                {
                    expression = p => p.NextMoveSan == filter.MoveText;
                }
                else if (filter.MoveFormat == MoveFormat.UCI)
                {
                    expression = p => p.NextMoveUci == filter.MoveText;
                }
                else
                {
                    throw new ArgumentException($"Unknown move filter format type: {filter.MoveFormat}");
                }
            }
            else if (direction == MoveDirection.PreviousMove)
            {
                if (filter.MoveFormat == MoveFormat.SAN)
                {
                    expression = p => p.PreviousMoveSan == filter.MoveText;
                }
                else if (filter.MoveFormat == MoveFormat.UCI)
                {
                    expression = p => p.PreviousMoveUci == filter.MoveText;
                }
                else
                {
                    throw new ArgumentException($"Unknown move filter format type: {filter.MoveFormat}");
                }
            }
            else
            {
                throw new ArgumentException($"Unknown move direction: {direction}");
            }

            return expression;
        }

        /// <summary>
        /// Converts an ECO code string to a LINQ expression.
        /// </summary>
        /// <param name="ecoCode">The ECO code.</param>
        /// <returns>The LINQ expression.</returns>
        public static Expression<Func<PositionRow, bool>> ToEcoExpression(
            this string ecoCode) => row => row.EcoCode == ecoCode;

        /// <summary>
        /// Converts an fuzzy FEN string to a LINQ expression.
        /// </summary>
        /// <param name="fuzzyFen">The fuzzy FEN string.</param>
        /// <returns>The LINQ expression.</returns>
        public static Expression<Func<PositionRow, bool>> ToFuzzyFenExpression(
            this string fuzzyFen) => row => EF.Functions.Like(
            row.BoardState,
            FenUtils.ToBoardStateRegex(fuzzyFen));

        /// <summary>
        /// Converts an result string to a LINQ expression.
        /// </summary>
        /// <param name="result">The result string.</param>
        /// <returns>The LINQ expression.</returns>
        /// <exception cref="ArgumentException">When an invalid MoveFormat is present inside the MoveFilter object.</exception>
        public static Expression<Func<PositionRow, bool>> ToResultExpression(
            this GameResult result) => row => row.GameResult == result;

        /// <summary>
        /// Creates an OR expression from a list of child expressions.
        /// </summary>
        /// <param name="expressions">The enumerable of expressions.</param>
        /// <returns>A boolean or expression.</returns>
        public static Expression<Func<PositionRow, bool>> AsOrExpression(
            this IEnumerable<Expression<Func<PositionRow, bool>>> expressions)
        {
            var items = expressions.ToList();
            if (items.Count == 0)
            {
                return p => true;
            }

            ParameterExpression parameterExpression = Expression.Parameter(typeof(PositionRow), "expression");
            Expression expr = Expression.Invoke(items.First(), new Expression[]
            {
                parameterExpression,
            });
            if (items.Count > 1)
            {
                foreach (var i in items.Skip(1))
                {
                    expr = Expression.OrElse(expr, Expression.Invoke(i, new Expression[]
                    {
                        parameterExpression,
                    }));
                }
            }

            return Expression.Lambda<Func<PositionRow, bool>>(expr, parameterExpression);
        }

        /// <summary>
        /// Converts a LINQ expression to an Athena SQL string.
        /// </summary>
        /// <param name="query">The query to get the Athena SQL string for.</param>
        /// <returns>The Athena SQL string.</returns>
        public static string ToAthenaSql(this IQueryable<PositionRow> query)
        {
            // Two transformations we need to do:
            string linqSql = query.ToQueryString();
            var parsedQuery = QueryParser.ParseQuery(linqSql);
            string finalSql = parsedQuery.Query;

            // 1. Replace the LIKE clauses with the
            //    presto-style regex function.
            foreach (var likeClause in parsedQuery.LikeClauses)
            {
                string regexpClause = $"regexp_like({likeClause.ColumnName}, {likeClause.RegexValue})";
                finalSql = finalSql.Replace(likeClause.LikeClauseText, regexpClause);
            }

            // 2. Remove LINQ param statements by putting
            //    the parameters into the query body.
            foreach (var p in parsedQuery.Parameters)
            {
                finalSql = finalSql.Replace(p.Name, p.Value);
            }

            return finalSql;
        }
    }
}