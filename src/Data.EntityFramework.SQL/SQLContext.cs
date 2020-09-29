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

	}

}