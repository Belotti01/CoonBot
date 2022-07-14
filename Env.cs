namespace CoonBot {
	public static class Env {
		public static string? DiscordToken { get; private set; }
		public static string? YoutubeKey { get; private set; }

		static Env() {
			Reload();
#if DEBUG
			Reload(true);
#endif
		}

		public static void Reload(bool useDebugFile = false) {
			string path = useDebugFile
				? Options.DebugEnvVarsPath
				: Options.EnvVarsPath;
			
			string[] lines;
			if(!File.Exists(path)) {
				File.Create(path).Close();
				lines = new[] { nameof(YoutubeKey), nameof(DiscordToken) };
				File.WriteAllLines(path, lines.Select(x => x + "="));
				if(!useDebugFile) {
					Log.Error($"CRITIAL: Environment file not found. Fill in the {path} file variables and retry.");
					Environment.Exit(0);
				}
			}
			lines = File.ReadAllLines(path);
			
			// Could handle with reflection
			foreach(string line in lines) {
				string[] parts = line
					.Split('=', 2)
					.Select(x => x.Trim('\n', ' '))
					.ToArray();
				
				if(parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1])) 
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
