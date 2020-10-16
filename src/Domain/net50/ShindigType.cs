namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents the type (user group, conference, code camp, etc) for a <see cref="Shindig"/>.
	/// </summary>
	/// <seealso cref="TaleLearnCode.SpeakingEngagementManager.Domain.Metadata" />
	public class ShindigType : Metadata
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="ShindigType"/> class.
		/// </summary>
		public ShindigType() : base(nameof(ShindigType), "1.0") { }

	}

}