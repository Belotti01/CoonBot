
namespace CoonBot.Connections {
	// Encodes the API key into a Base64 Key just to avoid leaving the key in plain sight.
	public class TokenReader {
		protected readonly string filepath;

		public TokenReader(string filepath) {
			this.filepath = filepath;
		}

		public void Set(string apiKey) {
			string encodedKey = Encode(apiKey);
			File.WriteAllText(filepath, encodedKey);
		}
		
		public bool TryRead([NotNullWhen(true)] out string? apiKey) {
			apiKey = null;
			
			if(!Exists())
				return false;

			string encodedKey = File.ReadAllText(filepath);

			apiKey = Decode(encodedKey);
			return true;
		}

		public void Delete() {
			if(Exists()) {
				File.Delete(filepath);
			}
		}

		public bool Exists() 
			=> File.Exists(filepath);

		
		#region Security
		protected static string Encode(string encodedKey) {
			byte[] unicode = Encoding.Unicode.GetBytes(encodedKey);
			string key = Convert.ToBase64String(unicode);

			return key;
		}

		protected static string Decode(string key) {
			byte[] decoded = Convert.FromBase64String(key);
			string encodedKey = Encoding.Unicode.GetString(decoded);

			return encodedKey;
		}
		#endregion
	}
}
