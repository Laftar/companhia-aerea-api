//using Desafio001.Dominio.Contratos;
//using Desafio001.Dominio.Exceptions;
//using Desafio001.Dominio.Interfaces;
//using Desafio001.Dominio.Mensagens;
//using Desafio001.Dominio.Repositorios;
//using Desafio001.Infra.Data;
//using Desafio001.Infra.Repositorios;
//using Desafio001.Servico.Conversores;
//using Desafio001.Servico.Servicos;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Abstractions;
//using NUnit.Framework;

//namespace TestesUnitarios
//{
//    [TestFixture]
//    public class AviaoServicoTests
//    {
//        private AppDbContext context;
//        private IAviaoRepositorio repo;
//        private AviaoConversor conversor;
//        private FakeMensageria mensageria;
//        private AviaoServico servico;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;

//            context = new AppDbContext(options);
//            repo = new AviaoRepositorio(context);
//            conversor = new AviaoConversor();
//            mensageria = new FakeMensageria();
//            var logger = NullLoggerFactory.Instance.CreateLogger<AviaoServico>();

//            servico = new AviaoServico(repo, conversor, logger, mensageria);
//        }

//        [TearDown]
//        public void TearDown() => context.Dispose();

//        [Test]
//        public async Task BuscarAviaoAsync_RetornaOk()
//        {
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            var av3 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 3));
//            var av4 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Cessna", "172", 2017, 4, 10));
//            await context.Avioes.AddRangeAsync(av1, av2, av3, av4);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Avioes.FirstAsync(a => a.Marca == "Boeing")).Id;

//            var resultado = await servico.BuscarAviaoAsync(idDb, CancellationToken.None);
//            resultado.Should().NotBeNull();
//            resultado.Id.Should().Be(idDb);
//            resultado.Marca.Should().Be("Boeing");
//            resultado.Modelo.Should().Be("737");
//            resultado.AnoFabricacao.Should().Be(2020);
//        }

//        [Test]
//        public async Task BuscarAviaoAsync_NaoEncontrando()
//        {
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            var av3 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 3));
//            var av4 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Cessna", "172", 2017, 4, 10));
//            await context.Avioes.AddRangeAsync(av1, av2, av3, av4);
//            await context.SaveChangesAsync();

//            Func<Task> act = async () =>
//                await servico.BuscarAviaoAsync(Guid.NewGuid(), CancellationToken.None);

//            await act.Should().ThrowAsync<CustomException>()
//               .WithMessage("{AviaoId} entidade não encontrada");
//        }

//        [Test]
//        public async Task BuscarAviaoAsync_EstaExcluido()
//        {
//            var avEx = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            avEx.Excluir();
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "777", 2015, 250, 10));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            var av3 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 3));
//            await context.Avioes.AddRangeAsync(avEx, av1, av2, av3);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Avioes.FirstAsync(a => a.Excluido)).Id;

//            Func<Task> act = async () =>
//                await servico.BuscarAviaoAsync(idDb, CancellationToken.None);

//            await act.Should().ThrowAsync<CustomException>()
//                .WithMessage("{AviaoExcluido} Entidade esta deletada");
//        }

//        [Test]
//        public async Task ListarAvioesAsync_RetornaLista_ApenasValidos()
//        {
//            var a1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            var a2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            var ex = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 3)); ex.Excluir();
//            var a3 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Cessna", "172", 2017, 4, 10));
//            var a4 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Bombardier", "CRJ200", 2016, 50, 200));
//            await context.Avioes.AddRangeAsync(a1, a2, ex, a3, a4);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarAvioesAsync(new ListarAvioesContrato(null, null), CancellationToken.None);
//            lista.Should().HaveCount(4);
//        }

//        [Test]
//        public async Task ListarAvioesAsync_RetornaLista_ComFiltros_ApenasValidos()
//        {
//            var a1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            var a2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            var ex = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2019, 100, 2)); ex.Excluir();
//            var a3 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 3));
//            var a4 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Cessna", "172", 2017, 4, 10));
//            await context.Avioes.AddRangeAsync(a1, a2, ex, a3, a4);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarAvioesAsync(new ListarAvioesContrato("Bo", "37"), CancellationToken.None);
//            lista.Should().HaveCount(1)
//                 .And.ContainSingle(x => x.Marca == "Boeing" && x.Modelo == "737");
//        }

//        [Test]
//        public async Task ListarAvioesAsync_RetornaVazio()
//        {
//            var ex1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2019, 100, 2)); ex1.Excluir();
//            var ex2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2018, 100, 2)); ex2.Excluir();
//            await context.Avioes.AddRangeAsync(ex1, ex2);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarAvioesAsync(new ListarAvioesContrato(null, null), CancellationToken.None);
//            lista.Should().BeEmpty();
//        }

//        [Test]
//        public async Task CriarAviaoAsync_CriacaoOk()
//        {
//            var contrato = new CriarAviaoContrato("Boeing", "737", 2020, 180, 0);
//            var resultado = await servico.CriarAviaoAsync(contrato, CancellationToken.None);
//            var saved = await context.Avioes.FirstAsync();
//            resultado.Id.Should().Be(saved.Id);
//        }

//        [Test]
//        public async Task AtualizarAviaoAsync_AtualizacaoOk()
//        {
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            await context.Avioes.AddRangeAsync(av1, av2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Avioes.FirstAsync(a => a.Marca == "Boeing")).Id;

//            var resultado = await servico.AtualizarAviaoAsync(idDb, new AtualizarAviaoContrato("737 MAX", 2024, 220), CancellationToken.None);
//            resultado.Modelo.Should().Be("737 MAX");
//            resultado.AnoFabricacao.Should().Be(2024);
//            resultado.QtdMaxPassageiros.Should().Be(220);
//        }

//        [Test]
//        public async Task AtualizarAviaoAsync_NaoEncontrado()
//        {
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            await context.Avioes.AddRangeAsync(av1, av2);
//            await context.SaveChangesAsync();
//            var idInvalid = Guid.NewGuid();

//            Assert.ThrowsAsync<CustomException>(
//                () => servico.AtualizarAviaoAsync(
//                    idInvalid,
//                    new AtualizarAviaoContrato("A320", 2021, 150),
//                    CancellationToken.None)
//                );
//        }

//        [Test]
//        public async Task AtualizarAviaoAsync_EstaExcluido()
//        {
//            var avEx = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 5)); avEx.Excluir();
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 2));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 3));
//            await context.Avioes.AddRangeAsync(avEx, av1, av2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Avioes.FirstAsync(a => a.Excluido)).Id;

//            Assert.ThrowsAsync<CustomException>(
//                () => servico.AtualizarAviaoAsync(
//                    idDb,
//                    new AtualizarAviaoContrato("737", 2020, 180),
//                    CancellationToken.None)
//                );
//        }

//        [Test]
//        public async Task DeletarAviaoAsync_ExcluidoOk()
//        {
//            var av1 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var av2 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Embraer", "E190", 2019, 100, 0));
//            var av3 = conversor.ConverterParaEntidade(new CriarAviaoContrato("Airbus", "A320", 2018, 150, 0));
//            await context.Avioes.AddRangeAsync(av1, av2, av3);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Avioes.FirstAsync(a => a.Marca == "Boeing")).Id;

//            var result = await servico.DeletarAviaoAsync(idDb, CancellationToken.None);
//            result.Should().Be(idDb);
//            (await context.Avioes.FindAsync(idDb))!.Excluido.Should().BeTrue();
//        }

//        [Test]
//        public void DeletarAviaoAsync_NaoEncontrado()
//        {
//            Assert.ThrowsAsync<CustomException>(() => servico.DeletarAviaoAsync(Guid.NewGuid(), CancellationToken.None));
//        }

//        public class FakeMensageria : IMensageriaProdutorServico
//        {
//            public List<(string Queue, Guid VooId)> Published { get; } = new();
//            public Task PublicarMensagemAsync<Tmensagem>(Tmensagem mensagem, CancellationToken ct)
//            {
//                return Task.CompletedTask;
//            }
//        }
//    }
//}