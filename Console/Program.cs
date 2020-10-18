using Azure;
using Azure.Cosmos;
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

			using var domainTesting = new DomainTesting();

			Console.WriteLine("Writing the shindig document...");
			var shindigId = await domainTesting.WriteShindigAsync();

			Console.WriteLine("Reading the shindig document distinctly...");
			await domainTesting.ReadShindigDistinctlyAsync(shindigId);

			Console.WriteLine("Reading the shindig document dynamically...");
			await domainTesting.ReadShindigDynamicallyAsync(shindigId);


			Console.WriteLine("Done testing");

		}

	}

}