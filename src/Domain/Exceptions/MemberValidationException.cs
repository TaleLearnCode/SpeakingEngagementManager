using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain.Exceptions
{

	public class MemberValidationException : Exception
	{

		public string MemberName { get; }

		public MemberValidationException() { }

		public MemberValidationException(string memberName) : base($"Member '{memberName}' is invalid.")
		{
			MemberName = memberName;
		}

		public MemberValidationException(string memberName, string message) : base(message)
		{
			MemberName = memberName;
		}


	}

}