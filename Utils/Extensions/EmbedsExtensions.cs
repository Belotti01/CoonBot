namespace CoonBot.Utils {
	public static class EmbedsExtensions {

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

		public static DiscordEmbedBuilder WithDescription(this DiscordEmbedBuilder builder, DiscordUser user, string description) {
			if(string.IsNullOrWhiteSpace(description))
				return builder;
			
			return builder
				.WithDescription($"{user.Username}, {description}");
		}

	}
}
