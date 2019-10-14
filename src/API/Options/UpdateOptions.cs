using System.Collections.Generic;
using System.IO;
using Sebagomez.TwitterLib.Helpers;

namespace Sebagomez.TwitterLib.API.Options
{
	public class UpdateOptions : TwitterOptions
	{
		public string Status { get; set; }
		public string ReplyId { get; set; }
		public List<string> MediaIds { get; set; }
		public List<FileInfo> MediaFiles { get; set; }
		public string OriginalSatatus { get; set; }

		public string BitLyLogin { get; set; }
		public string BitLyKey { get; set; }

		public UpdateOptions()
		{
			MediaIds = new List<string>();
			MediaFiles = new List<FileInfo>();
		}

		public bool HasMedia
		{
			get { return MediaFiles.Count > 0; }
		}

		public override Dictionary<string, string> GetParameters()
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>
			{
				{ "status", Status }
			};
			if (!string.IsNullOrEmpty(ReplyId))
				parameters.Add("in_reply_to_status_id", ReplyId);
			if (HasMedia)
				parameters.Add("media_ids", Util.EncodeString(string.Join(",", MediaIds.ToArray())));

			return parameters;
		}
	}
}
