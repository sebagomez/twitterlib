using System;

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
					System.Diagnostics.Debug.Write(status);
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
			StreamingOptions options = new StreamingOptions { Track = "trump", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingTimelineWithSpace()
		{
			StreamingOptions options = new StreamingOptions { Track = "twitter com", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingTimelineWithComa()
		{
			StreamingOptions options = new StreamingOptions { Track = "twitter,facebook", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingTimelineWithHashtag()
		{
			StreamingOptions options = new StreamingOptions { Track = "#Trump", User = m_user };
			Execute(options);
		}

		[Fact]
		public void GetStreamingUserTimeline()
		{
			//sebatestapi 108356361
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
