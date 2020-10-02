using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and shindigs.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationShindig" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationShindig : IPresentationShindig, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		public string OwnerEmailAddress { get; set; }

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
		public Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Shindig"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated shindig identifier.
		/// </value>
		public string ShindigId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Shindig"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Shindig"/> representing the associated tag.
		/// </value>
		public Shindig Shindig { get; set; }

	}

}