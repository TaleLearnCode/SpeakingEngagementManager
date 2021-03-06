﻿using Azure;
using Azure.Cosmos;
using Azure.Cosmos.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{

	public class DomainTesting : IDisposable
	{

		private CosmosClient _CosmosClient;
		private CosmosContainer _CosmosContainer;

		private CosmosConnection _CosmosConnection;

		public DomainTesting()
		{
			InitializeCosmosContainer();
		}

		public void Dispose()
		{
			if (_CosmosClient is not null) _CosmosClient.Dispose();
		}

		private void InitializeCosmosContainer()
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

		public async Task<string> WriteShindigAsync()
		{

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

			await _CosmosContainer.CreateItemAsync(shindig);

			return shindig.Id;

		}

		public async Task ReadShindigDistinctlyAsync(string id)
		{

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			List<Shindig> shindigs = new List<Shindig>();

			await foreach (Response response in _CosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<ShindigQueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				shindigs.AddRange(queryStream.Documents);
			}

			if (shindigs.Any())
				Console.WriteLine($"\tDistinct Shindig: {shindigs[0].Name}");
			else
				Console.WriteLine("\tNo shindigs were found");

		}

		public async Task ReadShindigDynamicallyAsync(string id)
		{

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			List<dynamic> shindigs = new();

			await foreach (Response response in _CosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				shindigs.AddRange(queryStream.Documents);
			}

			if (shindigs.Any())
				Console.WriteLine($"\tDynamic Shindig: {(JsonSerializer.Deserialize<Shindig>(shindigs[0].ToString(), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })).Name}");
			else
				Console.WriteLine("\tNo shindigs were found");

		}

		public async Task<string> WritePresentationAsync()
		{

			var presentation = new Presentation()
			{
				OwnerEmailAddress = "chadgreen@chadgreen.com",
				Name = "Building .NET Applications Using Azure Cosmos DB",
				Abstract = "A really great presentation",
				ShortAbstract = "Great presentation",
				ElevatorPitch = "Amazing",
				WhyAttend = "Because Cosmos is just really cool and you want to use it."
			};
			presentation.LearningObjectives.Add("The first objective");
			presentation.LearningObjectives.Add("The second objective");
			presentation.Tags.Add(new TagItem() { Name = "Azure" });
			presentation.Tags.Add(new TagItem() { Name = ".NET" });
			presentation.Tags.Add(new TagItem() { Name = "Cosmos DB" });
			presentation.Tags.Add(new TagItem() { Name = "Microsoft" });
			presentation.SessionTypes.Add(new SessionType { Name = "60-Minute Session", Duration = 60, OwnerEmailAddress = "chadgreen@chadgreen.com" });
			presentation.Outline.Add("Section One");
			presentation.Outline.Add("Section Two");
			presentation.Outline.Add("Section Three");

			if (presentation.IsValid()) await _CosmosContainer.CreateItemAsync(presentation);

			return presentation.Id;

		}

		public async Task ReadPresentationDistinctlyAsync(string id)
		{

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			List<Presentation> presentations = new();

			await foreach (Response response in _CosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<PresentationQueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				presentations.AddRange(queryStream.Documents);
			}

			if (presentations.Any())
				Console.WriteLine($"\tDistinct Presentation: {presentations[0].Name}");
			else
				Console.WriteLine("\tNo presentations were found");

		}

		public async Task ReadPresentationDynamicallyAsync(string id)
		{

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			List<dynamic> shindigs = new List<dynamic>();

			await foreach (Response response in _CosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				shindigs.AddRange(queryStream.Documents);
			}

			if (shindigs.Any())
				Console.WriteLine($"\tDynamic Presentation: {(JsonSerializer.Deserialize<Presentation>(shindigs[0].ToString(), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })).Name}");
			else
				Console.WriteLine("\tNo presentations were found");

		}

		public async Task<T> ReadDocumentByIdDynamicallyAsync<T>(string id)
		{

			QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{id}'");
			List<dynamic> shindigs = new List<dynamic>();

			await foreach (Response response in _CosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				shindigs.AddRange(queryStream.Documents);
			}

			if (shindigs.Any())
			{
				return JsonSerializer.Deserialize<T>(shindigs[0].ToString(), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\tNo shindigs were found");
				Console.ForegroundColor = ConsoleColor.White;
			}

			return default;

		}

		public async Task<List<T>> DocumentQueryAsync<T>(string query)
		{

			List<T> returnValue = new();

			var queryDefinition = new QueryDefinition(query);
			List<dynamic> documents = new();

			await foreach (Response response in _CosmosContainer.GetItemQueryStreamIterator(queryDefinition))
			{
				var queryStream = await JsonSerializer.DeserializeAsync<QueryStream>(
					response.ContentStream,
					new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				documents.AddRange(queryStream.Documents);
			}

			if (documents.Any())
			{
				foreach (var document in documents)
				{
					returnValue.Add(
						JsonSerializer.Deserialize<T>(
							document.ToString(),
							new JsonSerializerOptions()
							{
								PropertyNamingPolicy = JsonNamingPolicy.CamelCase
							}
							)
						);
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\tNo documents were found");
				Console.ForegroundColor = ConsoleColor.White;
			}

			return returnValue;

		}

	}

}