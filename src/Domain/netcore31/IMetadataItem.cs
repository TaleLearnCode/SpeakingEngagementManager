namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	public interface IMetadataItem
	{
		string Id { get; set; }
		string Name { get; set; }

		bool IsValid();
	}
}