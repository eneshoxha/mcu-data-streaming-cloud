using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MCU.Streaming.Extensions.DI
{
    public static class SwaggerInjector
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Rregullimi digjital me komunikim nepermjet cloud",
                    Description = "Mireserdhet ne mcu-data-streaming-api, ky sherbim eshte krijuar per temen e diplomes me titull Rregullimi digjital me komunikim nepermjet cloud"
                }) ;
            });
            return services;
        }
        public static IApplicationBuilder UseSwaggerView(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rregullimi digjital me komunikim nepermjet cloud");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
