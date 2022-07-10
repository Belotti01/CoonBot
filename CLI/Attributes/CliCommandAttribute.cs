namespace CoonBot.CLI {
	public class CliCommandAttribute : Attribute {
		public string[] aliases;
		public string? description;

		public CliCommandAttribute(params string[] aliases) {
			this.aliases = aliases;
		}
	}
}
