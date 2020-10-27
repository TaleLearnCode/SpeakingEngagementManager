using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.PopulateDatabase
{

	// TODO: Presentations need a featured/thumbnail image
	// TODO: Canceled shindigs
	// TODO: Co-Speakers
	// TODO: Recordings

	public class CreateDatabaseRecords
	{

		private CosmosConnection _CosmosConnection;
		private MetadataManager _MetadataManager;
		private PresentationManager _PresentationManager;
		private ShindigManager _ShindigManager;

		private Dictionary<string, Shindig> _Shindigs = new();
		private Dictionary<string, SessionType> _SessionTypes = new();
		private Dictionary<string, ShindigType> _ShindigTypes = new();
		private Dictionary<string, TagItem> _Tags = new();

		#region Constants

		private const string SessionType_Session45 = "45-Minute Session";
		private const string SessionType_Session60 = "60-Minute Session";
		private const string SessionType_Session75 = "75-Minute Session";
		private const string SessionType_HalfDay = "Half-Day Workshop";
		private const string SessionType_FullDay = "Full-Day Workshop";
		private const string SessionType_LighteningTalk = "Lightening Talk";

		private const string Tag_Agile = "Agile";
		private const string Tag_API = "APIs";
		private const string Tag_Architecture = "Architecture";
		private const string Tag_ASPNet = "ASP.NET";
		private const string Tag_Azure = "Azure";
		private const string Tag_AzureAppService = "Azure App Service";
		private const string Tag_AzureDevOps = "Azure DevOps";
		private const string Tag_AzureFunctions = "Azure Functions";
		private const string Tag_AzureMonitor = "Azure Monitor";
		private const string Tag_AzureSQL = "Azure SQL";
		private const string Tag_AzureStorage = "Azure Storage";
		private const string Tag_CaseStudy = "Case Study";
		private const string Tag_Cloud = "Cloud";
		private const string Tag_CosmosDB = "Cosmos DB";
		private const string Tag_ConflictResolution = "Conflict Resolution";
		private const string Tag_ContinousDeployment = "Continuous Deployment";
		private const string Tag_ContinousIntegration = "Continuous Integration";
		private const string Tag_Data = "Data";
		private const string Tag_Database = "Database";
		private const string Tag_DevOps = "DevOps";
		private const string Tag_EventDrivenArchitecture = "Event-Driven Architecture";
		private const string Tag_EventHubs = "Event Hubs";
		private const string Tag_GraphDatabases = "Graph Databases";
		private const string Tag_Leadership = "Leadership";
		private const string Tag_NoSQL = "No SQL";
		private const string Tag_Process = "Process";
		private const string Tag_ProjectManagement = "Project Management";
		private const string Tag_Serverless = "Serverless";
		private const string Tag_SoftSkills = "Soft Skills";
		private const string Tag_SoftwareCraftsmanship = "Software Craftsmanship";
		private const string Tag_SQLServer = "SQL Server";
		private const string Tag_TSQL = "T-SQL";
		private const string Tag_DotNet = ".NET";
		private const string Tag_DotNetFramework = ".NET Framework";
		private const string Tag_DotNetCore = ".NET Core";
		private const string Tag_DotNetStandard = ".NET Standard";

		private const string ShindigType_FirstTierConference = "First-Tier Conference";
		private const string ShindigType_SecondTierConference = "Second-Tier Conference";
		private const string ShindigType_RegionalConference = "Regional Conference";
		private const string ShindigType_UserGroup = "User Group / Meetup";
		private const string ShindigType_PrivateEvent = "Private Event";
		private const string ShindigType_CodeCamp = "Code Camp";

		private const string Shindig_CodeStock2018 = "CodeStock 2018";
		private const string Shindig_StirTrek2018 = "Stir Trek 2018";
		private const string Shindig_MusicCityTech2018 = "Music City Tech 2018";
		private const string Shindig_PittsburghTechFest2018 = "Pittsburgh TestFest 2018";
		private const string Shindig_BeerCityCode2018 = "Beer City Code 2018";
		private const string Shindig_KCDC2018 = "KCDC 2018";
		private const string Shindig_ThatConference2018 = "That Conference 2018";
		private const string Shindig_CoderCruise2018 = "Coder Cruise 2018";
		private const string Shindig_TechBash2018 = "TechBash 2018";
		private const string Shindig_DogFoodCon2018 = "DogFoodCon 2018";
		private const string Shindig_DevSpace2018 = "DevSpace 2018";
		private const string Shindig_CincyDayOfAgile2018 = "Cincy Day of Agile 2018";
		private const string Shindig_Evansville0218 = "Evansville Technology Group (February 2018)";
		private const string Shindig_LouDotNet0218 = "Louisville .NET Meetup (February 2018)";
		private const string Shindig_Evansville1218 = "Evansville Technology Group (December 2018)";
		private const string Shindig_LouDotNet1218 = "Louisville .NET Meetup (December 2018)";

		private const string Shindig_CodeMash2019 = "CodeMash v2.0.1.9";
		private const string Shindig_NebraskaCode2019 = "Nebraska.Code() 2019";
		private const string Shindig_IndyCode2019 = "Indy.Code() 2019";
		private const string Shindig_PrairieCode2019 = "Prairie.Code() 2019";
		private const string Shindig_Connectaha2019 = "Connectaha 2019";
		private const string Shindig_VSLiveNewOrleans2019 = "Visual Studio Live! (New Orleans 2019)";
		private const string Shindig_VSLiveBoston2019 = "Visual Studio Live! (Boston 2019)";
		private const string Shindig_VSLiveRedmond2019 = "Visual Studio Live (Redmond 2019)";
		private const string Shindig_DotNetSouth2019 = "DotNetSouth 2019";
		private const string Shindig_Refactr2019 = "Refactr 2019";
		private const string Shindig_NDCMinnesota2019 = "NDC Minnesota 2019";
		private const string Shindig_VSLiveSanDiego2019 = "Visual Studio Live! (San Diego 2019)";
		private const string Shindig_VSLiveChicago2019 = "Visual Studio Live! (Chicago 2019)";
		private const string Shindig_StirTrek2019 = "Stir Trek 2019";
		private const string Shindig_NDCOslo2019 = "NDC Oslo 2019";
		private const string Shindig_BeerCityCode2019 = "Beer City Code 2019";
		private const string Shindig_PrairieDevCon2019 = "Prairie Dev Con 2019";
		private const string Shindig_TechBash2019 = "TechBash 2019";
		private const string Shindig_MusicCityTech2019 = "Music City Tech 2019";
		private const string Shindig_ScenicCitySummit2019 = "Scenic City Summit 2019";
		private const string Shindig_Live360Orlando2019 = "Live! 360 Orlando 2019";
		private const string Shindig_Øredev2019 = "Øredev 2019";
		private const string Shindig_ThunderPlains2019 = "ThunderPlains 2019";
		private const string Shindig_DevUp2019 = "dev up 2019";
		private const string Shindig_DogFoodCon2019 = "DogFoodCon 2019";
		private const string Shindig_CreamCityCode2019 = "Cream City Code 2019";
		private const string Shindig_DevSpace2019 = "DevSpace 2019";
		private const string Shindig_LittleRockTechFest2019 = "Little Rock Tech Fest 2019";
		private const string Shindig_PrairieDevConRegina2019 = "Prairie Dev Con Regina 2019";
		private const string Shindig_TechCon2019 = "Tech Con '19";
		private const string Shindig_UpdateConference2019 = "Update Conference 2019";
		private const string Shindig_AtlantaCodeCamp2019 = "Atlanta Code Camp 2019";
		private const string Shindig_DevDay2019 = "DevDay 2019";
		private const string Shindig_Swetug2020 = "Swetugg 2019";
		private const string Shindig_SDD2019 = "Softwqare Design & Development 2019";
		private const string Shindig_CodeStock2019 = "Code Stock 2019";
		private const string Shindig_RevolutionConf2019 = "RevolutionConf 2019";
		private const string Shindig_SelfConference2019 = "self.conference 2019";
		private const string Shindig_DotNetSummit2019 = ".NET Summit 2019";
		private const string Shindig_CincyDeliver2019 = "Cincy Deliver 2019";
		private const string Shindig_ThatConfernece2019 = "That Conference 2019";
		private const string Shindig_Agile2019 = "Agile 2019";
		private const string Shindig_NDCTechTown2019 = "NDC Tech Town 2019";
		private const string Shindig_StrangeLoop2019 = "Stange Loop 2019";
		private const string Shindig_DevConf2019 = "DevConf 2019";
		private const string Shindig_Techorama2019 = "Techorama 2019";
		private const string Shindig_BuildStuffLithuania2019 = "Build Stuff 2019 (Lithuania)";
		private const string Shindig_BuildStuffKiev2019 = "Build Stuff 2019 (Kiev)";
		private const string Shindig_AgileDevOpsEast2019 = "Agile + DevOps East 2019";
		private const string Shindig_APIDaysParis2019 = "API Days (Paris 2019)";
		private const string Shindig_TechLadies0619 = "Tech Ladies (June 2019)";
		private const string Shindig_SoftwareGuild0619 = "Software Guild (June 2019)";
		private const string Shindig_TechFoundations0119 = "Tech Foundations (January 2019)";
		private const string Shindig_IgniteBeijing2019 = "Microsoft Ignite the Tour 2019 (Beijing)";
		private const string Shindig_LouDotNet0119 = "Louisville .NET Meetup (January 2019)";
		private const string Shindig_LouDotNet1019 = "Louisville .NET Meetup (October 2019)";

		private const string Shindig_STAREAST2020 = "STAREAST 2020";
		private const string Shindig_VSLiveLasVegas2020 = "Visual Studio Live! (Las Vegas 2020)";
		private const string Shindig_VSLiveAustin2020 = "Visual Studio Live! (Austin 2020)";
		private const string Shindig_DeveloperWeek2020 = "DeveloperWeek 2020";
		private const string Shindig_CodeMash2020 = "CodeMash v2.0.2.0";
		private const string Shindig_IgniteToronto2020 = "Microsoft Ignite the Tour 2020 (Toronto)";
		private const string Shindig_IgniteLondon2020 = "Microsoft Ignite the Tour 2020 (London)";
		private const string Shindig_IgniteMilan2020 = "Microsoft Ignite the Tour 2020 (Milan)";
		private const string Shindig_IgniteJohannesburg2020 = "Microsoft Ignite the Tour 2020 (Johannesburg)";
		private const string Shindig_IgniteWashingtonDC2020 = "Microsoft Ignite the Tour 2020 (Washington, DC)";
		private const string Shindig_IgniteDubai2020 = "Microsoft Ignite the Tour 2020 (Dubai)";
		private const string Shindig_IgniteSydney2020 = "Microsoft Ignite the Tour 2020 (Sydney)";
		private const string Shindig_IgniteTaipei2020 = "Microsoft Ignite the Tour 2020 (Taipei)";
		private const string Shindig_IgniteSingapore2020 = "Microsoft Ignite the Tour 2020 (Singapore)";
		private const string Shindig_IgnitePrague2020 = "Microsoft Ignite the Tour 2020 (Prague)";
		private const string Shindig_IgniteCopenhagen2020 = "Microsoft Ignite the Tour 2020 (Copenhagen)";
		private const string Shindig_IgniteZürich2020 = "Microsoft Ignite the Tour 2020 (Zürich)";
		private const string Shindig_IgniteAmsterdam2020 = "Microsoft Ignite the Tour 2020 (Amsterdam)";
		private const string Shindig_IgniteHongKong2020 = "Microsoft Ignite the Tour 2020 (Hong Kong)";
		private const string Shindig_IgniteMadrid2020 = "Microsoft Ignite the Tour 2020 (Madrid)";
		private const string Shindig_IgniteMumbai2020 = "Microsoft Ignite the Tour 2020 (Mumbai)";
		private const string Shindig_IgniteBangalore2020 = "Microsoft Ignite the Tour 2020 (Bangalore)";
		private const string Shindig_IgniteChicago2020 = "Microsoft Ignite the Tour 2020 (Chicago)";
		private const string Shindig_IgniteTelAviv2020 = "Microsoft Ignite the Tour 2020 (Tel Aviv)";
		private const string Shindig_IgniteBerlin2020 = "Microsoft Ignite the Tour 2020 (Berlin)";
		private const string Shindig_IgniteStockholm2020 = "Microsoft Ignite the Tour 2020 (Stockholm)";
		private const string Shindig_DevConf2020 = "DevConf 2020";
		private const string Shindig_Connectaha2020 = "Connectaha 2020";
		private const string Shindig_Refactr2020 = "Refactr 2020";
		private const string Shindig_NDCLondon2020 = "NDC {London} 2020";
		private const string Shindig_SDD2020 = "Software Design & Development 2020";
		private const string Shindig_CodeStock2020 = "CodeStock 2020";
		private const string Shindig_MicrosoftTechDays = "Microsoft TechDays 2020";
		private const string Shindig_CaribbeanDeveloeprsConference = "Caribbean Developers Conference 2020";
		private const string Shindig_MicroCPH2020 = "MicroCPH 2020";
		private const string Shindig_DevoxxUK2020 = "Devoxx UK 2020";
		private const string Shindig_t3chfest2020 = "t3chfest 2020";
		private const string Shindig_CircleCityCom2020 = "CircleCityCon 2020";
		private const string Shindig_OrlandoCodeCamp2020 = "Orlando Code Camp 2020";
		private const string Shindig_SouthFloridaCodeCamp2020 = "South Florida Software Dev Con 2020";
		private const string Shindig_OReillySofttwareArchitetureConference2020 = "O'Reilly Software Architecture Conference 2020";
		private const string Shindig_IndyCode2020 = "Indy.Code() 2020";
		private const string Shindig_ServerlessDaysCardiff2020 = "ServelessDays Cardiff 2020";
		private const string Shindig_NDCPorto2020 = "NDC Porto 2020";
		private const string Shindig_Agile2020 = "Agile 2020";
		private const string Shindig_KCDC2020 = "KCDC 2020";
		private const string Shindig_BostonCodeCamp2020 = "Boston Code Camp 2020";
		private const string Shindig_DotNetDeveloperConference2020 = ".NET Developer Conference 2020";
		private const string Shindig_EuropeanCloudConference2020 = "European Cloud Conference 2021";
		private const string Shindig_NDCSydney2020 = "NDC Sydney 2020";
		private const string Shindig_ThatConference2020 = "THAT Conference 2020";
		private const string Shindig_EuropeanSharePointOffice365AzureConference2020 = "European SharePoint Office 365 & Azure Conference 2020";
		private const string Shindig_Øredev2020 = "Øredev 2020";
		private const string Shindig_AzureFest2020 = "Azure Fest 2020";
		private const string Shindig_CincyDeliver2020 = "Cincy Deliver 2020";
		private const string Shindig_BeerCityCode2020 = "Beer City Code 2020";
		private const string Shindig_MusicCityTech2020 = "Music City Tech 2020";
		private const string Shindig_NDCManchester2020 = "NDC Manchester 2020";
		private const string Shindig_Momentum2020 = "Momentum 2020";
		private const string Shindig_STP2020 = "Software Test Professionals 2020";
		private const string Shindig_AgileDevOpsWest2020 = "Agile + DevOps West 2020";
		private const string Shindig_GraniteStateCC2020 = "Granite State Code Camp 2020";
		private const string Shindig_TechBash2020 = "TechBash 2020";
		private const string Shindig_DallasAustinAzure1220 = "Dallas & Austin Azure Meetup";
		private const string Shindig_NDCOslo2020 = "NDC Oslo 2020";
		private const string Shindig_TDevConf2020 = "TDevConf 2020";

		#endregion

		public CreateDatabaseRecords(CosmosConnection cosmosConnection)
		{
			_CosmosConnection = cosmosConnection;
			_MetadataManager = new MetadataManager(_CosmosConnection.Container);
			_PresentationManager = new PresentationManager(_CosmosConnection.Container);
			_ShindigManager = new ShindigManager(_CosmosConnection);
		}

		public async Task Execute()
		{

			using var progressBar = new ProgressBar(5, "Populating the database");

			await CreateSessionTypesAsync(progressBar);
			await CreateTagsAsync(progressBar);
			await CreateShindigTypesAsync(progressBar);
			await CreateShindigsAsync(progressBar);
			await CreatePresentations(progressBar);

		}

		private Venue GetVenue(Location location, string name, string streetAddress, Uri mapUrl)
		{
			return new Venue()
			{
				Name = name,
				StreetAddress = streetAddress,
				MapUrl = mapUrl,
				City = location.City,
				CountryId = location.CountryId,
				CountryName = location.CountryName,
				RegionCode = location.RegionCode,
				RegionName = location.RegionName,
				SubregionCode = location.SubregionCode,
				SubregionName = location.SubregionName,
				CountryFlag = location.CountryFlag,
				CountryDivisionId = location.CountryDivisionId,
				CountryDivisionName = location.CountryDivisionName,
				CountryDivisionCategory = location.CountryDivisionCategory
			};
		}

		private Location GetUSLocation(string city, string countryDivisionId, string countryDivisionName, string countryDivisionCategory = "State")
		{
			return new Location()
			{
				City = city,
				CountryId = "US",
				CountryName = "United States",
				RegionCode = "019",
				RegionName = "Americas",
				SubregionCode = "021",
				SubregionName = "Northern America",
				CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/us.svg"),
				CountryDivisionId = countryDivisionId,
				CountryDivisionName = countryDivisionName,
				CountryDivisionCategory = countryDivisionCategory
			};
		}

		#region Create Metadata

		private async Task CreateSessionTypesAsync(ProgressBar progressBar)
		{

			using var childProgressBar = progressBar.Spawn(6, "Creating the Session Types");

			SessionType sessionType;

			sessionType = await _MetadataManager.CreateMetadataAsync<SessionType>(new SessionType() { Name = SessionType_Session45, Duration = 45 });
			_SessionTypes.Add(sessionType.Name, sessionType);
			childProgressBar.Tick();

			sessionType = await _MetadataManager.CreateMetadataAsync<SessionType>(new SessionType() { Name = SessionType_Session60, Duration = 45 });
			_SessionTypes.Add(sessionType.Name, sessionType);
			childProgressBar.Tick();

			sessionType = await _MetadataManager.CreateMetadataAsync<SessionType>(new SessionType() { Name = SessionType_Session75, Duration = 45 });
			_SessionTypes.Add(sessionType.Name, sessionType);
			childProgressBar.Tick();

			sessionType = await _MetadataManager.CreateMetadataAsync<SessionType>(new SessionType() { Name = SessionType_HalfDay, Duration = 45 });
			_SessionTypes.Add(sessionType.Name, sessionType);
			childProgressBar.Tick();

			sessionType = await _MetadataManager.CreateMetadataAsync<SessionType>(new SessionType() { Name = SessionType_FullDay, Duration = 45 });
			_SessionTypes.Add(sessionType.Name, sessionType);
			childProgressBar.Tick();

			sessionType = await _MetadataManager.CreateMetadataAsync<SessionType>(new SessionType() { Name = SessionType_LighteningTalk, Duration = 45 });
			_SessionTypes.Add(sessionType.Name, sessionType);
			childProgressBar.Tick();

		}

		private async Task CreateTagsAsync(ProgressBar progressBar)
		{

			using var childProgressBar = progressBar.Spawn(12, "Creating the tags");

			await CreateTagAsync(Tag_Agile, childProgressBar);
			await CreateTagAsync(Tag_API, childProgressBar);
			await CreateTagAsync(Tag_Architecture, childProgressBar);
			await CreateTagAsync(Tag_ASPNet, childProgressBar);
			await CreateTagAsync(Tag_Azure, childProgressBar);
			await CreateTagAsync(Tag_AzureAppService, childProgressBar);
			await CreateTagAsync(Tag_AzureDevOps, childProgressBar);
			await CreateTagAsync(Tag_AzureFunctions, childProgressBar);
			await CreateTagAsync(Tag_AzureMonitor, childProgressBar);
			await CreateTagAsync(Tag_AzureSQL, childProgressBar);
			await CreateTagAsync(Tag_AzureStorage, childProgressBar);
			await CreateTagAsync(Tag_CaseStudy, childProgressBar);
			await CreateTagAsync(Tag_Cloud, childProgressBar);
			await CreateTagAsync(Tag_ConflictResolution, childProgressBar);
			await CreateTagAsync(Tag_ContinousDeployment, childProgressBar);
			await CreateTagAsync(Tag_ContinousIntegration, childProgressBar);
			await CreateTagAsync(Tag_CosmosDB, childProgressBar);
			await CreateTagAsync(Tag_Data, childProgressBar);
			await CreateTagAsync(Tag_Database, childProgressBar);
			await CreateTagAsync(Tag_DevOps, childProgressBar);
			await CreateTagAsync(Tag_EventDrivenArchitecture, childProgressBar);
			await CreateTagAsync(Tag_EventHubs, childProgressBar);
			await CreateTagAsync(Tag_GraphDatabases, childProgressBar);
			await CreateTagAsync(Tag_Leadership, childProgressBar);
			await CreateTagAsync(Tag_NoSQL, childProgressBar);
			await CreateTagAsync(Tag_Process, childProgressBar);
			await CreateTagAsync(Tag_ProjectManagement, childProgressBar);
			await CreateTagAsync(Tag_Serverless, childProgressBar);
			await CreateTagAsync(Tag_SoftSkills, childProgressBar);
			await CreateTagAsync(Tag_SoftwareCraftsmanship, childProgressBar);
			await CreateTagAsync(Tag_SQLServer, childProgressBar);
			await CreateTagAsync(Tag_TSQL, childProgressBar);
			await CreateTagAsync(Tag_DotNet, childProgressBar);
			await CreateTagAsync(Tag_DotNetFramework, childProgressBar);
			await CreateTagAsync(Tag_DotNetCore, childProgressBar);
			await CreateTagAsync(Tag_DotNetStandard, childProgressBar);

		}

		private async Task CreateTagAsync(string tagName, ChildProgressBar childProgressBar)
		{
			var tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = tagName });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();
		}

		private async Task CreateShindigTypesAsync(ProgressBar progressBar)
		{

			using var childProgressBar = progressBar.Spawn(3, "Creating the Shindig Types");

			ShindigType shindigType;

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_FirstTierConference });
			_ShindigTypes.Add(shindigType.Name, shindigType);
			childProgressBar.Tick();

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_SecondTierConference });
			_ShindigTypes.Add(shindigType.Name, shindigType);
			childProgressBar.Tick();

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_RegionalConference });
			_ShindigTypes.Add(shindigType.Name, shindigType);
			childProgressBar.Tick();

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_UserGroup });
			_ShindigTypes.Add(shindigType.Name, shindigType);
			childProgressBar.Tick();

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_PrivateEvent });
			_ShindigTypes.Add(shindigType.Name, shindigType);
			childProgressBar.Tick();

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_CodeCamp });
			_ShindigTypes.Add(shindigType.Name, shindigType);
			childProgressBar.Tick();

		}

		#endregion

		#region Create Shindigs

		private async Task CreateShindigsAsync(ProgressBar progressBar)
		{

			using var childProgressBar = progressBar.Spawn(127, "Creating the Shindigs");

			// 2018
			await CreateShindigAsync(Shindig_CodeStock2018, GetUSLocation("Knoxville", "TN", "Tennessee"), new DateTime(2018, 4, 20), new DateTime(2018, 4, 21), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_StirTrek2018, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2018, 5, 4), new DateTime(2018, 5, 4), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_MusicCityTech2018, GetUSLocation("Nashville", "TN", "Tennessee"), new DateTime(2018, 5, 31), new DateTime(2018, 6, 2), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_PittsburghTechFest2018, GetUSLocation("Pittsburgh", "PA", "Pennsylvania"), new DateTime(2018, 6, 2), new DateTime(2018, 6, 2), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_BeerCityCode2018, GetUSLocation("Grand Rapids", "MI", "Michigan"), new DateTime(2018, 6, 22), new DateTime(2018, 6, 23), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_KCDC2018, GetUSLocation("Kansas City", "MO", "Missouri"), new DateTime(2018, 7, 11), new DateTime(2018, 7, 13), _ShindigTypes[ShindigType_RegionalConference], childProgressBar, "The Kansas City Developer Conference is excited to announce our 10th annual event! Our 2018 conference will be held July 12th and 13th, with a pre-conference workshop day on July 11th. Once again, the event will be held at the Kansas City Convention Center in downtown Kansas City. Each year, we draw a large audience of new and experienced Developers, PMs, and Technology Managers from Missouri, Kansas, Illinois, Nebraska, Iowa, Minnesota, Oklahoma, and the Dakotas.", GetVenue(GetUSLocation("Kansas City", "MO", "Missouri"), "Kansas City Convention Center", "200 West 12th Street", new Uri("https://goo.gl/maps/GDQVXVzKYCQ2")), "$$$", new Uri("http://www.kcdc.info/"), 5);
			await CreateShindigAsync(Shindig_ThatConference2018, GetUSLocation("Wisconsin Dells", "WI", "Wisconsin"), new DateTime(2018, 8, 6), new DateTime(2018, 8, 8), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CoderCruise2018, GetUSLocation("Ft. Lauderdale", "FL", "Florida"), new DateTime(2018, 8, 30), new DateTime(2018, 9, 3), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_TechBash2018, GetUSLocation("Pocono Manor", "PA", "Pennsylvania"), new DateTime(2018, 10, 2), new DateTime(2018, 10, 5), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DogFoodCon2018, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2018, 10, 4), new DateTime(2018, 10, 5), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevSpace2018, GetUSLocation("Huntsville", "AL", "Alabama"), new DateTime(2018, 10, 12), new DateTime(2018, 10, 13), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Evansville0218, GetUSLocation("Evansville", "IN", "Indiana"), new DateTime(2018, 2, 15), new DateTime(2018, 2, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "This is a group for anyone interested in technology in the greater Evansville Indiana area. This group is an umbrella group that encompasses the Evansville SharePoint User Group, EVVPASS SQL Server User Group, Evansville .Net User Group and Southern Indiana Powershell User Group. We will meet on the Third Thursdays of the Month during lunch and do a evening social event on every 5th Thursday.", GetVenue(GetUSLocation("Evansville", "IN", "Indiana"), "Central Library Browning Room B", "200 SE Martin Luther King Jr. Blvd", new Uri("https://goo.gl/maps/FRQSMrvDAu72")), "Free", new Uri("https://www.meetup.com/Evansville-Technology-Group/events/246450171/"), -4);
			await CreateShindigAsync(Shindig_LouDotNet0218, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2018, 2, 15), new DateTime(2018, 2, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "The Louisville .NET Meetup group is dedicated to helping developers around Louisville to become better developers with an emphasis on the Microsoft .NET technology stack. We hold meetings on the third Thursday of each month.\nThe Louisville.NET Meetup group is also host to Code PaLOUsa, the Louisville Global Azure Bootcamp(louisville.azurebootcamp.net), and the Louisville Global DevOps Bootcamp.", GetVenue(GetUSLocation("Louisville", "KY", "Kentucky"), "Modis", "101 Bullitt Lane", new Uri("https://goo.gl/maps/bnN43qJZB192")), "Free", new Uri("https://www.meetup.com/Louisville-DotNet/events/246273386/"), -5);
			await CreateShindigAsync(Shindig_CincyDayOfAgile2018, GetUSLocation("Cincinnati", "OH", "Ohio"), new DateTime(2018, 7, 27), new DateTime(2018, 7, 27), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Evansville1218, GetUSLocation("Evansville", "IN", "Indiana"), new DateTime(2018, 2, 15), new DateTime(2018, 2, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "This is a group for anyone interested in technology in the greater Evansville Indiana area. This group is an umbrella group that encompasses the Evansville SharePoint User Group, EVVPASS SQL Server User Group, Evansville .Net User Group and Southern Indiana Powershell User Group. We will meet on the Third Thursdays of the Month during lunch and do a evening social event on every 5th Thursday.", GetVenue(GetUSLocation("Evansville", "IN", "Indiana"), "Central Library Browning Room B", "200 SE Martin Luther King Jr. Blvd", new Uri("https://goo.gl/maps/FRQSMrvDAu72")), "Free", new Uri("https://www.meetup.com/Evansville-Technology-Group/events/246450171/"), -4);
			await CreateShindigAsync(Shindig_LouDotNet1218, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2018, 2, 15), new DateTime(2018, 2, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "The Louisville .NET Meetup group is dedicated to helping developers around Louisville to become better developers with an emphasis on the Microsoft .NET technology stack. We hold meetings on the third Thursday of each month.\nThe Louisville.NET Meetup group is also host to Code PaLOUsa, the Louisville Global Azure Bootcamp(louisville.azurebootcamp.net), and the Louisville Global DevOps Bootcamp.", GetVenue(GetUSLocation("Louisville", "KY", "Kentucky"), "Modis", "101 Bullitt Lane", new Uri("https://goo.gl/maps/bnN43qJZB192")), "Free", new Uri("https://www.meetup.com/Louisville-DotNet/events/246273386/"), -5);

			// 2019
			await CreateShindigAsync(Shindig_CodeMash2019, GetUSLocation("Sandusky", "OH", "Ohio"), new DateTime(2019, 1, 8), new DateTime(2019, 1, 11), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Connectaha2019, GetUSLocation("Omaha", "NE", "Nebraska"), new DateTime(2019, 3, 8), new DateTime(2019, 3, 8), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CodeStock2019, GetUSLocation("Knoxville", "TN", "Tennessee"), new DateTime(2019, 4, 12), new DateTime(2019, 4, 13), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveNewOrleans2019, GetUSLocation("New Orleans", "LA", "Louisiana"), new DateTime(2019, 4, 22), new DateTime(2019, 4, 26), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IndyCode2019, GetUSLocation("Indianapolis", "IN", "Indiana"), new DateTime(2019, 4, 24), new DateTime(2019, 4, 26), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_StirTrek2019, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2019, 4, 26), new DateTime(2019, 4, 26), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCMinnesota2019, GetUSLocation("St. Paul", "MN", "Minnesota"), new DateTime(2019, 5, 6), new DateTime(2019, 5, 9), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_DotNetSouth2019, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2019, 5, 8), new DateTime(2019, 5, 10), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_BeerCityCode2019, GetUSLocation("Grand Rapids", "MI", "Michigan"), new DateTime(2019, 5, 31), new DateTime(2019, 6, 1), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_PrairieDevCon2019, new Location() { City = "Winnipeg", CountryDivisionCategory = "province", CountryDivisionId = "MB", CountryDivisionName = "Manitoba", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ca.svg"), CountryId = "CA", CountryName = "Canada", RegionCode = "019", RegionName = "Americas", SubregionCode = "021", SubregionName = "Northern America" }, new DateTime(2019, 6, 4), new DateTime(2019, 6, 5), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Refactr2019, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2019, 6, 5), new DateTime(2019, 6, 5), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_RevolutionConf2019, GetUSLocation("Virgina Beach", "VA", "Virginia"), new DateTime(2019, 6, 6), new DateTime(2019, 6, 7), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_SelfConference2019, GetUSLocation("Detroit", "MI", "Michigan"), new DateTime(2019, 6, 7), new DateTime(2019, 6, 8), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DotNetSummit2019, new Location() { City = "Minsk", CountryDivisionCategory = "city", CountryDivisionId = "BY-HM", CountryDivisionName = "Horad Minsk", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/by.svg"), CountryId = "BY", CountryName = "Belarus", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2019, 6, 8), new DateTime(2019, 6, 8), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCOslo2019, new Location() { City = "Oslo", CountryDivisionId = "NO-03", CountryDivisionCategory = "county", CountryDivisionName = "Oslo", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/no.svg"), CountryId = "NO", CountryName = "Norway", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 6, 17), new DateTime(2019, 6, 21), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_CincyDeliver2019, GetUSLocation("Cincinnati", "OH", "Ohio"), new DateTime(2019, 7, 26), new DateTime(2019, 7, 26), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_ThatConfernece2019, GetUSLocation("Wisconsin Dells", "WI", "Wisconsin"), new DateTime(2019, 8, 5), new DateTime(2019, 8, 8), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Agile2019, GetUSLocation("Washington, DC", "DC", "District of Columbia", "district"), new DateTime(2019, 8, 5), new DateTime(2019, 8, 9), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveRedmond2019, GetUSLocation("Redmond", "WA", "Washington"), new DateTime(2019, 8, 12), new DateTime(2019, 8, 16), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_NebraskaCode2019, GetUSLocation("Lincoln", "NE", "Nebraska"), new DateTime(2019, 8, 14), new DateTime(2019, 8, 16), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCTechTown2019, new Location() { City = "Kongsberg", CountryDivisionCategory = "county", CountryDivisionId = "NO-06", CountryDivisionName = "Buskerud", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/no.svg)"), CountryId = "NO", CountryName = "Norway", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 9, 2), new DateTime(2019, 9, 5), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_MusicCityTech2019, GetUSLocation("Nashville", "TN", "Tennessee"), new DateTime(2019, 9, 5), new DateTime(2019, 9, 7), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_PrairieCode2019, GetUSLocation("Des Moines", "IA", "Iowa"), new DateTime(2019, 9, 11), new DateTime(2019, 9, 13), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_StrangeLoop2019, GetUSLocation("St. Louis", "MO", "Missouri"), new DateTime(2019, 9, 12), new DateTime(2019, 9, 14), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevConf2019, new Location() { City = "Krakow", CountryDivisionCategory = "voivodship", CountryDivisionId = "PL-12", CountryDivisionName = "Małopolskie", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/pl.svg"), CountryId = "PL", CountryName = "Poland", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2019, 9, 25), new DateTime(2019, 9, 27), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveSanDiego2019, GetUSLocation("San Diego", "CA", "California"), new DateTime(2019, 9, 29), new DateTime(2019, 10, 3), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_Techorama2019, new Location() { City = "Ede", CountryDivisionCategory = "province", CountryDivisionId = "NL-GE", CountryDivisionName = "Gelderland", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/nl.svg"), CountryId = "NL", CountryName = "Netherlands", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2019, 10, 1), new DateTime(2019, 10, 2), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_ScenicCitySummit2019, GetUSLocation("Chattanooga", "TN", "Tennessee"), new DateTime(2019, 10, 3), new DateTime(2019, 10, 4), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DogFoodCon2019, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2019, 10, 3), new DateTime(2019, 10, 4), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CreamCityCode2019, GetUSLocation("Milwaukee", "WI", "Wisconsin"), new DateTime(2019, 10, 4), new DateTime(2019, 10, 5), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveChicago2019, GetUSLocation("Chicago", "IL", "Illinois"), new DateTime(2019, 10, 6), new DateTime(2019, 10, 10), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_LittleRockTechFest2019, GetUSLocation("Little Rock", "AR", "Arkansas"), new DateTime(2019, 10, 10), new DateTime(2019, 10, 11), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevSpace2019, GetUSLocation("Huntsville", "AL", "Alabama"), new DateTime(2019, 10, 11), new DateTime(2019, 10, 12), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevUp2019, GetUSLocation("St. Louis", "MO", "Missouri"), new DateTime(2019, 10, 14), new DateTime(2019, 10, 16), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_TechCon2019, GetUSLocation("Detroit", "MI", "Michigan"), new DateTime(2019, 10, 17), new DateTime(2019, 10, 17), _ShindigTypes[ShindigType_PrivateEvent], childProgressBar);
			await CreateShindigAsync(Shindig_ThunderPlains2019, GetUSLocation("Oklahoma City", "OK", "Oklahoma"), new DateTime(2019, 10, 22), new DateTime(2019, 10, 22), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_PrairieDevConRegina2019, new Location() { City = "Regina", CountryDivisionCategory = "province", CountryDivisionId = "CA-YT", CountryDivisionName = "Yukon", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ca.svg"), CountryId = "CA", CountryName = "Canada", RegionCode = "019", RegionName = "America", SubregionCode = "021", SubregionName = "Northern America" }, new DateTime(2019, 10, 22), new DateTime(2019, 10, 23), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Øredev2019, new Location() { City = "Malmö", CountryDivisionCategory = "county", CountryDivisionId = "SE-M", CountryDivisionName = "Skåne län", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/se.svg"), CountryId = "SE", CountryName = "Sweden", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2019, 11, 6), new DateTime(2019, 11, 8), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_TechBash2019, GetUSLocation("Pocono Manor", "PA", "Pennsylvania"), new DateTime(2019, 11, 12), new DateTime(2019, 11, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_BuildStuffLithuania2019, new Location() { City = "Vilnius", CountryDivisionCategory = "district municipality", CountryDivisionId = "LT-58", CountryDivisionName = "Vilnius", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/lt.svg"), CountryId = "LT", CountryName = "Lithuania", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 11, 13), new DateTime(2019, 11, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_BuildStuffKiev2019, new Location() { City = "Kiev", CountryDivisionCategory = "city", CountryDivisionId = "UA-30", CountryDivisionName = "Kyiv", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ua.svg"), CountryId = "UA", CountryName = "Ukraine", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2019, 11, 18), new DateTime(2019, 11, 19), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Live360Orlando2019, GetUSLocation("Orlando", "FL", "Florida"), new DateTime(2019, 11, 17), new DateTime(2019, 11, 22), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_UpdateConference2019, new Location() { City = "Prague", CountryDivisionCategory = "capital city", CountryDivisionId = "CZ-10", CountryDivisionName = "Hlavní město Praha", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/cz.svg"), CountryId = "CZ", CountryName = "Czech Republic", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2019, 11, 14), new DateTime(2019, 11, 16), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_AgileDevOpsEast2019, GetUSLocation("Orlando", "FL", "Florida"), new DateTime(2019, 11, 3), new DateTime(2019, 11, 8), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_AtlantaCodeCamp2019, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2019, 9, 14), new DateTime(2019, 9, 14), _ShindigTypes[ShindigType_CodeCamp], childProgressBar);
			await CreateShindigAsync(Shindig_APIDaysParis2019, new Location() { City = "Paris", CountryDivisionCategory = "metropolitan department", CountryDivisionId = "FR-75", CountryDivisionName = "Paris", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/fr.svg"), CountryId = "FR", CountryName = "France", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2019, 12, 10), new DateTime(2019, 12, 11), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevDay2019, new Location() { City = "Louvain-la-Neuve", CountryDivisionCategory = "province", CountryDivisionId = "BE-WBR", CountryDivisionName = "Brabant wallon", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/be.svg"), CountryId = "BE", CountryName = "Belgium", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2019, 11, 26), new DateTime(2019, 11, 26), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveBoston2019, GetUSLocation("Boston", "MA", "Massachusetts"), new DateTime(2019, 6, 9), new DateTime(2019, 6, 13), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_SDD2019, new Location() { City = "London", CountryDivisionCategory = "city corporation", CountryDivisionId = "GB-LND", CountryDivisionName = "City of London", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 5, 11), new DateTime(2019, 5, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_TechLadies0619, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2019, 6, 19), new DateTime(2019, 6, 19), _ShindigTypes[ShindigType_UserGroup], childProgressBar);
			await CreateShindigAsync(Shindig_SoftwareGuild0619, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2019, 6, 28), new DateTime(2019, 6, 28), _ShindigTypes[ShindigType_PrivateEvent], childProgressBar);
			await CreateShindigAsync(Shindig_TechFoundations0119, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2019, 1, 22), new DateTime(2019, 1, 22), _ShindigTypes[ShindigType_UserGroup], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteBeijing2019, new Location() { City = "Beijing", CountryDivisionCategory = "municipality", CountryDivisionId = "CN-BJ", CountryDivisionName = "Beijing Shi", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/cn.svg"), CountryId = "CN", CountryName = "China", RegionCode = "142", RegionName = "Asia", SubregionCode = "030", SubregionName = "Eastern Asia" }, new DateTime(2019, 12, 10), new DateTime(2019, 12, 11), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_LouDotNet0119, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2019, 1, 17), new DateTime(2019, 1, 17), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "The Louisville .NET Meetup group is dedicated to helping developers around Louisville to become better developers with an emphasis on the Microsoft .NET technology stack. We hold meetings on the third Thursday of each month.\nThe Louisville.NET Meetup group is also host to Code PaLOUsa, the Louisville Global Azure Bootcamp(louisville.azurebootcamp.net), and the Louisville Global DevOps Bootcamp.", GetVenue(GetUSLocation("Louisville", "KY", "Kentucky"), "Modis", "101 Bullitt Lane", new Uri("https://goo.gl/maps/bnN43qJZB192")), "Free", new Uri("https://www.meetup.com/Tech-Foundations-Louisville/events/256240462/"), -5);
			await CreateShindigAsync(Shindig_LouDotNet1019, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2019, 10, 17), new DateTime(2019, 10, 17), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "The Louisville .NET Meetup group is dedicated to helping developers around Louisville to become better developers with an emphasis on the Microsoft .NET technology stack. We hold meetings on the third Thursday of each month.\nThe Louisville.NET Meetup group is also host to Code PaLOUsa, the Louisville Global Azure Bootcamp(louisville.azurebootcamp.net), and the Louisville Global DevOps Bootcamp.", GetVenue(GetUSLocation("Louisville", "KY", "Kentucky"), "Modis", "101 Bullitt Lane", new Uri("https://goo.gl/maps/bnN43qJZB192")), "Free", new Uri("https://www.meetup.com/Louisville-DotNet/events/264034194/"), -5);

			// 2020
			await CreateShindigAsync(Shindig_Swetug2020, new Location() { City = "Stockholm", CountryDivisionCategory = "county", CountryDivisionId = "SE-AB", CountryDivisionName = "Stockholms län", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/se.svg"), CountryId = "SE", CountryName = "Sweden", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 2, 3), new DateTime(2020, 2, 4), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveLasVegas2020, GetUSLocation("Las Vegas", "NV", "Nevada"), new DateTime(2020, 3, 1), new DateTime(2020, 3, 6), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveAustin2020, GetUSLocation("Austin", "TX", "Texas"), new DateTime(2020, 3, 30), new DateTime(2020, 4, 3), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_DeveloperWeek2020, GetUSLocation("Oakland", "CA", "California"), new DateTime(2020, 2, 12), new DateTime(2020, 2, 16), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_CodeMash2020, GetUSLocation("Sandusky", "OH", "Ohio"), new DateTime(2020, 1, 6), new DateTime(2020, 1, 10), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteToronto2020, new Location() { City = "Toronto", CountryDivisionCategory = "province", CountryDivisionId = "CA-ON", CountryDivisionName = "Ontario", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ca.svg"), CountryId = "CA", CountryName = "Canada", RegionCode = "019", RegionName = "Americas", SubregionCode = "021", SubregionName = "Northern America" }, new DateTime(2020, 1, 8), new DateTime(2020, 1, 9), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteLondon2020, new Location() { City = "London", CountryDivisionCategory = "city corporation", CountryDivisionId = "GB-LND", CountryDivisionName = "City of London", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 1, 16), new DateTime(2020, 1, 17), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteMilan2020, new Location() { City = "Milan", CountryDivisionCategory = "metropolitan city", CountryDivisionId = "IT-MI", CountryDivisionName = "Milano", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/it.svg"), CountryId = "IT", CountryName = "Italy", RegionCode = "150", RegionName = "Europe", SubregionCode = "039", SubregionName = "Southern Europe" }, new DateTime(2020, 1, 27), new DateTime(2020, 1, 28), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteJohannesburg2020, new Location() { City = "Johannesburg", CountryDivisionCategory = "province", CountryDivisionId = "ZA-GT", CountryDivisionName = "Gauteng", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/za.svg"), CountryId = "ZA", CountryName = "South Africa", RegionCode = "002", RegionName = "Africa", SubregionCode = "018", SubregionName = "Southern Africa" }, new DateTime(2020, 1, 30), new DateTime(2020, 1, 31), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteWashingtonDC2020, GetUSLocation("Washington, DC", "DC", "District of Columbia", "district"), new DateTime(2020, 2, 6), new DateTime(2020, 2, 7), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteDubai2020, new Location() { City = "Dubai", CountryDivisionCategory = "emirate", CountryDivisionId = "AE-DU", CountryDivisionName = "Dubai", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ae.svg"), CountryId = "AE", CountryName = "United Arab Emirates", RegionCode = "142", RegionName = "Asia", SubregionCode = "145", SubregionName = "Western Asia" }, new DateTime(2020, 2, 10), new DateTime(2020, 2, 11), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteSydney2020, new Location() { City = "Sydney", CountryDivisionCategory = "state", CountryDivisionId = "AU-NSW", CountryDivisionName = "New South Wales", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/au.svg"), CountryId = "AU", CountryName = "Australia", RegionCode = "009", RegionName = "Oceania", SubregionCode = "036", SubregionName = "Australia and New Zealand" }, new DateTime(2020, 2, 13), new DateTime(2020, 2, 14), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteTaipei2020, new Location() { City = "Taipei", CountryDivisionCategory = "special municipality", CountryDivisionId = "TW-TPE", CountryDivisionName = "Taipei", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/tw.svg"), CountryId = "TW", CountryName = "Taiwan", RegionCode = "142", RegionName = "Asia", SubregionCode = "030", SubregionName = "Eastern Asia" }, new DateTime(2020, 2, 17), new DateTime(2020, 2, 18), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteSingapore2020, new Location() { City = "Singapore", CountryDivisionCategory = "district", CountryDivisionId = "SG-01", CountryDivisionName = "Central Singapore", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/sg.svg"), CountryId = "SG", CountryName = "Singapore", RegionCode = "142", RegionName = "Asia", SubregionCode = "035", SubregionName = "South-Eastern Asia" }, new DateTime(2020, 2, 20), new DateTime(2020, 2, 21), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgnitePrague2020, new Location() { City = "Prague", CountryDivisionCategory = "capital city", CountryDivisionId = "CZ-10", CountryDivisionName = "Hlavní město Praha", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/cz.svg"), CountryId = "CZ", CountryName = "Czech Republic", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2020, 2, 27), new DateTime(2020, 2, 28), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteCopenhagen2020, new Location() { City = "Copenhagen", CountryDivisionCategory = "region", CountryDivisionId = "DK-84", CountryDivisionName = "Hovedstaden", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/dk.svg"), CountryId = "DK", CountryName = "Denmark", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 2, 27), new DateTime(2020, 2, 28), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteZürich2020, new Location() { City = "Zürich", CountryDivisionCategory = "canton", CountryDivisionId = "CH-ZH", CountryDivisionName = "Zürich", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ch.svg"), CountryId = "CH", CountryName = "Switzerland", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 3, 4), new DateTime(2020, 3, 5), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteAmsterdam2020, new Location() { City = "Amsterdam", CountryDivisionCategory = "province", CountryDivisionId = "NL-NH", CountryDivisionName = "Noord-Holland", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/nl.svg"), CountryId = "NL", CountryName = "Netherlands", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 3, 11), new DateTime(2020, 3, 12), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteHongKong2020, new Location() { City = "Hong Kong", CountryDivisionCategory = "special administrative region", CountryDivisionId = "CN-HK", CountryDivisionName = "Hong Kong SAR", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/cn.svg"), CountryId = "CN", CountryName = "China", RegionCode = "142", RegionName = "Asia", SubregionCode = "030", SubregionName = "Eastern Asia" }, new DateTime(2020, 3, 25), new DateTime(2020, 3, 26), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteMadrid2020, new Location() { City = "Madrid", CountryDivisionCategory = "province", CountryDivisionId = "ES-M", CountryDivisionName = "Madrid", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/es.svg"), CountryId = "ES", CountryName = "Spain", RegionCode = "150", RegionName = "Europe", SubregionCode = "039", SubregionName = "Southern Europe" }, new DateTime(2020, 3, 25), new DateTime(2020, 3, 26), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteMumbai2020, new Location() { City = "Mumbai", CountryDivisionCategory = "state", CountryDivisionId = "IN-MH", CountryDivisionName = "Maharashtra", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/in.svg"), CountryId = "IN", CountryName = "India", RegionCode = "142", RegionName = "Asia", SubregionCode = "034", SubregionName = "Southern Asia" }, new DateTime(2020, 4, 2), new DateTime(2020, 4, 3), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteBangalore2020, new Location() { City = "Bangalore", CountryDivisionCategory = "state", CountryDivisionId = "IN-KA", CountryDivisionName = "Karnataka", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/in.svg"), CountryId = "IN", CountryName = "India", RegionCode = "142", RegionName = "Asia", SubregionCode = "034", SubregionName = "Southern Asia" }, new DateTime(2020, 4, 8), new DateTime(2020, 4, 9), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteChicago2020, GetUSLocation("Chicago", "IL", "Illinois"), new DateTime(2020, 4, 15), new DateTime(2020, 4, 16), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteTelAviv2020, new Location() { City = "Tel Aviv", CountryDivisionCategory = "district", CountryDivisionId = "IL-TA", CountryDivisionName = "Tel Aviv", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/il.svg"), CountryId = "IL", CountryName = "Israel", RegionCode = "142", RegionName = "Asia", SubregionCode = "145", SubregionName = "Western Asia" }, new DateTime(2020, 4, 22), new DateTime(2020, 4, 23), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteBerlin2020, new Location() { City = "Berlin", CountryDivisionCategory = "land", CountryDivisionId = "DE-BE", CountryDivisionName = "Berlin", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/de.svg"), CountryId = "DE", CountryName = "Germany", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 4, 29), new DateTime(2020, 4, 30), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IgniteStockholm2020, new Location() { City = "Stockholm", CountryDivisionCategory = "county", CountryDivisionId = "SE-AB", CountryDivisionName = "Stockholms län", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/se.svg"), CountryId = "SE", CountryName = "Sweden", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 5, 5), new DateTime(2020, 5, 6), _ShindigTypes[ShindigType_FirstTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevConf2020, new Location() { City = "Johannesburg", CountryDivisionCategory = "province", CountryDivisionId = "ZA-GT", CountryDivisionName = "Gauteng", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/za.svg"), CountryId = "ZA", CountryName = "South Africa", RegionCode = "002", RegionName = "Africa", SubregionCode = "018", SubregionName = "Southern Africa" }, new DateTime(2020, 3, 31), new DateTime(2020, 4, 2), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Connectaha2020, GetUSLocation("Omaha", "NE", "Nebraska"), new DateTime(2020, 3, 27), new DateTime(2020, 3, 27), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Refactr2020, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2020, 4, 22), new DateTime(2020, 4, 24), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCLondon2020, new Location() { City = "London", CountryDivisionCategory = "city corporation", CountryDivisionId = "GB-LND", CountryDivisionName = "City of London", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 1, 27), new DateTime(2020, 1, 31), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_SDD2020, new Location() { City = "London", CountryDivisionCategory = "city corporation", CountryDivisionId = "GB-LND", CountryDivisionName = "City of London", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 5, 11), new DateTime(2020, 5, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CodeStock2020, GetUSLocation("Knoxville", "TN", "Tennessee"), new DateTime(2020, 4, 17), new DateTime(2020, 4, 18), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_MicrosoftTechDays, new Location() { City = "Helsinki", CountryDivisionCategory = "region", CountryDivisionId = "FI-18", CountryDivisionName = "Uusimaa", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/fin.svg"), CountryId = "FI", CountryName = "Finland", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 3, 5), new DateTime(2020, 3, 6), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CaribbeanDeveloeprsConference, new Location() { City = "Punta Cana", CountryDivisionCategory = "province", CountryDivisionId = "DO-11", CountryDivisionName = "La Altagracia", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/do.svg"), CountryId = "DO", CountryName = "Dominican Republic", RegionCode = "019", RegionName = "Americas", SubregionCode = "029", SubregionName = "Caribbean" }, new DateTime(2020, 11, 11), new DateTime(2020, 11, 14), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_MicroCPH2020, new Location() { City = "Copenhagen", CountryDivisionCategory = "region", CountryDivisionId = "DK-84", CountryDivisionName = "Hovedstaden", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/dk.svg"), CountryId = "DK", CountryName = "Denmark", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 5, 11), new DateTime(2020, 5, 12), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevoxxUK2020, new Location() { City = "London", CountryDivisionCategory = "city corporation", CountryDivisionId = "GB-LND", CountryDivisionName = "City of London", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 5, 13), new DateTime(2020, 5, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_t3chfest2020, new Location() { City = "Madrid", CountryDivisionCategory = "province", CountryDivisionId = "ES-M", CountryDivisionName = "Madrid", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/es.svg"), CountryId = "ES", CountryName = "Spain", RegionCode = "150", RegionName = "Europe", SubregionCode = "039", SubregionName = "Southern Europe" }, new DateTime(2020, 3, 12), new DateTime(2020, 3, 14), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CircleCityCom2020, GetUSLocation("Indianapolis", "IN", "Indiana"), new DateTime(2020, 6, 12), new DateTime(2020, 6, 14), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_OrlandoCodeCamp2020, GetUSLocation("Samford", "FL", "Florida"), new DateTime(2020, 3, 28), new DateTime(2020, 3, 28), _ShindigTypes[ShindigType_CodeCamp], childProgressBar);
			await CreateShindigAsync(Shindig_SouthFloridaCodeCamp2020, GetUSLocation("Davie", "FL", "Florida"), new DateTime(2020, 2, 29), new DateTime(2020, 2, 29), _ShindigTypes[ShindigType_CodeCamp], childProgressBar);
			await CreateShindigAsync(Shindig_OReillySofttwareArchitetureConference2020, GetUSLocation("Santa Clara", "CA", "California"), new DateTime(2020, 6, 15), new DateTime(2020, 6, 18), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_IndyCode2020, GetUSLocation("Indianapolis", "IN", "Indiana"), new DateTime(2020, 4, 29), new DateTime(2020, 5, 1), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_ServerlessDaysCardiff2020, new Location() { City = "Cardiff", CountryDivisionCategory = "unitary authority", CountryDivisionId = "GB-CRF", CountryDivisionName = "Cardiff", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 2, 13), new DateTime(2020, 2, 13), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCPorto2020, new Location() { City = "Porto", CountryDivisionCategory = "district", CountryDivisionId = "PT-13", CountryDivisionName = "Porto", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/pt.svg"), CountryId = "PT", CountryName = "Portugal", RegionCode = "150", RegionName = "Europe", SubregionCode = "039", SubregionName = "Southern Europe" }, new DateTime(2020, 4, 21), new DateTime(2020, 4, 24), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_Agile2020, GetUSLocation("Orlando", "FL", "Florida"), new DateTime(2020, 7, 20), new DateTime(2020, 7, 24), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_KCDC2020, GetUSLocation("Kansas City", "MO", "Missouri"), new DateTime(2020, 6, 29), new DateTime(2020, 7, 1), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_BostonCodeCamp2020, GetUSLocation("Boston", "MA", "Massachusetts"), new DateTime(2020, 3, 21), new DateTime(2020, 3, 21), _ShindigTypes[ShindigType_CodeCamp], childProgressBar);
			await CreateShindigAsync(Shindig_DotNetDeveloperConference2020, new Location() { City = "Cologne", CountryDivisionCategory = "land", CountryDivisionId = "DE-NW", CountryDivisionName = " Nordrhein-Westfalen", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/de.svg"), CountryId = "DE", CountryName = "Germany", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 11, 23), new DateTime(2020, 11, 27), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_EuropeanCloudConference2020, new Location() { City = "Nice", CountryDivisionCategory = "metropolitan department", CountryDivisionId = "FR", CountryDivisionName = "Alpes-Maritimes", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/fr.svg"), CountryId = "FR", CountryName = "France", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 10, 26), new DateTime(2020, 10, 28), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCSydney2020, new Location() { City = "Sydney", CountryDivisionCategory = "state", CountryDivisionId = "AU-NSW", CountryDivisionName = "New South Wales", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/au.svg"), CountryId = "AU", CountryName = "Australia", RegionCode = "009", RegionName = "Oceania", SubregionCode = "036", SubregionName = "Australia and New Zealand" }, new DateTime(2020, 10, 12), new DateTime(2020, 10, 16), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_ThatConference2020, GetUSLocation("Wisconsin Dells", "WI", "Wisconsin"), new DateTime(2020, 8, 3), new DateTime(2020, 8, 6), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_EuropeanSharePointOffice365AzureConference2020, new Location() { City = "Amsterdam", CountryDivisionCategory = "province", CountryDivisionId = "NL-NH", CountryDivisionName = "Noord-Holland", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/nl.svg"), CountryId = "NL", CountryName = "Netherlands", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 11, 9), new DateTime(2020, 11, 12), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_Øredev2020, new Location() { City = "Malmö", CountryDivisionCategory = "county", CountryDivisionId = "SE-M", CountryDivisionName = "Skåne län", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/se.svg"), CountryId = "SE", CountryName = "Sweden", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 11, 3), new DateTime(2020, 11, 6), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_AzureFest2020, new Location() { City = "Veenendaal", CountryDivisionCategory = "province", CountryDivisionId = "NL-UT", CountryDivisionName = "Utrecht", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/nl.svg"), CountryId = "NL", CountryName = "Netherlands", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2020, 5, 28), new DateTime(2020, 5, 28), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_CincyDeliver2020, GetUSLocation("Cincinnati", "OH", "Ohio"), new DateTime(2020, 7, 31), new DateTime(2020, 7, 31), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_BeerCityCode2020, GetUSLocation("Grand Rapids", "MI", "Michigan"), new DateTime(2020, 7, 24), new DateTime(2020, 7, 25), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_MusicCityTech2020, GetUSLocation("Nashville", "TN", "Tennessee"), new DateTime(2020, 8, 27), new DateTime(2020, 8, 29), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCManchester2020, new Location() { City = "Manchester", CountryDivisionCategory = "metropolitan district", CountryDivisionId = "GB-MAN", CountryDivisionName = "Manchester", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 10, 1), new DateTime(2020, 10, 2), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_Momentum2020, GetUSLocation("Cincinnati", "OH", "Ohio"), new DateTime(2020, 10, 16), new DateTime(2020, 10, 16), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_STP2020, GetUSLocation("San Diego", "CA", "California"), new DateTime(2020, 3, 30), new DateTime(2020, 4, 2), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_STAREAST2020, GetUSLocation("Orlando", "FL", "Florida"), new DateTime(2020, 5, 3), new DateTime(2020, 5, 8), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_AgileDevOpsWest2020, GetUSLocation("Las Vegas", "NV", "Nevada"), new DateTime(2020, 6, 7), new DateTime(2020, 6, 12), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_GraniteStateCC2020, GetUSLocation("Manchester", "NH", "New Hampshire"), new DateTime(2020, 11, 14), new DateTime(2020, 11, 14), _ShindigTypes[ShindigType_CodeCamp], childProgressBar);
			await CreateShindigAsync(Shindig_TechBash2020, GetUSLocation("Pocono Manor", "PA", "Pennsylvania"), new DateTime(2020, 10, 13), new DateTime(2020, 10, 16), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DallasAustinAzure1220, GetUSLocation("Dallas", "TX", "Texas"), new DateTime(2020, 12, 15), new DateTime(2020, 12, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar);
			await CreateShindigAsync(Shindig_NDCOslo2020, new Location() { City = "Oslo", CountryDivisionId = "NO-03", CountryDivisionCategory = "county", CountryDivisionName = "Oslo", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/no.svg"), CountryId = "NO", CountryName = "Norway", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2020, 6, 8), new DateTime(2020, 6, 12), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_TDevConf2020, GetUSLocation("Memphis", "TN", "Tennessee"), new DateTime(2020, 10, 3), new DateTime(2020, 10, 3), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
		}

		private async Task CreateShindigAsync(string name, Location location, DateTime startDate, DateTime endDate, ShindigType shindigType, ChildProgressBar progressBar)
		{
			_Shindigs.Add(
				name,
				await _ShindigManager.AddShindigAsync(new Shindig()
				{
					Name = name,
					Location = location,
					StartDate = startDate,
					EndDate = endDate,
					ShindigType = shindigType
				}));
			progressBar.Tick();
		}

		private async Task CreateShindigAsync(string name, Location location, DateTime startDate, DateTime endDate, ShindigType shindigType, ChildProgressBar progressBar, string description, Venue venue, string cost, Uri url, decimal utcOffset, bool? isVirtual = null, Uri virtualLocation = null, bool? displayVirtualLocation = null)
		{
			_Shindigs.Add(
				name,
				await _ShindigManager.AddShindigAsync(new Shindig()
				{
					Name = name,
					Location = location,
					StartDate = startDate,
					EndDate = endDate,
					ShindigType = shindigType,
					Description = description,
					URL = url,
					Venue = venue,
					Cost = cost,
					IsVirtual = isVirtual,
					VirtualLocation = virtualLocation,
					DisplayVirtualLocation = displayVirtualLocation,
					UTCOffset = utcOffset
				}));
			progressBar.Tick();
		}

		#endregion

		#region Create Presentations

		private async Task CreatePresentations(ProgressBar progressBar)
		{
			using var childProgressBar = progressBar.Spawn(1, "Creating the Presentations");
			await WhichMicrosoftFrameworkAmISupposedToUse(childProgressBar);
			await BuildingGreatLibrariesWithDotNetStandard(childProgressBar);
			await BlackBoxMagic(childProgressBar);
			await FromZeroToServerless(childProgressBar);
			await HowToBeALeader(childProgressBar);
			await SecretsOfConflictResolution(childProgressBar);
			await SoftwareCraftsmanshipForNonDevelopers(childProgressBar);
			await SoftwareCraftsmanshipForNewDevelopers(childProgressBar);
			await ServerlessInAction(childProgressBar);
			await GettingStartedWithAzureSQLDatabase(childProgressBar);
			await BuildingAnUltraScalableAPIUsingAzureFunctions(childProgressBar);
			await TimeTravelingData(childProgressBar);
			await GettingStartedWithAzureDevOps(childProgressBar);
			await GraphingYourWayThroughTheCosmos(childProgressBar);
			await AzureDurableFunctionsForServerlessNetOrchestration(childProgressBar);
			await AzureServicesEveryDeveloperNeedsToKnow(childProgressBar);
			await DevelopACompleteServerlessSolutionInADay(childProgressBar);
			await GettingGremlinsToImporveYourData(childProgressBar);
			await EventDrivenArchitectureInTheCloud(childProgressBar);
			await BuildingHyperScaledEventProcessingSolutionsInAzure(childProgressBar);
			await TheHitchhikersGuideToTheCosmos(childProgressBar);
		}

		private async Task WhichMicrosoftFrameworkAmISupposedToUse(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Which Microsoft Framework Am I Supposed to Use?",
					Abstract = "For 15 years, the biggest decision was whether it was time to upgrade to the latest .NET framework or not.  Now there is the .NET Framework, .NET Core, and .NET Standard.  So many options and it not completely obvious which one (or ones) you should be using.  During this session, you see a down to earth description of what each of these are and get an idea of when you should be one or more of these and what version of them based upon what you are building or maintaining.",
					ShortAbstract = "With .NET Framework, .NET Core, and .NET Standard, which Microsoft .NET framework are you supposed to use?  Well it depends.  During this session we'll talk about the different reasons to use one framework over the other or when you should use a combination of frameworks.",
					IsRetired = true,
					LearningObjectives = new List<string>()
					{
						"Understand the history of Microsoft development strategies and how this brought on the development paradigms available now",
						"Understand the differences between .NET Framework, .NET Core, and .NET Standard",
						"Understand when to use one Microsoft .NET framework over another"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_DotNet],
						_Tags[Tag_DotNetFramework],
						_Tags[Tag_DotNetCore],
						_Tags[Tag_DotNetStandard]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			// TODO: Need to handle time zones
			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_KCDC2018], new DateTime(2018, 7, 12, 8, 45, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.PresentationId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Which%20Microsoft%20Framework%20Am%20I%20Supposed%20to%20Use%20-%20KCDC.pdf"));

			shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_LouDotNet0218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2018, 2, 15, 19, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.PresentationId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Which%20Microsoft%20Framework%20Am%20I%20Supposed%20to%20Use%20-%20Louisville%20NET%20Meetup.pdf"));

			shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_Evansville0218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2018, 2, 15, 11, 30, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.PresentationId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Which%20Microsoft%20Framework%20Am%20I%20Supposed%20to%20Use%20-%20Evansville%20Technology%20Group.pdf"));

			progressBar.Tick();

		}

		private async Task BuildingGreatLibrariesWithDotNetStandard(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Which Microsoft Framework Am I Supposed to Use?",
					Abstract = "For almost two decades, the biggest decision was whether it was time to upgrade to the latest .NET framework or not. Not there .NET Framework, .NET Core, and .NET Standard. So many options and it is not completely obviously which one (or ones) you should be using. During this session we will review the different frameworks/standards and talk about where you should be using the different frameworks/standards. Then we will focus on how you can easily support multiple platforms with .NET Standard and no compromises, thanks to multi-targeting. We will also over the other aspects of building .NET Standard libraries such as versioning, strong naming, and binding redirects.",
					ShortAbstract = "Start by learning about the different Microsoft frameworks and where you should use each and then focus on how you can easily support multiple platforms with .NET Standard and have no compromises.",
					IsRetired = true,
					LearningObjectives = new List<string>()
		{
						"Understanding the differences between .NET Framework, .NET Core, and .NET Standard",
						"How to use .NET Standard effectively to build cross-platform libraries",
						"Learn key library building concepts such as versioning, strong naming, and binding redirects"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Architecture],
						_Tags[Tag_DotNet],
						_Tags[Tag_DotNetFramework],
						_Tags[Tag_DotNetCore],
						_Tags[Tag_DotNetStandard]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCMinnesota2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveSanDiego2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveChicago2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Live360Orlando2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Live360Orlando2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThunderPlains2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThunderPlains2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_LittleRockTechFest2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_LittleRockTechFest2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevConRegina2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevConRegina2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_PrairieCode2019], new DateTime(2019, 9, 13, 13, 15, 0), "Iowa D");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Building%20Great%20Libraries%20with%20.NET%20Standard%20-%20Prairie.Code().pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_MusicCityTech2019], new DateTime(2019, 9, 7, 9, 0, 0), "178");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Building%20Great%20Libraries%20with%20.NET%20Standard%20-%20Nebraska.Code.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_NebraskaCode2019], new DateTime(2019, 8, 16, 15, 15, 0), "Osborne");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Building%20Great%20Libraries%20with%20.NET%20Standard%20-%20Nebraska.Code.pdf"));

			progressBar.Tick();

		}

		private async Task BlackBoxMagic(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Black Box Magic",
					Abstract = "Our customers and stakeholders often find what we do to be this black box that just magically spits out software to (hopefully) meet their needs.  While this mysticism can be very beneficial, there are a lot of times where it causes negative feelings from them about what we do.  During this session you will learn ways to properly manage your customers and stakeholders and keep them in the loop, while not losing them in the details.",
					IsRetired = true,
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session60]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			progressBar.Tick();

		}

		private async Task FromZeroToServerless(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "From Zero to Serverless",
					Abstract = "So many times, our customers need a simple routine that can be executed on a routine basis but the solution doesn’t need to be an elaborate solution without going the trouble of setting servers and other infrastructure.  Serverless computer is the abstraction of servers, infrastructure, and operating systems and make getting solutions to your customer’s needs much quicker and cheaper.  During this session we will look at how Azure Functions will enable you to run code on-demand without having to explicitly provision or manage infrastructure.",
					ShortAbstract = "Have you ever just needed to get a simple process that needs to be executed on a routine basis?  Well serverless computing will help you do that quickly and without the mess and fuss of dealing with infrastructure.",
					IsRetired = true,
					LearningObjectives = new List<string>()
		{
						"Understanding exactly what serverless is and why it’s so important",
						"Understanding how to start building serverless functions using Azure Functions",
						"Understand best practices while moving to a serverless architecture"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Architecture],
						_Tags[Tag_DotNet],
						_Tags[Tag_DotNetFramework],
						_Tags[Tag_DotNetCore],
						_Tags[Tag_DotNetStandard]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session60]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCMinnesota2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveSanDiego2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveChicago2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: true));
			submissions.Add(Shindig_ThatConfernece2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConfernece2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCTechTown2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCTechTown2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_StirTrek2018], new DateTime(2018, 5, 4, 13, 00, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/From%20Zero%20to%20Serverless%20-%20StirTrek%20(Connectivity).pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CoderCruise2018], new DateTime(2018, 8, 31, 10, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/From%20Zero%20to%20Serverless%20-%20CoderCruise.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DogFoodCon2018], new DateTime(2018, 10, 4, 15, 20, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/From%20Zero%20to%20Serverless%20-%20DogFoodCon%20(No%20Connectivity).pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeMash2019], new DateTime(2019, 1, 11, 14, 45, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/From%20Zero%20to%20Serverless%20-%20CodeMash.pdf"));

			progressBar.Tick();

		}

		private async Task HowToBeALeader(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "How to be a Leader",
					Abstract = "What is leadership? That term gets thrown around a lot, but what is it really? What do leader do that makes them leaders? Leadership can be learned and that is good as leaders are in high demand and in short supply. To be an effective leader, you must first understand what leadership is and what traits a leader exhibits.\nDuring this session, we will review leadership traits and principles and how you can implement them within your teams.By demonstrating these in your daily activities you will earn the respect, confidence,	and loyal cooperation of those on your team.",
					ShortAbstract = "In order to become an effective leader, you must first understand what leadership is and what traits a leader exhibits. Come and learn how to demonstrate key leadership traits and principles so you can earn the respect, confidence, and loyal cooperation of those on your team.",
					LearningObjectives = new List<string>()
		{
						"Understand the 13 Marine Corps leadership traits and how to apply them to your software development teams",
						"Understand the basics of leadership",
						"Understand the difference between a boss or manager and a leader"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Leadership],
						_Tags[Tag_SoftSkills]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SelfConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SelfConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_STP2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_STP2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeStock2018], new DateTime(2018, 4, 20));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/How%20to%20be%20a%20Leader%20-%20CodeStock.pdf"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DevSpace2018], new DateTime(2018, 10, 13, 16, 0, 0));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_NebraskaCode2019], new DateTime(2019, 8, 15, 10, 00, 0), "Ardis");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/How%20to%20be%20a%20Leader%20-%20Nebraska.Code().pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_MusicCityTech2019], new DateTime(2019, 9, 6, 13, 50, 0), "186");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/How%20to%20be%20a%20Leader%20-%20Music%20City%20Tech.pdf"));

			progressBar.Tick();

		}

		private async Task SecretsOfConflictResolution(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Secrets of COnflict Resolution",
					Abstract = "One of the most challenging aspect of being a leader is dealing with conflict among your team.  It’s vital to productivity to get the team running like a well-oiled machine, even in the face of adversity.  Improving your relationships with your coworkers, clients, and managers and find your way through conflict back to cooperation.  This session will provide you with the secrets of effective conflict resolution and how to prevent conflicts from ever starting.",
					ShortAbstract = "Conflict within your team is inevitable.  Come and learn how learn ways to deal with this conflict so it doesn't tear apart your team.",
					LearningObjectives = new List<string>()
		{
						"Understand the five conflict resolution methods and why you would use one over another.",
						"Learn about the Karpman Dram Triangle is and how we can use it to understand the motivations of those involved within a conflict.",
						"Learn the 10 useful tips for handling conflict that you can employ right away."
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_ConflictResolution],
						_Tags[Tag_ProjectManagement],
						_Tags[Tag_SoftSkills]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDayOfAgile2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDayOfAgile2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SelfConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SelfConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_STAREAST2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_STAREAST2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_STP2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_STP2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeStock2018], new DateTime(2018, 4, 20));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Secrets%20of%20Conflict%20Resolution%20-%20CodeStock%202018.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_MusicCityTech2018], new DateTime(2018, 6, 1));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Secrets%20of%20Conflict%20Resolution%20-%20Music%20City%202018.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_BeerCityCode2019], new DateTime(2019, 6, 1, 14, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Secrets%20of%20Conflict%20Resolution%20-%20Beer%20City%20Code.pdf"));

			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_TechLadies0619], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 6, 6, 18, 00, 0));

			progressBar.Tick();

		}

		private async Task SoftwareCraftsmanshipForNonDevelopers(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Software Craftsmanship for Non-Developers",
					Abstract = "Have you heard the developers on your team throw around terms like code smell, DRY, or SOLID you just look at them with a blank stare? When your senior developer tells you that they need to spend a sprint taking care of technical debt do you just think they are wanting some time to goof off? Then this session is for you. We will discuss what exactly software craftsmanship is and what is not and why it can be important on your team.",
					ShortAbstract = "What exactly are these software craftsmanship terms like code smell, DRY, and SOLID and why are my developers frustrated that I do not understand these terms? During this session we’ll discuss what exactly software craftsmanship is and why it’s important for your team.",
					LearningObjectives = new List<string>()
		{
						"Understand what Software Craftsmanship is and why it is important for your software development team",
						"Understand what technical debt is and how to use it to your benefit and prevent it from causing issues to the future of your projects",
						"Understand what the SOLID and principles are and how they help developers build high-quality applications",
						"Understand the software craftsmanship principles and how they help developers build high-quality applications",
						"Understand what code smells are, how to detect them, and how to mitigate them"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Process],
						_Tags[Tag_SoftwareCraftsmanship]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDayOfAgile2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDayOfAgile2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SelfConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SelfConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AgileDevOpsEast2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AgileDevOpsEast2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_STAREAST2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_STAREAST2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AgileDevOpsWest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AgileDevOpsWest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_STP2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_STP2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Agile2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Agile2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_MusicCityTech2019], new DateTime(2019, 9, 7), "186");

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CincyDayOfAgile2018], new DateTime(2018, 7, 27));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Software%20Craftsmanship%20for%20Non-Developers.pdf"));

			progressBar.Tick();

		}

		private async Task SoftwareCraftsmanshipForNewDevelopers(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Software Craftsmanship for New Developers",
					Abstract = "As a new developer, knowing language syntax is important; but just as important is understanding what software craftsmanship is.  In this session Chad will talk about what the software craftsmanship movement is all about and why it is important.  Chad will also explain important craftsmanship terms like code smells, DRY, and SOLID.  Even if you have been developing for a while, this session will be useful to brush up on how not to be a developer but how to be a professional developer.",
					ShortAbstract = "As a new developer, knowing language syntax is important; but just as important is understanding the concepts of software craftsmanship. In this session you will learn about terms like code smells, DRY, and SOLID. Even if for experienced developers, this will be a good refresher on the essentials.",
					LearningObjectives = new List<string>()
		{
						"Understand what Software Craftsmanship is and why it is important for your software development team",
						"Understand what technical debt is and how to use it to your benefit and prevent it from causing issues to the future of your projects",
						"Understand what the SOLID and principles are and how they help developers build high-quality applications",
						"Understand the software craftsmanship principles and how they help developers build high-quality applications",
						"Understand what code smells are, how to detect them, and how to mitigate them"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Process],
						_Tags[Tag_SoftwareCraftsmanship]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_TechFoundations0119], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 6, 22, 18, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Software%20Craftsmanship%20for%20New%20Developers%20(Tech%20Foundations%20Louisville).pdf"));

			shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_SoftwareGuild0619], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 6, 28, 12, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Software%20Craftsmanship%20for%20New%20Developers%20(Software%20Guild).pdf"));

			progressBar.Tick();

		}

		private async Task ServerlessInAction(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Serverless in Action",
					Abstract = "Have you ever needed the ability to develop something simple for a business request but did not want to go through the trouble of creating a full application?  Well that is where Azure Functions comes into play.  Azure Functions provides a serverless computer service that enables you to run code on-demand without having to explicitly provision or manage infrastructure.  Literally within a couple of minutes you can develop a function that can perform critical business need using the development languages you already use.\nDuring this session, we will walk through some real - world examples using C# and JavaScript that will allow you to back and start using this great service right away.",
					ShortAbstract = "Have you ever needed the ability to develop something simple for a business request but did not want to go through the trouble of creating a full application?  Serverless computing will help you do that quickly and without the mess and fuss of dealing with infrastructure.",
					WhyAttend = "Serverless is one of the hottest buzzwords and maybe you feel left out because you have not tried it out yet.  In this session you will learn about the overall concepts about serverless and more importantly Function as a Service (FaaS).  By the end of this session, you will not only understand the serverless concepts but how Microsoft has implemented FaaS with Azure Functions.",
					LearningObjectives = new List<string>()
		{
						"Understanding exactly what serverless is and why it’s so important",
						"Understanding how to start building serverless functions using Azure Functions",
						"Understand best practices while moving to a serverless architecture"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Architecture],
						_Tags[Tag_Cloud],
						_Tags[Tag_Serverless]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Live360Orlando2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Live360Orlando2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThunderPlains2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThunderPlains2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevConRegina2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevConRegina2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveLasVegas2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveAustin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DeveloperWeek2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DeveloperWeek2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicrosoftTechDays, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CaribbeanDeveloeprsConference], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeStock2019], new DateTime(2019, 4, 13, 13, 50, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Serverless%20in%20Action%20-%20CodeStock.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_ScenicCitySummit2019], new DateTime(2019, 10, 4, 13, 15, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/From%20Zero%20to%20Serverless%20-%20Scenic%20City%20Summit.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DevSpace2019], new DateTime(2019, 10, 11, 16, 0, 0), "Ballroom 4");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Serverless%20in%20Action%20-%20DevSpace.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_TechBash2019], new DateTime(2019, 11, 14, 16, 0, 0), "Aloeswood");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Serverless%20in%20Action%20-%20TechBash.pdf"));

			progressBar.Tick();

		}

		private async Task GettingStartedWithAzureSQLDatabase(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Getting Started with Azure SQL Database",
					Abstract = "Azure SQL Database is a cloud-based relationship database service based on Microsoft’s SQL Server engine.  It delivers predictable performance, dynamic scalability, and robust data protection so you can focus on front-end application development rather than managing machines and infrastructure.  SQL Database supports existing SQL Server tools, libraries, and APIs, making it simple to migrate existing database solutions to the cloud and leverage them using skills you already have.  And it features the largest compliance portfolio in the industry, including HIPAA, FERPA, and even Singapore MTCS Level 3.\nDuring this session, you will learn the features and benefits of Azure SQL Database.  There will also be demonstrations of how to create an Azure SQL Database and populate data into it.  We will create an Azure API App to expose the database securely to clients using REST calls, create a Universal Windows Platform app to access the database through the API App, and use security features of SQL database to limit the information returned.",
					ShortAbstract = "Are you still hosting your databases on your own SQL Server? Would you like to consider putting those up in the cloud? Then come and learn what exactly Azure SQL can do for you and how to go about moving your databases to the cloud.",
					LearningObjectives = new List<string>()
		{
						"Understand what Azure SQL is and how you can use it to power you applications"
		},
					Tags = new List<TagItem>()
		{
						_Tags[Tag_Azure],
						_Tags[Tag_AzureSQL],
						_Tags[Tag_Cloud],
						_Tags[Tag_Data],
						_Tags[Tag_Database],
						_Tags[Tag_SQLServer]
		},
					SessionTypes = new List<SessionType>()
		{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
		}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDayOfAgile2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDayOfAgile2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_PittsburghTechFest2018], new DateTime(2018, 6, 2, 9, 30, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Started%20with%20Azure%20SQL%20Database%20-%20TechFest.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeStock2019], new DateTime(2019, 4, 12, 10, 30, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Started%20with%20Azure%20SQL%20Database%20-%20CodeStock.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DotNetSouth2019], new DateTime(2019, 5, 14, 11, 20, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Started%20with%20Azure%20SQL%20Database%20-%20DotNetSouth.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_KCDC2020], new DateTime(2019, 7, 18, 10, 0, 0), "2204");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Started%20with%20Azure%20SQL%20Database%20-%20KCDC.pdf"));

			progressBar.Tick();

		}

		private async Task BuildingAnUltraScalableAPIUsingAzureFunctions(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Building an Ultra-Scalable API Using Azure Functions",
					Abstract = "The backend to most extensible applications are the APIs that provide the crux of the workload. We’ll identify what serverless computing is, why to consider having a serverless API backend, provide an example of how to develop a serverless architecture, and go over potential benefits and pitfalls.",
					ShortAbstract = "The backend to most extensible applications are the APIs that provide the crux of the workload. We’ll identify what serverless computing is, why to consider having a serverless API backend, provide an example of how to develop a serverless architecture, and go over potential benefits and pitfalls.",
					WhyAttend = "Building an API is easy.  Building an API that can scale to different regions of the world and handle large amounts of concurrent users take a lot of planning and work – but can be easy once you know how to do it.  Come hear from real-world experience on how to build an API that is located all over the world and can automatically scale to handle any number of users.",
					LearningObjectives = new List<string>()
					{
						"Understand how to architect serverless solutions that offers ultra-scalability",
						"Understand best practices for building solutions with the best scalability options",
						"See real-world examples how to implement ultra-scalable serverless solutions"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_API],
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_CaseStudy],
						_Tags[Tag_Cloud],
						_Tags[Tag_Serverless]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					},
					Outline = new List<string>()
					{
						"What is serverless computing",
						"Quick introduction into Function as a Service",
						"What are Azure Functions",
						"Why consider having a serverless API",
						"Real-world implementations",
						"Demo implementation"
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PittsburghTechFest2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDayOfAgile2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDayOfAgile2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CoderCruise2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCMinnesota2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveSanDiego2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveChicago2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBeijing2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBeijing2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteToronto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteToronto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMilan2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMilan2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteJohannesburg2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteJohannesburg2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteWashingtonDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteWashingtonDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteDubai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteDubai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTaipei2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTaipei2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSingapore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSingapore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgnitePrague2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgnitePrague2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteCopenhagen2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteCopenhagen2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteZürich2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteZürich2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteAmsterdam2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteAmsterdam2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteHongKong2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteHongKong2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMadrid2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMadrid2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMumbai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMumbai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBangalore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBangalore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteChicago2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteChicago2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTelAviv2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTelAviv2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBerlin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBerlin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteStockholm2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteStockholm2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicrosoftTechDays, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicroCPH2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicroCPH2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CircleCityCom2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CircleCityCom2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCPorto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCPorto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			// TODO: Submitted under "Building an Ultra-Scalable API Using Azure Functions Without Too Much Worry"
			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DogFoodCon2018], new DateTime(2018, 10, 5, 14, 10, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Building%20an%20Ultra-Scalable%20API%20Using%20Azure%20Functions%20Without%20Too%20Much%20Worry.pdf"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeStock2019], new DateTime(2019, 10, 4, 9, 30, 0), "12");

			progressBar.Tick();

		}

		private async Task TimeTravelingData(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Time Traveling Data: SQL Server Temporal Tables",
					Abstract = "Have you have built complicated triggers and procedures to track the history of data in your databases? What if SQL Server or Azure SQL could take care of all that for you and you just had to change a couple of settings? Starting with SQL Server 2016, there is support for system-versioned temporal tables as a database feature that brings built-in support for providing information about data stored in a table at any point in time rather than only the data that is correct as the current moment time. During this session, Chad will explain the key scenarios around the use of Temporal Tables, how system-time works, how to get started, and finish up with a demo to show you Temporal Tables in action, including the easy-to-use T-SQL syntax to implement all of the Temporal goodness.",
					ShortAbstract = "Have you built complicated triggers and procedures to track the history of data in your database? Come to this session to find out how you can use built-in support in SQL Server to do this more efficiently than your complicated code.",
					LearningObjectives = new List<string>()
					{
						"Understand the key scenarios around the use of SQL Server temporal tables",
						"Understand how to get started using Temporal Tables",
						"Understand best practices and limitations of Temporal Tables"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_AzureSQL],
						_Tags[Tag_Data],
						_Tags[Tag_Database],
						_Tags[Tag_SQLServer],
						_Tags[Tag_TSQL]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					},
					Outline = new List<string>()
					{
						"What is system-versioned temporal tables?",
						"Why Temporal?",
						"How does temporal work?",
						"Common Usage Scenarios",
						"Considerations and Limitations",
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCMinnesota2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveSanDiego2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveChicago2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Live360Orlando2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Live360Orlando2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThunderPlains2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThunderPlains2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveLasVegas2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveAustin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CaribbeanDeveloeprsConference], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CircleCityCom2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CircleCityCom2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SouthFloridaCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SouthFloridaCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BostonCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BostonCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Momentum2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Momentum2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_TechBash2019], new DateTime(2019, 11, 15, 11, 30, 0), "Rosewood");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Time%20Travelling%20Tables%20-%20TechBash.pdf"));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Demo Scripts", new Uri("https://chadgreen.blob.core.windows.net/slides/Time%20Travelling%20Tables%20-%20TechBash.pdf"));

			progressBar.Tick();

		}

		private async Task GettingStartedWithAzureDevOps(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Getting Started with Azure DevOps",
					Abstract = "DevOps is about people, process, and products; DevOps is about continually getting better in delivering value to your customers. Getting it right requires a lot of effort, but the benefits to your organization and customers are tremendous. Microsoft has a fantastic set of products that can help you get the most out of the cloud and help in deploying your application to any platform. In this demo-heavy session, Chad shows you how to go from zero to DevOps and how to being the transformation of your team to a well-oiled machine that is constantly making the customers happy.",
					ShortAbstract = "DevOps is about people, process, and products; DevOps is about continually getting better in delivering value to your customers. Come to this session to learn about Microsoft’s products to bring DevOps goodness to your development process.",
					LearningObjectives = new List<string>()
					{
						"Learning what exactly DevOps is and what is not",
						"Learning about the different features of Azure DevOps",
						"See first hand how to get started using Azure DevOps to start continuously deliver value to your customers"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_AzureSQL],
						_Tags[Tag_Data],
						_Tags[Tag_Database],
						_Tags[Tag_SQLServer],
						_Tags[Tag_TSQL]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_Evansville1218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 6, 6, 18, 00, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Gettings%20Started%20with%20Azure%20DevOps%20-%20Evansville.pdf"));

			shindigPresentation = shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_LouDotNet1218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 6, 6, 18, 00, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Gettings%20Started%20with%20Azure%20DevOps%20-%20Louisville.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DotNetSouth2019], new DateTime(2019, 5, 13, 11, 20, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Gettings%20Started%20with%20Azure%20DevOps%20-%20DotNetSouth.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CincyDeliver2019], new DateTime(2019, 7, 26, 13, 0, 0), "Carolina");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Gettings%20Started%20with%20Azure%20DevOps%20-%20CincyDeliver.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_PrairieCode2019], new DateTime(2019, 9, 12, 13, 15, 0), "Iowa D");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Gettings%20Started%20with%20Azure%20DevOps%20-%20Prairie.Code().pdf"));

			progressBar.Tick();

		}

		private async Task GraphingYourWayThroughTheCosmos(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Graphing Your Way Through the Cosmos: Common Data Problems Solved with Graphs",
					Abstract = "Data as it appears in the real world is naturally connected, but traditional data modeling focuses on entities which can cause for complicated joins of these naturally connected data. That is where graph databases come in as they store data more like what happens naturally in the real world. Sure, there a lot of talk about using graph databases for social networks, recommendation engines, and Internet of Things; but using graph databases can also make a lot of sense when working with common business data problems.\nIn this presentation, you will get a better understanding of what graph databases are, how to use the Gremlin API within Azure Cosmos DB to traverse such data structures, and see practical examples to common data problems.",
					ShortAbstract = "Data is naturally connected, but data modeling focuses on entities which cause complicated joins; this is where graph databases come in as they store data more naturally. See how to use Azure Cosmos DB Gremlin API to traverse such data structures and see practical examples to common data problems.",
					LearningObjectives = new List<string>()
					{
						"Understanding the basics of graph databases",
						"See real world examples of graph databases with common business data problems",
						"Understand best practices in building graphing data solutions"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_CosmosDB],
						_Tags[Tag_Data],
						_Tags[Tag_Database],
						_Tags[Tag_GraphDatabases]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_LighteningTalk],
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					},
					Outline = new List<string>()
					{
						"What are Graph Databases",
						"What is Cosmos DB",
						"Graph vs Relational",
						"Exploring Graph Traversals",
						"Summary"
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCMinnesota2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveSanDiego2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveChicago2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConfernece2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConfernece2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCTechTown2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCTechTown2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SelfConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SelfConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Live360Orlando2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Live360Orlando2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThunderPlains2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThunderPlains2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StrangeLoop2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StrangeLoop2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_LittleRockTechFest2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_LittleRockTechFest2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BuildStuffLithuania2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BuildStuffLithuania2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BuildStuffKiev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BuildStuffKiev2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevConRegina2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevConRegina2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveLasVegas2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveAustin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBeijing2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBeijing2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteToronto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteToronto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMilan2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMilan2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteJohannesburg2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteJohannesburg2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteWashingtonDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteWashingtonDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteDubai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteDubai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTaipei2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTaipei2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSingapore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSingapore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgnitePrague2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgnitePrague2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteCopenhagen2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteCopenhagen2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteZürich2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteZürich2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteAmsterdam2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteAmsterdam2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteHongKong2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteHongKong2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMadrid2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMadrid2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMumbai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMumbai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBangalore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBangalore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteChicago2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteChicago2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTelAviv2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTelAviv2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBerlin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBerlin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteStockholm2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteStockholm2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DeveloperWeek2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DeveloperWeek2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicrosoftTechDays, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CaribbeanDeveloeprsConference], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevoxxUK2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevoxxUK2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CircleCityCom2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CircleCityCom2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_OrlandoCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_OrlandoCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SouthFloridaCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SouthFloridaCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCPorto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCPorto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BostonCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BostonCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetDeveloperConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetDeveloperConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanCloudConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanCloudConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanSharePointOffice365AzureConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanSharePointOffice365AzureConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCManchester2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCManchester2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Momentum2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Momentum2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DogFoodCon2019], new DateTime(2019, 10, 3, 11, 00, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Graphing%20Your%20Way%20Through%20the%20Cosmos%20-%20DogFoodCon.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_TechBash2019], new DateTime(2019, 11, 15, 9, 0, 0), "Aloeswood");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Graphing%20Your%20Way%20Through%20the%20Cosmos%20-%20TechBash.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeMash2020], new DateTime(2019, 9, 12, 13, 15, 0), "Iowa D");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Graphing%20Your%20Way%20Through%20the%20Cosmos%20-%20CodeMash%202020.pdf"));

			// TODO: Submitted as 60-minute / scheduled as 15-minute
			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_IgniteWashingtonDC2020], new DateTime(2020, 2, 6, 12, 5, 0), "Theater 3");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/The%20Hitchhicker%27s%20Guide%20to%20the%20Cosmos%20-%20Ignite%20Government.pptx"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_SouthFloridaCodeCamp2020], new DateTime(2020, 2, 29, 13, 20, 0), "2072");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Graphing%20Your%20Way%20Through%20the%20Cosmos%20-%20SFSDC.pdf"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_OrlandoCodeCamp2020], new DateTime(2020, 3, 28, 15, 20, 0), "Auditorium");
			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_IgniteChicago2020]);
			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_CodeStock2020], new DateTime(2020, 04, 18, 13, 50, 0), "Ballroom A");
			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_NDCSydney2020], new DateTime(2020, 10, 14, 13, 40, 0), "Room 5");
			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_GraniteStateCC2020], new DateTime(2020, 11, 14));

			progressBar.Tick();

		}

		private async Task AzureDurableFunctionsForServerlessNetOrchestration(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Azure Durable Functions for Serverless .NET Orchestration",
					Abstract = "Durable Functions is a new open-source extension to Azure Functions that enable long running orchestrations and stateful processes to execute as serverless functions. Learn how to write durable functions, and patterns and best practices to write simple or complex stateful orchestrations.",
					ShortAbstract = "Durable Functions is a new open-source extension to Azure Functions that enable long running orchestrations and stateful processes to execute as serverless functions. Learn how to write durable functions, and patterns and best practices to write simple or complex stateful orchestrations.",
					LearningObjectives = new List<string>()
					{
						"Understand how to write Durable Azure Functions",
						"Understand patterns and where to use Durable Azure Functions",
						"Understand the best practices for writing stateful orchestrations"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_AzureFunctions],
						_Tags[Tag_Serverless]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveNewOrleans2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveNewOrleans2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveBoston2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveBoston2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveRedmond2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveRedmond2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCMinnesota2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveSanDiego2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveChicago2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StirTrek2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCTechTown2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCTechTown2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SelfConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SelfConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ScenicCitySummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ScenicCitySummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Live360Orlando2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Live360Orlando2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThunderPlains2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThunderPlains2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StrangeLoop2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StrangeLoop2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSummit2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSummit2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_LittleRockTechFest2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_LittleRockTechFest2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevConRegina2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevConRegina2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveLasVegas2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveAustin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicrosoftTechDays, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CaribbeanDeveloeprsConference], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanSharePointOffice365AzureConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanSharePointOffice365AzureConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Øredev2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Øredev2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Momentum2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Momentum2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBeijing2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBeijing2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteToronto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteToronto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMilan2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMilan2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteJohannesburg2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteJohannesburg2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteWashingtonDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteWashingtonDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteDubai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteDubai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTaipei2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTaipei2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSingapore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSingapore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgnitePrague2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgnitePrague2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteCopenhagen2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteCopenhagen2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteZürich2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteZürich2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteAmsterdam2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteAmsterdam2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteHongKong2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteHongKong2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMadrid2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMadrid2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMumbai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMumbai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBangalore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBangalore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteChicago2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteChicago2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTelAviv2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTelAviv2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBerlin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBerlin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteStockholm2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteStockholm2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_StirTrek2019], new DateTime(2019, 5, 26, 8, 30, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Azure%20Services%20Every%20Developer%20Needs%20to%20Know%20-%20Louisville%20.NET%20Meetup%20(January%202019).pdf"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_IndyCode2020], new DateTime(2020, 5, 1, 13, 15, 0), "Wabash");

			progressBar.Tick();

		}

		private async Task AzureServicesEveryDeveloperNeedsToKnow(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Azure Services Every Developer Needs to Know",
					Abstract = "Azure is a powerful platform with many amazing services, but it can also be hard to know which ones you need to know about when you are first getting started with cloud development. What can you do when you looking to modernize an existing ASP.NET application? What data services are the most applicable to .NET development? How can you get started with serverless? In this session, Chad will cover how to get started within cloud development in Azure using common services that most .NET applications running in the cloud will benefit from using.",
					ShortAbstract = "Azure is a powerful platform with many amazing services, but it can also be hard to know which ones you need to know about when you are first getting started with cloud development. You will learn the services you need to start with to modernize an existing ASP.NET application in the Azure cloud.",
					LearningObjectives = new List<string>()
					{
						"Understand the basics of Azure services and architecture",
						"Understand the primary services a developer needs to know about to build a robust ASP.NET application"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_ASPNet],
						_Tags[Tag_Azure],
						_Tags[Tag_AzureAppService],
						_Tags[Tag_AzureFunctions],
						_Tags[Tag_AzureMonitor],
						_Tags[Tag_AzureSQL],
						_Tags[Tag_AzureStorage],
						_Tags[Tag_Cloud],
						_Tags[Tag_Serverless]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NebraskaCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_RevolutionConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetSouth2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetSouth2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_LouDotNet0119], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 1, 17, 19, 0, 0));

			progressBar.Tick();

		}

		private async Task DevelopACompleteServerlessSolutionInADay(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Develop a Complete Serverless Solution in a Day",
					Abstract = "Serverless has become a movement about developer empowerment. No longer do development teams have to worry about infrastructure provisioning, scaling, or paying for resources they are not using because all of this is abstracted away so developers can focus on rapidly building applications with built-in support to handle production-level traffic. Microsoft, Amazon, Google, and other provide serverless functions as a service offering that allows developers to implement integrated end points leveraging the programming language of their choice without having to worry about the support structure.\nDuring this workshop, you will learn what exactly we mean by serverless computing and how to build complete serverless solutions that are connected to queues, web requests, and databases and seamlessly integrate with third part APIs. This hands-on lab will be using Microsoft Azure service offerings, but we will also provide highlights about how similar solutions could be developed using similar solutions could be developed using similar offerings from Amazon and Google.",
					ShortAbstract = "Serverless has become a movement about developer empowerment. During this workshop, you will learn what exactly we mean by serverless computing and how to build complete serverless solutions that are connected to queues, web requests, and databases and seamlessly integrate with third part APIs.",
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Azure],
						_Tags[Tag_AzureFunctions],
						_Tags[Tag_Serverless]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_FullDay]
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			progressBar.Tick();

		}

		private async Task GettingGremlinsToImporveYourData(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Getting Gremlins to Improve Your Data",
					Abstract = "Data as it appears in the real world is naturally connected, but traditional data modeling focuses on entities which can cause for complicated joins of naturally connected data. This is were graph databases come in as they store data more like what happens naturally in the real world. But how do you get started?\nDuring this workshop, we will discover just what is graph databases are, the use cases for them, and how to get started using them using the Graph API within Azure Cosmos DB. We will build a graph database from scratch and then hook it up to a web application to start to use the power of the Graph API.\nWe will use ASP.NET Core and C# for the workshop, but the focus will be on the Graph API and not these technologies. Examples will also be provided in Java, Node.js, Python, and PHP so that everyone can follow along.",
					ShortAbstract = "Data is naturally connected, but data modeling focuses on entities which cause complicated joins; this is where graph databases come in as they store data more naturally. Learn hands-on how to use Azure Cosmos DB Gremlin API to traverse graph data structures and implement them into applications.",
					LearningObjectives = new List<string>()
					{
						"Understand the basics of graph databases",
						"Get hands-on experience setting up, configuring, and optimizing a graph database",
						"Get hands-on experience working with graph databases in your application"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_CosmosDB],
						_Tags[Tag_Data],
						_Tags[Tag_Database],
						_Tags[Tag_GraphDatabases]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_FullDay],
						_SessionTypes[SessionType_HalfDay]
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_NDCMinnesota2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_KCDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_VSLiveSanDiego2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_VSLiveChicago2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_ThatConfernece2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_NDCOslo2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_NDCTechTown2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_MusicCityTech2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_HalfDay], accepted: false));
			submissions.Add(Shindig_Live360Orlando2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_Øredev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_LittleRockTechFest2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_BuildStuffLithuania2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_BuildStuffKiev2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_NDCLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_DotNetDeveloperConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));
			submissions.Add(Shindig_EuropeanCloudConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2019], presentation, _SessionTypes[SessionType_FullDay], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_BeerCityCode2019], new DateTime(2019, 5, 31, 9, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Gremlins%20to%20Improve%20Your%20Data%20-%20Beer%20City%20Code.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_MusicCityTech2019], new DateTime(2019, 9, 5, 8, 0, 0), "183");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Gremlins%20to%20Improve%20Your%20Data%20-%20Music%20City%20Tech.pdf"));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Labs", new Uri("http://bit.ly/2lXiKEZ"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_EuropeanCloudConference2020], new DateTime(2021, 10, 27));

			progressBar.Tick();

		}

		private async Task EventDrivenArchitectureInTheCloud(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Event-Driven Architecture in the Cloud",
					Abstract = "Event-driven architectures is a versatile approach to designing and integrating complex software systems with loosely coupled components. While not a new concept, event-driven architecture is seeing a new life as applications move to cloud as it provides much more robust possibilities to solve business problems of today and in the future. In this session, you will learn how to create a loosely coupled architecture for your business that has the domain at the core. You will learn the basics of event-driven architecture, how to transform your complex systems to become event driven, and what benefits this architecture brings to your businesses. The overall architecture is applicable to any cloud stack, but this session will have examples using Azure offerings.",
					ShortAbstract = "Event-driven architectures is a versatile approach to designing and integrating complex software systems with loosely coupled components.  In this session, you will learn how to create a loosely coupled architecture for your business that has the domain at the core.",
					ElevatorPitch = "Learn how to create a loosely coupled architecture for your business that has the domain at the core.",
					WhyAttend = "Over the last couple of years, microservices has been the answer to how to solve all your problems.  The primary goal of microservices is to break up a system into independent services focused on a particular business area.  But many times, we still see dependency between these “independent” services which causes numerous issues.  Event-driven architecture goes several steps further in order to truly get decoupled, encapsulated services which are highly responsive, scalable/distributed, and independent. In this session, you will learn about event-driven architecture and how to implement it within the Microsoft Azure stack.",
					LearningObjectives = new List<string>()
					{
						"Learn the basics of event-driven architecture",
						"Learn how to transform your complex systems to become event driven",
						"Learn about the benefits event-driven architecture brings to your business"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_AzureFunctions],
						_Tags[Tag_Cloud],
						_Tags[Tag_CosmosDB],
						_Tags[Tag_EventDrivenArchitecture],
						_Tags[Tag_EventHubs]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					},
					Outline = new List<string>()
					{
						"Preamble: What is Event-Driven Architecture",
						"Preamble: External Event Sources",
						"Preamble: What to use this architecture",
						"Preamble: Benefits",
						"Preamble: Drawbacks",
						"Preamble: Challenges",
						"Implementation Options: Market Landscape",
						"Implementation Options: Azure Event Hubs",
						"Demonstration",
						"Summary"
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_StrangeLoop2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_StrangeLoop2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_LittleRockTechFest2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_LittleRockTechFest2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBeijing2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBeijing2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteToronto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteToronto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMilan2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMilan2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteJohannesburg2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteJohannesburg2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteWashingtonDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteWashingtonDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteDubai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteDubai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTaipei2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTaipei2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSingapore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSingapore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgnitePrague2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgnitePrague2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteCopenhagen2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteCopenhagen2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteZürich2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteZürich2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteAmsterdam2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteAmsterdam2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteHongKong2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteHongKong2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMadrid2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMadrid2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMumbai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMumbai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBangalore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBangalore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteChicago2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteChicago2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTelAviv2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTelAviv2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBerlin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBerlin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteStockholm2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteStockholm2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicrosoftTechDays, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CaribbeanDeveloeprsConference], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicroCPH2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicroCPH2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevoxxUK2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevoxxUK2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CircleCityCom2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CircleCityCom2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_OrlandoCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_OrlandoCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SouthFloridaCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SouthFloridaCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCPorto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCPorto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BostonCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BostonCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetDeveloperConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetDeveloperConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanCloudConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanCloudConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Momentum2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Momentum2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_LittleRockTechFest2019], new DateTime(2019, 10, 16, 11, 0, 0), "Ballroom B");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Event-Driven%20Architecture%20in%20the%20Cloud%20-%20Little%20Rock%20Tech%20Fest.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DevUp2019], new DateTime(2019, 10, 16, 8, 0, 0), "Discovery B");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Event-Driven%20Architecture%20in%20the%20Cloud%20-%20DevUp.pdf"));

			shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_LouDotNet1019], presentation, _SessionTypes[SessionType_Session60], new DateTime(2019, 10, 17, 19, 0, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Event-Driven%20Architecture%20in%20the%20Cloud%20-%20Louisville%20.NET.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_TechBash2019], new DateTime(2019, 11, 13, 16, 0, 0), "Sagewood");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Event-Driven%20Architecture%20in%20the%20Cloud%20-%20TechBash.pdf"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_BostonCodeCamp2020], new DateTime(2020, 3, 21, 15, 0, 0), "Jefferson");

			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_DallasAustinAzure1220], presentation, _SessionTypes[SessionType_Session60], new DateTime(2020, 12, 15, 18, 0, 0));

			progressBar.Tick();

		}

		private async Task BuildingHyperScaledEventProcessingSolutionsInAzure(ChildProgressBar progressBar)
		{

			// TODO: Add AKA (Getting Started with Azure Event Hubs)

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "Building Hyper-Scaled Event-Processing Solutions in Azure",
					Abstract = "At the core of building hyper-scaled event-processing solutions in Azure is knowledge of how to use Azure Event Hubs. In this session, you will learn how to create an event-processing pipeline in Azure. First, we will discover how to set up an Azure Event Hub and how to send and receive events in a .NET application. Then you will see how build a real-time dashboard using Stream Analytics and Power BI. Finally, we will cover how to archive the events for long-time storage and how to trigger Azure Functions and Azure Logic Apps from the events within an Event Hub.",
					ShortAbstract = "At the core of building hyper-scaled event-processing solutions in Azure is knowledge of how to use Azure Event Hubs. In this session, you will learn how to create an event-processing pipeline in Azure.",
					ElevatorPitch = "Learn how to create a loosely coupled architecture for your business that has the domain at the core.",
					WhyAttend = "Over the last couple of years, microservices has been the answer to how to solve all your problems.  The primary goal of microservices is to break up a system into independent services focused on a particular business area.  But many times, we still see dependency between these “independent” services which causes numerous issues.  Event-driven architecture goes several steps further in order to truly get decoupled, encapsulated services which are highly responsive, scalable/distributed, and independent. In this session, you will learn about event-driven architecture and how to implement it within the Microsoft Azure stack.",
					LearningObjectives = new List<string>()
					{
						"Learn how to set up an Azure Event Hub and send and receive events in a .NET application",
						"Learn how to build a real-time dashboard using Stream Analytics and Power BI using data ingested by Azure Event Hubs",
						"Learn how archive events ingested by Azure Event Hubs for long-time storage",
						"Learn how to trigger Azure Functions and Azure Logic Apps from the events within an Event Hub"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_AzureFunctions],
						_Tags[Tag_Cloud],
						_Tags[Tag_EventDrivenArchitecture],
						_Tags[Tag_EventHubs]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					},
					Outline = new List<string>()
					{
						"Introduction to Event Hubs",
						"Demo: Setup Azure Event Hubs",
						"Demo: Send and Receive Events in .NET Core",
						"Demo: Send Event from an Azure Function",
						"Demo: Trigger Azure Function",
						"Demo: Trigger Azure Logic App",
						"Demo: Archive Events for Long - Term Storage",
						"Demo: Real - Time Dashboard",
						"Frequently Asked Questions",
						"Summary"
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBeijing2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBeijing2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveLasVegas2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveAustin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeMash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteToronto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteToronto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMilan2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMilan2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteJohannesburg2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteJohannesburg2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteWashingtonDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteWashingtonDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteDubai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteDubai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTaipei2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTaipei2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSingapore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSingapore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgnitePrague2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgnitePrague2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteCopenhagen2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteCopenhagen2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteZürich2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteZürich2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteAmsterdam2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteAmsterdam2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteHongKong2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteHongKong2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMadrid2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMadrid2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMumbai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMumbai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBangalore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBangalore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteChicago2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteChicago2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTelAviv2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTelAviv2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBerlin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBerlin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteStockholm2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteStockholm2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicroCPH2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicroCPH2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevoxxUK2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevoxxUK2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CircleCityCom2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CircleCityCom2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_OrlandoCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_OrlandoCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SouthFloridaCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SouthFloridaCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCPorto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCPorto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_KCDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BostonCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BostonCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetDeveloperConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetDeveloperConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanCloudConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanCloudConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DevUp2019], new DateTime(2019, 10, 16, 10, 30, 0), "Discovery D");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Getting%20Started%20with%20Azure%20Event%20Hubs%20-%20DevUp.pdf"));

			progressBar.Tick();

		}

		private async Task TheHitchhikersGuideToTheCosmos(ChildProgressBar progressBar)
		{

			var presentation = await _PresentationManager.CreatePresentationAsync(
				new Presentation()
				{
					Name = "The Hitchhiker's Guide to the Cosmos",
					Abstract = "Today’s applications are required to be highly responsible and always online. Cosmos DB was built from the ground up to provide a globally distributed, multi-model database service that enables you to elastically and independently scale throughput and storage across any number of Azure regions worldwide. Because of its ARS (atoms, records, and sequences) design, Azure Cosmos DB accessing data via SQL, MongoDB, Table, Gremlin, and Cassandra APIs. All of this with transparent multi-master replication, high availability at 99.999%, and guarantees of less than 10-ms latencies both reads and (indexed) writes.\nIn this session, you will learn what you can do with Cosmos DB, the benefits of each of these data models, and how to use everything Cosmos DB has to offer to make your applications rock solid. Come find out when and how to implement Cosmos DB and which options you should use based upon your needs.",
					ShortAbstract = "In this session, you will learn what you can do with Cosmos DB, the benefits of each of these data models, and how to use everything Cosmos DB has to offer to make your applications rock solid. Come find out when and how to implement Cosmos DB and which options you should use based upon your needs.",
					ElevatorPitch = "Cosmos DB provides several data models to store vast amounts of data; learn which is right for you.",
					WhyAttend = "You probably have heard about Cosmos DB, but are not sure what exactly it is or how it could help you develop better software solutions.  Come hear about Cosmos DB, understand how to get started, and most importantly, understand the different data APIs Cosmos DB supports in order to model your data the best way for your application needs.",
					LearningObjectives = new List<string>()
					{
						"Learn about the different Azure Cosmos DB data models",
						"Learn how to use the benefits of Azure Cosmos DB to build the best data solution",
						"Learn about some of the common Cosmos DB use cases"
					},
					Tags = new List<TagItem>()
					{
						_Tags[Tag_Architecture],
						_Tags[Tag_Azure],
						_Tags[Tag_Cloud],
						_Tags[Tag_CosmosDB],
						_Tags[Tag_Data],
						_Tags[Tag_Database],
						_Tags[Tag_NoSQL]
					},
					SessionTypes = new List<SessionType>()
					{
						_SessionTypes[SessionType_Session45],
						_SessionTypes[SessionType_Session60],
						_SessionTypes[SessionType_Session75]
					},
					Outline = new List<string>()
					{
						"What is Cosmos DB",
						"Cosmos Use Cases",
						"Integrations",
						"Navigating the 5 API Models",
						"Call to Action"
					}
				});

			Dictionary<string, ShindigSubmission> submissions = new();
			submissions.Add(Shindig_DevConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevUp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevUp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DogFoodCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CreamCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CreamCityCode2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevSpace2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_LittleRockTechFest2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_LittleRockTechFest2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieDevCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_PrairieDevCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechCon2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_UpdateConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_UpdateConference2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_AtlantaCodeCamp2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_AtlantaCodeCamp2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBeijing2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBeijing2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevDay2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevDay2019], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveLasVegas2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveLasVegas2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_VSLiveAustin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_VSLiveAustin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteToronto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteToronto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteLondon2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteLondon2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMilan2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMilan2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteJohannesburg2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteJohannesburg2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteWashingtonDC2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteWashingtonDC2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteDubai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteDubai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSydney2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSydney2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTaipei2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTaipei2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteSingapore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteSingapore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgnitePrague2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgnitePrague2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteCopenhagen2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteCopenhagen2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteZürich2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteZürich2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteAmsterdam2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteAmsterdam2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteHongKong2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteHongKong2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMadrid2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMadrid2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteMumbai2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteMumbai2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBangalore2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBangalore2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteChicago2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteChicago2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteTelAviv2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteTelAviv2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteBerlin2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteBerlin2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IgniteStockholm2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IgniteStockholm2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SDD2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevConf2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevConf2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Connectaha2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Connectaha2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Refactr2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Swetug2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Swetug2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MicrosoftTechDays, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CaribbeanDeveloeprsConference, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MicrosoftTechDays], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevoxxUK2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DevoxxUK2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_t3chfest2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_t3chfest2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CircleCityCom2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CircleCityCom2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_OrlandoCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_OrlandoCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SouthFloridaCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_SouthFloridaCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_IndyCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCPorto2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCPorto2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NDCOslo2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_NDCOslo2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BostonCodeCamp2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BostonCodeCamp2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DotNetDeveloperConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_DotNetDeveloperConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanCloudConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanCloudConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_ThatConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_EuropeanSharePointOffice365AzureConference2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_EuropeanSharePointOffice365AzureConference2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CincyDeliver2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_BeerCityCode2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_TechBash2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_MusicCityTech2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Momentum2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_Momentum2020], presentation, _SessionTypes[SessionType_Session60], accepted: false));

			ShindigPresentation shindigPresentation;

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_AtlantaCodeCamp2019], new DateTime(2019, 9, 14, 15, 45, 0));
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/The%20Hitchhicker%27s%20Guide%20to%20the%20Cosmos%20-%20Atlanta%20Code%20Camp.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_DevSpace2019], new DateTime(2019, 10, 12, 13, 0, 0), "Ballroom 3");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/The%20Hitchhicker%27s%20Guide%20to%20the%20Cosmos%20-%20DevSpace.pdf"));

			shindigPresentation = await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_IgniteWashingtonDC2020], new DateTime(2020, 2, 7, 13, 45, 0), "Theater 1");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri("https://chadgreen.blob.core.windows.net/slides/Graphing%20Your%20Way%20Through%20the%20Cosmos%20-%20Ignite%20Government.pptx"));

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_IgniteChicago2020]);

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_BostonCodeCamp2020], new DateTime(2020, 3, 21, 13, 45, 0), "Monroe");

			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_EuropeanCloudConference2020]);

			shindigPresentation = await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_TDevConf2020], presentation, _SessionTypes[SessionType_Session60], new DateTime(2020, 10, 3, 9, 00, 0), "Knoxville");
			await _PresentationManager.AddDownloadToShindigPresentation(shindigPresentation.PresentationId, shindigPresentation.ShindigId, shindigPresentation.OwnerEmailAddress, "Slides", new Uri(@"https://chadgreen.blob.core.windows.net/slides/The%20Hitchhicker's%20Guide%20to%20the%20Cosmos%20-%20TDevConf.pdf"));

			progressBar.Tick();

		}

		#endregion

	}

}