using System.Text.Json.Serialization;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.ConsoleApp
{

	public class QueryStream
	{
		[JsonPropertyName("Documents")]
		public dynamic[] Documents { get; set; }
	}

}