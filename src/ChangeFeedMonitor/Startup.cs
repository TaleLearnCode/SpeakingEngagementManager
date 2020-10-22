using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaleLearnCode.SpeakingEngagementManager.Services;

[assembly: FunctionsStartup(typeof(TaleLearnCode.SpeakingEngagementManager.ChangeFeedMonitor.Startup))]
namespace TaleLearnCode.SpeakingEngagementManager.ChangeFeedMonitor
{

	/// <summary>
	/// Configures the Azure Functions App for use.
	/// </summary>
	/// <seealso cref="FunctionsStartup" />
	class Startup : FunctionsStartup
	{

		public override void Configure(IFunctionsHostBuilder builder)
		{

			var cosmosConnection = new CosmosConnection(
				Environment.GetEnvironmentVariable("CosmosConnectionString"),
				DatabaseSettings.DatabaseName,
				DatabaseSettings.ContainerName);

			builder.Services.AddSingleton((s) => { return new MetadataManager(cosmosConnection.Container); });
			builder.Services.AddSingleton((s) => { return new PresentationManager(cosmosConnection.Container); });

		}

	}

}