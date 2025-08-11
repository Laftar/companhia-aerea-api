using Desafio001.Dominio.Interfaces;
using Desafio001.Infra.Data;
using Desafio001.Infra.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseLazyLoadingProxies()
            .UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

        return services;
    }
}