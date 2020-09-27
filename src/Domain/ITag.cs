namespace TaleLearnCode.SpeakingEngagementManager.Domain
{


	/// <summary>
	/// Interface for objects representing a tag for a presentation.
	/// </summary>
	public interface ITag
	{

		/// <summary>
		/// Gets the identifier of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag identifier.
		/// </value>
		string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the tag.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the tag's name.
		/// </value>
		string Name { get; set; }

	}

}