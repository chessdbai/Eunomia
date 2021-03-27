// -----------------------------------------------------------------------
// <copyright file="QueryBuilderTest.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Tests.Queries
{
    using Eunomia.Storage.Queries;
    using Xunit;

    /// <summary>
    /// A QueryBuilderTest class.
    /// </summary>
    public class QueryBuilderTests
    {
        [Fact(DisplayName = "Generates correct SQL query for non-fuzzy query.")]
        public void GeneratesNonFuzzySql()
        {
            var query = new QueryBuilder()
                .WithECOs("B98", "B99")
                .WithRatingLowerBound(100)
                .WithResults(GameResult.BlackWon, GameResult.WhiteWon)
                .WithPrevMoves(
                    new MoveFilter() { MoveFormat = MoveFormat.SAN, MoveText = "e4" },
                    new MoveFilter() { MoveFormat = MoveFormat.UCI, MoveText = "d2d4" })
                .WithNextMoves(
                    new MoveFilter() { MoveFormat = MoveFormat.SAN, MoveText = "Nf3" },
                    new MoveFilter() { MoveFormat = MoveFormat.UCI, MoveText = "b1c3" })
                .WithResult("1-0")
                .BuildSqlQuery();
            Assert.Equal(QueryResources.GeneratedNonFuzzyQuery, query);
        }

        [Fact(DisplayName = "Generates correct SQL query for fuzzy query.")]
        public void GeneratesFuzzyFenSql()
        {
            var query = new QueryBuilder()
                .WithFuzzyFen("8/pppp1ppp/8/8/4Pp2/8/PPPP2PP/8 w - - 0 1")
                .WithFuzzyFen("8/pppp1ppp/8/8/3PPp2/8/PPP3PP/8 w - - 0 1")
                .BuildSqlQuery();
            Assert.Equal(QueryResources.GeneratedFuzzyQuery, query);
        }
    }
}