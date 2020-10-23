using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ChangeFeedMonitor
{

	/// <summary>
	/// Processes changes to the Presentation documents.
	/// </summary>
	public class PresentationChangeFeed
	{

		private readonly PresentationManager _PresentationManager;
		private readonly MetadataManager _MetadataManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentationChangeFeed"/> class.
		/// </summary>
		/// <param name="presentationManager">A reference to an initialized <see cref="PresentationManager"/>.</param>
		/// <param name="metadataManager">A reference to an initialized <see cref="MetadataManager"/>.</param>
		public PresentationChangeFeed(PresentationManager presentationManager, MetadataManager metadataManager)
		{
			_PresentationManager = presentationManager;
			_MetadataManager = metadataManager;
		}

		/// <summary>
		/// Performs the necessary tasks upon the creation/update of a presentation document.
		/// </summary>
		/// <param name="documents">The documents that have been changed.</param>
		/// <param name="log">A reference to the logger.</param>
		[FunctionName("Presentation")]
		public async Task RunAsync([CosmosDBTrigger(
			databaseName: DatabaseSettings.DatabaseName,
			collectionName: DatabaseSettings.ContainerName,
			ConnectionStringSetting = "CosmosConnectionString",
			LeaseCollectionPrefix = "Presentation",
			LeaseCollectionName = "leases")]IReadOnlyList<Document> documents, ILogger log)
		{
			if (documents != null && documents.Count > 0)
			{
				foreach (var document in documents)
				{

					bool documentUpdated = false;
					var presentation = JsonConvert.DeserializeObject<Presentation>(document.ToString());

					foreach (var tagItem in presentation.Tags.FindAll(t => t.Id is null))
					{
						var tag = await _MetadataManager.CreateMetadataIfNonexistant<Tag>(tagItem);
						tagItem.Id = tag.Id;
						documentUpdated = true;
					}

					if (documentUpdated)
						await _PresentationManager.UpdatePresentationAsync(presentation);

				}
			}
		}

	}

}