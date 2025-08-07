using itransition_task6_server.Models;
using itransition_task6_server.Services.Interfaces;
using System.Collections.Concurrent;


namespace itransition_task6_server.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly ConcurrentDictionary<string, HubConnectionInfo> _connections
            = new();

        public bool TryGet(string connectionId, out HubConnectionInfo info)
            => _connections.TryGetValue(connectionId, out info);

        public void Add(string connectionId, HubConnectionInfo info)
            => _connections[connectionId] = info;

        public void Remove(string connectionId)
            => _connections.TryRemove(connectionId, out _);

        public IEnumerable<KeyValuePair<string, HubConnectionInfo>> GetByPresentation(string presentationId)
            => _connections
               .Where(kvp => kvp.Value.PresentationId == presentationId);
    }
}
