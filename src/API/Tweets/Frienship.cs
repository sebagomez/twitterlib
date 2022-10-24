using System.Collections.Generic;
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
		const string FRIENDS_IDS = "https://api.twitter.com/1.1/friends/ids.json";
		const string FRIENDS_LIST = "https://api.twitter.com/1.1/friends/list.json";
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

		public static async Task<FriendList> ListFollowers(FollowerListOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, FRIENDS_LIST, options);

			return await GetData<FriendList>(reqMsg);
		}

		public static async Task<FriendIDsList> ListFollowersIDs(FollowerListOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, FRIENDS_IDS, options);

			return await GetData<FriendIDsList>(reqMsg);
		}
	}
}
