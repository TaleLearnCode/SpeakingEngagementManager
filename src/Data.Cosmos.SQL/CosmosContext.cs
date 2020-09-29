using Microsoft.EntityFrameworkCore;
using TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework;

namespace TaleLearnCode.SpeakingEngagementManager.Data.Cosmos.SQL
{
	public class CosmosContext : DbContext
	{

		private readonly string _accountEndpoint;
		private readonly string _accountKey;
		private readonly string _databaseName;

		public CosmosContext(string accountEndpoint, string accountKey, string databaseName)
		{
			_accountEndpoint = accountEndpoint;
			_accountKey = accountKey;
			_databaseName = databaseName;
		}

		public DbSet<Presentation> Presentations { get; set; }
		public DbSet<SessionType> SessionTypes { get; set; }
		public DbSet<Shindig> Shindigs { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<PresentationSessionType> PresentationSessionTypes { get; set; }
		public DbSet<PresentationShindig> PresentationShindigs { get; set; }
		public DbSet<PresentationTag> PresentationTags { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseCosmos(
					_accountEndpoint,
					_accountKey,
					databaseName: _databaseName
				);

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// Setting the container to use for our documents
			modelBuilder.HasDefaultContainer("SEM");

			// Setting the partition keys for all the documents
			modelBuilder.Entity<Presentation>().HasPartitionKey(m => m.OwnerEmailAddress);
			modelBuilder.Entity<SessionType>().HasPartitionKey(m => m.OwnerEmailAddress);
			modelBuilder.Entity<Shindig>().HasPartitionKey(m => m.OwnerEmailAddress);
			modelBuilder.Entity<Tag>().HasPartitionKey(m => m.OwnerEmailAddress);
			modelBuilder.Entity<PresentationSessionType>().HasPartitionKey(m => m.OwnerEmailAddress);
			modelBuilder.Entity<PresentationShindig>().HasPartitionKey(m => m.OwnerEmailAddress);
			modelBuilder.Entity<PresentationTag>().HasPartitionKey(m => m.OwnerEmailAddress);

			// Many-to-Many relationships
			modelBuilder.Entity<PresentationSessionType>().HasKey(m => new { m.PresentationId, m.SessionTypeId });
			modelBuilder.Entity<PresentationShindig>().HasKey(m => new { m.PresentationId, m.ShindigId });
			modelBuilder.Entity<PresentationTag>().HasKey(m => new { m.PresentationId, m.TagId });
		}

	}
}