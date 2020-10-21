using Azure;
using Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	internal static class Common
	{

		internal static async Task<T> GetCosmosDataAsync<T>(QueryDefinition query, CosmosContainer cosmosContainer)
		{

			List<dynamic> documents = new List<dynamic>();

			await foreach (Response response in cosmosContainer.GetItemQueryStreamIterator(query))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				documents.AddRange(queryStream.Documents);
			}

			if (documents.Any())
				return JsonSerializer.Deserialize<T>(documents[0].ToString(), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
			else
				return default;

		}

		internal static async Task<T> SaveDocumentAsync<T>(CosmosContainer cosmosContainer, IDocument document)
		{
			return (await cosmosContainer.CreateItemAsync((T)document, new PartitionKey(document.OwnerEmailAddress))).Value;
		}

		internal static async Task<List<T>> GetDocumentsAsync<T>(QueryDefinition query, CosmosContainer cosmosContainer)
		{
			List<dynamic> documents = new List<dynamic>();

			await foreach (Response response in cosmosContainer.GetItemQueryStreamIterator(query))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				documents.AddRange(queryStream.Documents);
			}

			List<T> returnValue = new List<T>();
			if (documents.Any())
				foreach (var document in documents)
					returnValue.Add(JsonSerializer.Deserialize<T>(document.ToString(), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
			return returnValue;

		}

		internal static async Task<T> GetDocumentByIdAsync<T>(string discriminator, string id, string ownerEmailAddress, CosmosContainer cosmosContainer)
		{
			var queryResults = await GetDocumentsAsync<T>(
				new QueryDefinition("SELECT * FROM c WHERE c.ownerEmailAddress = @OwnerEmailAddress AND c.id = @Id AND c.discriminator = @Discriminator")
					.WithParameter("@OwnerEmailAddress", ownerEmailAddress)
					.WithParameter("@Id", id)
					.WithParameter("@Discriminator", discriminator),
				cosmosContainer);
			if (queryResults.Any())
				return queryResults[0];
			else
				return default;
		}

		internal static async Task<T> UpdateDocumentAsync<T>(CosmosContainer cosmosContainer, IDocument document)
		{
			return (await cosmosContainer.UpsertItemAsync((T)document, new PartitionKey(document.OwnerEmailAddress))).Value;
		}

	}

}