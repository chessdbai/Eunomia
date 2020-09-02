//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension class.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Split up a collection into smaller chunks.
        /// </summary>
        /// <param name="source">The large collection.</param>
        /// <param name="size">The size of each batch.</param>
        /// <typeparam name="T">The type of item in the collection.</typeparam>
        /// <returns>A collection of batches.</returns>
        public static IEnumerable<List<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            var sourceList = source.ToList();
            for (int i = 0; i < Math.Ceiling(sourceList.Count() / (double)size); i++)
            {
                yield return new List<T>(sourceList.Skip(size * i).Take(size));
            }
        }
    }
}