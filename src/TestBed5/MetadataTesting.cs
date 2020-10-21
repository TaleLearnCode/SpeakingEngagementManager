using Azure.Cosmos;
using Azure.Cosmos.Serialization;
using System;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{

	public class MetadataTesting : IDisposable
	{

		private CosmosClient _CosmosClient;
		private CosmosContainer _CosmosContainer;
		private MetadataManager _MetadataManager;

		public MetadataTesting()
		{
			InitializeContainer();
			_MetadataManager = new MetadataManager(_CosmosContainer);
		}

		public void Dispose()
		{
			if (_CosmosClient is not null) _CosmosClient.Dispose();
		}

		private void InitializeContainer()
		{
			_CosmosClient = new CosmosClient(
				Settings.CosmosConnectionString,
				new CosmosClientOptions
				{
					SerializerOptions = new CosmosSerializationOptions
					{
						PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
						IgnoreNullValues = true
					}
				});
			var database = _CosmosClient.GetDatabase(Settings.DatabaseName);
			_CosmosContainer = database.GetContainer(Settings.ContainerName);
		}

		public async Task<SessionType> CreateSessionTypeAsync()
		{
			var sessionType = new SessionType()
			{
				Name = "Test Session Type",
				Duration = 180,
				OwnerEmailAddress = "chadgreen@chadgreen.com"
			};
			return (SessionType)(await _MetadataManager.CreateMetadataAsync<SessionType>(sessionType));
		}

		public async Task<ShindigType> CreateShindigTypeAsync()
		{
			var shindigType = new ShindigType()
			{
				Name = "User Group",
				OwnerEmailAddress = "chadgreen@chadgreen.com"
			};
			return (ShindigType)(await _MetadataManager.CreateMetadataAsync<ShindigType>(shindigType));
		}

		public async Task<Tag> CreateTagAsync()
		{
			var tag = new Tag()
			{
				Name = "Cosmos DB",
				OwnerEmailAddress = "chadgreen@chadgreen.com"
			};
			return (Tag)(await _MetadataManager.CreateMetadataAsync<Tag>(tag));
		}

		public async Task<string> ReadTagAsync(string id)
		{
			return (await _MetadataManager.GetMetadataByIdAsync<Tag>(id, "chadgreen@chadgreen.com")).Name;
		}


	}

}