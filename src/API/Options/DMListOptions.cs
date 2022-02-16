using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class DMListOptions : TwitterOptions
	{
		public int Count { get; set; } = 20;
		public string Cursor { get; set; }

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			if (Count != 0)
				parameters.Add("count", Count.ToString());
			if (!string.IsNullOrEmpty(Cursor))
				parameters.Add("next_cursor", Cursor.Trim());

			return parameters;
		}

	}
}
