﻿using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagmentManager.Data.EntityFramework.SQL
{

	/// <summary>
	/// Represents the many-to-many relationship between presentations and session types.
	/// </summary>
	/// <seealso cref="EntityFramework.PresentationSessionType" />
	/// <seealso cref="IPartitionKey" />
	public class PresentationSessionType : IPresentationSessionType
	{

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated Presentation identifier.
		/// </value>
		public string PresentationId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="Presentation"/>.
		/// </summary>
		/// <value>
		/// A <see cref="Presentation"/> representing the associated presentation.
		/// </value>
		public Presentation Presentation { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the associated <see cref="SessionType"/>.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the associated SessionType identifier.
		/// </value>
		public string SessionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the associated <see cref="SessionType"/>.
		/// </summary>
		/// <value>
		/// A <see cref="SessionType"/> representing the associated session type.
		/// </value>
		public SessionType SessionType { get; set; }

	}

}