using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and tags.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationTag" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationTag : Data.EntityFramework.PresentationTag, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		[JsonPropertyName("presentation")]
		public new Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Tag"/> representing the associated tag.
		/// </value>
		[JsonPropertyName("tag")]
		public new Tag Tag { get; set; }


	}

}