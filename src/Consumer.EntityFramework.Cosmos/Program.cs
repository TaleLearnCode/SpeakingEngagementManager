using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos;

namespace TaleLearnCode.SpeakingEngagementManager.Consumer.EntityFramework.Cosmos
{

	class Program
	{
		static async Task Main(string[] args)
		{
			await CreatePresentationAsync();
			Console.WriteLine("Done");
		}


		//public static async Task CreatePresentationAsync()
		//{

		//	using var context = new CosmosContext(Settings.AccountEndpoint, Settings.AccountKey, Settings.DatabseName, Settings.DefaultContainerName);

		//	var presentation = new Presentation()
		//	{
		//		Name = "JSon Test",
		//		OwnerEmailAddress = "chadgreen@chadgreen.com"
		//	};
		//	context.Presentations.Add(presentation);

		//	await context.SaveChangesAsync();

		//}

		public static async Task CreatePresentationAsync()
		{

			Console.WriteLine("Creating a Presentation");

			using var context = new CosmosContext(Settings.AccountEndpoint, Settings.AccountKey, Settings.DatabseName, Settings.DefaultContainerName);

			var myPresentation = new Presentation()
			{
				Name = "Building a .NET Application Using Azure Cosmos DB",
				Abstract = "This will be absolutely amazing talk!",
				ShortAbstract = "AMAZING!",
				OwnerEmailAddress = "chadgreen@chadgreen.com"
			};

			context.Add(myPresentation);
			await context.SaveChangesAsync();


			//var tag = new Tag() { Name = "Azure Cosmos DB", OwnerEmailAddress = "chadgreen@chadgreen.com" };
			//tag.PresentationTags.Add(new PresentationTag() { Presentation = myPresentation, PresentationId = myPresentation.Id, Tag = tag, TagId = tag.Id });
			//context.Add(tag);

			//await context.SaveChangesAsync();



			//var tags = new List<Tag>()
			//{
			//	new Tag() { Name = "Azure Cosmos DB", ownerEmailAddress = "chadgreen@chadgreen.com" },
			//	new Tag() { Name = "Azure", ownerEmailAddress = "chadgreen@chadgreen.com" },
			//	new Tag() { Name = "Microsoft", ownerEmailAddress = "chadgreen@chadgreen.com" },
			//	new Tag() { Name = "C#", ownerEmailAddress = "chadgreen@chadgreen.com" },
			//	new Tag() { Name = "Application Development", ownerEmailAddress = "chadgreen@chadgreen.com" }
			//};
			//context.Tags.AddRange(tags);
			//await context.SaveChangesAsync();

			//var presentationId = myPresentation.Id;
			//myPresentation.PresentationTags = new List<PresentationTag>();
			//foreach (var tag in tags)
			//{
			//	myPresentation.PresentationTags.Add(new PresentationTag()
			//	{
			//		PresentationId = presentationId,
			//		TagId = tag.Id,
			//		ownerEmailAddress = "chadgreen@chadgreen.com"
			//	});
			//}
			//await context.SaveChangesAsync();



		}

	}

}