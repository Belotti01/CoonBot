using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoonBot {
	public static class Bot {
		// No need to always check for nullability. Give the initialization of these for granted.
		[NotNull]
		public static DiscordClient? Client { get; private set; }
		[NotNull]
		public static CommandsNextExtension? Commands { get; private set; }
		[NotNull]
		public static SlashCommandsExtension? SlashCommands { get; private set; }
		[NotNull]
		public static InteractivityExtension? Interactivity { get; private set; }

		private static IDiscordClientFactory? _clientFactory;
		private static readonly CliObserver _cli = new();

		/// <summary>
		/// Load configuration, commands, slash commands, and interactions, then connect to the Discord 
		/// Bot service using the given <paramref name="apiToken"/>.
		/// </summary>
		/// <param name="apiToken"> The Bot's API Token. </param>
		public static async Task RunAsync(string apiToken) {
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
			Interactivity = Client.UseInteractivity(new InteractivityConfiguration() {
				PollBehaviour = PollBehaviour.DeleteEmojis,
				Timeout = TimeSpan.FromSeconds(Options.INTERACTIVITY_TIMEOUT),
				PaginationBehaviour = PaginationBehaviour.WrapAround
			});

			// Slash Commands
			SlashCommands = Client.UseSlashCommands();
			SlashCommands.RegisterCommands(Assembly.GetExecutingAssembly());
			Log.Success($"{SlashCommands.RegisteredCommands.Count} Slash Commands loaded.");
		}

		private static async Task Connect() {
			Log.Write("\nWaking up Coon...");
			await Client.ConnectAsync();

			Log.Write($"Connection established successfully.", ConsoleColor.Green);
			Log.Info($"\nPrefixes loaded: '{string.Join("', '", Options.Bot.CommandPrefixes)}'");
		}
	}
}
