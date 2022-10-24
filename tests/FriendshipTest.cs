
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sebagomez.TwitterLib.API.Options;
using Sebagomez.TwitterLib.API.Tweets;
using Sebagomez.TwitterLib.Helpers;
using Xunit;

namespace TwitterLibTests
{
	public class FriendshipTest : BaseTests
	{
		[Fact]
		public async Task FollowUnfollowOKByName()
		{
			try
			{
				string screen_name = "sebagomez";

				FriendshipOptions options = new FriendshipOptions { ScreenName = screen_name, User = m_user };
				Sebagomez.TwitterLib.Entities.User user = await Frienship.Follow(options);
				Assert.True(user.screen_name == screen_name, "Wrong user followed");

				user = await Frienship.Unfollow(options);
				Assert.True(user.screen_name == screen_name, "Wrong user unfollowed");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task FollowUnfollowOKByID()
		{
			try
			{
				long id = 9096202;

				FriendshipOptions options = new FriendshipOptions { UserId = id, User = m_user };
				Sebagomez.TwitterLib.Entities.User user = await Frienship.Unfollow(options);
				Assert.True(user.id == id, "Wrong user");

				user = await Frienship.Unfollow(options);
				Assert.True(user.id == id, "Wrong user unfollowed");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task GetFollowersByName()
		{
			try
			{
				FollowerListOptions options = new FollowerListOptions { ScreenName = "sebatestapi", User = m_user };
				Sebagomez.TwitterLib.Entities.FriendList friends = await Frienship.ListFollowers(options);
				Assert.True(friends.users.Length == 1, "Wrong ammount of followers");
				Assert.True(friends.next_cursor == 0, "Wrong cursor");
				Assert.True(friends.previous_cursor == 0, "Wrong prev cursor");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}

		[Fact]
		public async Task GetFollowersById()
		{
			try
			{
				long id = 108356361;

				FollowerListOptions options = new FollowerListOptions { UserId = id, User = m_user };
				Sebagomez.TwitterLib.Entities.FriendList friends = await Frienship.ListFollowers(options);
				Assert.True(friends.users.Length == 1, "Wrong ammount of followers");
				Assert.True(friends.next_cursor == 0, "Wrong cursor");
				Assert.True(friends.previous_cursor == 0, "Wrong prev cursor");
			}
			catch (Exception ex)
			{
				Assert.True(false, Util.ExceptionMessage(ex));
			}
		}
	}
}
