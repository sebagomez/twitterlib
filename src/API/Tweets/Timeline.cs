using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Timeline : BaseAPI
	{
		const string HOME_TIMELINE = "https://api.twitter.com/1.1/statuses/home_timeline.json";

		public static async Task<List<Status>> GetTimeline(TimelineOptions options)
		{
			if (options.User == null)
				options.User = AuthenticatedUser.CurrentUser;

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, HOME_TIMELINE, options);
			
			return await GetData<Statuses>(reqMsg);
		}
	}
}
