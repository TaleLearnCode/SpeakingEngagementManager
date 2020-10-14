using Microsoft.EntityFrameworkCore;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.Data.EnttityFramework.Cosmos
{

	public class CosmosContext : DbContext
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseCosmos(
				"https://building-cosmos-app.documents.azure.com:443/",
				"ApykmTxV68RwgYz5HwkjRyrRAz3sj29BxeepayIRpDthUUB0VYimEal870K3bjz5CjvmhnXF87XklFF07WZ6QQ==",
				databaseName: "newDev");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultContainer("new");
			modelBuilder.Entity<Presentation>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);
					entity.Property(p => p.Id).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Presentation.Id);
					entity.Property(p => p.OwnerEmailAddress).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
					entity.Property(p => p.Name).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Presentation.Name);
					entity.Property(p => p.Abstract).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Presentation.Abstract);
					entity.Property(p => p.ShortAbstract).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Presentation.ShortAbstract);
					entity.Property(p => p.HundredCharacterAbstract).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Presentation.HundredCharacterAbstract);
				});

			modelBuilder.Entity<Tag>(
				entity =>
				{
					entity.HasPartitionKey(p => p.OwnerEmailAddress);
					entity.Property(t => t.Id).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Tag.Id);
					entity.Property(t => t.Name).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.Tag.Name);
					entity.Property(t => t.OwnerEmailAddress).ToJsonProperty(SpeakingEngagementManager.Domain.PropertyNames.PartitionKey.CosmosPartitionKey);
				});
		}
	}

}