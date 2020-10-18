using Azure;
using Azure.Cosmos;
using Azure.Cosmos.Fluent;
using Azure.Cosmos.Serialization;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;


namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.ConsoleApp
{
	class Program
	{
		static async Task Main(string[] args)
		{

			var id = await WriteShindig();
			await ReadShindig3(id);


		}

		static async Task<string> WriteShindig()
		{

			using var client = new CosmosClientBuilder(Settings.CosmosConnectionString)
				.WithSerializerOptions(new CosmosSerializationOptions
				{
					PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
					IgnoreNullValues = true
				})
				.Build();

			var cosmosDatabase = client.GetDatabase("newDev");
			var cosmosContainer = cosmosDatabase.GetContainer("new");

			var shindig = new Shindig()
			{
				OwnerEmailAddress = "chadgreen@chadgreen.com",
				Name = "Tulsa .NET User Group",
				Location = new Location()
				{
					CountryId = "US",
					CountryName = "United States",
					RegionCode = "019",
					RegionName = "Americas",
					SubregionCode = "021",
					SubregionName = "Northern America",
					CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/us.svg"),
					CountryDivisionId = "OK",
					CountryDivisionName = "Oklahoma",
					CountryDivisionCategory = "state"
				},
				StartDate = new DateTime(2020, 11, 9),
				EndDate = new DateTime(2020, 11, 9),
				Cost = "Free",
				ShindigType = new ShindigType()
				{
					OwnerEmailAddress = "chadgreen@chadgreen.com",
					Name = "User Group"
				},
				IsVirtual = true,
				DisplayVirtualLocation = false
			};

			await cosmosContainer.CreateItemAsync(shindig);

			return shindig.Id;

		}











		static async Task ReadShindig(string id)
		{

			using var client = new CosmosClient(Settings.CosmosConnectionString);
			var database = client.GetDatabase("newDev");
			var container = database.GetContainer("new");

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			await foreach (Shindig shindig1 in container.GetItemQueryIterator<Shindig>(queryDefinition))
			{
				Console.WriteLine("\tRead {0}\n", shindig1.Name);
			}

		}

		static async Task ReadShindig2(string id, CosmosContainer container)
		{

			//using var client = new CosmosClient(Settings.CosmosConnectionString);
			//var database = client.GetDatabase("newDev");
			//var container = database.GetContainer("new");

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			await foreach (Shindig shindig1 in container.GetItemQueryIterator<Shindig>(queryDefinition))
			{
				Console.WriteLine("\tRead {0}\n", shindig1.Name);
			}

		}


		static async Task ReadShindig3(string id)
		{
			CosmosClientOptions options = new CosmosClientOptions
			{
				SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.Default }
			};

			CosmosClient cosmosClient = new CosmosClient(Settings.CosmosConnectionString, options);

			CosmosContainer cosmosContainer = cosmosClient.GetDatabase("newDev").GetContainer("new");
			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			List<Shindig> msgs = new List<Shindig>();

			await foreach (Response response in cosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{

				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream2>(response.ContentStream,
						new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

				msgs.AddRange(queryStream.Documents);
			}

			//Console.WriteLine(msgs.Count);

			Console.WriteLine(msgs[0].Location.CountryDivisionName);

			//Shindig shindig = JsonSerializer.Deserialize<Shindig>(msgs[0].ToString(), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
			//Console.WriteLine(shindig.Name);



		}

	}

	public class QueryStream
	{
		[JsonPropertyName("Documents")]
		public dynamic[] Documents { get; set; }
	}

	public class QueryStream2
	{
		[JsonPropertyName("Documents")]
		public Shindig[] Documents { get; set; }
	}

}