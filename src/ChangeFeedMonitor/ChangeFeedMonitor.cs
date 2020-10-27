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
	public class ChangeFeedMonitor
	{

		private readonly PresentationManager _PresentationManager;
		private readonly MetadataManager _MetadataManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChangeFeedMonitor"/> class.
		/// </summary>
		/// <param name="presentationManager">A reference to an initialized <see cref="PresentationManager"/>.</param>
		/// <param name="metadataManager">A reference to an initialized <see cref="MetadataManager"/>.</param>
		public ChangeFeedMonitor(PresentationManager presentationManager, MetadataManager metadataManager)
		{
			_PresentationManager = presentationManager;
			_MetadataManager = metadataManager;
		}

		/// <summary>
		/// Performs the necessary tasks upon the creation/update of a presentation document.
		/// </summary>
		/// <param name="documents">The documents that have been changed.</param>
		/// <param name="log">A reference to the logger.</param>
		[FunctionName("ChangeFeedMonitor")]
		public async Task RunAsync([CosmosDBTrigger(
			databaseName: DatabaseSettings.DatabaseName,
			collectionName: DatabaseSettings.ContainerName,
			ConnectionStringSetting = "CosmosConnectionString",
			LeaseCollectionPrefix = "ChangeFeedMonitor",
			LeaseCollectionName = "leases")]IReadOnlyList<Document> documents, ILogger log)
		{
			if (documents != null && documents.Count > 0)
			{
				foreach (var document in documents)
				{
					var semDocument = JsonConvert.DeserializeObject<IDocument>(document.ToString());
					switch (semDocument.Discriminator)
					{
						case Discriminators.Presentation:
							await PresentationAsync(document.ToString());
							break;
						case Discriminators.Metadata:
							await MetadataAsync(document.ToString());
							break;
					}
				}
			}
		}

		private async Task PresentationAsync(string document)
		{

			bool documentUpdated = false;

			var presentation = JsonConvert.DeserializeObject<Presentation>(document);

			foreach (var tagItem in presentation.Tags.FindAll(t => t.Id is null))
			{
				var tag = await _MetadataManager.CreateMetadataIfNonexistant<Tag>(tagItem);
				tagItem.Id = tag.Id;
				documentUpdated = true;
			}

			if (documentUpdated)
				await _PresentationManager.UpdatePresentationAsync(presentation);

		}

		private async Task MetadataAsync(string document)
		{
			var metadata = JsonConvert.DeserializeObject<IMetadata>(document);
			switch (metadata.Type)
			{
				case MetadataTypes.Tag:
					await TagAsync(document);
					break;
			}
		}

		private async Task TagAsync(string document)
		{
			var tag = JsonConvert.DeserializeObject<Tag>(document.ToString());
			var presentationsToUpdate = await _MetadataManager.GetPresentationWithMetadataAsync("tags", tag.Id, tag.OwnerEmailAddress);
			foreach (var presentationId in presentationsToUpdate)
			{
				var presentation = await _PresentationManager.GetPresentationAsync(presentationId, tag.OwnerEmailAddress);
				foreach (var tagItem in presentation.Tags.FindAll(t => t.Id == tag.Id))
				{
					tagItem.Name = tag.Name;
				}
				await _PresentationManager.UpdatePresentationAsync(presentation);
			}
		}


	}

}