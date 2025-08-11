using Desafio001.Dominio.Interfaces;
using Desafio001.Servico.Conversores;
using Desafio001.Servico.Mensageria;
using Desafio001.Servico.Servicos;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static IServiceCollection AdicionarServicos(this IServiceCollection services, string connectionString)
    {
        services.AdicionarBancoDeDados(connectionString);

        services.AddSingleton<IMensageriaConexao, MensageriaConexao>();
        services.AddSingleton<IMensageriaProdutorServico, MensageriaProdutorServico>();
        services.AddScoped<IMensageriaConsumidorServico, MensageriaConsumidorServico>();

        services.AddScoped<IPilotoServico, PilotoServico>();
        services.AddScoped<IPilotoConversor, PilotoConversor>();

        services.AddScoped<IAviaoServico, AviaoServico>();
        services.AddScoped<IAviaoConversor, AviaoConversor>();

        services.AddScoped<IVooServico, VooServico>();
        services.AddScoped<IVooConversor, VooConversor>();

        services.AddScoped<IPassageiroServico, PassageiroServico>();
        services.AddScoped<IPassageiroConversor, PassageiroConversor>();

        services.AddScoped<IVooPassageiroServico, VooPassageiroServico>();
        services.AddScoped<IVooPassageiroConversor, VooPassageiroConversor>();

        services.AddScoped<IEmailServico, EmailServico>();

        return services;
    }
}