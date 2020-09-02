namespace Eunomia.Storage.Games.Tests
{
    using System.IO;
    using Newtonsoft.Json;

    public class TestResources
    {
        public static IndexedGame GameObject => JsonConvert.DeserializeObject<IndexedGame>(GetTestResource(nameof(GameObject) + ".json"));

        private static string GetTestResource(string name)
        {
            var myType = typeof(TestResources);
            string fullName = myType.Namespace + $".Documents.{name}";
            using var stream = myType.Assembly.GetManifestResourceStream(fullName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}