using App.API.Extensions;
using App.Application.Extensions;
using App.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllersWithFiltersExt()
    .AddSwaggerGenExt()
    .AddExceptionHandlerExt()
    .AddCachingExt();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();