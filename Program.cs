using System.Reflection;

namespace CoonBot {
	public static class Program {
		
		public static async Task Main(string[] args) {
			Console.OutputEncoding = Encoding.Unicode;
			Console.Title = "Coon Bot - Discord";

			string? apiToken = GetApiToken(args);
			if(apiToken is null) {
				Log.Fail($"No Discord API Token was found. Set it inside \"{Options.EnvVarsPath}\" and retry.");
				return;
			}
			Log.Success($"Discord API Token (starting with {apiToken[..4]}) loaded successfully.");
			await Bot.RunAsync(apiToken);
		}

		private static string? GetApiToken(string[] args) {
			string? token;

			// See if an environment variable is present
			token = Env.DiscordToken;

			return token;
		}
	}
}