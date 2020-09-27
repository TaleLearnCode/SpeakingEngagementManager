using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework
{

	/// <summary>
	/// Represents an event that a speaker submits to and/or speakers at.
	/// </summary>
	/// <seealso cref="IShindig" />
	public class Shindig : IShindig
	{

		/// <summary>
		/// Gets the identifier of the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig identifier.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Shindig.Id)]
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the name of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the shindig's name.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Shindig.Name)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the start date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the start date and time of the shindig.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Shindig.StartDateTime)]
		public DateTime StartDateTime { get; set; }

		/// <summary>
		/// Gets or sets the end date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the end date and time of the shindig.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Shindig.EndDateTime)]
		public DateTime EndDateTime { get; set; }

		/// <summary>
		/// Gets or sets the location of the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig location.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Shindig.Location)]
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the shindig is a virtual event.
		/// </summary>
		/// <value>
		///   <c>true</c> if the shindig is a virtual event; otherwise, <c>false</c>.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Shindig.IsVirtual)]
		public bool IsVirtual { get; set; }

		/// <summary>
		/// Gets or sets the presentations associated with the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="List{PresentationShindig}"/> representing the list of associated presentations.
		/// </value>
		[JsonPropertyName("presentationShindigs")]
		public List<PresentationShindig> PresentationShindigs { get; set; } = new();

	}

}