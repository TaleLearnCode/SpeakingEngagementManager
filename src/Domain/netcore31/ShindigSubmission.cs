using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	/// <summary>
	/// Represents a <see cref="Presentation"/> submission at a <see cref="Shindig"/>.
	/// </summary>
	/// <seealso cref="IDocument" />
	public class ShindigSubmission : Document
	{

		/// <summary>
		/// Gets or sets the identifier of the <see cref="Presentation"/> being submitted.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the submitted presentation identifier.
		/// </value>
		public string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the title of the <see cref="Presentation"/> being submitted.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the submitted presentation title.
		/// </value>
		public string PresentationTitle { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the <see cref="Shindig"/> where the presentation has been submitted to.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig identifier.
		/// </value>
		public string ShindigId { get; set; }

		/// <summary>
		/// Gets or sets the name of the <see cref="Shindig"/> where the presentation has been submitted to.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig name.
		/// </value>
		public string ShindigName { get; set; }

		/// <summary>
		/// Gets or sets the date the shindig is scheduled to start.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> representing the shindig start date.
		/// </value>
		public DateTime ShindigStartDate { get; set; }

		/// <summary>
		/// Gets or sets the date the shindig is scheduled to end.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> representing the shindig end date.
		/// </value>
		public DateTime ShindigEndDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the session submission (75-minute session, full-day workshop, etc.).
		/// </summary>
		/// <value>
		/// A <see cref="SessionType"/> representing the session type for the submission.
		/// </value>
		public SessionType SessionType { get; set; }

		/// <summary>
		/// Gets or sets the date the presentation was submitted to the shindig.
		/// </summary>
		/// <value>
		/// A nullable <see cref="DateTime"/> representing the submission date.
		/// </value>
		public DateTime? SubmissionDate { get; set; }

		/// <summary>
		/// Gets or sets the date the accept/decline notification was received.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> representing the date the accept/decline notification was received.
		/// </value>
		public DateTime? NotificationDate { get; set; }

		/// <summary>
		/// Gets or sets flag indicating whether the submission has been accepted.
		/// </summary>
		/// <value>
		/// <c>True</c> if the submission has been accepted; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>A null value indicates that an acceptance indication has not been provided by the shindig.</remarks>
		public bool? Accepted { get; set; }

		public ShindigSubmission() : base(Discriminators.ShindigSubmission, "1.0") { }

	}

}