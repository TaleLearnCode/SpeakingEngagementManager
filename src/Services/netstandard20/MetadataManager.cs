using Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class MetadataManager
	{

		private CosmosContainer _CosmosContainer;

		public MetadataManager(CosmosContainer cosmosContainer)
		{
			_CosmosContainer = cosmosContainer;
		}

		public async Task<T> CreateMetadataAsync<T>(IMetadata metadata)
		{
			metadata.IsValid();  // Will throw an exception if not valid
			return (await _CosmosContainer.CreateItemAsync((T)metadata, new PartitionKey(metadata.OwnerEmailAddress))).Value;
		}

		public async Task<T> GetMetadataByIdAsync<T>(string id, string ownerEmailAddress)
		{
			return await Common.GetCosmosDataAsync<T>(
				new QueryDefinition($"SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.id = @Id AND c.discriminator = '{Discriminators.Metadata}'")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Id", id),
				_CosmosContainer);
		}

		public async Task<T> GetMetadataByNameAsync<T>(string name, string ownerEmailAddress)
		{
			return await Common.GetCosmosDataAsync<T>(
				new QueryDefinition($"SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.name = @Name AND c.discriminator = @Discriminator")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Name", name)
					.WithParameter("@Discriminator", Discriminators.Metadata),
				_CosmosContainer);
		}

		public async Task<List<T>> GetMetadataByTypeAsync<T>(string ownerEmailAddress)
		{
			return await Common.GetDocumentsAsync<T>(
				new QueryDefinition("SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.discriminator = @Discriminator")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Discriminator", Discriminators.Metadata)
					.WithParameter("@MetadataType", Metadata.GetMetadataTypeByType(typeof(T))),
				_CosmosContainer);
		}

		public async Task<TMetadata> GetMetadataByItemName<TMetadataItem, TMetadata>(IMetadataItem metadataItem, string ownerEmailAddress)
		{
			var metadata = await GetMetadataByNameAsync<TMetadata>(metadataItem.Name, ownerEmailAddress);
			if (metadata is null)
			{

			}
			return metadata;
		}


		public async Task<Tag> GetTagByName(string name, string ownerEmailAddress)
		{
			var tag = await GetMetadataByNameAsync<Tag>(name, ownerEmailAddress);
			if (tag is null)
			{
				tag = await CreateMetadataAsync<Tag>(new Tag()
				{
					Name = name,
					OwnerEmailAddress = ownerEmailAddress
				});
			}
			return tag;
		}


	}

}