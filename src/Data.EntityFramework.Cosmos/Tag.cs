using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents a tag for a presentation.
	/// </summary>
	/// <seealso cref="EntityFramework.Tag" />
	/// <seealso cref="IPartitionKey" />
	public class Tag : Data.EntityFramework.Tag, IPartitionKey
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
		/// Gets or sets the presentations associated with the tag.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationTag}"/> representing the associated presentations.
		/// </value>
		[JsonPropertyName("presentationTags")]
		public new List<PresentationTag> PresentationTags { get; set; } = new();


	}

}