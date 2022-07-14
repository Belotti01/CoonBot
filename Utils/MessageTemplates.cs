
namespace CoonBot.Utils {
	public static class Embeds {
		public static DiscordEmbedBuilder Default => 
			new DiscordEmbedBuilder()
			.SetDefaults();

		public static DiscordEmbedBuilder Error =>
			new DiscordEmbedBuilder()
			.SetDefaults()
			.WithColor(DiscordColor.Red);

		public static DiscordEmbedBuilder Request =>
			new DiscordEmbedBuilder()
			.SetDefaults()
			.WithColor(DiscordColor.Yellow);

		public static DiscordEmbedBuilder Aborted =>
			new DiscordEmbedBuilder()
			.SetDefaults()
			.WithColor(DiscordColor.Red);
		
		public static DiscordEmbedBuilder TimedOut =>
			new DiscordEmbedBuilder()
			.SetDefaults()
			.WithDescription("Request timed out.")
			.WithColor(DiscordColor.Red);
	}
}
