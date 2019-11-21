using System;
using ChatLib;
using Microsoft.AspNet.SignalR;

namespace Servr
{
    public class ChatHub : Hub<IServerHub>, IClientHub
    {
        public void Send(MessageObject message)
        {
            // Overwrite Time and Date to Servertime
            message.Timestamp = DateTime.Now;
            Clients.All.AddNewMessage(message);
        }
    }
}