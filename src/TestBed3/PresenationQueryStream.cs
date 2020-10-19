using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.TestBed
{

	public class PresentationQueryStream
	{
		[JsonPropertyName("Documents")]
		public Presentation[] Documents { get; set; }
	}

}