namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a tag for a presentation.
	/// </summary>
	/// <seealso cref="Metadata" />
	public class Tag : Metadata
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Tag"/> class.
		/// </summary>
		public Tag() : base(MetadataTypes.Tag, "1.0") { }

	}

}