using System;
using System.Collections.Generic;
using System.Text;

namespace Sebagomez.TwitterLib.Helpers
{
	public class AppCredentials
	{
		public static AppCredentials Current;

		public string AppKey { get; set; }
		public string AppSecret { get; set; }

		public static AppCredentials Initialize(string appKey, string appSecret)
		{
			return (Current = new AppCredentials { AppKey = appKey, AppSecret = appSecret });
		}

	}
}
