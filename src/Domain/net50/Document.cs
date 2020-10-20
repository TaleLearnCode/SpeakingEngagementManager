namespace TaleLearnCode.SpeakingEngagementManager.Domain.net50
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

		protected Document(string discriminator, string documentVersion, string id, string ownerEmailAddress)
		{
			Discriminator = discriminator;
			DocumentVersion = documentVersion;
			Id = id;
			OwnerEmailAddress = ownerEmailAddress;
		}

	}

}