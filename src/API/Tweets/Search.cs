﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Search : BaseAPI
	{
		const int MAX_QUERY_LENGTH = 500;
		const string SEARCH_URL = "https://api.twitter.com/1.1/search/tweets.json";

		public static async Task<SearchResult> SearchTweets(SearchOptions options)
		{
			CheckData(options);

			if (string.IsNullOrEmpty(options.Query))
				throw new ArgumentNullException("query","The text query cannot be null");

			if (options.Query.Length > MAX_QUERY_LENGTH)
				throw new IndexOutOfRangeException($"Query too long. The query string cannot exceed {MAX_QUERY_LENGTH} chars.");

			options.Query = Util.EncodeString(WebUtility.HtmlDecode(options.Query));

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, SEARCH_URL, options);

			return await GetData<SearchResult>(reqMsg);
		}
	}
}
