using System.Reflection;

namespace CoonBot.CLI {
	public class CliCommandsHandler : ICliCommandsHandler {
		protected readonly CommandsData Commands;
		public bool ExitRequested { get; protected set; } = false;

		public CliCommandsHandler(Type commandsContainer) {
			Commands = new CommandsData()
				.Add(new() {
					Aliases = new[] { "help", "h" },
					Description = "Shows this help message."
				}, SendHelpMessage);

			CliCommandAttribute? commandData;
			var methods = commandsContainer
				.GetMethods();
			
			foreach(var method in methods) {
				commandData = method.GetCustomAttribute<CliCommandAttribute>();
				if(commandData is null)
					continue;

				Commands.Add(new() {
					Aliases = commandData.aliases,
					Description = commandData.description
				}, method.CreateDelegate<Action>());
			}
		}

		protected void SendHelpMessage() {
			string helpMessage = Commands.GetHelpMessage();
			Log.Write(helpMessage, ConsoleColor.Yellow);
		}

		public bool TryExecute(string input) {
			if(Commands.CanRun(input)) {
				try {
					Commands.Run(input);
					return true;
				} catch(CommandException ex) {
					Log.Warning(ex.Message);
				}catch(Exception ex) {
					Log.Error($"Error while executing command:\n- Command: {input}\n- Error: {ex.Message}");
				}
			}

			return false;
		}
	}
}
