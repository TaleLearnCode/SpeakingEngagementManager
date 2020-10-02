using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	internal static partial class CreateModel
	{

		/// <summary>
		/// Creates the EF model for the <see cref="Cosmos.Shindig"/>
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		internal static void Shindig(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Shindig>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);
					entity.Property(p => p.OwnerEmailAddress).ToJsonProperty(Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
					entity.Property(p => p.Id).ToJsonProperty(Domain.PropertyNames.Shindig.Id);
					entity.Property(p => p.Name).ToJsonProperty(Domain.PropertyNames.Shindig.Name);
					entity.Property(p => p.StartDateTime).ToJsonProperty(Domain.PropertyNames.Shindig.StartDateTime);
					entity.Property(p => p.EndDateTime).ToJsonProperty(Domain.PropertyNames.Shindig.EndDateTime);
					entity.Property(p => p.Location).ToJsonProperty(Domain.PropertyNames.Shindig.Location);
					entity.Property(p => p.IsVirtual).ToJsonProperty(Domain.PropertyNames.Shindig.IsVirtual);
					entity.Property(p => p.PresentationShindigs).ToJsonProperty(Domain.PropertyNames.Shindig.PresentationShindigs);
				});
		}

	}

}