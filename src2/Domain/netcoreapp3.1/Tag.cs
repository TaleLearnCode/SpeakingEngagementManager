namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	public class Tag : ITag, IPartitionKey
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string OwnerEmailAddress { get; set; }
	}

}