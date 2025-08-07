using itransition_task6_server.Models;

namespace itransition_task6_server.Services.Interfaces
{
    public interface IConnectionService
    {
        bool TryGet(string connectionId, out HubConnectionInfo info);
        void Add(string connectionId, HubConnectionInfo info);
        void Remove(string connectionId);
        IEnumerable<KeyValuePair<string, HubConnectionInfo>> GetByPresentation(string presentationId);
    }
}
