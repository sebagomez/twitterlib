
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class LikesTest : BaseTests
	{
		[Fact]
		public async Task GetUserLikes()
		{
			try
			{
				LikesOptions options = new LikesOptions { ScreenName = "sebagomez", User = m_user };
				List<Sebagomez.TwitterLib.Entities.Status> ss = await Likes.GetUserLikes(options);
				Assert.True(ss.Count > 0, "Ningún tweet!");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
