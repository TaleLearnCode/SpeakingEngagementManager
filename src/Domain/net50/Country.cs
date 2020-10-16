using System;
using System.Collections.Generic;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents the basic details about a country.
	/// </summary>
	/// <seealso cref="Metadata" />
	public class Country : Metadata
	{

		/// <summary>
		/// Gets the code for the world-region the country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the code for the world region the country is a part of.
		/// </value>
		public string RegionCode { get; init; }

		/// <summary>
		/// Gets or sets the name of the world-region the country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the world region the country is a part of.
		/// </value>
		public string RegionName { get; init; }

		/// <summary>
		/// Gets or sets the code for the world-subregion the country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the world-subregion the country is a part of.
		/// </value>
		public string SubregionCode { get; init; }

		/// <summary>
		/// Gets or sets the name of the world-subregion the country is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the world-subregion the country is a part of.
		/// </value>
		public string SubregionName { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the country has divisions (states, provinces, regions, etc.)
		/// </summary>
		/// <value>
		///   <c>true</c> if the country has divisions; otherwise, <c>false</c>.
		/// </value>
		public bool HasDivisions { get; init; }

		/// <summary>
		/// Gets or sets the name of the division within the country (if there are any).
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the country division name.
		/// </value>
		public string DivisionName { get; init; }

		/// <summary>
		/// Gets or sets the URL for the country's flag.
		/// </summary>
		/// <value>
		/// A <see cref="Uri"/> for an image representing the country's flag.
		/// </value>
		public Uri FlagUrl { get; init; }

		/// <summary>
		/// Gets or sets the list of the country's divisions.
		/// </summary>
		/// <value>
		/// A <see cref="List{CountryDivision}"/> representing the list of the country's divisions.
		/// </value>
		public List<CountryDivision> CountryDivisions { get; init; } = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="Country"/> class.
		/// </summary>
		public Country() : base(nameof(Country), "1.0") { }

	}

}