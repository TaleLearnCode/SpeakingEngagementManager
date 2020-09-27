using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Entities;

namespace TaleLearnCode.SpeakingEngagementManager.Repository.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and tags.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationTag" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationTag : EntityFramework.PresentationTag, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

	}

}