using System.Text.Json.Serialization;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and tags.
	/// </summary>
	public class PresentationTag : IPresentationTag
	{

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated Presentation identifier.
		/// </value>
		[JsonPropertyName("presentationId")]
		public string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		[JsonPropertyName("presentation")]
		public Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated tag identifier.
		/// </value>
		[JsonPropertyName("tagId")]
		public string TagId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Tag"/> representing the associated tag.
		/// </value>
		[JsonPropertyName("tag")]
		public Tag Tag { get; set; }

	}

}