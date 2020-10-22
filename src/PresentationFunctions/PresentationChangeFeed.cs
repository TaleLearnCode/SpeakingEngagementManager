using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace PresentationFunctions
{
	public static class PresentationChangeFeed
	{

		private static readonly MetadataManager _MetadataManager;
		private static readonly PresentationManager _PresentationManager;

		static PresentationChangeFeed()
		{
			var cosmosConnection = new CosmosConnection(
				Environment.GetEnvironmentVariable("CosmosConnectionString"),
				Environment.GetEnvironmentVariable("CosmosDatabaseName"),
				Environment.GetEnvironmentVariable("CosmosContainerName"));
			_MetadataManager = new MetadataManager(cosmosConnection.Container);
			_PresentationManager = new PresentationManager(cosmosConnection.Container);
		}

		[FunctionName("PresentationChangeFeed")]
		public static async System.Threading.Tasks.Task RunAsync([CosmosDBTrigger(
			databaseName: "newDev",
			collectionName: "new",
			ConnectionStringSetting = "CosmosConnectionString",
			LeaseCollectionName = "leases",
			LeaseCollectionPrefix = "Presentation",
			CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> documents, ILogger log)
		{
			if (documents != null && documents.Count > 0)
			{
				// TODO: documents.select(d=> to presentation).where(p=> has tags...).selectMany(p=> get tags). where(t=> tag is not empty)

				foreach (var document in documents)
				{
					bool documentsNeedsToBeUpdated = false;
					var presentation = JsonConvert.DeserializeObject<Presentation>(document.ToString());
					if (presentation.Tags.Any())
					{
						foreach (var tagItem in presentation.Tags)
						{
							if (string.IsNullOrWhiteSpace(tagItem.Id))
							{
								var tag = await _MetadataManager.GetTagByName(tagItem.Name, presentation.OwnerEmailAddress);
								tagItem.Id = tag.Id;
								documentsNeedsToBeUpdated = true;
							}
						}
					}
					if (documentsNeedsToBeUpdated)
						await _PresentationManager.UpdatePresentationAsync(presentation);

				}




			}
		}
	}
}
