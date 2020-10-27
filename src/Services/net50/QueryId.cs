using Newtonsoft.Json;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class QueryId
	{

		[JsonProperty(propertyName: "id")]
		public string Id { get; set; }

	}

}