namespace CoonBot.CLI {
	public class CliObserver {
		protected readonly ICliCommandsHandler _cliCommandsHandler = new CliCommandsHandler(typeof(CliCommands));
		public bool ExitRequested => _cliCommandsHandler.ExitRequested;
		
		public async Task RunAsync() {
			Task task = new(Run);
			task.Start();
			await task;
		}

		public void Run() {
			string? input;

			while(!(ExitRequested)) {
				input = Console.ReadLine();
				if(string.IsNullOrWhiteSpace(input)) {
					continue;
				}

				_cliCommandsHandler.TryExecute(input);
			}
		}
		
	}
}
