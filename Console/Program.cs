using System;
using System.Threading.Tasks;


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