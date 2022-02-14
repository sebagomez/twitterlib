using System;
using System.Collections.Generic;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Options
{
	public class NewWebhookOptions : TwitterOptions
	{
		public string Url { get; set; }
		public string Environment { get; set; }

		public NewWebhookOptions()
		{
			ForceUrlParams = true;
		}

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(Url))
				parameters.Add("url", Util.EncodeString(Url.Trim()));

			return parameters;
		}
	}
}
