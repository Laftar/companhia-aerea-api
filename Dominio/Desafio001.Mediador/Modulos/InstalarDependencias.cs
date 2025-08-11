using Desafio001.Mediador.Comandos;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static IServiceCollection AdicionarInfraEstrutura(this IServiceCollection services, string connectionString)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CriarPilotoComando).Assembly));

        services.AdicionarServicos(connectionString);

        return services;
    }

}