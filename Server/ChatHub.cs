using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatLib;
using Microsoft.AspNet.SignalR;

namespace Servr
{
    public class ChatHub : Hub<IServerHub>, IClientHub
    {
        private static readonly Dictionary<string, string> _users = new Dictionary<string, string>();

        public void Send(MessageObject message)
        {
            // Overwrite Time and Date to Servertime
            message.Timestamp = DateTime.Now;
            Clients.All.AddNewMessage(message);
        }

        public bool ConnectUser(string username)
        {
            if (_users.ContainsKey(username))
            {
                return false;
            }
            _users.Add(username, Context.ConnectionId);
            Clients.All.AddNewMessage(new MessageObject { Message = $"User {username} joined the room.", Username = "James", Timestamp = DateTime.Now });
            return true;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = _users.Where(i => i.Value == Context.ConnectionId).Select(i => i.Key).FirstOrDefault();
            if (!string.IsNullOrEmpty(user))
            {
                _users.Remove(user);
                Clients.All.AddNewMessage(new MessageObject { Message = $"User {user} left the room.", Username = "James", Timestamp = DateTime.Now });
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}