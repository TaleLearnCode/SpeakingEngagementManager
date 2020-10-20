﻿using Azure;
using Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{
	public static class Common
	{

		public static async Task<T> GetCosmosDataAsync<T>(string query, CosmosContainer cosmosContainer)
		{

			QueryDefinition queryDefinition = new QueryDefinition(query);
			List<dynamic> documents = new List<dynamic>();

			await foreach (Response response in cosmosContainer.GetItemQueryStreamIterator(queryDefinition))
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