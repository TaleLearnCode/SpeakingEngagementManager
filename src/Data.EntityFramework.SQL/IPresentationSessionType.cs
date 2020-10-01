namespace TaleLearnCode.SpeakingEngagmentManager.Data.EntityFramework.SQL
{
	interface IPresentationSessionType : TaleLearnCode.SpeakingEngagementManager.Domain.IPresentationSessionType
	{

		/// <summary>
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="SessionType"/>.
		/// </summary>
		/// <value>
		/// A <see cref="SessionType"/> representing the associated session type.
		/// </value>
		SessionType SessionType { get; set; }

	}
}