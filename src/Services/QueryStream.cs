using System.Text.Json.Serialization;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class QueryStream
	{
		[JsonPropertyName("Documents")]
		public dynamic[] Documents { get; set; }
	}

}