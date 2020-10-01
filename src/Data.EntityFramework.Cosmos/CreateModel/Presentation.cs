using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{
	internal partial class CreateModel
	{

		internal void Presentation(ModelBuilder modelBuilder)
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
					entity.Property(p => p.PresentationSessionTypes).ToJsonProperty(Domain.PropertyNames.Presentation.PresentationSessionTypes);
					entity.Property(p => p.PresentationShindigs).ToJsonProperty(Domain.PropertyNames.Presentation.PresentationShindigs);
					entity.Property(p => p.PresentationTags).ToJsonProperty(Domain.PropertyNames.Presentation.PresentationTags);
				});

		}

	}

}