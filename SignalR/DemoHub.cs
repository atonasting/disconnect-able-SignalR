using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disconnect_able_SignalR.SignalR
{
    public class DemoHub : Hub
    {
        protected IDemoHubManager _demoHubData;

        public DemoHub(IDemoHubManager demoHubData)
        {
            _demoHubData = demoHubData;
        }

        public override Task OnConnectedAsync()
        {
            _demoHubData.AddContext(Context);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _demoHubData.RemoveContext(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, message);
        }

        public async Task DisconnectOthers()
        {
            var contexts = _demoHubData.GetContexts();
            _demoHubData.ContextForEach((context) =>
            {
                context.Abort();
            }, Context.ConnectionId);

            _demoHubData.RemoveAll(Context.ConnectionId);

            await Clients.Caller.SendAsync("ReceiveMessage", Context.ConnectionId, "All others disconnected.");
        }
    }
}