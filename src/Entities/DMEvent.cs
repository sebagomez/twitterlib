namespace Sebagomez.TwitterLib.Entities
{
    public class DMEvent
    {
        public Event @event { get; set; }
    }

    public class Event
    {
        public string type { get; set; } = "message_create";
        public MessageCreate message_create { get; set; }
    }

    public class MessageCreate
    {
		public string sender_id { get; set; }
		public Target target { get; set; }
        public MessageData message_data { get; set; }
    }

    public class Target
    {
        public string recipient_id { get; set; }
    }

    public class MessageData
    {
        public string text { get; set; }
    }
}
