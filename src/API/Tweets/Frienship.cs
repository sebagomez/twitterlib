using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Frienship : BaseAPI
	{
		const string FRIENDSHIP_TEMPLATE = "https://api.twitter.com/1.1/friendships/{0}";
		const string CREATE = "create.json";
		const string DESTROY = "destroy.json";

		public static async Task<User> Follow(FriendshipOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, string.Format(FRIENDSHIP_TEMPLATE, CREATE), options);

			return await GetData<User>(reqMsg);
		}

		public static async Task<User> Unfollow(FriendshipOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, string.Format(FRIENDSHIP_TEMPLATE, DESTROY), options);

			return await GetData<User>(reqMsg);
		}
	}
}
