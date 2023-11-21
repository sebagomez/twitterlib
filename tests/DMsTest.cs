
using System;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class DMTests : BaseTests
	{
		[Fact]
		public async Task SendDM()
		{
			try
			{
				DMOptions options = new DMOptions { User = m_user, Text = "Hi!, I'm running tests!", RecipientId = "9096202" };
				string result = await DirectMessages.SendDM(options);

				Assert.True(result == "OK");

			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task ListrDMs()
		{
			try
			{
				DMListOptions options = new DMListOptions { User = m_user };
				EventList result = await DirectMessages.GetDMList(options);

				Assert.True(result != null);
				Assert.True(result.events != null);
				Assert.True(result.events.Count > 0);

			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}
	}
}
