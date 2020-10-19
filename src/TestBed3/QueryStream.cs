using System.Text.Json.Serialization;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{

	public class QueryStream
	{
		[JsonPropertyName("Documents")]
		public dynamic[] Documents { get; set; }
	}

}