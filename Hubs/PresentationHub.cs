using itransition_task6_server.Helpers;
using itransition_task6_server.Models;
using itransition_task6_server.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace itransition_task6_server.Hubs
{
    public class PresentationHub(IPresentationService  service, IConnectionService connections) : Hub
    {

        public async Task JoinPresentation(string presentationId, string userName)
        {
            var pres = service.GetById(presentationId);
            if (pres is null) return;
            string role = pres.CreatorName == userName ? "creator" : "viewer";
            connections.Add(Context.ConnectionId, new HubConnectionInfo(presentationId, userName, role));
            await Groups.AddToGroupAsync(Context.ConnectionId, presentationId);

            var currentUsers = connections
                .GetByPresentation(presentationId)
                .Select(kvp => new
                {
                    name = kvp.Value.UserName,
                    role = kvp.Value.Role
                })
                .ToList();
            await Clients.Caller.SendAsync("InitialUsers", currentUsers);
            await Clients.OthersInGroup(presentationId)
                         .SendAsync("UserJoined", userName, role);
        }
        public async Task AddSlide(string presentationId)
        {
            if (!connections.TryGet(Context.ConnectionId, out var info)) return;
            if (info.PresentationId != presentationId) return;
            var pres = service.GetById(presentationId);
            if (pres is null || pres.CreatorName != info.UserName) return;

   
            var newSlide = new Slide
            {
                Order = pres.Slides.Count + 1,
                Elements = new List<SlideElement>()
            };
            service.AddSlide(presentationId, newSlide);

            await Clients.Group(presentationId)
                         .SendAsync("SlideAdded", newSlide);
        }
        public async Task AddImageBlock(string presentationId, string slideId, string url)
        {
            if (!HubAuthorizationHelper.AuthorizeEditor(Context, connections, presentationId))
                return;

            var block = new ImageBlock
            {
                X = 10,
                Y = 10,
                Width = 300,
                Height = 200,
                Url = url
            };

            service.AddElement(presentationId, slideId, block);

            await Clients.Group(presentationId)
                         .SendAsync("ElementAdded", slideId, block);
        }
        public async Task AddTextBlock(string presentationId, string slideId, string text)
        {
            if (!HubAuthorizationHelper.AuthorizeEditor(Context, connections, presentationId))
                return;
            var block = new TextBlock
            {
    
                X = 20,          
                Y = 20,
                Width = 200,
                Height = 50,
                Text = text
            };

            service.AddElement(presentationId, slideId, block);

            await Clients.Group(presentationId)
                         .SendAsync("ElementAdded", slideId, block);
        }
        public async Task UpdateElement(string presentationId, string slideId, SlideElement element)
        {
            if (!HubAuthorizationHelper.AuthorizeEditor(Context, connections, presentationId))
                return;

            service.UpdateElement(presentationId, slideId, element);
            await Clients.Group(presentationId).SendAsync("ElementUpdated", slideId, element);
        }
        public async Task RemoveElement(string presentationId, string slideId, string elementId)
        {
            if (!HubAuthorizationHelper.AuthorizeEditor(Context, connections, presentationId))
                return;

            service.RemoveElement(presentationId, slideId, elementId);
            await Clients.Group(presentationId).SendAsync("ElementRemoved", slideId, elementId);
        }
        public async Task ChangeRole(string presentationId, string targetUserName, string newRole)
        {
            if (!connections.TryGet(Context.ConnectionId, out var info)) return;

            var presentation = service.GetById(presentationId);
            if (presentation is null || info.UserName != presentation.CreatorName) return;
            if (newRole != "editor" && newRole != "viewer") return;

                var entry = connections
        .GetByPresentation(presentationId)
        .FirstOrDefault(kvp => kvp.Value.UserName == targetUserName);

            if (entry.Equals(default(KeyValuePair<string, HubConnectionInfo>)))
                return;
            var targetConnectionId = entry.Key;
            connections.Add(targetConnectionId, new HubConnectionInfo(presentationId, targetUserName, newRole));
            await Clients.Client(targetConnectionId).SendAsync("RoleChanged", newRole);
            await Clients.Group(presentationId).SendAsync("UserRoleUpdated", targetUserName, newRole);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (connections.TryGet(Context.ConnectionId, out var info))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, info.PresentationId);
                await Clients.Group(info.PresentationId).SendAsync("UserLeft", info.UserName);
                connections.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
