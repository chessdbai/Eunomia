//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Eunomia.BulkImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Aletheia.Pgn;
    using Aletheia.Pgn.Model;
    using Amazon;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DocumentModel;
    using Amazon.ResourceGroupsTaggingAPI;
    using Amazon.ResourceGroupsTaggingAPI.Model;
    using Amazon.Runtime;
    using Amazon.Runtime.CredentialManagement;
    using Eunomia.Storage.Games;

    /// <summary>
    /// The entry point class for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point method for the application.
        /// </summary>
        /// <param name="file">The PGN file.</param>
        /// <param name="dataset">The dataset.</param>
        /// <param name="batchSize">The number of items to batch together before saving.</param>
        /// <param name="awsProfileName">The AWS profile name to use. By default, the default credential chain will be used.</param>
        /// <param name="awsRegionName">The AWS region name to use. By default, the default region chain will be used.</param>
        public static void Main(
            string file,
            string dataset,
            int batchSize = 1000,
            string awsProfileName = null,
            string awsRegionName = null)
        {
            var datasetId = new DatasetId()
            {
                Name = dataset,
                Author = "system",
            };
            IGameFlattener flattener = new RudzFlattener();

            // var gameManager = await CreateGameManagerAsync(awsProfileName, awsRegionName);
            var gameBatch = new List<IndexedGame>();
            using (var pgnStream = new PgnGameStream(file))
            {
                while (!pgnStream.EndOfStream)
                {
                    PgnGame game;
                    try
                    {
                        game = pgnStream.ParseNextGame();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    var (flatGame, positions) = flattener.FlattenPgnGame(
                        game,
                        pgnStream.LastAttemptedParseContent,
                        datasetId);

                    gameBatch.Add(flatGame);

                    if (gameBatch.Count > batchSize)
                    {
                        // await gameManager.SaveGameBatchAsync(gameBatch);
                        gameBatch.Clear();
                    }
                }
            }

            if (gameBatch.Count > batchSize)
            {
                // await gameManager.SaveGameBatchAsync(gameBatch);
                gameBatch.Clear();
            }
        }

        private static async Task<IGameManager> CreateGameManagerAsync(string awsProfileName = null, string awsRegionName = null)
        {
            AWSCredentials creds;
            RegionEndpoint region;

            if (string.IsNullOrEmpty(awsProfileName))
            {
                creds = FallbackCredentialsFactory.GetCredentials();
            }
            else
            {
                var chain = new CredentialProfileStoreChain();
                AWSCredentials awsCredentials;
                if (chain.TryGetAWSCredentials(awsProfileName, out awsCredentials))
                {
                    // use awsCredentials
                    creds = awsCredentials;
                }
                else
                {
                    throw new ArgumentException($"AWS Profile Name '{awsProfileName}' not found.");
                }
            }

            if (string.IsNullOrEmpty(awsRegionName))
            {
                if (!string.IsNullOrEmpty(awsProfileName))
                {
                    var chain = new CredentialProfileStoreChain();
                    CredentialProfile basicProfile;
                    if (chain.TryGetProfile(awsProfileName, out basicProfile))
                    {
                        region = basicProfile.Region;
                    }
                    else
                    {
                        throw new ArgumentException($"AWS Profile Name '{awsProfileName}' not found.");
                    }
                }
                else
                {
                    region = FallbackRegionFactory.GetRegionEndpoint();
                }
            }
            else
            {
                region = RegionEndpoint.GetBySystemName(awsRegionName);
            }

            var dynamo = new AmazonDynamoDBClient(creds, region);
            var tagging = new AmazonResourceGroupsTaggingAPIClient();
            var tableName = await GetTableNameAsync(tagging);
            var table = Table.LoadTable(dynamo, tableName);
            return new DynamoGameManager(table);
        }

        private static async Task<string> GetTableNameAsync(IAmazonResourceGroupsTaggingAPI tagging)
        {
            var resourcesResponse = await tagging.GetResourcesAsync(new GetResourcesRequest()
            {
                TagFilters = new List<TagFilter>(new[]
                {
                    new TagFilter()
                    {
                        Key = "Aspect",
                        Values = new List<string>(new[]
                        {
                            "GameStorage",
                        }),
                    },
                }),
                ResourceTypeFilters = new List<string>(new[]
                {
                    "dynamodb:table",
                }),
            });
            return resourcesResponse.ResourceTagMappingList.First().ResourceARN.Split("/").Last();
        }
    }
}