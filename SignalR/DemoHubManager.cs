using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace disconnect_able_SignalR.SignalR
{
    public class DemoHubManager : IDemoHubManager
    {
        private Dictionary<string, HubCallerContext> _contexts = new Dictionary<string, HubCallerContext>();
        private object _contextsLock = new object();

        public void AddContext(HubCallerContext Context)
        {
            lock (_contextsLock)
            {
                _contexts.TryAdd(Context.ConnectionId, Context);
            }
        }

        public void RemoveContext(string ConnectionId)
        {
            lock (_contextsLock)
            {
                _contexts.Remove(ConnectionId);
            }
        }

        public List<HubCallerContext> GetContexts()
        {
            return _contexts.Values.ToList();
        }

        public void ContextForEach(Action<HubCallerContext> Action, string ExceptConnectionId = null)
        {
            lock (_contextsLock)
            {
                var actionContexts = string.IsNullOrEmpty(ExceptConnectionId) ?
                _contexts.Values.ToList() :
                _contexts.Where(c => c.Key != ExceptConnectionId).Select(c => c.Value).ToList();

                actionContexts.ForEach(context => Action.Invoke(context));
            }
        }

        public void RemoveAll(string ExceptConnectionId = null)
        {
            lock (_contextsLock)
            {
                if (string.IsNullOrEmpty(ExceptConnectionId))
                {
                    _contexts.Clear();
                }
                else
                {
                    ContextForEach((context) =>
                    {
                        _contexts.Remove(context.ConnectionId);
                    }, ExceptConnectionId);
                }
            }
        }
    }
}
