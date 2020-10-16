namespace TaleLearnCode.SpeakingEngagementManager.Domain
{

	/// <summary>
	/// Interface representing key details needed with objects representing documents.
	/// </summary>
	public interface IDocument
	{

		/// <summary>
		/// Gets the version of the document.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the document version.
		/// </value>
		string DocumentVersion { get => "1.0"; }

		/// <summary>
		/// Gets the discriminator for the document.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the document discriminator.
		/// </value>
		string Discriminator { get; }

		/// <summary>
		/// Gets the identifier of the document.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the document identifier.
		/// </value>
		string Id { get; init; }

		/// <summary>
		/// Gets or sets the email address for the owner of the document.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> representing the owner email address.
		/// </value>
		string OwnerEmailAddress { get; set; }

	}

}