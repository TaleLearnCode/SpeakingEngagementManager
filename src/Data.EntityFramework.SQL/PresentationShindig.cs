using System.Collections.Generic;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagmentManager.Data.EntityFramework.SQL
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and shindigs.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationShindig" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationShindig
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

		/// <summary>
		/// Gets or sets the presentations associated with the shindigs.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationShindig}"/> representing the associated presentations.
		/// </value>
		public List<PresentationShindig> PresentationShindigs { get; set; }

	}

}