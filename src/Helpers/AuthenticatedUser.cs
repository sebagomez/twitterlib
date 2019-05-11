using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Sebagomez.TwitterLib.API.OAuth;

namespace Sebagomez.TwitterLib.Helpers
{
	public class AuthenticatedUser
	{

		const string USER_FILE = "twit.usr";
		const string OAUTH_TOKEN = "oauth_token";
		const string OAUTH_TOKEN_SECRET = "oauth_token_secret";

		static string s_configFile = Path.Combine(Util.FilesLocation, USER_FILE);

		public AppCredentials AppSettings { get; set; }

		public string OAuthToken { get; set; }

		public string OAuthTokenSecret { get; set; }

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

		public static AuthenticatedUser GetUserCrdentials(string username)
		{
			username = username.Replace(Path.DirectorySeparatorChar, '.');

			string userPath = Path.Combine(Util.FilesLocation, username);

			if (!File.Exists(userPath))
				return null;

			return Deserialize();
		}

		public void SaveUserCredentials(string username)
		{
			username = username.Replace(Path.DirectorySeparatorChar, '.');

			string userPath = Path.Combine(Util.FilesLocation, username);

			Serialize(userPath);
		}

		public void SerializeTokens(string accessTokens)
		{
			string[] tokens = accessTokens.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string tok in tokens)
			{
				string[] props = tok.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (props[0] != OAUTH_TOKEN && props[0] != OAUTH_TOKEN_SECRET)
					continue;

				if (props[0] == OAUTH_TOKEN)
					OAuthToken = props[1];
				else if (props[0] == OAUTH_TOKEN_SECRET)
					OAuthTokenSecret = props[1];

				if (!string.IsNullOrEmpty(OAuthToken) && !string.IsNullOrEmpty(OAuthTokenSecret))
					break;
			}

			Serialize();
		}

		private void Serialize()
		{
			Serialize(s_configFile);
		}

		public void Serialize(string fileName)
		{
			Util.Serialize(this, fileName);
		}

		static AuthenticatedUser Deserialize()
		{
			using (FileStream file = File.Open(s_configFile, FileMode.Open))
				return Util.Deserialize<AuthenticatedUser>(file);
		}

		public static void ClearCredentials()
		{
			if (File.Exists(s_configFile))
				File.Delete(s_configFile);
		}
	}
}
