using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Interface for objects representing an event that a speaker submits to and/or speaks at.
	/// </summary>
	public interface IShindig
	{

		/// <summary>
		/// Gets the identifier of the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig identifier.
		/// </value>
		string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the shindig's name.
		/// </value>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the start date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the start date and time of the shindig.
		/// </value>
		DateTime StartDateTime { get; set; }

		/// <summary>
		/// Gets or sets the end date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the end date and time of the shindig.
		/// </value>
		DateTime EndDateTime { get; set; }

		/// <summary>
		/// Gets or sets the location of the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig location.
		/// </value>
		string Location { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the shindig is a virtual event.
		/// </summary>
		/// <value>
		///   <c>true</c> if the shindig is a virtual event; otherwise, <c>false</c>.
		/// </value>
		bool IsVirtual { get; set; }

	}

}