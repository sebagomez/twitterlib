using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class DMOptions : TwitterOptions
	{
		public string Text { get; set; }
		public string RecipientId { get; set; }

		public override Dictionary<string, string> GetParameters() => new Dictionary<string, string>();
	}
}
