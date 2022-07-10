namespace CoonBot.CLI {
	public class CommandsData : ICommandsData<CommandsData> {
		protected List<CommandInformation> Commands = new();
		protected Dictionary<string, Action> CommandHandlers = new();

		public CommandsData Add(CommandInformation command, Action handler) {
			Commands.Add(command);
			foreach(string alias in command.Aliases) {
				var task = new Task(handler);
				CommandHandlers.Add(alias, handler);
			}
			return this;
		}


		public string GetHelpMessage() {
			StringBuilder messageBuilder = new();

			foreach(var command in Commands.OrderBy(x => x.Aliases.First().ToLower())) {
				messageBuilder.AppendLine(string.Join(" | ", command.Aliases));

				if(!string.IsNullOrWhiteSpace(command.Description))
					messageBuilder
						.AppendLine("\t- Description:")
						.AppendLine($"\t\t{command.Description}");

				if(!string.IsNullOrWhiteSpace(command.Usage))
					messageBuilder
						.AppendLine("\t- Usage:")
						.AppendLine($"\t\t{command.Usage}");

				messageBuilder.AppendLine();
			}

			return messageBuilder.ToString();
		}

		public bool CanRun(string command) {
			return CommandHandlers.ContainsKey(command);
		}

		public void Run(string command) {
			CommandHandlers[command].Invoke();
		}

	}

	public struct CommandInformation {
		public string[] Aliases;
		public string? Description;
		public string? Usage;
	}
}
