using System;
using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Entities;

namespace TaleLearnCode.SpeakingEngagementManager.Repository.CosmosSDK
{

	/// <summary>
	/// Represents an event that a speaker submits to and/or speaks at.
	/// </summary>
	/// <seealso cref="IShindig" />
	/// <seealso cref="IPartitionKey" />
	public class Shindig : IShindig, IPartitionKey
	{

		/// <summary>
		/// Gets the identifier of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type identifier.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Shindig.Id)]
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the name of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the shindig's name.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Shindig.Name)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the start date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the start date and time of the shindig.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Shindig.StartDateTime)]
		public DateTime StartDateTime { get; set; }

		/// <summary>
		/// Gets or sets the end date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the end date and time of the shindig.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Shindig.EndDateTime)]
		public DateTime EndDateTime { get; set; }

		/// <summary>
		/// Gets or sets the location of the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig location.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Shindig.Location)]
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the shindig is a virtual event.
		/// </summary>
		/// <value>
		///   <c>true</c> if the shindig is a virtual event; otherwise, <c>false</c>.
		/// </value>
		[JsonPropertyName(Entities.PropertyNames.Shindig.IsVirtual)]
		public bool IsVirtual { get; set; }

	}

}