using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework
{

	/// <summary>
	/// Represents a presentation presented by a speaker.
	/// </summary>
	/// <seealso cref="IPresentation" />
	public class Presentation : IPresentation
	{

		/// <summary>
		/// Gets the identifier of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation identifier.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.Id)]
		public string Id { get; init; } = Guid.NewGuid().ToString();

		/// <summary>
		/// Gets or sets the name of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation name.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.Name)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the abstract for the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's abstract.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.Abstract)]
		public string Abstract { get; set; }

		/// <summary>
		/// Gets or sets the short version of the presentation's abstract.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the short version of the presentation abstract.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.ShortAbstract)]
		public string ShortAbstract { get; set; }

		/// <summary>
		/// Gets or sets a version of the presentation's abstract that is 100 characters or less.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the 100-character limited version of the presentation's abstract.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.HundredCharacterAbstract)]
		public string HundredCharacterAbstract { get; set; }

		/// <summary>
		/// Gets or sets the learning objectives for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{string}"/> representing the presentation learning objectives.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.LearningObjectives)]
		public List<string> LearningObjectives { get; set; } = new List<string>();

		/// <summary>
		/// Gets or sets the associated session types.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationSessionType}"/> representing the session types associated with the presentation.
		/// </value>
		[JsonPropertyName("presentationSessionTypes")]
		public List<PresentationSessionType> PresentationSessionTypes { get; set; } = new List<PresentationSessionType>();

		/// <summary>
		/// Gets or sets the associated shindigs.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationShindig}"/> representing the shindigs associated with the presentation.
		/// </value>
		[JsonPropertyName("presentationShindigs")]
		public List<PresentationShindig> PresentationShindigs { get; set; } = new List<PresentationShindig>();

		/// <summary>
		/// Gets or sets the associated tags.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationTag}"/> representing the tags associated with the presentation.
		/// </value>
		[JsonPropertyName("presentationTags")]
		public List<PresentationTag> PresentationTags { get; set; } = new List<PresentationTag>();

	}

}