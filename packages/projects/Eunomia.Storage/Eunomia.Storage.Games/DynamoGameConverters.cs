//-----------------------------------------------------------------------
// <copyright file="DynamoGameConverters.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia.Storage.Games
{
    using System.Collections.Generic;
    using Amazon.DynamoDBv2.DocumentModel;

    /// <summary>
    /// Static utility class for the logic to convert from
    /// <see cref="IndexedGame"/> to a DynamoDB-storable <see cref="Document"/>.
    /// </summary>
    public static class DynamoGameConverters
    {
        /// <summary>
        /// Convert a <see cref="IndexedGame"/> to a DynamoDB <see cref="Document"/>.
        /// </summary>
        /// <param name="game">The <see cref="IndexedGame"/>.</param>
        /// <returns>The DynamoDB <see cref="Document"/>.</returns>
        public static Document ToDocument(this IndexedGame game)
        {
            var doc = new Document();

            // Write game identifiers
            doc[nameof(game.Id.UniqueId)] = game.Id.UniqueId;
            doc[nameof(game.Id.CanonicalizedId)] = game.Id.CanonicalizedId;
            doc[nameof(game.Id.PublicId)] = game.Id.PublicId;

            // Write top-level game metadata
            doc[nameof(game.ImportTimestamp)] = game.ImportTimestamp;
            doc["DatasetName"] = game.Dataset.Name;
            doc["DatasetAuthor"] = game.Dataset.Author;

            // Write seven-tag-roster
            doc[nameof(game.Event)] = game.Event;
            doc[nameof(game.Site)] = game.Site;
            doc[nameof(game.Date)] = game.Date;
            doc[nameof(game.Round)] = game.Round;
            doc[nameof(game.Result)] = game.Result;
            doc[nameof(game.White)] = game.White;
            doc[nameof(game.Black)] = game.Black;

            // Write remaining tags
            var otherTagDoc = new Document();
            foreach (var kvp in game.OtherTags)
            {
                otherTagDoc[kvp.Key] = kvp.Value;
            }

            doc[nameof(game.OtherTags)] = otherTagDoc;

            // Write game text
            doc[nameof(game.GamePgn)] = game.GamePgn;

            return doc;
        }

        /// <summary>
        /// Convert a DynamoDB <see cref="Document"/> to an <see cref="IndexedGame"/>.
        /// </summary>
        /// <param name="doc">The DynamoDB <see cref="Document"/>.</param>
        /// <returns>The <see cref="IndexedGame"/>.</returns>
        public static IndexedGame ToGame(this Document doc)
        {
            var game = new IndexedGame();

            // Write game identifiers
            game.Id = new GameId()
            {
                UniqueId = doc[nameof(game.Id.UniqueId)].AsString(),
                CanonicalizedId = doc[nameof(game.Id.CanonicalizedId)].AsString(),
                PublicId = doc[nameof(game.Id.PublicId)].AsString(),
            };

            // Write top-level game metadata
            game.ImportTimestamp = doc[nameof(game.ImportTimestamp)].AsDateTime();
            game.Dataset = new DatasetId()
            {
                Name = doc["DatasetName"].AsString(),
                Author = doc["DatasetAuthor"].AsString(),
            };

            // Write seven-tag-roster
            game.Event = doc[nameof(game.Event)].AsString();
            game.Site = doc[nameof(game.Site)].AsString();
            game.Date = doc[nameof(game.Date)].AsDateTime();
            game.Round = doc[nameof(game.Round)].AsString();
            game.Result = doc[nameof(game.Result)].AsString();
            game.White = doc[nameof(game.White)].AsString();
            game.Black = doc[nameof(game.Black)].AsString();

            // Write remaining tags
            var otherTagDoc = doc[nameof(game.OtherTags)].AsDocument();
            var otherTags = new Dictionary<string, string>();
            foreach (var kvp in otherTagDoc)
            {
                otherTags.Add(kvp.Key, kvp.Value.AsString());
            }

            game.OtherTags = otherTags;

            // Write game text
            game.GamePgn = doc[nameof(game.GamePgn)].AsString();

            return game;
        }
    }
}