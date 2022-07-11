namespace CoonBot {
	public static class Env {
		public static string? DiscordToken { get; private set; }
		public static string? YoutubeKey { get; private set; }

		static Env() {
			Reload();
		}

		public static void Reload() {
			string[] lines;
			if(!File.Exists(Options.EnvVarsPath)) {
				File.Create(Options.EnvVarsPath).Close();
				lines = new[] { nameof(YoutubeKey), nameof(DiscordToken) };
				File.WriteAllLines(Options.EnvVarsPath, lines.Select(x => x + "="));
				Log.Error($"CRITIAL: Environment file not found. Fill in the {Options.EnvVarsPath} file variables and retry.");
				Environment.Exit(0);
			}
			lines = File.ReadAllLines(Options.EnvVarsPath);
			
			// Could handle with reflection
			foreach(string line in lines) {
				string[] parts = line
					.Split('=', 2)
					.Select(x => x.Trim('\n', ' '))
					.ToArray();
				if(parts.Length < 2) 
					continue;

				switch(parts[0]) {
					case nameof(YoutubeKey):
						YoutubeKey = parts[1];
						break;
					case nameof(DiscordToken):
						DiscordToken = parts[1];
						break;
					default:
						break;
				}
			}
		}
	}
}
