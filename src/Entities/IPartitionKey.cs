namespace TaleLearnCode.SpeakingEngagementManager.Entities
{

	/// <summary>
	/// Interface defining the partition key used by SEM in Cosmos DB databases.
	/// </summary>
	public interface IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		string OwnerEmailAddress { get; set; }

	}

}