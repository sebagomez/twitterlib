using System;
using System.Collections.Generic;

namespace Sebagomez.TwitterLib.Entities
{
	public class WebHook
	{
		public string id { get; set; }
		public string url { get; set; }
		public bool valid { get; set; }
		public string created_timestamp { get; set; }
	}

	public class Environment
	{
		public string environment_name { get; set; }
		public List<WebHook> webhooks { get; set; }
	}

	public class Environments
	{
		public List<Environment> environments { get; set; }
	}
}
