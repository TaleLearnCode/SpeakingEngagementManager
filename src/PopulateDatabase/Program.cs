using System;
using TaleLearnCode.SpeakingEngagementManager.Services;

namespace TaleLearnCode.SpeakingEngagementManager.ConsoleTaleLearnCode.SpeakingEngagementManager.PopulateDatabase
{

	class Program
	{
		static void Main(string[] args)
		{
			WelcomeUser();
			using var cosmosConnection = new CosmosConnection(Settings.CosmosConnectionString, Settings.DatabaseName, Settings.ContainerName);



		}

		private static void WelcomeUser()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(@"            __________                   .__          __                         ");
			Console.WriteLine(@"            \______   \____ ______  __ __|  | _____ _/  |_  ____                 ");
			Console.WriteLine(@"             |     ___/  _ \\____ \|  |  \  | \__  \\   __\/ __ \                ");
			Console.WriteLine(@"             |    |  (  <_> )  |_> >  |  /  |__/ __ \|  | \  ___/                ");
			Console.WriteLine(@"             |____|   \____/|   __/|____/|____(____  /__|  \___  >               ");
			Console.WriteLine(@"                            |__|                   \/          \/                ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(@"             _________                     __   .__                              ");
			Console.WriteLine(@"            /   _____/_____   ____ _____  |  | _|__| ____    ____                ");
			Console.WriteLine(@"            \_____  \\____ \_/ __ \\__  \ |  |/ /  |/    \  / ___\               ");
			Console.WriteLine(@"            /        \  |_> >  ___/ / __ \|    <|  |   |  \/ /_/  >              ");
			Console.WriteLine(@"           /_______  /   __/ \___  >____  /__|_ \__|___|  /\___  /               ");
			Console.WriteLine(@"                   \/|__|        \/     \/     \/       \//_____/                ");
			Console.WriteLine(@"___________                                                          __          ");
			Console.WriteLine(@"\_   _____/ ____    _________     ____   ____   _____   ____   _____/  |_  ______");
			Console.WriteLine(@" |    __)_ /    \  / ___\__  \   / ___\_/ __ \ /     \_/ __ \ /    \   __\/  ___/");
			Console.WriteLine(@" |        \   |  \/ /_/  > __ \_/ /_/  >  ___/|  Y Y  \  ___/|   |  \  |  \___ \ ");
			Console.WriteLine(@"/_______  /___|  /\___  (____  /\___  / \___  >__|_|  /\___  >___|  /__| /____  >");
			Console.WriteLine(@"        \/     \//_____/     \//_____/      \/      \/     \/     \/          \/ ");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine();
			Console.WriteLine();
		}

		private static void WhichMicrosoftFrameworkAmISupposedToUse(CosmosConnection cosmosConnection)
		{
			Console.WriteLine();
			Console.WriteLine("Which Microsoft Framework Am I Supposed to Use?");
		}



	}

}