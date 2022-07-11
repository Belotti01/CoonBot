namespace CoonBot.Utils {
	public static class DiscordEmbedsExtensions {

		public static DiscordEmbedBuilder SetDefaults(this DiscordEmbedBuilder builder) {
			return builder.WithColor(Options.Colors.Default)
				.WithTimestamp(DateTime.Now)
				.WithFooter("CoonBot");
		}

		public static DiscordEmbedBuilder WithAuthor(this DiscordEmbedBuilder builder, DiscordUser user, bool? useAvatar = null) {
			builder.WithAuthor(user.Username);
			useAvatar ??= string.IsNullOrEmpty(builder.Author.IconUrl);

			if(useAvatar.Value) {
				builder.Author.IconUrl = user.AvatarUrl;
			}
			return builder;
		}
	
	}
}
