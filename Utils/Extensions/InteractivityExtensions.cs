namespace CoonBot.Utils {
	public static class InteractivityExtensions {

		private static async Task<DiscordMessage> SendRequestMessageAsync(this CommandContext ctx, string request) {
			return await Embeds
				.Request
				.WithDescription(ctx.User, request)
				.SendEmbedAsync(ctx);
		}

		public static async Task<DiscordMessage?> AwaitResponseAsync(this CommandContext ctx, string? request = null) {
			DiscordMessage? requestMessage = null;

			if(!string.IsNullOrWhiteSpace(request)) {
				requestMessage = await ctx.SendRequestMessageAsync(request);
			}

			var result = await Bot.Interactivity
				.WaitForMessageAsync(x => x.Author == ctx.User);

			if(result.TimedOut) {
				if(requestMessage is not null) {
					await requestMessage.ModifyAsync(Embeds.TimedOut.Build());
				}
				return null;
			}

			if(requestMessage is not null) {
				await requestMessage.DeleteAsync();
				await result.Result.DeleteAsync();
			}
			return result.Result;
		}

	}
}
