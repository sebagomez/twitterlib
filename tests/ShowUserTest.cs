using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class ShowUserTest : BaseTests
	{
		[Fact]
		public async Task GetSebaGomez()
		{
			await InternalGetUser("sebagomez");
		}

		[Fact]
		public async Task GetSebaTestAPI()
		{
			await InternalGetUser("sebatestapi");
		}

		private async Task InternalGetUser(string screenname)
		{
			try
			{
				UserShowOptions options = new UserShowOptions { ScreenName = screenname, User = m_user };
				User user = await Sebagomez.TwitterLib.API.Tweets.UserData.GetUser(options);
				Assert.True(user.screen_name == screenname, "OK");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
