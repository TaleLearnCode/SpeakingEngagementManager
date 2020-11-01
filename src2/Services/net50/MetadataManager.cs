using Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class MetadataManager
	{

		private readonly CosmosContainer _CosmosContainer;

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

		public Task<T> GetMetadataByNameAsync<T>(string name, string ownerEmailAddress)
		{
			return Common.GetCosmosDataAsync<T>(
				new QueryDefinition($"SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.name = @Name AND c.discriminator = @Discriminator AND c.type = @MetadataType")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Name", name)
					.WithParameter("@Discriminator", Discriminators.Metadata)
					.WithParameter("@MetadataType", Metadata.GetMetadataTypeNameByType(typeof(T))),
				_CosmosContainer);
		}

		public async Task<List<T>> GetMetadataByTypeAsync<T>(string ownerEmailAddress)
		{
			return await Common.GetDocumentsAsync<T>(
				new QueryDefinition("SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.discriminator = @Discriminator AND c.type = @MetadataType")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Discriminator", Discriminators.Metadata)
					.WithParameter("@MetadataType", Metadata.GetMetadataTypeNameByType(typeof(T))),
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

		private Task<T> CreateMetadataFromMetadataItemAsync<T>(IMetadataItem metadataItem) where T : IMetadata, new()
		{
			var newMetadata = new T();
			foreach (var propertyInfo in metadataItem.GetType().GetProperties())
				if (propertyInfo.CanRead &&
						newMetadata.GetType().GetProperty(propertyInfo.Name).CanWrite &&
						newMetadata.GetType().GetProperty(propertyInfo.Name).GetValue(newMetadata) == null)
					newMetadata.GetType().GetProperty(propertyInfo.Name).SetValue(newMetadata, propertyInfo.GetValue(metadataItem));

			return CreateMetadataAsync<T>(newMetadata);
		}

		public async Task<T> CreateMetadataIfNonexistant<T>(IMetadataItem metadataItem) where T : IMetadata, new()
		{
			var metadata = await GetMetadataByNameAsync<T>(metadataItem.Name, metadataItem.OwnerEmailAddress);
			if (metadata is null)
				metadata = await CreateMetadataFromMetadataItemAsync<T>(metadataItem);
			return metadata;
		}

		public async Task<List<string>> GetPresentationWithMetadataAsync(string type, string metadataId)
		{
			var presentationIds = new List<string>();
			//var queryDefinition = new QueryDefinition($"SELECT presentations.Id JOIN {type} IN presentations.{type} WHERE {type}.id = @MetadataId").WithParameter("@MetadataId", metadataId);
			var sql = "SELECT presentations.Id JOIN @Type IN presentations.@Type WHERE @Type.id = @MetadataId";
			var queryDefinition = new QueryDefinition(sql)
				.WithParameter("@Type", type)
				.WithParameter("@MetadataId", metadataId);
			await foreach (QueryId queryId in _CosmosContainer.GetItemQueryIterator<QueryId>(queryDefinition))
				presentationIds.Add(queryId.Id);
			return presentationIds;
		}


	}

}