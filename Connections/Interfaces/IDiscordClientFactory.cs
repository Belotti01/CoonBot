namespace CoonBot.Connections {
	public interface IDiscordClientFactory {
		DiscordConfiguration ClientConfiguration { get; }

		DiscordClient CreateClient();
	}
}