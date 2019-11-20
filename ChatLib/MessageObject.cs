using System;

namespace ChatLib
{
    public class MessageObject
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}