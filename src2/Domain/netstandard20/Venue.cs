using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents the location details for a shindig venue.
	/// </summary>
	/// <seealso cref="Location" />
	public class Venue : Location
	{

		/// <summary>
		/// Gets or sets the name of the venue.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the venue name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the street address of the venue.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the venue street address.
		/// </value>
		public string StreetAddress { get; set; }

		/// <summary>
		/// Gets or sets the URL for a map to the venue.
		/// </summary>
		/// <value>
		/// A <see cref="Uri"/> representing the venue map URL.
		/// </value>
		public Uri MapUrl { get; set; }

	}

}