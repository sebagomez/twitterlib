using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Timeline : BaseAPI
	{
		const string HOME_TIMELINE = "https://api.twitter.com/1.1/statuses/home_timeline.json";
		const string SHOW_STATUS = "https://api.twitter.com/1.1/statuses/show.json";

		public static async Task<List<Status>> GetTimeline(TimelineOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, HOME_TIMELINE, options);
			
			return await GetData<Statuses>(reqMsg);
		}

		public static async Task<Status> GetStatus(StatusOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, SHOW_STATUS, options);

			return await GetData<Status>(reqMsg);
		}
	}
}
