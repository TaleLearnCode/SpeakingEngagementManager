using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents a presentation presented by a speaker.
	/// </summary>
	/// <seealso cref="EntityFramework.Presentation" />
	/// <seealso cref="IPartitionKey" />
	public class Presentation : EntityFramework.Presentation, IPartitionKey
	{
	}

}