namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Interface to for types representing the many-to-many relationship between presentations and shindigs.
	/// </summary>
	public interface IPresentationShindig
	{

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated Presentation identifier.
		/// </value>
		string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Shindig"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated shindig identifier.
		/// </value>
		string ShindigId { get; set; }

	}

}
