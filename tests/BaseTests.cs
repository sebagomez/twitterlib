using System;
using System.IO;
using Sebagomez.TwitterLib.Helpers;
using Xunit.Abstractions;

namespace TwitterLibTests
{
	public partial class BaseTests
	{

		internal const string RESOURCES_FOLDER = "Resources";
		internal const string MEDIA1_NAME = "Noooooooo.jpg";
		internal const string MEDIA2_NAME = "Snapshot.jpg";
		internal const string HUGE_MEDIA = "BiggerThan5MB.jpg";

		protected AuthenticatedUser m_user;
		protected AppCredentials m_app;

		protected AuthenticatedUser LoadTestUser(string userhandle)
		{
			using (FileStream file = File.OpenRead(Path.Combine(ResourcesDirectory, $"{userhandle}.usr")))
				return Util.Deserialize<AuthenticatedUser>(file);
		}

		string m_resourcesDir;
		protected string ResourcesDirectory => m_resourcesDir ?? (m_resourcesDir = Path.Combine(AppContext.BaseDirectory, RESOURCES_FOLDER));

		public BaseTests()
		{
			m_app = AppCredentials.Initialize(Credentials.Current.SHELLTWIT_KEY, Credentials.Current.SHELLTWIT_SECRET);
			m_user = LoadTestUser("sebatestapi");
		}

		private readonly ITestOutputHelper output;

		public BaseTests(ITestOutputHelper output)
			: this()
		{
			this.output = output;

			Sebagomez.TwitterLib.API.Tweets.Update.SetMessageAction(s =>
			{
				this.output.WriteLine(s);
			});
		}

	}
}
