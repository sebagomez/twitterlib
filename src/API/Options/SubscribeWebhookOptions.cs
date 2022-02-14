using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class SubscribeWebhookOptions : TwitterOptions
	{
		public string Environment { get; set; }

		public override Dictionary<string, string> GetParameters()
		{
			return new Dictionary<string, string>();
		}
	}
}
