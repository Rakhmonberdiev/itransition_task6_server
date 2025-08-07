using itransition_task6_server.Services.Interfaces;

namespace itransition_task6_server.Endpoints.Presentation
{
    public static class GetById
    {
        public static void MapGetByIdPresentation(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/presentations/{id}", (string id, IPresentationService service) =>
            {
                var presentation = service.GetById(id);
                return presentation is null
                    ? Results.NotFound(new { message = "Presentation not found" })
                    : Results.Ok(presentation);
            });
        }
    }
}
