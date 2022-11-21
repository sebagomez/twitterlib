using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwitterLibTests
{
	public sealed class Credentials
	{
		public static Credentials Current = new Credentials();

		const string SHELLTWIT_API_KEY = "SHELLTWIT-API-KEY";
		const string SHELLTWIT_API_SECRET = "SHELLTWIT-API-SECRET";


#if DEBUG

		private Credentials()
		{
			if (File.Exists("Properties\\launchSettings.json"))
			{
				using (var file = File.OpenText("Properties\\launchSettings.json"))
				{
					var reader = new JsonTextReader(file);
					var jObject = JObject.Load(reader);

					var variables = jObject
						.GetValue("profiles")
						.SelectMany(profiles => profiles.Children())
						.SelectMany(profile => profile.Children<JProperty>())
						.Where(prop => prop.Name == "environmentVariables")
						.SelectMany(prop => prop.Value.Children<JProperty>())
						.ToList();

					foreach (var variable in variables)
					{
						Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
					}
				}
			}
		}
#endif

		#region Properties

		private string m_apiSecret;
		public string SHELLTWIT_SECRET
		{
			get
			{
				if (string.IsNullOrEmpty(m_apiSecret))
					m_apiSecret = Environment.GetEnvironmentVariable(SHELLTWIT_API_SECRET);

				return m_apiSecret;
			}
		}

		private string m_apiKey;
		public string SHELLTWIT_KEY
		{
			get
			{
				if (string.IsNullOrEmpty(m_apiKey))
					m_apiKey = Environment.GetEnvironmentVariable(SHELLTWIT_API_KEY);

				return m_apiKey;
			}
		}

		#endregion
	}
}
