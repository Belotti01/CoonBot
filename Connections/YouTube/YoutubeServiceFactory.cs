using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace CoonBot.Connections.YouTube {
	public class YoutubeServiceFactory {
		private static readonly Lazy<YoutubeServiceFactory> _shared = new(() => new YoutubeServiceFactory("youtube-key"));
		public static YoutubeServiceFactory Shared => _shared.Value;

        protected string? apiKey;

        public YoutubeServiceFactory(string apiKeyEnvVarName) {
			apiKey = Environment.GetEnvironmentVariable(apiKeyEnvVarName);
		}
		
		public YouTubeService? CreateService() {
            YouTubeService? service = null;
			
			if(apiKey is null)
                Log.Error($"Failed to connect to the YouTube Service:\r\nEnvironment variable not found.");
            try {
                service = new(new BaseClientService.Initializer() {
                    ApiKey = apiKey,
                    ApplicationName = typeof(Program).Namespace
                });
            }catch(Exception ex) {
                Log.Error($"Failed to connect to the YouTube Service:\r\n{ex.Message}");
            }

            return service;
        }
    }
}
