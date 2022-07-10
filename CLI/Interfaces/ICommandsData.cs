namespace CoonBot.CLI {
	public interface ICommandsData<TSelf> where TSelf : ICommandsData<TSelf> {
		TSelf Add(CommandInformation command, Action handler);
		bool CanRun(string command);
		string GetHelpMessage();
		void Run(string command);
	}
}