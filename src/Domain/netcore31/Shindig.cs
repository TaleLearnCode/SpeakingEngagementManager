using System;
using System.Collections.Generic;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a shindig (event) within the system.
	/// </summary>
	/// <seealso cref="IDocument" />
	public class Shindig : SEMDocument
	{

		/// <summary>
		/// Gets or sets the name of the shindig (event).
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the name of the shindig.
		/// </value>
		public string Name { get; set; }

		public string Description { get; set; }

		public Uri URL { get; set; }

		/// <summary>
		/// Gets or sets the location of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="Location"/> representing the shindig location.
		/// </value>
		public Location Location { get; set; }

		/// <summary>
		/// Gets or sets the venue details for the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="Venue"/> representing the details of the venue where the shindig will be held.
		/// </value>
		public Venue Venue { get; set; }

		/// <summary>
		/// Gets or sets the start date of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> representing the shindig's start date.
		/// </value>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date of the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> representing the shindig's end date.
		/// </value>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Gets or sets the cost for someone to attend the shindig.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the cost for someone to attend the shindig.
		/// </value>
		public string Cost { get; set; }

		/// <summary>
		/// Gets or sets the type of the shindig (conference, user group, etc.).
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the type of the shindig.
		/// </value>
		public ShindigType ShindigType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the shindig is being held virtually.
		/// </summary>
		/// <value>
		///   <c>true</c> if the shindig is being held virtually; otherwise, <c>false</c>.
		/// </value>
		public bool? IsVirtual { get; set; }

		/// <summary>
		/// Gets or sets the virtual location for the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="Uri"/> representing the virtual location for the shindig.
		/// </value>
		public Uri VirtualLocation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to display the <see cref="VirtualLocation"/> for the shindig.
		/// </summary>
		/// <value>
		///   <c>true</c> if the virtual location URL should be shown; otherwise, <c>false</c>.
		/// </value>
		public bool? DisplayVirtualLocation { get; set; }

		/// <summary>
		/// Gets or sets the presentation submissions to the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="List{ShindigSubmission}"/> representing the presentations that have been submitted to the shindig.
		/// </value>
		public List<ShindigSubmission> Submissions { get; set; } = new List<ShindigSubmission>();

		/// <summary>
		/// Gets or sets the presentations scheduled at the shindig.
		/// </summary>
		/// <value>
		/// A <see cref="List{ShindigPresentation}"/> representing the list of scheduled presentations for the shindig.
		/// </value>
		public List<ShindigPresentation> Presentations { get; set; } = new List<ShindigPresentation>();

		public decimal UTCOffset { get; set; }

		public Shindig() : base(Discriminators.Shindig, "1.0") { }

		public override bool IsValid()
		{
			if (base.IsValid())
			{
				if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentNullException(nameof(Name));
				if (ShindigType != null)
					ShindigType.IsValid();
			}
			return true;
		}

	}

}