using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Likes : BaseAPI
	{
		const string USER_LIKES = "https://api.twitter.com/1.1/favorites/list.json";

		public static async Task<List<Status>> GetUserLikes(LikesOptions options)
		{
			if (options.User == null)
				options.User = AuthenticatedUser.CurrentUser;

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, USER_LIKES, options);

			return await GetData<Statuses>(reqMsg);
		}
	}
}
