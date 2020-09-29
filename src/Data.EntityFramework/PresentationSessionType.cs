using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and session types.
	/// </summary>
	public class PresentationSessionType : IPresentationSessionType, IPartitionKey
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
		/// Gets or sets the identifier of the associated <see cref="SessionType"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated SessionType identifier.
		/// </value>
		[JsonPropertyName("sessionTypeId")]
		public string SessionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="SessionType"/>.
		/// </summary>
		/// <value>
		/// A <see cref="SessionType"/> representing the associated session type.
		/// </value>
		[JsonPropertyName("sessionType")]
		public SessionType SessionType { get; set; }

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }


	}

}
