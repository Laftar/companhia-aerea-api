using Desafio001.Dominio.Configuracoes;
using Desafio001.Workers.Consumidores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


await Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var connStr = ctx.Configuration.GetConnectionString("pessoal");
        if (connStr is not null) services.AdicionarInfraEstrutura(connStr);
        services.Configure<MensageriaConfig>(ctx.Configuration.GetSection("RabbitMQ"));
        services.Configure<SmtpConfig>(ctx.Configuration.GetSection("Smtp"));
        services.AddHostedService<OverbookingVooConsumidor>();
        services.AddHostedService<CancelamentoVooConsumidor>();
    })
    .RunConsoleAsync();
