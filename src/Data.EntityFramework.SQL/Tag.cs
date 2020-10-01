using System.Collections.Generic;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagmentManager.Data.EntityFramework.SQL
{

	/// <summary>
	/// Represents a tag for a presentation.
	/// </summary>
	/// <seealso cref="EntityFramework.Tag" />
	/// <seealso cref="IPartitionKey" />
	public class Tag : ITag
	{

		/// <summary>
		/// Gets the identifier of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag identifier.
		/// </value>
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag's name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the presentations associated with the tag.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationTag}"/> representing the associated presentations.
		/// </value>
		public List<PresentationTag> PresentationTags { get; set; } = new();


	}

}