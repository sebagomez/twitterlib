using System;
using System.Threading;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class StreamingAPITests : BaseTests
	{
		void Execute(StreamingOptions options)
		{
			try
			{
				StreamingEndpoint streamingFilter = new StreamingEndpoint();
				int count = 0;
				foreach (Status status in streamingFilter.GetStreamingStatus(options))
				{
					System.Diagnostics.Debug.WriteLine(status);
					count++;

					if (count == 5)
						break;
				}

			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public void GetStreamingTimeline()
		{
			Thread.Sleep(2000);
			StreamingOptions options = new StreamingOptions { Track = "twitter", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingTimelineWithSpace()
		{
			Thread.Sleep(2000);
			StreamingOptions options = new StreamingOptions { Track = "twitter com", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingTimelineWithComa()
		{
			Thread.Sleep(2000);
			StreamingOptions options = new StreamingOptions { Track = "twitter,facebook", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingTimelineWithHashtag()
		{
			Thread.Sleep(2000);
			StreamingOptions options = new StreamingOptions { Track = "#twitter", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingUserTimeline()
		{
			StreamingOptions options = new StreamingOptions { User = m_user, Follow = "sebatestapi" };
			try
			{
				StreamingEndpoint streamingService = new StreamingEndpoint();

				int count = 0;
				foreach (Status status in streamingService.GetStreamingStatus(options))
				{
					System.Diagnostics.Debug.Write(status);
					count++;

					if (count == 1)
						break;
				}
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
