using System.Collections.Generic;
using System.Text;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Options
{
	public abstract class TwitterOptions
	{
		public AuthenticatedUser User { get; set; }
		public AppCredentials Application { get; set; }
		public abstract Dictionary<string, string> GetParameters();

		public string GetUrlParameters()
		{
			StringBuilder builder = new StringBuilder();
			foreach (var item in GetParameters())
				builder.Append($"&{item.Key}={item.Value}");

			if (builder.Length > 0)
				return builder.ToString().Substring(1);
			else
				return string.Empty;
		}
	}
}
