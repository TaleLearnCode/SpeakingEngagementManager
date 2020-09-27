using TaleLearnCode.SpeakingEngagementManager.Entities;

namespace TaleLearnCode.SpeakingEngagementManager.Repository.EntityFramework
{

	/// <summary>
	/// Interface to for types representing the many-to-many relationship between presentations and shindigs.
	/// </summary>
	public interface IPresentationTag
	{

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated Presentation identifier.
		/// </value>
		string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated tag identifier.
		/// </value>
		string TagId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Tag"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Tag"/> representing the associated tag.
		/// </value>
		Tag Tag { get; set; }

	}

}