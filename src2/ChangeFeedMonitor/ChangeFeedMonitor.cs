using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.Functions
{

	/// <summary>
	/// Processes changes to the Cosmos documents.
	/// </summary>
	public class ChangeFeedMonitor
	{

		private readonly PresentationManager _presentationManager;
		private readonly MetadataManager _metadataManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChangeFeedMonitor"/> class.
		/// </summary>
		/// <param name="presentationManager">A reference to an initialized <see cref="PresentationManager"/>.</param>
		/// <param name="metadataManager">A reference to an initialized <see cref="MetadataManager"/>.</param>
		public ChangeFeedMonitor(PresentationManager presentationManager, MetadataManager metadataManager)
		{
			_presentationManager = presentationManager;
			_metadataManager = metadataManager;
		}

		/// <summary>
		/// Performs the necessary tasks upon the creation/update of a Cosmos document.
		/// </summary>
		/// <param name="documents">The documents that have been changed.</param>
		/// <param name="log">A reference to the logger.</param>
		[FunctionName("ChangeFeedMonitor")]
		public async Task RunAsync([CosmosDBTrigger(
			databaseName: DatabaseSettings.DatabaseName,
			collectionName: DatabaseSettings.ContainerName,
			ConnectionStringSetting = "CosmosConnectionString",
			LeaseCollectionPrefix = "ChangeFeedMonitor",
			LeaseCollectionName = "leases")]IReadOnlyList<Microsoft.Azure.Documents.Document> documents, ILogger log)
		{
			if (documents != null && documents.Count > 0)
			{
				foreach (var document in documents)
				{

					var settings = new JsonSerializerSettings();
					settings.Converters.Add(new DocumentConverter());
					var semDocument = JsonConvert.DeserializeObject<IDocument>(document.ToString(), settings);

					switch (semDocument.Discriminator)
					{
						case Discriminators.Presentation:
							await PresentationAsync((Presentation)semDocument);
							break;
						case Discriminators.Metadata:
							await TagAsync((Tag)semDocument);
							break;
					}
				}
			}
		}
		private async Task PresentationAsync(Presentation presentation)
		{

			bool documentUpdated = false;

			foreach (var tagItem in presentation.Tags.FindAll(t => t.Id is null))
			{
				var tag = await _metadataManager.CreateMetadataIfNonexistant<Tag>(tagItem);
				tagItem.Id = tag.Id;
				documentUpdated = true;
			}

			if (documentUpdated)
				await _presentationManager.UpdatePresentationAsync(presentation);

		}

		private async Task TagAsync(Tag tag)
		{
			var presentationsToUpdate = await _metadataManager.GetPresentationWithMetadataAsync("tags", tag.Id, tag.OwnerEmailAddress);
			foreach (var presentationId in presentationsToUpdate)
			{
				var presentation = await _presentationManager.GetPresentationAsync(presentationId, tag.OwnerEmailAddress);
				foreach (var tagItem in presentation.Tags.FindAll(t => t.Id == tag.Id))
				{
					tagItem.Name = tag.Name;
				}
				await _presentationManager.UpdatePresentationAsync(presentation);
			}
		}

	}

}