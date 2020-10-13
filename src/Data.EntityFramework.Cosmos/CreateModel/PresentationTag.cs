using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	internal static partial class CreateModel
	{

		/// <summary>
		/// Creates the EF model for the <see cref="Cosmos.PresentationTag"/>
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		internal static void PresentationTag(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PresentationTag>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);

					entity.Property(p => p.OwnerEmailAddress).ToJsonProperty(Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
					entity.Property(p => p.PresentationId).ToJsonProperty(Domain.PropertyNames.PresentationTag.PresentationId);
					entity.Property(p => p.Presentation).ToJsonProperty(Domain.PropertyNames.PresentationTag.Presentation);
					entity.Property(p => p.TagId).ToJsonProperty(Domain.PropertyNames.PresentationTag.TagId);
					entity.Property(p => p.Tag).ToJsonProperty(Domain.PropertyNames.PresentationTag.Tag);

					//entity.HasKey(m => new { m.PresentationId, m.TagId });
					//entity.HasOne(m => m.Presentation).WithMany(m => m.PresentationTags).HasForeignKey(k => k.PresentationId);
					//entity.HasOne(m => m.Tag).WithMany(m => m.PresentationTags).HasForeignKey(k => k.TagId);
				});
		}

	}

}