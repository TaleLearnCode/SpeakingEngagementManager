using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a presentation given by a speaker.
	/// </summary>
	/// <seealso cref="IDocument" />
	public class Presentation : Document
	{

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
		public List<string> LearningObjectives { get; set; } = new();

		/// <summary>
		/// Gets or sets the tags for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{TagItem}"/> representing the tags for the presentation.
		/// </value>
		public List<TagItem> Tags { get; set; } = new();

		/// <summary>
		/// Gets or sets the session types for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{SessionType}"/> representing the different session types for the presentation.
		/// </value>
		public List<SessionType> SessionTypes { get; set; } = new();

		/// <summary>
		/// Gets or sets the outline for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{string}"/> representing the presentation outline.
		/// </value>
		public List<string> Outline { get; set; } = new();

		/// <summary>
		/// Gets or sets the presentation submissions.
		/// </summary>
		/// <value>
		/// A <see cref="List{ShindigSubmission}"/> representing the list of where the presentation has been submitted.
		/// </value>
		public List<ShindigSubmission> Submissions { get; set; } = new();

		/// <summary>
		/// Gets or sets the speaking engagements for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{ShindigPresentation}"/> representing the list of where the presentation has been scheduled.
		/// </value>
		public List<ShindigPresentation> SpeakingEngagements { get; set; } = new();

		/// <summary>
		/// Gets or sets a value indicating whether the presentation has been retired.
		/// </summary>
		/// <value>
		///   <c>true</c> if the presentation has been retired; otherwise, <c>false</c>.
		/// </value>
		public bool IsRetired { get; set; }

		public Presentation() : base("Presentation", "1.0") { }

		public override bool IsValid()
		{
			if (base.IsValid())
			{
				if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentNullException(nameof(Name));
				if (SessionTypes.Any())
					foreach (var sessionType in SessionTypes)
						sessionType.IsValid();
				if (Tags.Any())
					foreach (var tag in Tags)
						tag.IsValid();
			}
			return true;
		}
	}

}