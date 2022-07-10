namespace CoonBot.CLI {
	// Dump of all Console commands, which are automatically implemented in the CliCommandsHandler constructor.
	// No need to properly structure these as they're mostly for debugging purposes.

	public static class CliCommands {
		[CliCommand("ping", description = "Retrieve the ping to the Discord service.")]
		public static void Ping() {
			int ping = Program.Client.Ping;
			string pingMessage = $"Discord Service ping: {ping}ms";
			
			if(ping >= 200) {
				Log.Fail(pingMessage);
			}else {
				Log.Success(pingMessage);
			}
		}
		
		[CliCommand("close", "exit", "quit", "disconnect", "dc", description = "Disconnect the Bot and halt the application.")]
		public static async void CloseAsync() {
			Log.Write("Disconnecting...");
			await Program.Client.DisconnectAsync();
			Log.Write("Closing...");
			Program.Client.Dispose();
			Environment.Exit(0);
		}
	}
}
