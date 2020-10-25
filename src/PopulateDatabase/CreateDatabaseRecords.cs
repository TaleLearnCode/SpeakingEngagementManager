using Microsoft.VisualBasic;
using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.PopulateDatabase
{

	// TODO: Presentations need a featured/thumbnail image

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

		private const string Tag_Architecture = "Architecture";
		private const string Tag_Azure = "Azure";
		private const string Tag_Cloud = "Cloud";
		private const string Tag_Leadership = "Leadership";
		private const string Tag_Serverless = "Serverless";
		private const string Tag_SoftSkills = "Soft Skills";
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
		private const string Shindig_BuildStuff2019 = "Build Stuff 2019";
		private const string Shindig_AgileDevOpsEast2019 = "Agile + DevOps East 2019";
		private const string Shindig_APIDaysParis2019 = "API Days (Paris 2019)";

		private const string Shindig_STAREAST2019 = "STAREAST 2019";

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
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_Azure });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_Cloud });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_Leadership });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_Serverless });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_SoftSkills });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNet });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNetFramework });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNetCore });
			_Tags.Add(tag.Name, tag.ToTagItem());
			childProgressBar.Tick();

			tag = await _MetadataManager.CreateMetadataAsync<Tag>(new Tag() { Name = Tag_DotNetStandard });
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

			using var childProgressBar = progressBar.Spawn(14, "Creating the Shindigs");

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
			await CreateShindigAsync(Shindig_BuildStuff2019, new Location() { City = "Vilnius", CountryDivisionCategory = "district municipality", CountryDivisionId = "LT-58", CountryDivisionName = "Vilnius", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/lt.svg"), CountryId = "LT", CountryName = "Lithuania", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 11, 13), new DateTime(2019, 11, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_Live360Orlando2019, GetUSLocation("Orlando", "FL", "Florida"), new DateTime(2019, 11, 17), new DateTime(2019, 11, 22), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_UpdateConference2019, new Location() { City = "Prague", CountryDivisionCategory = "capital city", CountryDivisionId = "CZ-10", CountryDivisionName = "Hlavní město Praha", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/cz.svg"), CountryId = "CZ", CountryName = "Czech Republic", RegionCode = "150", RegionName = "Europe", SubregionCode = "151", SubregionName = "Eastern Europe" }, new DateTime(2019, 11, 14), new DateTime(2019, 11, 16), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_AgileDevOpsEast2019, GetUSLocation("Orlando", "FL", "Florida"), new DateTime(2019, 11, 3), new DateTime(2019, 11, 8), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_AtlantaCodeCamp2019, GetUSLocation("Atlanta", "GA", "Georgia"), new DateTime(2019, 9, 14), new DateTime(2019, 9, 14), _ShindigTypes[ShindigType_CodeCamp], childProgressBar);
			await CreateShindigAsync(Shindig_APIDaysParis2019, new Location() { City = "Paris", CountryDivisionCategory = "metropolitan department", CountryDivisionId = "FR-75", CountryDivisionName = "Paris", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/fr.svg"), CountryId = "FR", CountryName = "France", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2019, 12, 10), new DateTime(2019, 12, 11), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_DevDay2019, new Location() { City = "Louvain-la-Neuve", CountryDivisionCategory = "province", CountryDivisionId = "BE-WBR", CountryDivisionName = "Brabant wallon", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/be.svg"), CountryId = "BE", CountryName = "Belgium", RegionCode = "150", RegionName = "Europe", SubregionCode = "155", SubregionName = "Western Europe" }, new DateTime(2019, 11, 26), new DateTime(2019, 11, 26), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);
			await CreateShindigAsync(Shindig_VSLiveBoston2019, GetUSLocation("Boston", "MA", "Massachusetts"), new DateTime(2019, 6, 9), new DateTime(2019, 6, 13), _ShindigTypes[ShindigType_SecondTierConference], childProgressBar);
			await CreateShindigAsync(Shindig_SDD2019, new Location() { City = "London", CountryDivisionCategory = "city corporation", CountryDivisionId = "GB-LND", CountryDivisionName = "City of London", CountryFlag = new Uri("https://countriespoc.blob.core.windows.net/flags/gb.svg"), CountryId = "GB", CountryName = "United Kingdom", RegionCode = "150", RegionName = "Europe", SubregionCode = "154", SubregionName = "Northern Europe" }, new DateTime(2019, 5, 11), new DateTime(2019, 5, 15), _ShindigTypes[ShindigType_RegionalConference], childProgressBar);

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
			submissions.Add(Shindig_StirTrek2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_MusicCityTech2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PittsburghTechFest2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_KCDC2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CincyDeliver2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_ThatConference2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CoderCruise2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechBash2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DogFoodCon2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_DevSpace2018, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeMash2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_CodeStock2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_NebraskaCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_IndyCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_PrairieCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_RevolutionConf2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_Refactr2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_StirTrek2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_BeerCityCode2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SelfConference2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_TechCon2019, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));
			submissions.Add(Shindig_SDD2020, await _ShindigManager.SubmitPresentationToShindigAsync(_Shindigs[Shindig_CodeStock2018], presentation, _SessionTypes[SessionType_Session60], accepted: false));

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

		#endregion

	}

}