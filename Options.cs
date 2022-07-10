﻿namespace CoonBot {
	public static class Options {

		public const string API_KEY_FILENAME = "ConnectionKey.config";
		public static string ApiKeyFilepath => Path.Combine(Environment.CurrentDirectory, API_KEY_FILENAME);

		public static class Bot {
			public static readonly string[] CommandPrefixes = { "coon" };
		}
	}
}