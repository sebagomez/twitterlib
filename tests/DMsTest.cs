
using System;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
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
				DMOptions options = new DMOptions { User = m_user, Text = "Hello World!", RecipientId = "9096202" };
				string result = await DirectMessages.SendDM(options);

				Assert.True(result == "OK");

			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
