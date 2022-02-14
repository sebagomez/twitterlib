using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class StatusOptions : TwitterOptions
	{
		public string Id { get; set; }

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(Id))
				parameters.Add("id", Id);

			return parameters;
		}
	}
}
