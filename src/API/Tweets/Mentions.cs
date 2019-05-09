using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Mentions : BaseAPI
	{
		const string MENTIONS_STATUS = "https://api.twitter.com/1.1/statuses/mentions_timeline.json";

		public static async Task<List<Status>> GetMentions(MentionsOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, MENTIONS_STATUS, options);

			return await GetData<Statuses>(reqMsg);
		}
	}
}
