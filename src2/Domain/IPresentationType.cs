using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
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
		/// Gets or sets a version of the presentation's abstract that is 100 characters or less.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the 100-character limited version of the presentation's abstract.
		/// </value>
		string HundredCharacterAbstract { get; set; }

		///// <summary>
		///// Gets or sets the learning objectives for the presentation.
		///// </summary>
		///// <value>
		///// A <see cref="List{string}"/> representing the presentation learning objectives.
		///// </value>
		//List<string> LearningObjectives { get; set; }

	}

}