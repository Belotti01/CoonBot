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
			string? token;

			// See if an environment variable is present
			token = Environment.GetEnvironmentVariable("bot_token");
			if(!string.IsNullOrWhiteSpace(token) && token.Length > 30) {
				Log.Success($"API Token starting with {token[..4]} found as an environment variable.");
				// Don't serialize it - keep it fully hidden
				return token;
			}

			TokenReader keyReader = new(Options.ApiKeyFilepath);
			
			// Look for previously saved API key
			if(keyReader.TryRead(out token)) {
				Log.Success("Discord API Token loaded.");
				return token;
			}

			token = args.FirstOrDefault(x => x.Length is > 30);
			if(!string.IsNullOrWhiteSpace(token)) {
				keyReader.Set(token);
				Log.Success($"Discord API Token (starting with {token[..4]}) has been updated successfully.");
				return token;
			}

			Log.Fail("No Discord API Token was found. Set an environment variable \"bot_token\" or run the bot with the token as an argument.");
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