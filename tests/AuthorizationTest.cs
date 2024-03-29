﻿using System;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.OAuth;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class AuthorizationTest : BaseTests
	{
		[Fact]
		public async Task GetOAuthToken()
		{
			try
			{
				string token = await OAuthAuthenticator.GetOAuthToken(m_user.AppSettings.AppKey, m_user.AppSettings.AppSecret);

				Assert.True(token.Length == 27, "OK");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		//[Fact] Not automatic... it needs the PIN and oAuthToken
		public void GetPINAuthToken()
		{
			try
			{
				string pin = "3616991";
				string oAuthToken = "Kn5i6AAAAAAAARPWAAABXHm2bfw";
				string accessTokens = OAuthAuthenticator.GetPINToken(oAuthToken, pin, m_user.AppSettings.AppKey, m_user.AppSettings.AppSecret).Result;

				AuthenticatedUser user = new AuthenticatedUser();
				user.ParseTokens(accessTokens);

			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}
	}
}
