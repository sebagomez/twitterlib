using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class TimelineTest : BaseTests
	{
		[Fact]
		public async Task GetTimeline()
		{
			try
			{
				List<Sebagomez.TwitterLib.Entities.Status> ss = await Timeline.GetTimeline(new TimelineOptions { User = m_user });
				Assert.True(ss.Count > 0, "Ningún tweet!");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task GetUserTimeline()
		{
			try
			{
				UserTimelineOptions options = new UserTimelineOptions { ScreenName = "sebagomez", ExcludeReplies = true, IncludeRTs = false, User = m_user };
				List<Sebagomez.TwitterLib.Entities.Status> ss = await UserTimeline.GetUserTimeline(options);
				Assert.True(ss.Count > 0, "Ningún tweet!");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task GetMaxUserTimeline()
		{
			try
			{
				int count = 200;
				UserTimelineOptions options = new UserTimelineOptions { ScreenName = "sebagomez", Count = count, User = m_user };
				List<Sebagomez.TwitterLib.Entities.Status> ss = await UserTimeline.GetUserTimeline(options);
				Assert.True(ss.Count > 0, "Ningún tweet!");
				Assert.True(ss.Count <= count, $"No trajo {count}?");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task GetStatus()
		{
			try
			{
				Sebagomez.TwitterLib.Entities.Status s = await Timeline.GetStatus(new StatusOptions { User = m_user, Id = "20" });
				Assert.True(s != null, "No twit!");
				Assert.True(s.text == "just setting up my twttr", "Wrong twit text!");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}
	}
}
