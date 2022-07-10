namespace CoonBot {
	public static class Log {

		public static void Write(string message, ConsoleColor color = ConsoleColor.White, string? prefix = null) {
			if(prefix is not null) {
				message = Prefix(prefix, message);
			}
			Output.WriteLine(message, color);
		}

		public static void Info(string message)
			=> Write(message, ConsoleColor.Cyan, "INFO");

		public static void Success(string message)
			=> Write(message, ConsoleColor.Green);

		public static void Fail(string message)
			=> Write(message, ConsoleColor.Red);

		public static void Error(string message)
			=> Write(message, ConsoleColor.Red, "ERR ");

		public static void Warning(string message)
			=> Write(message, ConsoleColor.Yellow, "WARN");

		public static void Debug(string message)
			=> Write(message, ConsoleColor.Gray, "TEST");

		private static string Prefix(string prefix, string message) {
			return $"[{prefix}] {message}";
		}
	}
}
