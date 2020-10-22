namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	public interface IMetadata : IDocument
	{
		string Name { get; set; }
		string Type { get; set; }
		bool IsValid();
	}
}