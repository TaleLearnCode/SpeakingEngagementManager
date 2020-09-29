using Microsoft.EntityFrameworkCore;
using TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework;

namespace Data.EntityFramework.SQL
{

	public class SQLContext : DbContext
	{

		public DbSet<Presentation> Presentations { get; set; }
		public DbSet<SessionType> SessionTypes { get; set; }
		public DbSet<Shindig> Shindigs { get; set; }
		public DbSet<Tag> Tags { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = SEM");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// Define the many-to-many relationships
			modelBuilder.Entity<PresentationSessionType>().HasKey(m => new { m.PresentationId, m.SessionTypeId });
			modelBuilder.Entity<PresentationSessionType>().HasOne(m => m.Presentation).WithMany(m => m.PresentationSessionTypes).HasForeignKey(k => k.PresentationId);
			modelBuilder.Entity<PresentationSessionType>().HasOne(m => m.SessionType).WithMany(m => m.PresentationSessionTypes).HasForeignKey(k => k.SessionTypeId);

			modelBuilder.Entity<PresentationShindig>().HasKey(m => new { m.PresentationId, m.ShindigId });
			modelBuilder.Entity<PresentationShindig>().HasOne(m => m.Presentation).WithMany(m => m.PresentationShindigs).HasForeignKey(k => k.PresentationId);
			modelBuilder.Entity<PresentationShindig>().HasOne(m => m.Shindig).WithMany(m => m.PresentationShindigs).HasForeignKey(k => k.ShindigId);

			modelBuilder.Entity<PresentationTag>().HasKey(m => new { m.PresentationId, m.TagId });
			modelBuilder.Entity<PresentationTag>().HasOne(m => m.Presentation).WithMany(m => m.PresentationTags).HasForeignKey(k => k.PresentationId);
			modelBuilder.Entity<PresentationTag>().HasOne(m => m.Tag).WithMany(m => m.PresentationTags).HasForeignKey(k => k.TagId);

		}

	}

}