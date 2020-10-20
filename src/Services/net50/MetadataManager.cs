using Azure.Cosmos;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class MetadataManager
	{

		private CosmosContainer _WriteContainer;
		private CosmosContainer _ReadContainer;

		public MetadataManager(CosmosContainer writeContainer, CosmosContainer readContainer)
		{
			_WriteContainer = writeContainer;
			_ReadContainer = readContainer;
		}

		public async Task<T> CreateMetadataAsync<T>(IMetadata metadata)
		{
			metadata.IsValid();  // Will throw an exception if not valid
			return (await _WriteContainer.CreateItemAsync((T)metadata, new PartitionKey(metadata.OwnerEmailAddress))).Value;
		}

		public async Task<T> GetMetadataByIdAsync<T>(string id, string ownerEmailAddress)
		{
			return await Common.GetCosmosDataAsync<T>(
				new QueryDefinition("SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.id = @Id AND c.discriminator = 'Metadata'")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Id", id),
				_ReadContainer);
		}



	}

}