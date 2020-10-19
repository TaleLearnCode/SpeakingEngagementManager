using System;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{
	class Program
	{

		static async Task Main()
		{

			using var domainTesting = new DomainTesting();

			Console.WriteLine("Writing the shindig document...");
			var shindigId = await domainTesting.WriteShindigAsync();

			Console.WriteLine("Reading the shindig document distinctly...");
			await domainTesting.ReadShindigDistinctlyAsync(shindigId);

			Console.WriteLine("Reading the shindig document dynamically...");
			await domainTesting.ReadShindigDynamicallyAsync(shindigId);

			Console.WriteLine("Reading the shindig document dynamically generically...");
			var shindig = await domainTesting.ReadDocumentByIdDistrinclyAsync<Shindig>(shindigId);
			Console.WriteLine($"\tDynamic Shindig:{shindig.Name}");

			Console.WriteLine("Reading the shindig documents via a query dynamically, generically...");
			var shindigs = await domainTesting.DocumentQueryAsync<Shindig>($"SELECT * FROM c WHERE c.id = '{shindigId}'");
			Console.WriteLine($"\tShindig Query Returned {shindigs.Count} documents");

			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("Writing the presentation document...");
			var presentationId = await domainTesting.WritePresentationAsync();

			Console.WriteLine("Reading the presentation document distinctly...");
			await domainTesting.ReadPresentationDistinctlyAsync(presentationId);

			Console.WriteLine("Reading the presentation document dynamically...");
			await domainTesting.ReadPresentationDynamicallyAsync(presentationId);

			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("Done testing");

		}

	}

}