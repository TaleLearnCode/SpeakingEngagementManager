using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Repository.CosmosSDK
{

	/// <summary>
	/// Represents a tag for a presentation.
	/// </summary>
	/// <seealso cref="ITag" />
	/// <seealso cref="IPartitionKey" />
	public class Tag : ITag, IPartitionKey
	{

		/// <summary>
		/// Gets the identifier of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag identifier.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Tag.Id)]
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the name of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag's name.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Tag.Name)]
		public string Name { get; set; }

	}

}