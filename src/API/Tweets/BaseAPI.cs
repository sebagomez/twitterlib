using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public abstract class BaseAPI
	{
		static Action<string> s_messageFunction;

		public static void CheckData(TwitterOptions options)
		{
			if (options.User == null)
				throw new Exception("NO USER");

			if (options.User.AppSettings == null)
				throw new Exception("NO APPLICATION");
		}

		public static void SetMessageAction(Action<string> func)
		{
			s_messageFunction = func;
		}

		public static void WriteMessage(string message)
		{
			s_messageFunction?.Invoke(message);
		}

		public static async Task<T> GetData<T>(HttpRequestMessage reqMsg)
		{
			var (stream, _) = await GetResponseStream(reqMsg);

			return Util.Deserialize<T>(stream);
		}

		public static async Task<HttpStatusCode> GetStatusCode(HttpRequestMessage reqMsg)
		{ 
			var (_, statusCode) = await GetResponseStream(reqMsg);

			return statusCode;
		}

		private static async Task<(Stream, HttpStatusCode)> GetResponseStream(HttpRequestMessage reqMsg)
		{
			HttpResponseMessage response = await Util.Client.SendAsync(reqMsg);
			Stream stream = await response.Content.ReadAsStreamAsync();

			if (!response.IsSuccessStatusCode)
			{
				using (StreamReader reader = new StreamReader(stream))
					throw new Exception($"{response.StatusCode}:{ reader.ReadToEnd()}");
			}

			return (stream, response.StatusCode);
		}
	}
}
