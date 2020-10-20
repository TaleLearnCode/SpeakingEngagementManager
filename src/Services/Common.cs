using Azure;
using Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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

	}

}