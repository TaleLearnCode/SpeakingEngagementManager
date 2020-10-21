using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.PopulateDatabase
{

	public class CreateDatabaseRecords
	{

		private CosmosConnection _CosmosConnection;
		private MetadataManager _MetadataManager;
		private PresentationManager _PresentationManager;
		private ShindigManager _ShindigManager;

		private Dictionary<string, Shindig> _Shindigs = new();
		private Dictionary<string, SessionType> _SessionTypes = new();
		private Dictionary<string, ShindigType> _ShindigTypes = new();
		private Dictionary<string, Tag> _Tags = new();

		#region Constants

		private const string SessionType_Session45 = "45-Minute Session";
		private const string SessionType_Session60 = "60-Minute Session";
		private const string SessionType_Session75 = "75-Minute Session";
		private const string SessionType_HalfDay = "Half-Day Workshop";
		private const string SessionType_FullDay = "Full-Day Workshop";
		private const string SessionType_LighteningTalk = "Lightening Talk";

		private const string Tag_Architecture = "Architecture";
		private const string Tag_DotNet = ".NET";
		private const string Tag_DotNetFramework = ".NET Framework";
		private const string Tag_DotNetCore = ".NET Core";
		private const string Tag_DotNetStandard = ".NET Standard";

		private const string ShindigType_Conference = "Conference";
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
		private const string Shindig_Evansville0218 = "Evansville Technology Group";
		private const string Shindig_LouDotNet0218 = "Louisville .NET Meetup";

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
		private const string Shindig_Oredev2019 = "Øredev 2019";
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
		private const string Shindig_Swetug2019 = "Swetugg 2019";
		private const string Shindig_SDD2020 = "Software Design & Development 2020";
		private const string Shindig_Connectaha2020 = "Connectaha 2020";
		private const string Shindig_Refactr2020 = "Refactr 2020";
		private const string Shindig_CodeStock2019 = "Code Stock 2019";
		private const string Shindig_RevolutionConf2019 = "RevolutionConf 2019";
		private const string Shindig_SelfConference2019 = "self.conference 2019";
		private const string Shindig_DotNetSummit2019 = ".NET Summit 2019"
		private const string Shindig_CincyDeliver = "Cincy Deliver 2019";
		private const string Shindig_ThatConfernece2019 = "That Conference 2019";
		private const string Shindig_Agile2019 = "Agile 2019";
		private const string Shindig_NDCTechTown2019 = "NDC Tech Town 2019";
		private const string Shindig_StrangeLoop2019 = "Stange Loop 2019";
		private const string Shindig_DevConf2019 = "DevConf 2019";
		private const string Shindig_Techorama2019 = "Techorama 2019";
		private const string Shindig_BuildStuff2019 = "Build Stuff 2019";
		private const string Shindig_AgileDevOpsEast2019 = "Agile + DevOps East 2019";
		private const string Shindig_APIDaysParis2019 = "API Days (Paris 2019)";


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

			using var childProgressBar = progressBar.Spawn(5, "Creating the tags");

			Tag tag;

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_Architecture });
			_Tags.Add(tag.Name, tag);
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNet });
			_Tags.Add(tag.Name, tag);
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNetFramework });
			_Tags.Add(tag.Name, tag);
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNetCore });
			_Tags.Add(tag.Name, tag);
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNetStandard });
			_Tags.Add(tag.Name, tag);
			childProgressBar.Tick();

		}

		private async Task CreateShindigTypesAsync(ProgressBar progressBar)
		{

			using var childProgressBar = progressBar.Spawn(3, "Creating the Shindig Types");

			ShindigType shindigType;

			shindigType = await _MetadataManager.CreateMetadataAsync<ShindigType>(new ShindigType() { Name = ShindigType_Conference });
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

			using var childProgressBar = progressBar.Spawn(14, "Creating the Shindigs");

			// 2018
			await CreateShindigAsync(Shindig_CodeStock2018, GetUSLocation("Knoxville", "TN", "Tennessee"), new DateTime(2018, 4, 20), new DateTime(2018, 4, 21), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_StirTrek2018, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2018, 5, 4), new DateTime(2018, 5, 4), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_MusicCityTech2018, GetUSLocation("Nashville", "TN", "Tennessee"), new DateTime(2018, 5, 31), new DateTime(2018, 6, 2), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_PittsburghTechFest2018, GetUSLocation("Pittsburgh", "PA", "Pennsylvania"), new DateTime(2018, 6, 2), new DateTime(2018, 6, 2), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_BeerCityCode2018, GetUSLocation("Grand Rapids", "MI", "Michigan"), new DateTime(2018, 6, 22), new DateTime(2018, 6, 23), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_KCDC2018, GetUSLocation("Kansas City", "MO", "Missouri"), new DateTime(2018, 7, 11), new DateTime(2018, 7, 13), _ShindigTypes[ShindigType_Conference], childProgressBar, "The Kansas City Developer Conference is excited to announce our 10th annual event! Our 2018 conference will be held July 12th and 13th, with a pre-conference workshop day on July 11th. Once again, the event will be held at the Kansas City Convention Center in downtown Kansas City. Each year, we draw a large audience of new and experienced Developers, PMs, and Technology Managers from Missouri, Kansas, Illinois, Nebraska, Iowa, Minnesota, Oklahoma, and the Dakotas.", GetVenue(GetUSLocation("Kansas City", "MO", "Missouri"), "Kansas City Convention Center", "200 West 12th Street", new Uri("https://goo.gl/maps/GDQVXVzKYCQ2")), "$$$", new Uri("http://www.kcdc.info/"), 5);
			await CreateShindigAsync(Shindig_ThatConference2018, GetUSLocation("Wisconsin Dells", "WI", "Wisconsin"), new DateTime(2018, 8, 6), new DateTime(2018, 8, 8), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_CoderCruise2018, GetUSLocation("Ft. Lauderdale", "FL", "Florida"), new DateTime(2018, 8, 30), new DateTime(2018, 9, 3), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_TechBash2018, GetUSLocation("Pocono Manor", "PA", "Pennsylvania"), new DateTime(2018, 10, 2), new DateTime(2018, 10, 5), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_DogFoodCon2018, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2018, 10, 4), new DateTime(2018, 10, 5), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_DevSpace2018, GetUSLocation("Huntsville", "AL", "Alabama"), new DateTime(2018, 10, 12), new DateTime(2018, 10, 13), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_Evansville0218, GetUSLocation("Evansville", "IN", "Indiana"), new DateTime(2018, 2, 15), new DateTime(2018, 2, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "This is a group for anyone interested in technology in the greater Evansville Indiana area. This group is an umbrella group that encompasses the Evansville SharePoint User Group, EVVPASS SQL Server User Group, Evansville .Net User Group and Southern Indiana Powershell User Group. We will meet on the Third Thursdays of the Month during lunch and do a evening social event on every 5th Thursday.", GetVenue(GetUSLocation("Evansville", "IN", "Indiana"), "Central Library Browning Room B", "200 SE Martin Luther King Jr. Blvd", new Uri("https://goo.gl/maps/FRQSMrvDAu72")), "Free", new Uri("https://www.meetup.com/Evansville-Technology-Group/events/246450171/"), -4);
			await CreateShindigAsync(Shindig_LouDotNet0218, GetUSLocation("Louisville", "KY", "Kentucky"), new DateTime(2018, 2, 15), new DateTime(2018, 2, 15), _ShindigTypes[ShindigType_UserGroup], childProgressBar, "The Louisville .NET Meetup group is dedicated to helping developers around Louisville to become better developers with an emphasis on the Microsoft .NET technology stack. We hold meetings on the third Thursday of each month.\nThe Louisville.NET Meetup group is also host to Code PaLOUsa, the Louisville Global Azure Bootcamp(louisville.azurebootcamp.net), and the Louisville Global DevOps Bootcamp.", GetVenue(GetUSLocation("Louisville", "KY", "Kentucky"), "Modis", "101 Bullitt Lane", new Uri("https://goo.gl/maps/bnN43qJZB192")), "Free", new Uri("https://www.meetup.com/Louisville-DotNet/events/246273386/"), -5);

			// 2019
			await CreateShindigAsync(Shindig_CodeMash2019, GetUSLocation("Sandusky", "OH", "Ohio"), new DateTime(2019, 1, 8), new DateTime(2019, 1, 11), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_Connectaha2019, GetUSLocation("Omaha", "NE", "Nebraska"), new DateTime(2019, 3, 8), new DateTime(2019, 3, 8), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_CodeStock2019, GetUSLocation("Knoxville", "TN", "Tennessee"), new DateTime(2019, 4, 12), new DateTime(2019, 4, 13), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveNewOrleans2019, GetUSLocation("New Orleans", "LA", "Louisiana"), new DateTime(2019, 4, 22), new DateTime(2019, 4, 26), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_IndyCode2019, GetUSLocation("Indianapolis", "IN", "Indiana"), new DateTime(2019, 4, 24), new DateTime(2019, 4, 26), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_StirTrek2019, GetUSLocation("Columbus", "OH", "Ohio"), new DateTime(2019, 4, 26), new DateTime(2019, 4, 26), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCMinnesota2019, GetUSLocation("St. Paul", "MN", "Minnesota"), new DateTime(2019, 5, 6), new DateTime(2019, 5, 9), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_DotNetSouth2019, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2019, 5, 8), new DateTime(2019, 5, 10), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_BeerCityCode2019, GetUSLocation("Grand Rapids", "MI", "Michigan"), new DateTime(2019, 5, 31), new DateTime(2019, 6, 1), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_PrairieDevCon2019, new Location() { City = "Winnipeg", CountryDivisionCategory = "province", CountryDivisionId = "MB", CountryDivisionName = "Manitoba", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/ca.svg"), CountryId = "CA", CountryName = "Canada", RegionCode = "019", RegionName = "Americas", SubregionCode = "021", SubregionName = "Northern America" }, new DateTime(2019, 6, 4), new DateTime(2019, 6, 5), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_Refactr2019, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2019, 6, 5), new DateTime(2019, 6, 5), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_RevolutionConf2019, GetUSLocation("Virgina Beach", "VA", "Virginia"), new DateTime(2019, 6, 6), new DateTime(2019, 6, 7), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_SelfConference2019, GetUSLocation("Detroit", "MI", "Michigan"), new DateTime(2019, 6, 7), new DateTime(2019, 6, 8), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_DotNetSummit2019, new Location() { City = "Minsk", CountryDivisionCategory = "city", CountryDivisionId = 'BY-HM', CountryDivisionName = 'Horad Minsk', CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/by.svg"), CountryId = "BY", CountryName = "Belarus", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2019, 6, 8), new DateTime(2019, 6, 8), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_NDCOslo2019, new Location() { City = "Oslo", CountryDivisionId = "NO-03", CountryDivisionCategory = "county", CountryDivisionName = "Oslo", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/no.svg"), CountryId = "NO", CountryName = "Norway", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 6, 17), new DateTime(2019, 6, 21), _ShindigTypes[ShindigType_Conference], childProgressBar);
			//await CreateShindigAsync(Shindig_CincyDeliver
			//await CreateShindigAsync(Shindig_ThatConfernece2019
			//await CreateShindigAsync(Shindig_Agile2019
			await CreateShindigAsync(Shindig_VSLiveRedmond2019, GetUSLocation("Redmond", "WA", "Washington"), new DateTime(2019, 8, 12), new DateTime(2019, 8, 16), _ShindigTypes[ShindigType_Conference], childProgressBar);
			await CreateShindigAsync(Shindig_NebraskaCode2019, GetUSLocation("Lincoln", "NE", "Nebraska"), new DateTime(2019, 8, 14), new DateTime(2019, 8, 16), _ShindigTypes[ShindigType_Conference], childProgressBar);
			//await CreateShindigAsync(Shindig_NDCTechTown2019
			//await CreateShindigAsync(Shindig_MusicCityTech2019
			await CreateShindigAsync(Shindig_PrairieCode2019, GetUSLocation("Des Moines", "IA", "Iowa"), new DateTime(2019, 9, 11), new DateTime(2019, 9, 13), _ShindigTypes[ShindigType_Conference], childProgressBar);
			//Shindig_StrangeLoop2019
			//Shindig_DevConf2019
			await CreateShindigAsync(Shindig_VSLiveSanDiego2019, GetUSLocation("San Diego", "CA", "California"), new DateTime(2019, 9, 29), new DateTime(2019, 10, 3), _ShindigTypes[ShindigType_Conference], childProgressBar);
			//await CreateShindigAsync(Shindig_Techorama2019
			//await CreateShindigAsync(Shindig_ScenicCitySummit2019
			//await CreateShindigAsync(Shindig_DogFoodCon2019
			//await CreateShindigAsync(Shindig_CreamCityCode2019
			//await CreateShindigAsync(Shindig_VSLiveChicago2019
			//await CreateShindigAsync(Shindig_LittleRockTechFest2019
			//await CreateShindigAsync(Shindig_DevSpace2019
			//await CreateShindigAsync(Shindig_DevUp2019
			//await CreateShindigAsync(Shindig_TechCon2019
			//await CreateShindigAsync(Shindig_ThunderPlains2019
			//await CreateShindigAsync(Shindig_PrairieDevConRegina2019
			//await CreateShindigAsync(Shindig_Oredev2019
			//await CreateShindigAsync(Shindig_TechBash2019
			//await CreateShindigAsync(Shindig_BuildStuff2019
			//await CreateShindigAsync(Shindig_Live360Orlando2019
			//await CreateShindigAsync(Shindig_UpdateConference2019
			//await CreateShindigAsync(Shindig_AgileDevOpsEast2019
			//await CreateShindigAsync(Shindig_AtlantaCodeCamp2019
			//await CreateShindigAsync(Shindig_APIDaysParis2019
			//await CreateShindigAsync(Shindig_DevDay2019


			await CreateShindigAsync(Shindig_VSLiveBoston2019, GetUSLocation("Boston", "MA", "Massachusetts"), new DateTime(2019, 6, 9), new DateTime(2019, 6, 13), _ShindigTypes[ShindigType_Conference], childProgressBar);
			//await CreateShindigAsync(Shindig_Swetug2019
			//await CreateShindigAsync(Shindig_SDD2020
			//await CreateShindigAsync(Shindig_Connectaha2020
			//await CreateShindigAsync(Shindig_Refactr2020


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
					Tags = new List<Tag>()
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
			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_KCDC2018], new DateTime(2018, 7, 12, 8, 45, 0));
			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_LouDotNet0218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2018, 2, 15, 19, 0, 0));
			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_Evansville0218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2018, 2, 15, 11, 30, 0));

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
					Tags = new List<Tag>()
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
			await _ShindigManager.SubmissionAcceptedAsync(submissions[Shindig_KCDC2018], new DateTime(2018, 7, 12, 8, 45, 0));
			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_LouDotNet0218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2018, 2, 15, 19, 0, 0));
			await _ShindigManager.PresentationScheuledAsync(_Shindigs[Shindig_Evansville0218], presentation, _SessionTypes[SessionType_Session60], new DateTime(2018, 2, 15, 11, 30, 0));

			progressBar.Tick();

		}

		#endregion

	}

}