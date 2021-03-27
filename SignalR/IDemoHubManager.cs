using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disconnect_able_SignalR.SignalR
{
    public interface IDemoHubManager
    {
        void AddContext(HubCallerContext Context);
        void RemoveContext(string ConnectionId);
        List<HubCallerContext> GetContexts();
        void ContextForEach(Action<HubCallerContext> Action, string ExceptConnectionId = null);
        void RemoveAll(string ExceptConnectionId = null);
    }
}
