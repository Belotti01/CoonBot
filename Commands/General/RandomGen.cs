namespace CoonBot.Commands {
	public class RandomGen : CommandBase {
		[SlashCommand("random", "Generate a random number.")]
		[Command("random"), Aliases("rng", "pick", "roll")]
		[Description("Generate a random number.")]
		// MIN & MAX args
		public async Task Generate(CommandContext msg, [Description("Minimum value.")] int min, [Description("Maximum value.")] int max) {
			int rng;
			//Reorder min-max
			if(min > max) {
				(max, min) = (min, max);
			}

			if(min == max) {
				await msg.Channel.SendMessageAsync($"... {min}");
				return;
			}

			try {
				rng = new Random().Next(min, max + 1);
			} catch(Exception) {
				await msg.Channel.SendMessageAsync("Please input valid values.");
				return;
			}

			await msg.Channel.SendMessageAsync($"You rolled {rng}.");
		}

		//From 0 to MAX
		[Command("random")]
		public async Task Generate(CommandContext msg, [Description("Maximum value.")] int max)
			=> await Generate(msg, 0, max);
	}
}
