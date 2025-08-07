using itransition_task6_server.Services.Interfaces;

namespace itransition_task6_server.Endpoints.Presentation
{
    public static class GetAll
    {
        public static void MapGetAllPresentation(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/presentations", (IPresentationService service) =>
            {
                return Results.Ok(service.GetAll());
            });
        }
    }
}
