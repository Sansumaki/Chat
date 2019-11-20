using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servr
{
    public class ChatHub : Hub<IServerHub>, IClientHub
    {
        public void Send(string name, string message)
        {
            Clients.Others.AddNewMessage(name, message);
        }
    }
}