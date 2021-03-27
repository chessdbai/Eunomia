// -----------------------------------------------------------------------
// <copyright file="QueryResources.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
// -----------------------------------------------------------------------

namespace Eunomia.Storage.Tests.Queries
{
    using System.IO;

    /// <summary>
    /// A QueryResources class.
    /// </summary>
    public static class QueryResources
    {
        public static string GeneratedNonFuzzyQuery => GetResourceAsString(nameof(GeneratedNonFuzzyQuery));

        public static string GeneratedFuzzyQuery => GetResourceAsString(nameof(GeneratedFuzzyQuery));

        private static string GetResourceAsString(string resourceKey)
        {
            var myType = typeof(QueryResources);
            string fullName = myType.Namespace + ".Resources." + resourceKey + ".sql";
            using var s = myType.Assembly.GetManifestResourceStream(fullName);
            using var reader = new StreamReader(s!);
            return reader.ReadToEnd();
        }
    }
}