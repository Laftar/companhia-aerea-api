using Desafio001.Api.Conversores;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static IServiceCollection AdicionarApp(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IPilotoConversorApp, PilotoConversorApp>();
        services.AddScoped<IAviaoConversorApp, AviaoConversorApp>();
        services.AddScoped<IVooConversorApp, VooConversorApp>();
        services.AddScoped<IPassageiroConversorApp, PassageiroConversorApp>();

        services.AdicionarInfraEstrutura(connectionString);

        return services;
    }
}

