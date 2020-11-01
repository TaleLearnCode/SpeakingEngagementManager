namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	public interface IMetadataItem
	{
		string Id { get; init; }
		string Name { get; set; }
		string OwnerEmailAddress { get; init; }

		bool IsValid();
	}
}