using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	internal static partial class CreateModel
	{

		/// <summary>
		/// Creates the EF model for the <see cref="Cosmos.PresentationShindig"/>
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		internal static void PresentationShindig(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PresentationShindig>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);

					entity.Property(p => p.OwnerEmailAddress).ToJsonProperty(Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
					entity.Property(p => p.PresentationId).ToJsonProperty(Domain.PropertyNames.PresentationSessionType.PresentationId);
					entity.Property(p => p.Presentation).ToJsonProperty(Domain.PropertyNames.PresentationSessionType.Presentation);
					entity.Property(p => p.ShindigId).ToJsonProperty(Domain.PropertyNames.PresentationSessionType.SessionTypeId);
					entity.Property(p => p.Shindig).ToJsonProperty(Domain.PropertyNames.PresentationSessionType.SessionType);

					entity.HasKey(m => new { m.PresentationId, m.ShindigId });
					entity.HasOne(m => m.Presentation).WithMany(m => m.PresentationShindigs).HasForeignKey(k => k.PresentationId);
					entity.HasOne(m => m.Shindig).WithMany(m => m.PresentationShindigs).HasForeignKey(k => k.ShindigId);
				});
		}

	}

}
