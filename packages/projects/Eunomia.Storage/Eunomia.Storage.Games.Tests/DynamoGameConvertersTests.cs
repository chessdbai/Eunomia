namespace Eunomia.Storage.Games.Tests
{
    using Xunit;

    public class DynamoGameConvertersTests
    {
        [Fact(DisplayName = "Conversion to and from document succeeds")]
        public void ConversionToAndFromDocumentSucceeds()
        {
            var gameObj = TestResources.GameObject;
            var doc = gameObj.ToDocument();
            var convertedGame = doc.ToGame();

            Assert.Equal(gameObj.Id, convertedGame.Id);
            Assert.Equal(gameObj.Dataset, convertedGame.Dataset);
        }
    }
}