using System.Collections.Generic;

namespace TaleLearnCode.SpeakingEngagementManager.Entities
{

	/// <summary>
	/// Interface for objects representing a presentation presented by a speaker.
	/// </summary>
	public interface IPresentation
	{

		/// <summary>
		/// Gets the identifier of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation identifier.
		/// </value>
		string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation name.
		/// </value>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the abstract for the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's abstract.
		/// </value>
		string Abstract { get; set; }

		/// <summary>
		/// Gets or sets the short version of the presentation's abstract.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the short version of the presentation abstract.
		/// </value>
		string ShortAbstract { get; set; }

		/// <summary>
		/// Gets or sets a one-sentence version of the presentation's abstract.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the one-sentence version of the presentation's abstract.
		/// </value>
		string OneSentenceAbstract { get; set; }

		/// <summary>
		/// Gets or sets the learning objectives for the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{string}"/> representing the presentation learning objectives.
		/// </value>
		List<string> LearningObjectives { get; set; }

	}

}