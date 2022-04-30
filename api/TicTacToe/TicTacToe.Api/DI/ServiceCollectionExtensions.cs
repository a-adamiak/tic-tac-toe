using Asp.Versioning;
using Microsoft.OpenApi.Models;
using TicTacToe.Api.Attributes;

namespace TicTacToe.Api.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1.0);
                })
            .AddApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                });

        return services;
    }

    public static IServiceCollection AddVersionedSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            foreach (var fi in dir.EnumerateFiles("*.xml"))
            {
                options.IncludeXmlComments(fi.FullName);
            }

            options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Tic Tac Toe", Version = "v1" });
            options.OperationFilter<RemoveVersionFromParameter>();

            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
        });

        return services;
    }
}