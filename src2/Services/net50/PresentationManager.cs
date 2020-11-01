using Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class PresentationManager
	{

		private readonly CosmosContainer _CosmosContainer;

		public PresentationManager(CosmosContainer cosmosContainer)
		{
			_CosmosContainer = cosmosContainer;
		}

		public async Task<Presentation> CreatePresentationAsync(Presentation presentation)
		{
			presentation.IsValid(); // Method will throw exception if document is not valid
			return await Common.CreateDocumentAsync<Presentation>(_CosmosContainer, presentation);
		}

		public async Task<Presentation> UpdatePresentationAsync(Presentation presentation)
		{
			presentation.IsValid(); // Method will throw exception if document is not valid
			return await Common.UpdateDocumentAsync<Presentation>(_CosmosContainer, presentation);
		}

		public async Task<Presentation> GetPresentationAsync(string id, string ownerEmailAddress)
		{
			return await Common.GetDocumentByIdAsync<Presentation>(Discriminators.Presentation, id, ownerEmailAddress, _CosmosContainer);
		}


		public async Task<List<Presentation>> GetPresentationsAsync(string ownerEmailAddress)
		{
			return await Common.GetDocumentsAsync<Presentation>(
				new QueryDefinition($"SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.discriminator = '{Discriminators.Presentation}'")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress),
				_CosmosContainer);
		}

		public async Task<ShindigPresentation> GetShindigPresentation(string presenationId, string shindigId, string ownerEmailAddress)
		{
			var resultsList = await Common.GetDocumentsAsync<ShindigPresentation>(
				new QueryDefinition($"SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.discriminator = @Discriminator AND c.presentationId = @PresentationId AND c.shindigId = @ShindigId")
				.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
				.WithParameter("@Discriminator", Discriminators.ShindigPresentation)
				.WithParameter("@PresentationId", presenationId)
				.WithParameter("@ShindigId", shindigId),
				_CosmosContainer);
			if (resultsList.Any())
				return resultsList[0];
			else
				return default;
		}

		public async Task AddDownloadsToShindigPresentationAsync(string presentationId, string shindigId, string ownerEmailAddress, Dictionary<string, Uri> downloads)
		{
			var presentationShindig = await GetShindigPresentation(presentationId, shindigId, ownerEmailAddress);
			if (presentationShindig is null) throw new Exception("Unable to find the specified the ShindigPresentation.");
			foreach (var download in downloads)
				presentationShindig.Downloads.Add(download.Key, download.Value);
			await _CosmosContainer.ReplaceItemAsync<ShindigPresentation>(presentationShindig, presentationShindig.Id, new PartitionKey(presentationShindig.OwnerEmailAddress));
		}

		public Task AddDownloadToShindigPresentation(string presentationId, string shindigId, string ownerEmailAddress, string downloadName, Uri downloadUrl)
		{
			return AddDownloadsToShindigPresentationAsync(
				presentationId,
				shindigId,
				ownerEmailAddress,
				new Dictionary<string, Uri>() { { downloadName, downloadUrl } });
		}

	}

}