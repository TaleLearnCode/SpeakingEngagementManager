using System.Collections.Generic;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a presentation given by a speaker.
	/// </summary>
	/// <seealso cref="IDocument" />
	public class Presentation : IDocument
	{

		/// <summary>
		/// Gets the version of the document.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the document version.
		/// </value>
		public string DocumentVersion { get; } = "1.0";

		/// <summary>
		/// Gets the discriminator for the document.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the document discriminator.
		/// </value>
		public string Discriminator { get; } = nameof(Presentation);

		/// <summary>
		/// Gets the identifier of the document.
		/// </summary>
		/// <value>
		/// A <see cref="string" /> representing the document identifier.
		/// </value>
		public string Id { get; set; } = IDGenerator.Generate();

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the name (title) of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the abstract for the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's abstract.
		/// </value>
		public string Abstract { get; set; }

		/// <summary>
		/// Gets or sets the short abstract for the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's short abstract.
		/// </value>
		public string ShortAbstract { get; set; }

		/// <summary>
		/// Gets or sets the elevator pitch for the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's elevator pitch.
		/// </value>
		/// <remarks>This should be limited to 100 to 120 characters.</remarks>
		public string ElevatorPitch { get; set; }

		/// <summary>
		/// Gets or sets the description of why an attendee should attend the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the description of why an attendee should attend the presentation.
		/// </value>
		public string WhyAttend { get; set; }

		/// <summary>
		/// Gets or sets the learning objectives for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{string}"/> representing the presentation's learning objectives.
		/// </value>
		public List<string> LearningObjectives { get; set; } = new List<string>();

		/// <summary>
		/// Gets or sets the tags for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{Tag}"/> representing the tags for the presentation.
		/// </value>
		public List<Tag> Tags { get; set; } = new List<Tag>();

		/// <summary>
		/// Gets or sets the session types for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{SessionType}"/> representing the different session types for the presentation.
		/// </value>
		public List<SessionType> SessionTypes { get; set; } = new List<SessionType>();

		/// <summary>
		/// Gets or sets the outline for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{string}"/> representing the presentation outline.
		/// </value>
		public List<string> Outline { get; set; } = new List<string>();

		/// <summary>
		/// Gets or sets the presentation submissions.
		/// </summary>
		/// <value>
		/// A <see cref="List{ShindigSubmission}"/> representing the list of where the presentation has been submitted.
		/// </value>
		public List<ShindigSubmission> Submissions { get; set; } = new List<ShindigSubmission>();

		/// <summary>
		/// Gets or sets the speaking engagements for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{ShindigPresentation}"/> representing the list of where the presentation has been scheduled.
		/// </value>
		public List<ShindigPresentation> SpeakingEngagements { get; set; } = new List<ShindigPresentation>();

	}

}