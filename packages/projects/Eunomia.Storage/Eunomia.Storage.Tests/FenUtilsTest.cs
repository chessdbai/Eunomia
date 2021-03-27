// -----------------------------------------------------------------------
// <copyright file="FenUtilsTest.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Tests
{
    using Xunit;

    /// <summary>
    /// A FenUtilsTest class.
    /// </summary>
    public class FenUtilsTest
    {
        [Fact]
        public void CovertsFenToRegexCorrectly()
        {
            string fen = "8/ppp2p1p/3p2p1/8/8/2P1P3/PP3PPP/8 w - - 0 1";

            string converted = FenUtils.ToBoardStateRegex(fen);
            Assert.Equal(".{8}.{5}pbp.{6}p.{1}.{8}.{8}.{6}P.{1}.{5}PBP.{8}", converted);
        }
    }
}