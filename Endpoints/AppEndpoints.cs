using itransition_task6_server.Endpoints.Presentation;
using itransition_task6_server.Hubs;

namespace itransition_task6_server.Endpoints
{
    public static class AppEndpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetByIdPresentation();
            app.MapGetAllPresentation();
            app.MapImageUpload();
            app.MapAddPresentations();
            app.MapHub<PresentationHub>("/hubs/presentation");
        }
    }
}
