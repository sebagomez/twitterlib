namespace Sebagomez.TwitterLib.Entities
{
	public class FriendList
	{
		public User[] users { get; set; }
		public long previous_cursor { get; set; }

		public string previous_cursor_str { get; set; }

		public long next_cursor { get; set; }

		public string next_cursor_str { get; set; }
	}
}
