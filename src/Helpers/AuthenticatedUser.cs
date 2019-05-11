using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Sebagomez.TwitterLib.Helpers
{
	public class AuthenticatedUser
	{
		const string OAUTH_TOKEN = "oauth_token";
		const string OAUTH_TOKEN_SECRET = "oauth_token_secret";
		const string USER_ID = "user_id";
		const string SCREEN_NAME = "screen_name";

		public AppCredentials AppSettings { get; set; }
		public string OAuthToken { get; set; }
		public string OAuthTokenSecret { get; set; }
		public string UserId { get; set; }
		public string ScreenName { get; set; }

		public AuthenticatedUser() { }

		public AuthenticatedUser(string token, string tokensecret)
		{
			OAuthToken = token;
			OAuthTokenSecret = tokensecret;
		}

		public bool IsOk()
		{
			return !string.IsNullOrEmpty(OAuthToken) &&
					!string.IsNullOrEmpty(OAuthTokenSecret) &&
					AppSettings != null &&
					!string.IsNullOrEmpty(AppSettings.AppKey) &&
					!string.IsNullOrEmpty(AppSettings.AppSecret);
		}

		public void SaveUserCredentials(string username)
		{
			username = username.Replace(Path.DirectorySeparatorChar, '.');

			string userPath = Path.Combine(Util.FilesLocation, username);

			Serialize(userPath);
		}

		public void ParseTokens(string accessTokens)
		{
			string[] tokens = accessTokens.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string tok in tokens)
			{
				string[] props = tok.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

				if (props[0] == OAUTH_TOKEN)
					OAuthToken = props[1];
				else if (props[0] == OAUTH_TOKEN_SECRET)
					OAuthTokenSecret = props[1];
				else if (props[0] == USER_ID)
					UserId = props[1];
				else if (props[0] == SCREEN_NAME)
					ScreenName = props[1];
			}
		}

		public void Serialize(string fileName)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AuthenticatedUser));
				serializer.WriteObject(ms, this);
				string serialized = Encoding.Default.GetString(ms.ToArray());

				var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(serialized);
				string encoded = System.Convert.ToBase64String(plainTextBytes);

				File.WriteAllText(fileName, encoded);
			}
		}

		public static AuthenticatedUser Deserialize(string filePath)
		{
			if (!File.Exists(filePath))
				return null;

			try
			{
				using (StreamReader file = File.OpenText(filePath))
				{
					string content = file.ReadToEnd();
					var base64EncodedBytes = Convert.FromBase64String(content);
					string decoded = Encoding.UTF8.GetString(base64EncodedBytes);

					return Util.Deserialize<AuthenticatedUser>(decoded);
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
