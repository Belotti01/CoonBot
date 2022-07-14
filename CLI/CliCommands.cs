namespace CoonBot.CLI {
	// Dump of all Console commands, which are automatically implemented in the CliCommandsHandler constructor.
	// No need to properly structure these as they're few and only for debugging purposes.

	public static class CliCommands {
		[CliCommand("ping", description = "Retrieve the ping to the Discord service.")]
		public static void Ping() {
			int ping = Bot.Client.Ping;
			string pingMessage = $"Discord Service ping: {ping}ms";
			
			if(ping > 250) {
				Log.Fail(pingMessage);
			}else {
				Log.Success(pingMessage);
			}
		}
		
		[CliCommand("close", "exit", "quit", "disconnect", "dc", description = "Disconnect the Bot and halt the application.")]
		public static async void CloseAsync() {
			Log.Write("Disconnecting...");
			await Bot.Client.DisconnectAsync();
			Log.Write("Closing...");
			Bot.Client.Dispose();
			Environment.Exit(0);
		}
	}
}
