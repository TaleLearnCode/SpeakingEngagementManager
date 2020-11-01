using System;

namespace TaleLearnCode.SpeakingEngagementManager.Domain
{
	/// <summary>
	/// Type for representing metadata objects.
	/// </summary>
	/// <seealso cref="IMetadata" />
	/// <seealso cref="IPartitionKey" />
	public abstract class Metadata : IMetadata
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
		public string Discriminator { get => "Metadata"; }

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

		/// <summary>
		/// Gets or sets the name of the metadata object.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the metadata object name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the metadata.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the metadata type.
		/// </value>
		public string Type { get; init; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Metadata"/> class.
		/// </summary>
		/// <param name="metadataType">The type of metadata represented by the document.</param>
		/// <param name="documentVersion">The version for the document.</param>
		protected Metadata(string metadataType, string documentVersion)
		{
			DocumentVersion = documentVersion;
			Type = metadataType;
		}

		public static string GetMetadataType(IMetadata metadata)
		{
			return metadata switch
			{
				SessionType => MetadataTypes.SessionType,
				ShindigType => MetadataTypes.ShindigType,
				Tag => MetadataTypes.Tag,
				_ => throw new Exception("Invalid metadata type"),
			};
		}

		public static string GetMetadataTypeNameByType(Type metadataType)
		{
			return metadataType.Name switch
			{
				MetadataTypes.SessionType => MetadataTypes.SessionType,
				MetadataTypes.ShindigType => MetadataTypes.ShindigType,
				MetadataTypes.Tag => MetadataTypes.Tag,
				_ => throw new Exception("Invalid metadata type"),
			};
		}

		public virtual bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(OwnerEmailAddress)) throw new Exception("The document must define the OwnerEmailAddress value.");
			if (string.IsNullOrWhiteSpace(Name)) throw new Exception("The document must define the Name value.");
			if (GetMetadataType(this) != Type) throw new Exception("The Type value and the document type must match.");
			return true;
		}

		public TagItem ToTagItem()
		{
			return new TagItem()
			{
				Id = this.Id,
				Name = this.Name
			};
		}

	}

}