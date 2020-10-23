using TaleLearnCode.SpeakingEngagementManager.Domain.Exceptions;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	public abstract class MetadataItem
	{

		public string Id { get; init; }

		public string Name { get; init; }

		public string OwnerEmailAddress { get; init; }

		public bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(Name)) throw new MemberRequiredException(nameof(Name));
			return true;
		}

	}
}