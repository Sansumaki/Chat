using ChatLib;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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