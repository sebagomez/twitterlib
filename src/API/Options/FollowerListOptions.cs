using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class FollowerListOptions : TwitterOptions
	{
		public long UserId { get; set; } = -1;
		public string ScreenName { get; set; }
		public int Count { get; set; } = -1;
		public string Cursor { get; set; }
		public bool SkipStatus { get; set; } = false;
		public bool IncludeUserEntities { get; set; } = true;

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			if (UserId != -1)
				parameters.Add("user_id", UserId.ToString());
			if (!string.IsNullOrEmpty(ScreenName))
				parameters.Add("screen_name", ScreenName.ToString());
			if (!string.IsNullOrEmpty(Cursor))
				parameters.Add("next_cursor", Cursor.Trim());
			if (Count != -1)
				parameters.Add("count", Count.ToString());
			if (SkipStatus)
				parameters.Add("skip_status", SkipStatus.ToString().ToLower());
			if (!IncludeUserEntities)
				parameters.Add("include_user_entities", IncludeUserEntities.ToString().ToLower());

			return parameters;
		}

	}
}
