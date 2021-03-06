﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class UserData : BaseAPI
	{
		const string SHOW_USER_URL = "https://api.twitter.com/1.1/users/show.json";

		public static async Task<User> GetUser(UserShowOptions options)
		{
			CheckData(options);

			if (string.IsNullOrEmpty(options.UserId) && string.IsNullOrEmpty(options.ScreenName))
				throw new Exception("You mst set either a screen name or a user id");

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, SHOW_USER_URL, options);

			return await GetData<User>(reqMsg);
		}
	}
}
