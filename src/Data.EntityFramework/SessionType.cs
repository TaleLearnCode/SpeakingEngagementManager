using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework
{

	/// <summary>
	/// Represents a type of session (i.e. 60-minute presentation, 4-hour workshop, etc.).
	/// </summary>
	/// <seealso cref="ISessionType" />
	public class SessionType : ISessionType
	{

		/// <summary>
		/// Gets the identifier of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type identifier.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.SessionType.Id)]
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type name.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.SessionType.Name)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the duration of a session of this type.
		/// </summary>
		/// <value>
		/// A <c>int</c> representing the duration of a session of this type in minutes.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.SessionType.Duration)]
		public int Duration { get; set; }

	}

}