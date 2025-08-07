using itransition_task6_server.Endpoints.Presentation.DTOs;
using itransition_task6_server.Services.Interfaces;

namespace itransition_task6_server.Endpoints.Presentation
{
    public static class AddPresentation
    {
        public static void MapAddPresentations(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/presentations/new", async (CreatePresentationDto dto, IPresentationService service) =>
            {
                var validationError = Validate(dto);
                if (validationError is not null)
                    return validationError;
                var id = service.CreatePresentation(dto.Title, dto.CreatorName);
         
                return Results.Created(
                    $"/api/presentations/{id}",
                    new { id }
                );
            });
        }

        private static IResult? Validate(CreatePresentationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Results.BadRequest(new { error = "Title is required and cannot be empty." });

            if (string.IsNullOrWhiteSpace(dto.CreatorName))
                return Results.BadRequest(new { error = "CreatorName is required and cannot be empty." });

            return null;
        }
    }
}
