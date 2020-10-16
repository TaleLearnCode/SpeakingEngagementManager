namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	/// <summary>
	/// Type for representing metadata objects.
	/// </summary>
	/// <seealso cref="IMetadata" />
	/// <seealso cref="IPartitionKey" />
	public abstract class Metadata : IDocument
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
		public string Id { get; set; } = IDGenerator.Generate();

		/// <summary>
		/// Gets or sets the email address of the data owner.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the data owner's email address.
		/// </value>
		public string OwnerEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the name of the metadata object.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the metadata object name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Metadata"/> class.
		/// </summary>
		/// <param name="discriminator">The discriminator for the document.</param>
		/// <param name="documentVersion">The version for the document.</param>
		protected Metadata(string discriminator, string documentVersion)
		{
			DocumentVersion = documentVersion;
			Discriminator = discriminator;
		}

	}

}