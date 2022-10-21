using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class FriendshipOptions : TwitterOptions
	{
		public FriendshipOptions()
		{
			ForceUrlParams = true;
		}

		public long UserId { get; set; }
		public string ScreenName { get; set; }
		public bool Follow { get; set; } = false;

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			if (UserId != 0)
				parameters.Add("user_id", UserId.ToString());
			if (!string.IsNullOrEmpty(ScreenName))
				parameters.Add("screen_name", ScreenName.Trim());
			if (Follow)
				parameters.Add("follow", Follow.ToString().ToLower());

			return parameters;
		}
	}
}
