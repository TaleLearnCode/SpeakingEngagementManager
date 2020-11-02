namespace TaleLearnCode.SpeakingEngagementManager.Functions
{

	/// <summary>
	/// Represents the database settings necessary for the Azure Functions.
	/// </summary>
	/// <remarks>
	/// This is done because the CosmosDBTrigger requires static values be used for its settings.
	/// </remarks>
	internal static class DatabaseSettings
	{
		internal const string DatabaseName = "newDev";
		internal const string ContainerName = "new";
	}

}