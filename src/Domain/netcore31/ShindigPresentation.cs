using System;
using System.Collections.Generic;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a presentation at a shindig.
	/// </summary>
	/// <seealso cref="IDocument" />
	public class ShindigPresentation : IDocument
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
		public string Discriminator { get; } = nameof(ShindigPresentation);

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
		/// Gets or sets the identifier of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated presentation identifier.
		/// </value>
		public string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the title of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated presentation's title.
		/// </value>
		public string PresentationTitle { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Shindig"/>
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated shindig identifier.
		/// </value>
		public string ShindigId { get; set; }

		/// <summary>
		/// Gets or sets the name of the associated <see cref="Shindig"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated shindig name.
		/// </value>
		public string ShindigName { get; set; }

		/// <summary>
		/// Gets or sets the date and time of the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> representing the start time for the presentation.
		/// </value>
		public DateTime? DateTime { get; set; }

		/// <summary>
		/// Gets or sets the type of the session (45-minute session, half-day workshop, etc.) being presented.
		/// </summary>
		/// <value>
		/// A <see cref="SessionType"/> representing the session type for the presentation.
		/// </value>
		public SessionType SessionType { get; set; }

		/// <summary>
		/// Gets or sets the name of the room where the presentation will be given.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the room name where the presentation will be given.
		/// </value>
		public string Room { get; set; }

		/// <summary>
		/// Gets or sets the list of downloads for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="Dictionary{string, Uri}"/> representing the presentation downloads.
		/// </value>
		/// <remarks>The key represents the name of the download (i.e. slides, demo, etc.).  The value is the URL to the download.</remarks>
		public Dictionary<string, Uri> Downloads { get; set; } = new Dictionary<string, Uri>();

	}

}