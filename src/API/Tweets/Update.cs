﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Sebagomez.TwitterLib.Web;

namespace Sebagomez.TwitterLib.API.Tweets
{
	public class Update : BaseAPI
	{
		const string UPDATE_STATUS = "https://api.twitter.com/1.1/statuses/update.json";
		const string MEDIA_UPLOAD = "https://upload.twitter.com/1.1/media/upload.json";
		const string STATUS = "status";
		const string MEDIA = "media_ids";
		const string IN_REPLY_TO = "in_reply_to_status_id";

		const int MAX_MEDIA = 4;

		#region Update Status

		public static string UpdateStatusSync(UpdateOptions options)
		{
			return UpdateStatus(options).Result;
		}

		public static async Task<string> UpdateStatus(UpdateOptions options)
		{
			try
			{
				CheckData(options);

				Regex regex = new Regex(@"\[(.*?)\]");
				foreach (Match match in regex.Matches(options.Status))
				{
					options.Status = options.Status.Replace(match.Value, "");
					FileInfo file = new FileInfo(match.Value.Replace("[", "").Replace("]", ""));
					if (!file.Exists)
						throw new FileNotFoundException("File not found", file.FullName);
					options.MediaFiles.Add(file);
				}

				if (options.MediaFiles.Count > MAX_MEDIA) //limited by the twitter API
					throw new ArgumentOutOfRangeException(nameof(options.MediaFiles), $"Up to {MAX_MEDIA} media files are allowed per tweet");

				if (string.IsNullOrEmpty(options.OriginalSatatus))
					options.OriginalSatatus = options.Status;

				options.Status = Util.EncodeString(WebUtility.HtmlDecode(options.Status));

				if (options.HasMedia)
					await UploadMedia(options);

				return await InternalUpdateStatus(options);
			}
			catch (Exception ex)
			{
				return Util.ExceptionMessage(ex);
			}
		}

		private static async Task<string> InternalUpdateStatus(UpdateOptions options)
		{
			string media = null;
			if (options.HasMedia)
				media = string.Join(",", options.MediaIds.ToArray());

			HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, UPDATE_STATUS, options, true, false);
			var postData = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>(STATUS, WebUtility.HtmlDecode(options.OriginalSatatus))
			};
			if (!string.IsNullOrEmpty(options.ReplyId))
				postData.Add(new KeyValuePair<string, string>(IN_REPLY_TO, options.ReplyId));
			if (options.HasMedia)
				postData.Add(new KeyValuePair<string, string>(MEDIA, media));

			reqMsg.Content = new FormUrlEncodedContent(postData);
			reqMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(Constants.CONTENT_TYPE.X_WWW_FORM_URLENCODED);

			WriteMessage($"Updating status...");
			HttpResponseMessage response = await Util.Client.SendAsync(reqMsg);

			if (response.IsSuccessStatusCode)
				return response.ReasonPhrase;

			return await response.Content.ReadAsStringAsync();
		}

		private static async Task UploadMedia(UpdateOptions options)
		{
			foreach (FileInfo file in options.MediaFiles)
			{
				WriteMessage($"Uploading {file.Name}");
				using (HttpRequestMessage reqMsg = OAuthHelper.GetRequest(HttpMethod.Post, MEDIA_UPLOAD, options, true, true))
				{
					MultipartFormDataContent content = new MultipartFormDataContent(GetMultipartBoundary());
					using (FileStream fs = File.Open(file.FullName, FileMode.Open))
					{
						StreamContent streamContent = new StreamContent(fs);
						streamContent.Headers.Add(Constants.HEADERS.CONTENT_TYPE, Constants.CONTENT_TYPE.OCTET_STREAM);
						streamContent.Headers.Add(Constants.HEADERS.CONTENT_DISPOSITION, string.Format(Constants.FORM_DATA, file.Name));
						content.Add(streamContent);

						reqMsg.Content = content;

						Media media = await GetData<Media>(reqMsg);
						options.MediaIds.Add(media.media_id_string);
					}
				}
			}
		}

		private static string GetMultipartBoundary()
		{
			return $"======{Guid.NewGuid().ToString().Substring(18).Replace("-", "")}======";
		}

		#endregion
	}
}
