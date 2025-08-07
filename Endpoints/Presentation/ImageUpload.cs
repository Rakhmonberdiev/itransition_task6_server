using itransition_task6_server.Helpers;
using itransition_task6_server.Hubs;
using itransition_task6_server.Models;
using itransition_task6_server.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using static System.Net.WebRequestMethods;

namespace itransition_task6_server.Endpoints.Presentation
{
    public static class ImageUpload
    {
        public static void MapImageUpload(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/presentations/{presentationId}/slides/{slideId}/upload-image", async (HttpContext http,
                    string presentationId,
                    string slideId,
                    IPhotoService photoService,
                    IPresentationService presentationService,
                    IConnectionService connections,
                    IHubContext<PresentationHub> hubContext) =>
            {
                var req = http.Request;
                if (!req.HasFormContentType)
                    return Results.BadRequest("Expected form-data");

                var form = await req.ReadFormAsync();
                var file = form.Files.GetFile("file");
                if (file == null || file.Length == 0)
                    return Results.BadRequest("No file");
                if (!req.Headers.TryGetValue("X-Connection-ID", out var connVals))
                    return Results.Unauthorized();
                var connectionId = connVals.FirstOrDefault();
                if (string.IsNullOrEmpty(connectionId)
                    || !connections.TryGet(connectionId, out var info)
                    || info.PresentationId != presentationId
                    || (info.Role != "creator" && info.Role != "editor"))
                {
                    return Results.Forbid();
                }
                var imageUrl = await photoService.AddPhoto(file);
                var block = new ImageBlock
                {
                    X = 10,
                    Y = 10,
                    Width = 300,
                    Height = 200,
                    Url = imageUrl
                };
                presentationService.AddElement(presentationId, slideId, block);
                await hubContext.Clients.Group(presentationId).SendAsync("ElementAdded", slideId, block);
                return Results.NoContent();
            });
        }
    }
}
