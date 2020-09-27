﻿using System.Text.Json.Serialization;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Repository.EntityFramework.Cosmos
{

	/// <summary>
	/// Represents an event that a speaker submits to and/or speakers at.
	/// </summary>
	/// <seealso cref="EntityFramework.Shindig" />
	/// <seealso cref="IPartitionKey" />
	public class Shindig : Data.EntityFramework.Shindig, IPartitionKey
	{

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

	}

}