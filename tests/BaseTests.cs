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

		protected AuthenticatedUser LoadTestUser(string userhandle)
		{
			string fullPath = Path.Combine(ResourcesDirectory, $"{userhandle}.data");
			if (!File.Exists(fullPath))
				throw new FileNotFoundException($"Could not find user file:{userhandle}", fullPath);

			return AuthenticatedUser.Deserialize(fullPath);
		}

		string m_resourcesDir;
		protected string ResourcesDirectory => m_resourcesDir ?? (m_resourcesDir = Path.Combine(AppContext.BaseDirectory, RESOURCES_FOLDER));

		public BaseTests()
		{
			m_user = LoadTestUser("sebatestapi");
			//m_user = LoadTestUser("sebagomez");
			//m_user = LoadTestUser("isit737MAX");
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
