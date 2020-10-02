using Microsoft.EntityFrameworkCore;


namespace TaleLearnCode.SpeakingEngagementManager.Data.EntityFramework.Cosmos
{

	public class CosmosContext : DbContext
	{

		private readonly string _accountEndpoint;
		private readonly string _accountKey;
		private readonly string _databaseName;
		private readonly string _defaultContainerName;

		/// <summary>
		/// Initializes a new instance of the <see cref="CosmosContext"/> class.
		/// </summary>
		/// <param name="accountEndpoint">The Azure Cosmos DB account endpoint to connect to.</param>
		/// <param name="accountKey">The account key for the Azure Cosmos DB to connect to.</param>
		/// <param name="databaseName">The name of the database within the Azure Cosmos DB account to connect to.</param>
		/// <param name="defaultContainerName">The default container name that will be used if no name is explicitly configured for an entity type.</param>
		public CosmosContext(string accountEndpoint, string accountKey, string databaseName, string defaultContainerName) : base()
		{
			_accountEndpoint = accountEndpoint;
			_accountKey = accountKey;
			_databaseName = databaseName;
			_defaultContainerName = defaultContainerName;
		}

		public DbSet<Presentation> Presentations { get; set; }
		public DbSet<PresentationSessionType> PresentationSessionTypes { get; set; }
		public DbSet<PresentationShindig> PresentationShindigs { get; set; }
		public DbSet<PresentationTag> PresentationTags { get; set; }
		public DbSet<SessionType> SessionTypes { get; set; }
		public DbSet<Shindig> Shindigs { get; set; }
		public DbSet<Tag> Tags { get; set; }

		/// <summary>
		/// <para>
		/// Override this method to configure the database (and other options) to be used for this context.
		/// This method is called for each instance of the context that is created.
		/// The base implementation does nothing.
		/// </para>
		/// <para>
		/// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
		/// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
		/// the options have already been set, and skip some or all of the logic in
		/// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
		/// </para>
		/// </summary>
		/// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
		/// typically define extension methods on this object that allow you to configure the context.</param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseCosmos(
				_accountEndpoint,
				_accountKey,
				databaseName: _databaseName);

		/// <summary>
		/// Override this method to further configure the model that was discovered by convention from the entity types
		/// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
		/// and re-used for subsequent instances of your derived context.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		/// <remarks>
		/// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
		/// then this method will not be run.
		/// </remarks>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			CreateModel.Presentation(modelBuilder);
			CreateModel.PresentationSessionType(modelBuilder);
			CreateModel.PresentationShindig(modelBuilder);
			CreateModel.PresentationTag(modelBuilder);
			CreateModel.SessionType(modelBuilder);
			CreateModel.Shindig(modelBuilder);
			CreateModel.Tag(modelBuilder);
		}

	}

}