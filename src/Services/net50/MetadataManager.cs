using Azure;
using Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
			return (await _WriteContainer.CreateItemAsync((T)metadata, new PartitionKey(metadata.OwnerEmailAddress))).Value;
		}

		public async Task<T> GetMetadataByIdAsync<T>(string id, string ownerEmailAddress)
		{
			return await Common.GetCosmosDataAsync<T>($"SELECT * FROM c WHERE c.ownerEmailAddress = '{ownerEmailAddress}' AND c.id = '{id}' AND c.discriminator = 'Metadata'", _ReadContainer);
		}

	}

}