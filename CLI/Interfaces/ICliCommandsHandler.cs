namespace CoonBot.CLI {
	public interface ICliCommandsHandler {
		bool ExitRequested { get; }

		bool TryExecute(string input);
	}
}
