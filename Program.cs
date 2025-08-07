using itransition_task6_server.Endpoints;
using itransition_task6_server.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddCors();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "Open API");
    });
}
app.UseCors();
app.MapEndpoints();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.Run();
