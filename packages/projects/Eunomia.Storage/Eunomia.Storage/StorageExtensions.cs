// -----------------------------------------------------------------------
// <copyright file="StorageExtensions.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage
{
    using System;

    /// <summary>
    /// A StorageExtensions class.
    /// </summary>
    public static class StorageExtensions
    {
        /// <summary>
        /// Convert a <see cref="GameResult"/> enum to a result string.
        /// </summary>
        /// <param name="result">The result enum value.</param>
        /// <returns>The result string.</returns>
        /// <exception cref="ArgumentException">If an unknown <see cref="GameResult"/> value is provided.</exception>
        public static string ToResultString(this GameResult result) => result switch
        {
            GameResult.WhiteWon => "1-0",
            GameResult.BlackWon => "0-1",
            GameResult.Draw => "1/2-1/2",
            GameResult.Indeterminate => "*",
            GameResult.DoubleForfeit => "0-0",
            _ => throw new ArgumentException($"Unknown game result value: '{result}'."),
        };

        /// <summary>
        /// Convert a result string to a <see cref="GameResult"/> enum value.
        /// </summary>
        /// <param name="resultStr">The result string.</param>
        /// <returns>The <see cref="GameResult"/> enum value.</returns>
        /// <exception cref="ArgumentException">If an unknown game result string is provided.</exception>
        public static GameResult ToGameResult(this string resultStr) => resultStr switch
        {
            "1-0" => GameResult.WhiteWon,
            "0-1" => GameResult.BlackWon,
            "1/2-1/2" => GameResult.Draw,
            "*" => GameResult.Indeterminate,
            "0-0" => GameResult.DoubleForfeit,
            _ => throw new ArgumentException($"Unknown game result value: '{resultStr}'."),
        };

        /// <summary>
        /// Convert a <see cref="MoveFormat"/> enum to a move format string.
        /// </summary>
        /// <param name="result">The move format enum value.</param>
        /// <returns>The move format string.</returns>
        /// <exception cref="ArgumentException">If an unknown <see cref="MoveFormat"/> value is provided.</exception>
        public static string ToMoveFormatString(this MoveFormat result) => result switch
        {
            MoveFormat.UCI => "UCI",
            MoveFormat.SAN => "SAN",
            _ => throw new ArgumentException($"Unknown move format value: '{result}'."),
        };

        /// <summary>
        /// Convert a move format string to a <see cref="MoveFormat"/> enum value.
        /// </summary>
        /// <param name="moveFormatStr">The move format string.</param>
        /// <returns>The <see cref="MoveFormat"/> enum value.</returns>
        /// <exception cref="ArgumentException">If an unknown move format string is provided.</exception>
        public static MoveFormat ToMoveFormat(this string moveFormatStr) => moveFormatStr.ToUpper() switch
        {
            "UCI" => MoveFormat.UCI,
            "SAN" => MoveFormat.SAN,
            _ => throw new ArgumentException($"Unknown move format string value: '{moveFormatStr}'."),
        };
    }
}