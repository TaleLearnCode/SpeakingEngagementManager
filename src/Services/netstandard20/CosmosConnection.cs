using Azure.Cosmos;
using Azure.Cosmos.Serialization;
using System;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class CosmosConnection : IDisposable
	{

		private CosmosClient _CosmosClient;
		private CosmosContainer _CosmosContainer;

		public CosmosConnection(string connectionString, string databaseName, string containerName)
		{
			_CosmosClient = new CosmosClient(
				connectionString,
				new CosmosClientOptions
				{
					SerializerOptions = new CosmosSerializationOptions
					{
						PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
						IgnoreNullValues = true
					}
				});
			_CosmosContainer = _CosmosClient.GetDatabase(databaseName).GetContainer(containerName);
		}

		public void Dispose()
		{
			if (_CosmosClient != null) _CosmosClient.Dispose();
		}

		public CosmosContainer Container { get => _CosmosContainer; }

	}

}