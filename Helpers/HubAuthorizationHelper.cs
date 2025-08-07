using itransition_task6_server.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace itransition_task6_server.Helpers
{
    public static class HubAuthorizationHelper
    {
        public static bool AuthorizeEditor(HubCallerContext context, IConnectionService connections, string presentationId)
        {
            if (!connections.TryGet(context.ConnectionId, out var info))
                return false;
            if (info.PresentationId != presentationId)
                return false;
            return info.Role == "creator" || info.Role == "editor";
        }
    }
}
