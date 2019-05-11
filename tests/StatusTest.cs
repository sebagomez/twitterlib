using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class StatusTest : BaseTests
	{
		[Fact]
		public async Task StatusBasico()
		{
			string status = string.Format("Hello World:{0}", DateTime.Now);
			try
			{
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions() { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusBasicoRepetido()
		{
			DateTime now = DateTime.Now;
			string status = string.Format("Hello World:{0}", now);
			UpdateOptions options = new UpdateOptions { Status = status, User = m_user };
			string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(options);
			Assert.Equal("OK", response);

			Thread.Sleep(1500);
			try
			{
				options.Status = status; //it gets encoded after an update
				response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(options);
				Assert.Equal("{\"errors\":[{\"code\":187,\"message\":\"Status is a duplicate.\"}]}", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusConEmojis()
		{
			string status = string.Format("Hello Emoji World 😉 :{0}", DateTime.Now);
			try
			{
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusConUrl()
		{
			string status = string.Format("Hello World @ http://twitter.com :{0}", DateTime.Now);
			try
			{
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusConImagen()
		{
			try
			{
				string mediaPath = Path.Combine(ResourcesDirectory, MEDIA1_NAME);

				string status = $"Este viene con imagen [{mediaPath}]: {DateTime.Now}";
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusConImagenGigante()
		{
			try
			{
				string mediaPath = Path.Combine(ResourcesDirectory, HUGE_MEDIA);

				string status = string.Format(@"Este viene con imagen GRANDE [{0}]: {1}", mediaPath, DateTime.Now);
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("{\"errors\":[{\"code\":324,\"message\":\"Image file size must be <= 5242880 bytes\"}]}", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusConDosImagenes()
		{
			try
			{
				string mediaPath1 = Path.Combine(ResourcesDirectory, MEDIA1_NAME);
				string mediaPath2 = Path.Combine(ResourcesDirectory, MEDIA2_NAME);

				string status = string.Format(@"Este viene con 2 imagenes [{0}] [{1}]: {2}", mediaPath1, mediaPath2, DateTime.Now);
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		//[Fact]
		//public void StatusConDosImagenesSinBrackets()
		//{
		//	try
		//	{
		//		string mediaPath1 = Path.Combine(BASE_PATH, MEDIA1_NAME);
		//		string mediaPath2 = Path.Combine(BASE_PATH, MEDIA2_NAME);

		//		string status = string.Format(@"Este viene con 2 imagenes {0} {1}: {2}", mediaPath1, mediaPath2, DateTime.Now);
		//		string response = Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(status);
		//		Assert.Equal("OK", response);
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.True(false, Util.ExceptionMessage(ex));
		//	}
		//}

		[Fact]
		public async Task StatusConImagenyCaracteresEspeciales()
		{
			try
			{
				string mediaPath = Path.Combine(ResourcesDirectory, MEDIA1_NAME);

				string status = $"á é í ó ú ñ ! # @ [{mediaPath}]: {DateTime.Now}";
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task CaracteresEspeciales()
		{
			string status = string.Format("Los tildes son á é í ó ú y la ñ. Las myúsculas son Á É Í Ó Ú Ñ: {0}", DateTime.Now);
			try
			{
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task AConTilde()
		{
			string status = string.Format("á:{0}", DateTime.Now);
			try
			{
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task Exclamacion()
		{
			string status = string.Format("!:{0}", DateTime.Now);
			try
			{
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusProblematico()
		{
			string status = string.Format("des-i5 y la reconchaqueteparió!!!:{0}", DateTime.Now);
			try
			{
				string response = await Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task Menciones()
		{
			try
			{
				List<Sebagomez.TwitterLib.Entities.Status> result = await Mentions.GetMentions(new MentionsOptions { User = m_user });
				Assert.True(result.Count > 0, "Ningún tweet!");

			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task StatusEncodeado()
		{
			try
			{
				string status = string.Format("Excepción JAVA -Can't execute dynamic call:{0}", DateTime.Now);
				status = WebUtility.HtmlEncode(status);
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task AreYouSure()
		{
			try
			{
				string status = string.Format("3 millones de tsunaminólogos:{0}", DateTime.Now);
				string response = await Sebagomez.TwitterLib.API.Tweets.Update.UpdateStatus(new UpdateOptions { Status = status, User = m_user });
				Assert.Equal("OK", response);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task ReplyTwit()
		{
			try
			{
				string status = $"this is your reply: {DateTime.Now}";
				string response = await Update.UpdateStatus(new UpdateOptions { ReplyId = "1108908809358098432", Status = status, User = m_user });
				Assert.Equal("OK", response);

			}
			catch (Exception ex)
			{

				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
