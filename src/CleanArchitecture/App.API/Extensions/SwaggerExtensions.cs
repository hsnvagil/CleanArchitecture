namespace App.API.Extensions;

public static class SwaggerExtensions {
    public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app) {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.API v1"));

        return app;
    }
}