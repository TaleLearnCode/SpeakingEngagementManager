using TaleLearnCode.SpeakingEngagementManager.Entities;

namespace TaleLearnCode.SpeakingEngagementManager.Repository.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents an event that a speaker submits to and/or speakers at.
	/// </summary>
	/// <seealso cref="EntityFramework.Shindig" />
	/// <seealso cref="IPartitionKey" />
	public class Shindig : EntityFramework.Shindig, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		public string OwnerEmailAddress { get; set; }

	}

}