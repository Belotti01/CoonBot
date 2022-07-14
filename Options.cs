namespace CoonBot {
	public static class Options {

		public static string EnvVarsPath => Path.Combine(Environment.CurrentDirectory, "config.env");
		public static string DebugEnvVarsPath => Path.Combine(Environment.CurrentDirectory, "debug-config.env");
		public const string API_KEY_FILENAME = "ConnectionKey.config";
		public static string ApiKeyFilepath => Path.Combine(Environment.CurrentDirectory, API_KEY_FILENAME);
		public const int INTERACTIVITY_TIMEOUT = 30;

		public static class Bot {
			public static readonly string[] CommandPrefixes = {
#if DEBUG
				"test", "debug"
#else
				"coon", "c!"
#endif
			};
		}

		public static class Colors {
			public static DiscordColor Default => DiscordColor.PhthaloBlue;
			public static DiscordColor CommandError => DiscordColor.Red;
		}

		public static class YouTube {
			public const int MIN_QUERY_LENGTH = 3;
		}
	}
}
