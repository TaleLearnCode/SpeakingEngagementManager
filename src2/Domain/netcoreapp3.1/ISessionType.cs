namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Interface for objects representing a type of session (i.e. 60-minute presentation, 4-hour workshop, etc.).
	/// </summary>
	public interface ISessionType
	{

		/// <summary>
		/// Gets the identifier of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type identifier.
		/// </value>
		string Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type name.
		/// </value>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the duration of a session of this type.
		/// </summary>
		/// <value>
		/// A <c>int</c> representing the duration of a session of this type in minutes.
		/// </value>
		int Duration { get; set; }

	}

}