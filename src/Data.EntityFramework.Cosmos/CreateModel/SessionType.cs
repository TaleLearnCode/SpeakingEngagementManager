using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	internal static partial class CreateModel
	{

		/// <summary>
		/// Creates the EF model for the <see cref="Cosmos.SessionType"/>
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		internal static void SessionType(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SessionType>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);
					entity.Property(p => p.OwnerEmailAddress).ToJsonProperty(Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
					entity.Property(p => p.Id).ToJsonProperty(Domain.PropertyNames.SessionType.Id);
					entity.Property(p => p.Name).ToJsonProperty(Domain.PropertyNames.SessionType.Name);
					entity.Property(p => p.Duration).ToJsonProperty(Domain.PropertyNames.SessionType.Duration);
					entity.Property(p => p.PresentationSessionTypes).ToJsonProperty(Domain.PropertyNames.SessionType.PresentationSessionTypes);
				});
		}

	}

}