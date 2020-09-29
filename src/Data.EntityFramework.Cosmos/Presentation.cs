using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents a presentation presented by a speaker.
	/// </summary>
	/// <seealso cref="EntityFramework.Presentation" />
	/// <seealso cref="IPartitionKey" />
	public class Presentation : EntityFramework.Presentation, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the associated session types.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationSessionType}"/> representing the session types associated with the presentation.
		/// </value>
		[JsonPropertyName("presentationSessionTypes")]
		public new List<PresentationSessionType> PresentationSessionTypes { get; set; } = new();

		/// <summary>
		/// Gets or sets the associated shindigs.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationShindig}"/> representing the shindigs associated with the presentation.
		/// </value>
		[JsonPropertyName("presentationShindigs")]
		public new List<PresentationShindig> PresentationShindigs { get; set; } = new();

		/// <summary>
		/// Gets or sets the associated tags.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationTag}"/> representing the tags associated with the presentation.
		/// </value>
		[JsonPropertyName("presentationTags")]
		public new List<PresentationTag> PresentationTags { get; set; } = new();

	}

}