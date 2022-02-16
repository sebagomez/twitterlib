using System.Collections.Generic;

namespace Sebagomez.TwitterLib.API.Options
{
	public class NoOptions: TwitterOptions
	{
		public override Dictionary<string, string> GetParameters() => new Dictionary<string, string>();
	}
}
