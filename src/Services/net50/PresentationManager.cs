using Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class PresentationManager
	{

		private CosmosContainer _WriteContainer;
		private CosmosContainer _ReadContainer;

		public PresentationManager(CosmosContainer writeContainer, CosmosContainer readContainer)
		{
			_WriteContainer = writeContainer;
			_ReadContainer = readContainer;
		}

		public async Task<Presentation> CreatePresentationAsync(Presentation presentation)
		{
			presentation.IsValid(); // Method will throw exception if document is not valid
			return await Common.SaveDocumentAsync<Presentation>(_WriteContainer, presentation);
		}

		public async Task<Presentation> GetPresentationAsync(string id, string ownerEmailAddress)
		{
			return await Common.GetDocumentByIdAsync<Presentation>(Discriminators.Presentation, id, ownerEmailAddress, _ReadContainer);
		}

		public async Task<List<Presentation>> GetPresentationsAsync(string ownerEmailAddress)
		{
			return await Common.GetDocumentsAsync<Presentation>(
				new QueryDefinition($"SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.discriminator = '{Discriminators.Presentation}'")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress),
				_ReadContainer);
		}

	}

}