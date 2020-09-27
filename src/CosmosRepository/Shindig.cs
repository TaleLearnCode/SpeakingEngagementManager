using System;
using TaleLearnCode.SpeakingEngagementManager.Entities;

namespace CosmosRepository
{

	public class Shindig : IShindig, IPartitionKey
	{

		/// <summary>
		/// Gets the identifier of the session type.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the session type identifier.
		/// </value>
		public string Id { get; init; }

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the name of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the shindig's name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the start date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the start date and time of the shindig.
		/// </value>
		public DateTime StartDateTime { get; set; }

		/// <summary>
		/// Gets or sets the end date and time of the shindig.
		/// </summary>
		/// <value>
		/// a <c>DateTime</c> representing the end date and time of the shindig.
		/// </value>
		public DateTime EndDateTime { get; set; }

		/// <summary>
		/// Gets or sets the location of the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the shindig location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the shindig is a virtual event.
		/// </summary>
		/// <value>
		///   <c>true</c> if the shindig is a virtual event; otherwise, <c>false</c>.
		/// </value>
		public bool IsVirtual { get; set; }

	}

}