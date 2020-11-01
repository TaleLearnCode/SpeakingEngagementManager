namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents the basic details about a country division.
	/// </summary>
	/// <seealso cref="Metadata" />
	public class CountryDivision : Metadata
	{

		/// <summary>
		/// Gets or sets the identifier of the country the division is a part of.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the identifier of the associated country.
		/// </value>
		public string CountryId { get; set; }

		/// <summary>
		/// Gets or sets the name of the country the division is associated with.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the associated country.
		/// </value>
		public string CountryName { get; set; }

		/// <summary>
		/// Gets or sets the name of the division category (state, province, region, etc.).
		/// </summary>
		/// <value>
		/// A <c>string</c>
		/// </value>
		public string Category { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CountryDivision"/> class.
		/// </summary>
		public CountryDivision() : base(nameof(CountryDivision), "1.0") { }

	}

}