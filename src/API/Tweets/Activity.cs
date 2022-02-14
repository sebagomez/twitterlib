using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Activity : BaseAPI
	{
		const string ADD_WEBHOOK_URL = "https://api.twitter.com/1.1/account_activity/all/{0}/webhooks.json";
		const string ENVIRONMENTS_URL = "https://api.twitter.com/1.1/account_activity/all/webhooks.json";
		const string DELETE_WEBHOOK_URL = "https://api.twitter.com/1.1/account_activity/all/{0}/webhooks/{1}.json";
		const string SUBSCRIBE_WEBHOOK_URL = "https://api.twitter.com/1.1/account_activity/all/{0}/subscriptions.json";

		public static async Task<WebHook> AddWebHook(NewWebhookOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, string.Format(ADD_WEBHOOK_URL, options.Environment), options);

			return await GetData<WebHook>(reqMsg);
		}

		public static async Task<Environments> GetAllEnvironments(NoOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, ENVIRONMENTS_URL, options);

			return await GetData<Environments>(reqMsg);
		}

		public static async Task<HttpStatusCode> DeleteWebHook(DeleteWebhookOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Delete, string.Format(DELETE_WEBHOOK_URL, options.Environment, options.WebhookId), options);

			return await GetStatusCode(reqMsg);
		}

		public static async Task<HttpStatusCode> SubscribeWebHook(SubscribeWebhookOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, string.Format(SUBSCRIBE_WEBHOOK_URL, options.Environment), options);

			return await GetStatusCode(reqMsg);
		}

	}
}
