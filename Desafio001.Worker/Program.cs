using Desafio001.Dominio.Configuracoes;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Repositorios;
using Desafio001.Infra.Data;
using Desafio001.Infra.Repositorios;
using Desafio001.Mensageria.Handlers;
using Desafio001.Servico.Mensageria;
using Desafio001.Servico.Servicos;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

// 1) Banco
var connStr = builder.Configuration.GetConnectionString("pessoal");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(connStr));


//builder.Services.AddScoped<IAviaoRepositorio, AviaoRepositorio>();
//builder.Services.AddScoped<IPilotoRepositorio, PilotoRepositorio>();
//builder.Services.AddScoped<IPassageiroRepositorio, PassageiroRepositorio>();
//builder.Services.AddScoped<IVooRepositorio, VooRepositorio>();
builder.Services.AddScoped<IVooPassageiroRepositorio, VooPassageiroRepositorio>();

//builder.Services.AddScoped<IPassageiroServico, PassageiroServico>();
//builder.Services.AddScoped<IVooServico, VooServico>();
builder.Services.AddScoped<IEmailServico, EmailServico>();


builder.Services.Configure<MensageriaConfig>(
  builder.Configuration.GetSection("RabbitMQ")
);
builder.Services.AddSingleton<IMensageriaConexao, MensageriaConexao>();
builder.Services.AddScoped<IMensageriaConsumidorServico, MensageriaConsumidorServico>();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<NotificarOverbookingHandler>();
});


// 5) Worker
builder.Services.AddHostedService<Worker>();

await builder.Build().RunAsync();
