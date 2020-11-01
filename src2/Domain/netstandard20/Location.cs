using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a location (Country and Country Division)
	/// </summary>
	public class Location
	{

		public string City { get; set; }

		/// <summary>
		/// Gets the identifier of the associated country.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated country identifier.
		/// </value>
		public string CountryId { get; set; }

		/// <summary>
		/// Gets or sets the name of the associated country.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated country name.
		/// </value>
		public string CountryName { get; set; }

		/// <summary>
		/// Gets or sets the code of the world-region the associated country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the code of the world-region the associated country is a part of.
		/// </value>
		public string RegionCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the world-region the associated country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the world-region the associated country is a part of..
		/// </value>
		public string RegionName { get; set; }

		/// <summary>
		/// Gets or sets the code of the world-subregion the associated country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the code of the world-subregion the associated country is a part of.
		/// </value>
		public string SubregionCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the world-subregion the associated country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the world-subregion the associated country is a part of.
		/// </value>
		public string SubregionName { get; set; }

		/// <summary>
		/// Gets or sets the URL for the associated country's flag.
		/// </summary>
		/// <value>
		/// A <see cref="Uri"/> for an image representing the associated country's flag.
		/// </value>
		public Uri CountryFlag { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated country division.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated country division identifier.
		/// </value>
		public string CountryDivisionId { get; set; }

		/// <summary>
		/// Gets or sets the name of the associated country division.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated country division name.
		/// </value>
		public string CountryDivisionName { get; set; }

		/// <summary>
		/// Gets or sets the name of the category (state, province, region, etc.) for the associated country division.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated country division category.
		/// </value>
		public string CountryDivisionCategory { get; set; }

	}

}