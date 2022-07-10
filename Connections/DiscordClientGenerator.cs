namespace CoonBot.Connections {
	public class DiscordClientFactory : IDiscordClientFactory {
		protected string _apiToken;
		public DiscordConfiguration ClientConfiguration { get; protected set; }

		public DiscordClientFactory(string apiToken) {
			_apiToken = apiToken;

			ClientConfiguration = new() {
				TokenType = TokenType.Bot,
				AutoReconnect = true
			};
		}


		public DiscordClient CreateClient() {
			//TODO: Token can not be set for some reason when using this constructor...
			//DiscordConfiguration config = new(ClientConfiguration) {
			//	Token = _apiToken
			//};
			//DiscordClient client = new(config);

			//<PLACEHOLDER>
			ClientConfiguration.Token = _apiToken;
			DiscordClient client = new(ClientConfiguration);
			//</PLACEHOLDER>
			
			client.ClientErrored += OnClientErrored;
			client.SocketErrored += OnSocketErrored;

			return client;
		}


		#region EventHandlers
		protected virtual Task OnClientErrored(DiscordClient sender, ClientErrorEventArgs e) {
			Log.Error($"Client Error: {e.Exception.Message}");
			return Task.CompletedTask;
		}

		protected virtual Task OnSocketErrored(DiscordClient sender, SocketErrorEventArgs e) {
			Log.Error($"Socket Error: {e.Exception.Message}");
			return Task.CompletedTask;
		}
		#endregion

	}
}
