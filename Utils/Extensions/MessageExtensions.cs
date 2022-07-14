namespace CoonBot.Utils {
	public static class MessageExtensions {
		public static async Task<DiscordMessage> SendEmbedAsync(this DiscordEmbedBuilder embed, CommandContext ctx)
			=> await embed.SendEmbedAsync(ctx.Channel);

		public static async Task<DiscordMessage> SendEmbedAsync(this DiscordEmbedBuilder embed, DiscordChannel channel) {
			return await channel.SendMessageAsync(embed);
		}

		public static async Task<DiscordMessage> RespondAsync(this DiscordEmbedBuilder embed, CommandContext ctx) {
			return await ctx.RespondAsync(embed);
		}

		public static DiscordEmbedBuilder CreateEmbed(this CommandContext ctx, bool setDefaults = true) {
			DiscordEmbedBuilder builder = new();
			if(setDefaults) {
				builder.SetDefaults();
			}
			return builder;
		}

		public static DiscordEmbedBuilder CreateErrorEmbed(this CommandContext ctx, string description = "An unknown error occurred.") {
			DiscordEmbedBuilder builder = new();

			return builder
				.SetDefaults()
				.WithDescription(description)
				.WithColor(Options.Colors.CommandError);
		}


	}
}
