using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.Helpers;
using Sebagomez.TwitterLib.Web;

namespace Sebagomez.TwitterLib.API.OAuth
{
	public class OAuthAuthenticator
	{
		const string ACCESS_TOKEN = "https://api.twitter.com/oauth/access_token";
		const string REQUEST_TOKEN = "https://api.twitter.com/oauth/request_token";

		public static async Task<string> GetOAuthToken(string appKey, string appSecret)
		{
			string nonce = OAuthHelper.GetNonce();
			string timestamp = Util.EncodeString(OAuthHelper.GetTimestamp());
			string callback = "oob";

			Dictionary<string, string> dic = GetWebAccessTokenParms(nonce, timestamp, callback, null, appKey);

			string signatureBase = OAuthHelper.SignatureBsseString(HttpMethod.Post.Method, REQUEST_TOKEN, dic);
			string signature = SignBaseString(signatureBase, string.Empty, appSecret);
			string authHeader = AuthorizationHeader(nonce, signature, timestamp, string.Empty, true, string.Empty, appKey);

			HttpRequestMessage reqMsg = new HttpRequestMessage(HttpMethod.Post, REQUEST_TOKEN);

			reqMsg.Headers.Add(Constants.HEADERS.AUTHORIZATION, authHeader);

			HttpResponseMessage response = await Util.Client.SendAsync(reqMsg);
			if (!response.IsSuccessStatusCode)
				throw new Exception(await response.Content.ReadAsStringAsync());

			string strResponse = await response.Content.ReadAsStringAsync();

			const string oauth_token = "oauth_token=";
			int start = strResponse.IndexOf(oauth_token);
			int length = strResponse.IndexOf("&", start);

			return strResponse.Substring(start + oauth_token.Length, length - oauth_token.Length);

		}

		public static async Task<string> GetPINToken(string oAuthToken, string pin, string appKey, string appSecret)
		{
			string nonce = OAuthHelper.GetNonce();
			string timestamp = Util.EncodeString(OAuthHelper.GetTimestamp());

			Dictionary<string, string> dic = GetWebAccessTokenParms(nonce, timestamp, null, pin, appKey);

			string signatureBase = OAuthHelper.SignatureBsseString(HttpMethod.Post.Method, ACCESS_TOKEN, dic);
			string signature = SignBaseString(signatureBase, string.Empty, appSecret);
			string authHeader = AuthorizationHeader(nonce, signature, timestamp, oAuthToken, false, pin, appSecret);

			HttpRequestMessage reqMsg = new HttpRequestMessage(HttpMethod.Post, ACCESS_TOKEN);

			reqMsg.Headers.Add(Constants.HEADERS.AUTHORIZATION, authHeader);

			HttpResponseMessage response = await Util.Client.SendAsync(reqMsg);
			if (!response.IsSuccessStatusCode)
				throw new Exception(await response.Content.ReadAsStringAsync());

			return await response.Content.ReadAsStringAsync();
		}

		private static Dictionary<string, string> GetWebAccessTokenParms(string nonce, string timestamp, string callback, string verifier, string appKey)
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(callback))
				dic.Add(OAuthHelper.OAUTH_CALLBACK, Util.EncodeString(callback));
			dic.Add(OAuthHelper.OAUTH_CONSUMER_KEY, Util.EncodeString(appKey));
			dic.Add(OAuthHelper.OAUTH_NONCE, nonce);
			dic.Add(OAuthHelper.OAUTH_SIGNATURE_METHOD, OAuthHelper.HMAC_SHA1);
			dic.Add(OAuthHelper.OAUTH_TIMESTAMP, timestamp);
			dic.Add(OAuthHelper.OAUTH_VERSION, OAuthHelper.OAUTH_VERSION_10);
			if (!string.IsNullOrEmpty(verifier))
				dic.Add(OAuthHelper.OAUTH_VERIFIER, verifier);

			return dic;
		}

		internal static string AuthorizationHeader(string nonce, string signature, string timestamp, string oAuthToken, bool withCallback, string pin, string appKey)
		{
			string token = string.Empty;
			if (!string.IsNullOrEmpty(oAuthToken))
				token = $"{OAuthHelper.OAUTH_TOKEN}=\"{Util.EncodeString(oAuthToken)}\", ";

			string callBack = "";
			if (withCallback)
				callBack = $"{OAuthHelper.OAUTH_CALLBACK}=\"oob\", ";
			if (!string.IsNullOrEmpty(pin))
				pin = $"{OAuthHelper.OAUTH_VERIFIER}=\"{pin}\", ";

			return $"OAuth {callBack}oauth_nonce=\"{nonce}\", oauth_signature_method=\"{OAuthHelper.HMAC_SHA1}\", oauth_timestamp=\"{timestamp}\", oauth_consumer_key=\"{Util.EncodeString(appKey)}\", {token} oauth_signature=\"{Util.EncodeString(signature)}\", {pin}oauth_version=\"1.0\"";
		}

		#region utils

		public static string SignBaseString(string signatureBase, string oAuthSecret, string appSecret)
		{
			HMACSHA1 hmacsha1 = new HMACSHA1();
			hmacsha1.Key = Util.GetUTF8EncodingBytes(string.Format("{0}&{1}", Util.EncodeString(appSecret), string.IsNullOrEmpty(oAuthSecret) ? "" : Util.EncodeString(oAuthSecret)));

			byte[] dataBuffer = Encoding.ASCII.GetBytes(signatureBase);
			byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

			return Convert.ToBase64String(hashBytes);
		}

		#endregion

	}
}
