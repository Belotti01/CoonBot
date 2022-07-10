

namespace CoonBot {
	public static class Program {
		[NotNull]
		public static DiscordClient? Client { get; private set; }

		private static IDiscordClientFactory? _clientFactory;
		private static readonly CliObserver _cli = new();
		
		
		public static async Task Main(string[] args) {
			Console.OutputEncoding = Encoding.Unicode;
			Console.Title = "Coon Bot - Discord";
			
			string apiToken = GetApiToken();
			Console.Clear();

			_clientFactory = new DiscordClientFactory(apiToken);
			Client = _clientFactory.CreateClient();

			await Connect();
			await _cli.RunAsync();
		}

		
		private static string GetApiToken() {
			string? token;
			TokenReader keyReader = new(Options.ApiKeyFilepath);
			
			// Look for previously saved API key
			if(keyReader.TryRead(out token)) {
				Output.WriteLine("> Use previously saved Token? ", ConsoleColor.Yellow);
				bool usePrevious = Input.ReadCharToUpper('y', 'Y', 'n', 'N') == 'Y';
				if(usePrevious) {
					return token;
				}
			}
			
			// Request API Token
			Output.WriteLine("> Enter your Discord Bot's API Token: ");
			token = Input.ReadLine(40, 80);
			// Save new Token
			keyReader.Set(token);

			return token;
		}



		private static async Task Connect() {
			Log.Write("\nWaking up Coon...");
			await Client.ConnectAsync();
			
			Log.Write($"Connection established successfully.", ConsoleColor.Green);
			Log.Info($"\nPrefixes loaded: '{string.Join("', '", Options.Bot.CommandPrefixes)}'");
		}
	}
}