using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	internal static partial class CreateModel
	{

		/// <summary>
		/// Creates the EF model for the <see cref="Cosmos.Presentation"/>
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		internal static void Presentation(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Presentation>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);
					entity.Property(p => p.Id).ToJsonProperty(Domain.PropertyNames.Presentation.Id);
					entity.Property(p => p.OwnerEmailAddress).ToJsonProperty(Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
					entity.Property(p => p.Name).ToJsonProperty(Domain.PropertyNames.Presentation.Name);
					entity.Property(p => p.Abstract).ToJsonProperty(Domain.PropertyNames.Presentation.Abstract);
					entity.Property(p => p.ShortAbstract).ToJsonProperty(Domain.PropertyNames.Presentation.ShortAbstract);
					entity.Property(p => p.HundredCharacterAbstract).ToJsonProperty(Domain.PropertyNames.Presentation.HundredCharacterAbstract);
					//entity.Property(p => p.PresentationSessionTypes).ToJsonProperty(Domain.PropertyNames.Presentation.PresentationSessionTypes);
					//entity.Property(p => p.PresentationShindigs).ToJsonProperty(Domain.PropertyNames.Presentation.PresentationShindigs);
					//entity.Property(p => p.PresentationTags).ToJsonProperty(Domain.PropertyNames.Presentation.PresentationTags);
				});

		}

	}

}