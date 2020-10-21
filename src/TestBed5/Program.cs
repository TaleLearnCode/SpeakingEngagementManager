using System;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{
	class Program
	{

		static async Task Main()
		{
			await DomainTesting();
		}

		static async Task DomainTesting()
		{

			using var domainTesting = new DomainTesting();

			Console.WriteLine("Writing the shindig document...");
			var shindigId = await domainTesting.WriteShindigAsync();

			Console.WriteLine("Reading the shindig document distinctly...");
			await domainTesting.ReadShindigDistinctlyAsync(shindigId);

			Console.WriteLine("Reading the shindig document dynamically...");
			await domainTesting.ReadShindigDynamicallyAsync(shindigId);

			Console.WriteLine("Reading the shindig document dynamically generically...");
			var shindig = await domainTesting.ReadDocumentByIdDynamicallyAsync<Shindig>(shindigId);
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

		static async Task MetadataTesting()
		{
			using var metadataTesting = new MetadataTesting();

			Console.WriteLine("Creating session type...");
			SessionType sessionType = await metadataTesting.CreateSessionTypeAsync();
			Console.WriteLine($"\t{sessionType.Name} is {sessionType.Duration} minutes long");

			Console.WriteLine("Creating shindig type...");
			ShindigType shindigType = await metadataTesting.CreateShindigTypeAsync();
			Console.WriteLine($"\t{shindigType.Name}");

			Console.WriteLine("Creating tag...");
			Tag tag = await metadataTesting.CreateTagAsync();
			Console.WriteLine($"\t{tag.Name}");

			Console.WriteLine("Reading tag...");
			Console.WriteLine($"\t{tag.Name}");



		}

	}

}