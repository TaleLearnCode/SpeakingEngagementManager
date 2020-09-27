using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and session types.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationSessionType" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationSessionType : Data.EntityFramework.PresentationSessionType, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

	}

}