using System;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class SearchTest : BaseTests
	{
		[Fact]
		public async Task SearchWord()
		{
			try
			{
				SearchOptions options = new SearchOptions { Query = "genexus", User = m_user };
				SearchResult result = await Sebagomez.TwitterLib.API.Tweets.Search.SearchTweets(options);
				Assert.True(result.search_metadata.count >= result.statuses.Length, "OK");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task SearchManyWords()
		{
			try
			{
				SearchOptions options = new SearchOptions { Query = "genexus uruguay", User = m_user };
				SearchResult result = await Sebagomez.TwitterLib.API.Tweets.Search.SearchTweets(options);
				Assert.True(result.search_metadata.count >= result.statuses.Length, "OK");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task SearchHashtag()
		{
			try
			{
				SearchOptions options = new SearchOptions { Query = "#Trump", User = m_user };
				SearchResult result = await Sebagomez.TwitterLib.API.Tweets.Search.SearchTweets(options);
				Assert.True(result.search_metadata.count >= result.statuses.Length, "OK");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task SearchCount()
		{
			try
			{
				int count = 3;
				SearchOptions options = new SearchOptions { Query = "uruguay", Count = count, User = m_user };
				SearchResult result = await Sebagomez.TwitterLib.API.Tweets.Search.SearchTweets(options);
				Assert.True(result.search_metadata.count >= result.statuses.Length, "OK");
				Assert.True(result.statuses.Length == count, "OK");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task SearchSince()
		{
			try
			{
				SearchOptions options = new SearchOptions { Query = "uruguay", User = m_user };
				SearchResult result = await Sebagomez.TwitterLib.API.Tweets.Search.SearchTweets(options);
				Assert.True(result.search_metadata.count >= result.statuses.Length, "OK");
				options = new SearchOptions { Query = "uruguay", SinceId = result.search_metadata.max_id, User = m_user };
				result = await Sebagomez.TwitterLib.API.Tweets.Search.SearchTweets(options);
				Assert.True(result.statuses.Length >= 0, "OK");

			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}
	}
}
