using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Represents a type of session is being presented (i.e. 60-minute session; lightening talk; half-day workshop; full-day workshop)
	/// </summary>
	/// <seealso cref="Metadata" />
	public class SessionType : Metadata
	{

		/// <summary>
		/// Gets the duration of the presentations of this session type.
		/// </summary>
		/// <value>
		/// An <c>int</c> representing the number of minutes presentations of this session type run for.
		/// </value>
		public int Duration { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SessionType"/> class.
		/// </summary>
		public SessionType() : base(nameof(SessionType), "1.0") { }

		public override bool IsValid()
		{
			if (base.IsValid())
				if (Duration <= 0) throw new Exception("The document must define the Duration value.");
			return true;
		}

	}
}