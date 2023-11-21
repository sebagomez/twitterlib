
using System;
using System.Net;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class ActivityTest : BaseTests
	{
		//[Fact]
		public async Task AddWebHook()
		{
			try
			{
				string code = "<GET_FROM_AZURE_PORTAL>";
				string baseUrl = "https://isit737max.azurewebsites.net/api/IsIt737MAX";
				string parm = code.Length > 0 ? $"?code={code}" : "";

				NewWebhookOptions options = new NewWebhookOptions { User = m_user, Environment = "WebHook", Url = $"{baseUrl}{parm}" };
				WebHook result = await Activity.AddWebHook(options);

				Assert.True(result.valid, "WTF!");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		//[Fact]
		public async Task GetEnvWebHooks()
		{
			try
			{
				NoOptions options = new NoOptions { User = m_user};
				Environments result = await Activity.GetAllEnvironments(options);

				Assert.True(result.environments.Count == 1, "WTF!");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		//[Fact]
		public async Task DeleteWebHooks()
		{
			try
			{
				DeleteWebhookOptions options = new DeleteWebhookOptions { User = m_user, Environment = "shelltwitActivity", WebhookId = "1488877615696035848" };
				HttpStatusCode result = await Activity.DeleteWebHook(options);

				Assert.True(result == HttpStatusCode.NoContent, "WTF!");
			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		//[Fact]
		public async Task DeleteAllWebHooks()
		{
			try
			{

				NoOptions options = new NoOptions { User = m_user };
				Environments envs = await Activity.GetAllEnvironments(options);

				foreach(Sebagomez.TwitterLib.Entities.Environment env in envs.environments)
				{
					foreach (WebHook webHook in env.webhooks)
					{
						DeleteWebhookOptions delOptions = new DeleteWebhookOptions { User = m_user, Environment = env.environment_name, WebhookId = webHook.id };
						HttpStatusCode result = await Activity.DeleteWebHook(delOptions);

						Assert.True(result == HttpStatusCode.NoContent, "WTF!");
					}
				}

				Environments results = await Activity.GetAllEnvironments(options);

				foreach (Sebagomez.TwitterLib.Entities.Environment env in results.environments)
				{
					Assert.True(env.webhooks.Count == 0, "WTF!");
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}

		//[Fact]
		public async Task SubscribeToWebHooks()
		{
			try
			{

				NoOptions options = new NoOptions { User = m_user };
				Environments envs = await Activity.GetAllEnvironments(options);

				foreach (Sebagomez.TwitterLib.Entities.Environment env in envs.environments)
				{
					foreach (WebHook webHook in env.webhooks)
					{
						SubscribeWebhookOptions subOptions = new SubscribeWebhookOptions { User = m_user, Environment = env.environment_name };
						HttpStatusCode result = await Activity.SubscribeWebHook(subOptions);

						Assert.True(result == HttpStatusCode.NoContent, "WTF!");
					}
				}

				//Environments results = await Activity.GetAllEnvironments(options);

				//foreach (Sebagomez.TwitterLib.Entities.Environment env in results.environments)
				//{
				//	Assert.True(env.webhooks.Count == 0, "WTF!");
				//}

			}
			catch (Exception ex)
			{
				Assert.Fail(Util.ExceptionMessage(ex));
			}
		}
	}
}
