using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Entities;


namespace TaleLearnCode.SpeakingEngagementManager.Repository.EntityFramework
{

	/// <summary>
	/// Represents a tag for a presentation.
	/// </summary>
	/// <seealso cref="ITag" />
	public class Tag : ITag
	{

		/// <summary>
		/// Gets the identifier of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag identifier.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Tag.Id)]
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag's name.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Tag.Name)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the presentations associated with the tag.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationTag}"/> representing the associated presentations.
		/// </value>
		[JsonPropertyName("presentationTags")]
		public List<PresentationTag> PresentationTags { get; set; } = new List<PresentationTag>();

	}

}