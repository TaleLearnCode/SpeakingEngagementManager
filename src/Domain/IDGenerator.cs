using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Helper class to generate document identifiers.
	/// </summary>
	internal static class IDGenerator
	{

		/// <summary>
		/// Generates a document identifier.
		/// </summary>
		/// <returns>A <c>string</c> representing an identifier for a document.</returns>
		internal static string Generate()
		{
			return Guid.NewGuid().ToString().Replace("-", "");
		}

	}

}