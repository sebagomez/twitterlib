using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class DMOptions : TwitterOptions
	{
		public string Text { get; set; }
		public string RecipientId { get; set; }

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			//if (!string.IsNullOrEmpty(RecipientId))
			//	parameters.Add("recipient_id", RecipientId);

			//if (!string.IsNullOrEmpty(Text))
			//	parameters.Add("text", Text);

			return parameters;
		}
	}
}
