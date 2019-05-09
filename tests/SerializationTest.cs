using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Sebagomez.TwitterLib.Entities;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class SerializationTest : BaseTests
	{
		[Fact]
		public void DeserializeStatuses()
		{
			try
			{
				string path = Path.Combine(ResourcesDirectory, "serialized.json");

				Sebagomez.TwitterLib.Entities.Statuses ss = null;

				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Statuses));
				using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
					ss = (Statuses)serializer.ReadObject(file); ;

				Assert.True(ss.Count == 2, "Failed to load every status");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public void SerializeStatuses()
		{
			try
			{
				string path = Path.Combine(ResourcesDirectory, "serialized.json");

				Sebagomez.TwitterLib.Entities.Statuses ss = new Sebagomez.TwitterLib.Entities.Statuses
				{
					new Sebagomez.TwitterLib.Entities.Status() { id = 1, full_text = "One", user = new Sebagomez.TwitterLib.Entities.User { id = 1, name = "User1", screen_name = "screenName1" } },
					new Sebagomez.TwitterLib.Entities.Status() { id = 2, full_text = "Two", user = new Sebagomez.TwitterLib.Entities.User { id = 2, name = "User2", screen_name = "ScreenName2" } }
				};


				Util.Serialize(ss, path);

				//DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Statuses));
				//using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
				//	serializer.WriteObject(file, ss);
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
