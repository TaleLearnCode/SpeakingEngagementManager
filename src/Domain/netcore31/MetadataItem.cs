﻿using TaleLearnCode.SpeakingEngagementManager.Domain.Exceptions;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	public abstract class MetadataItem : IMetadataItem
	{

		public string Id { get; set; }

		public string Name { get; set; }

		public string OwnerEmailAddress { get; set; }

		public bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(Name)) throw new MemberRequiredException(nameof(Name));
			return true;
		}

	}
}