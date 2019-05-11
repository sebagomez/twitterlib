using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class UserTimeline : BaseAPI
	{
		const string USER_TIMELINE = "https://api.twitter.com/1.1/statuses/user_timeline.json";

		public static async Task<List<Status>> GetUserTimeline(UserTimelineOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, USER_TIMELINE, options);

			return await GetData<Statuses>(reqMsg);
		}
	}
}
