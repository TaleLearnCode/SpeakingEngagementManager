using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	public abstract class Document : IDocument
	{

		/// <summary>
		/// Gets the version of the document.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the document version.
		/// </value>
		public string DocumentVersion { get; }

		/// <summary>
		/// Gets the discriminator for the document.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the document discriminator.
		/// </value>
		public string Discriminator { get; }

		/// <summary>
		/// Gets the identifier of the metadata object.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the metadata object identifier.
		/// </value>
		public string Id { get; init; } = IDGenerator.Generate();

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		public string OwnerEmailAddress { get; set; }

		protected Document(string discriminator, string documentVersion)
		{
			Discriminator = discriminator;
			DocumentVersion = documentVersion;
		}

		public virtual bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(Id)) throw new ArgumentNullException(nameof(Id));
			if (string.IsNullOrWhiteSpace(OwnerEmailAddress)) throw new ArgumentNullException(nameof(OwnerEmailAddress));
			return true;
		}

	}

}