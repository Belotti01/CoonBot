

using System.Reflection;

namespace CoonBot {
	public static class Program {
		[NotNull]
		public static DiscordClient? Client { get; private set; }
		public static CommandsNextExtension? Commands { get; private set; }
		public static SlashCommandsExtension? SlashCommands { get; private set; }

		private static IDiscordClientFactory? _clientFactory;
		private static readonly CliObserver _cli = new();
		
		
		public static async Task Main(string[] args) {
			Console.OutputEncoding = Encoding.Unicode;
			Console.Title = "Coon Bot - Discord";
			
			string? apiToken = GetApiToken(args);
			if(apiToken is null) {
				return;
			}

			_clientFactory = new DiscordClientFactory(apiToken);
			Client = _clientFactory.CreateClient();
			SetupClient();

			await Connect();
			await _cli.RunAsync();
		}

		private static void SetupClient() {
			// Commands
			Commands = Client.UseCommandsNext(new CommandsNextConfiguration() {
				StringPrefixes = Options.Bot.CommandPrefixes,
				CaseSensitive = false
			});
			Commands.RegisterCommands(Assembly.GetExecutingAssembly());
			Log.Success($"{Commands.RegisteredCommands.Count} Slash Commands loaded.");

			// Interactivity
			Client.UseInteractivity(new InteractivityConfiguration() {
				PollBehaviour = PollBehaviour.DeleteEmojis,
				Timeout = TimeSpan.FromSeconds(Options.INTERACTIVITY_TIMEOUT),
				PaginationBehaviour = PaginationBehaviour.WrapAround
			});

			// Slash Commands
			SlashCommands = Client.UseSlashCommands();
			SlashCommands.RegisterCommands(Assembly.GetExecutingAssembly());
			Log.Success($"{SlashCommands.RegisteredCommands.Count} Slash Commands loaded.");
		}
		private static string? GetApiToken(string[] args) {
			string? token = args.FirstOrDefault(x => x.Length is > 40 and < 80);
			TokenReader keyReader = new(Options.ApiKeyFilepath);

			if(token is not null) {
				keyReader.Set(token);
				Log.Success("Discord API Token has been updated successfully.");
				return token;
			}

			// Look for previously saved API key
			if(keyReader.TryRead(out token)) {
				Log.Success("Discord API Token loaded.");
				return token;
			}

			Log.Fail("No Discord API Token was found. Include it as a startup argument to set one.");
			return null;
		}



		private static async Task Connect() {
			Log.Write("\nWaking up Coon...");
			await Client.ConnectAsync();
			
			Log.Write($"Connection established successfully.", ConsoleColor.Green);
			Log.Info($"\nPrefixes loaded: '{string.Join("', '", Options.Bot.CommandPrefixes)}'");
		}
	}
}