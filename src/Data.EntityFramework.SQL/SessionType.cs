using System.Collections.Generic;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagmentManager.Data.EntityFramework.SQL
{

	/// <summary>
	/// Represents a type of session (i.e. 60-minute presentation, 4-hour workshop, etc.).
	/// </summary>
	/// <seealso cref="EntityFramework.SessionType" />
	/// <seealso cref="IPartitionKey" />
	public class SessionType : ISessionType
	{

		/// <summary>
		/// Gets the identifier of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type identifier.
		/// </value>
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the duration of a session of this type.
		/// </summary>
		/// <value>
		/// A <c>int</c> representing the duration of a session of this type in minutes.
		/// </value>
		public int Duration { get; set; }

		/// <summary>
		/// Gets or sets the presentations associated with the session type.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationSessionType}"/> representing the associated presentations.
		/// </value>
		public List<PresentationSessionType> PresentationSessionTypes { get; set; } = new();

	}

}