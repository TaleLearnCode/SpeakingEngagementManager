
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a presentation presented by a speaker.
	/// </summary>
	/// <seealso cref="IPresentation" />
	/// <seealso cref="IPartitionKey" />
	public class Presentation : IPresentation, IPartitionKey
	{

		/// <summary>
		/// Gets the identifier of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation identifier.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.Id)]
		public string Id { get; init; } = Guid.NewGuid().ToString();

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.PartitionKey.CosmosPartitionKey)]
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the name of the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation name.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.Name)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the abstract for the presentation.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the presentation's abstract.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.Abstract)]
		public string Abstract { get; set; }

		/// <summary>
		/// Gets or sets the short version of the presentation's abstract.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the short version of the presentation abstract.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.ShortAbstract)]
		public string ShortAbstract { get; set; }

		/// <summary>
		/// Gets or sets a one-sentence version of the presentation's abstract.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the one-sentence version of the presentation's abstract.
		/// </value>
		[JsonPropertyName(Domain.PropertyNames.Presentation.HundredCharacterAbstract)]
		public string HundredCharacterAbstract { get; set; }

		///// <summary>
		///// Gets or sets the learning objectives for the presentation.
		///// </summary>
		///// <value>
		///// A <see cref="List{string}"/> representing the presentation learning objectives.
		///// </value>
		//[JsonPropertyName(Domain.PropertyNames.Presentation.LearningObjectives)]
		//public List<string> LearningObjectives { get; set; } = new();

		///// <summary>
		///// Gets or sets the session types associated with the presentation.
		///// </summary>
		///// <value>
		///// A <see cref="List{SessionType}"/> representing the presentation session types.
		///// </value>
		//[JsonPropertyName("sessionTypes")]
		//public List<SessionType> SessionTypes { get; set; } = new();

		///// <summary>
		///// Gets or sets the shindigs associated with the presentation.
		///// </summary>
		///// <value>
		///// A <see cref="List{Shindig}"/> representing the presentation shindigs.
		///// </value>
		//[JsonPropertyName("shindigs")]
		//public List<Shindig> Shindigs { get; set; } = new();

		/// <summary>
		/// Gets or sets the tags associated with the presentation.
		/// </summary>
		/// <value>
		/// A <see cref="List{Tag}"/> representing the presentation tags.
		/// </value>
		[JsonPropertyName("tags")]
		public List<Tag> Tags { get; set; } = new();

	}

}