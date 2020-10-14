using System.Threading.Tasks;
using TaleLearnCode.Data.EnttityFramework.Cosmos;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace Consumer.EntityFramework.Cosmos
{
	class Program
	{

		static async Task Main(string[] args)
		{
			using var context = new CosmosContext();

			var myPresentation = new Presentation()
			{
				Name = "Building a .NET Application Using Azure Cosmos DB",
				Abstract = "This will be an absolutely amazing talk!",
				ShortAbstract = "AMAZING!",
				OwnerEmailAddress = "chadgreen@chadgreen.com"
			};
			myPresentation.Tags.Add(new Tag() { Id = "Tag-1", Name = "Tag 1", OwnerEmailAddress = "chadgreen@chadgreen.com" });

			context.Add(myPresentation);
			await context.SaveChangesAsync();

		}




	}

}