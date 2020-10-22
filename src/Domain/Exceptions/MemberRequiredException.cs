using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain.Exceptions
{
	public class MemberRequiredException : Exception
	{

		public string MemberName { get; }

		public MemberRequiredException() { }

		public MemberRequiredException(string memberName) : base($"Member '{memberName}' is required.")
		{
			MemberName = memberName;
		}

		public MemberRequiredException(string memberName, string message) : base(message)
		{
			MemberName = memberName;
		}

	}

}