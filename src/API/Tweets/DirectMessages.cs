using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class DirectMessages : BaseAPI
	{
		const string DM_EVENTS = "https://api.twitter.com/1.1/direct_messages/events/new.json";
		const string DM_LIST = "https://api.twitter.com/1.1/direct_messages/events/list.json";
		const string CONTENT_TYPE = "application/json";

		public static async Task<string> SendDM(DMOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, DM_EVENTS, options, true, false);
			
			DMEvent dm = new DMEvent()
			{
				@event = new Event
				{
					message_create = new MessageCreate
					{
						target = new Target
						{
							recipient_id = options.RecipientId
						},
						message_data = new MessageData
						{
							text = options.Text
						}
					}
				}
			};

			reqMsg.Content = new StringContent(JsonSerializer.Serialize(dm), Encoding.UTF8, CONTENT_TYPE);
			HttpResponseMessage response = await Util.Client.SendAsync(reqMsg);

			if (response.IsSuccessStatusCode)
				return response.ReasonPhrase;

			return await response.Content.ReadAsStringAsync();
		}

		public static async Task<EventList> GetDMList(DMListOptions options)
		{
			CheckData(options);

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Get, DM_LIST, options);

			return await GetData<EventList>(reqMsg);
		}
	}
}
