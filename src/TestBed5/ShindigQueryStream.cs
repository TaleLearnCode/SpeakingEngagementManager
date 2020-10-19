using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{

	public class ShindigQueryStream
	{
		[JsonPropertyName("Documents")]
		public Shindig[] Documents { get; set; }
	}

}