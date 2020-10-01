using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagmentManager.Data.EntityFramework.SQL
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and tags.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationTag" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationTag
	{

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated Presentation identifier.
		/// </value>
		public string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		public virtual Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated tag identifier.
		/// </value>
		public string TagId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Tag"/> representing the associated tag.
		/// </value>
		public Tag Tag { get; set; }

	}

}