using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and shindigs.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationShindig" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationShindig : EntityFramework.PresentationShindig, IPartitionKey
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
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		[JsonPropertyName("presentation")]
		public new Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Shindig"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Shindig"/> representing the associated tag.
		/// </value>
		[JsonPropertyName("shindig")]
		public new Shindig Shindig { get; set; }

		/// <summary>
		/// Gets or sets the presentations associated with the shindigs.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationShindig}"/> representing the associated presentations.
		/// </value>
		[JsonPropertyName("presentationShindigs")]
		public new List<PresentationShindig> PresentationShindigs { get; set; }

	}

}