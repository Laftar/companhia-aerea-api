//using Desafio001.Dominio.Contratos;
//using Desafio001.Dominio.Entidades;
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

//namespace TestesUnitarios
//{
//    [TestFixture]
//    public class PilotoServicoTests
//    {
//        private AppDbContext context;
//        private IPilotoRepositorio repo;
//        private PilotoConversor conversor;
//        private FakeMensageria mensageria;
//        private PilotoServico servico;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            context = new AppDbContext(options);
//            repo = new PilotoRepositorio(context);
//            conversor = new PilotoConversor();
//            mensageria = new FakeMensageria();
//            var logger = NullLoggerFactory.Instance.CreateLogger<PilotoServico>();
//            servico = new PilotoServico(repo, conversor, logger, mensageria);
//        }

//        [TearDown]
//        public void TearDown() => context.Dispose();

//        [Test]
//        public async Task BuscarPilotoAsync_RetornaOk()
//        {
//            var c1 = new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5);
//            var c2 = new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10);
//            var c3 = new CriarPilotoContrato("Caio", "22233344455", DateOnly.FromDateTime(DateTime.Today), 3);
//            var c4 = new CriarPilotoContrato("Dara", "33344455566", DateOnly.FromDateTime(DateTime.Today), 7);
//            var p1 = conversor.ConverterParaEntidade(c1);
//            var p2 = conversor.ConverterParaEntidade(c2);
//            var p3 = conversor.ConverterParaEntidade(c3);
//            var p4 = conversor.ConverterParaEntidade(c4);
//            await context.Pilotos.AddRangeAsync(p1, p2, p3, p4);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Pilotos.FirstAsync(x => x.Nome == "Ana")).Id;

//            var result = await servico.BuscarPilotoAsync(idDb, CancellationToken.None);
//            result.Id.Should().Be(idDb);
//            result.Nome.Should().Be("Ana");
//            result.Documento.Should().Be("12345678901");
//            result.QtdVoosRealizados.Should().Be(5);
//        }

//        [Test]
//        public async Task BuscarPilotoAsync_NaoEncontrando()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5));
//            var p2 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10));
//            await context.Pilotos.AddRangeAsync(p1, p2);
//            await context.SaveChangesAsync();

//            Func<Task> act = () => servico.BuscarPilotoAsync(Guid.NewGuid(), CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("*não encontrada*");
//        }

//        [Test]
//        public async Task BuscarPilotoAsync_EstaExcluido()
//        {
//            var ex = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5)); ex.Excluir();
//            var p2 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10));
//            await context.Pilotos.AddRangeAsync(ex, p2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Pilotos.FirstAsync(x => x.Excluido)).Id;

//            Func<Task> act = () => servico.BuscarPilotoAsync(idDb, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("*deletada*");
//        }

//        [Test]
//        public async Task ListarPilotosAsync_RetornaLista_ApenasValidos()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5));
//            var p2 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10));
//            var ex = conversor.ConverterParaEntidade(new CriarPilotoContrato("Caio", "22233344455", DateOnly.FromDateTime(DateTime.Today), 3)); ex.Excluir();
//            var p3 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Dara", "33344455566", DateOnly.FromDateTime(DateTime.Today), 7));
//            var p4 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Eli", "44455566677", DateOnly.FromDateTime(DateTime.Today), 2));
//            await context.Pilotos.AddRangeAsync(p1, p2, ex, p3, p4);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarPilotosAsync(new ListarPilotosContrato(null, null), CancellationToken.None);
//            lista.Should().HaveCount(4);
//        }

//        [Test]
//        public async Task ListarPilotosAsync_RetornaLista_ComFiltros_ApenasValidos()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5));
//            var p2 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10));
//            var ex = conversor.ConverterParaEntidade(new CriarPilotoContrato("Caio", "22233344455", DateOnly.FromDateTime(DateTime.Today), 3)); ex.Excluir();
//            await context.Pilotos.AddRangeAsync(p1, p2, ex);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarPilotosAsync(new ListarPilotosContrato(null, "109"), CancellationToken.None);
//            lista.Should().HaveCount(1).And.ContainSingle(x => x.Documento == "10987654321");
//        }

//        [Test]
//        public async Task ListarPilotosAsync_RetornaVazio()
//        {
//            var ex1 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5)); ex1.Excluir();
//            await context.Pilotos.AddAsync(ex1);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarPilotosAsync(new ListarPilotosContrato(null, null), CancellationToken.None);
//            lista.Should().BeEmpty();
//        }

//        [Test]
//        public async Task CriarPilotoAsync_CriacaoOk()
//        {
//            var contrato = new CriarPilotoContrato("Ana", "123.456.789-01", DateOnly.FromDateTime(DateTime.Today), 5);
//            var result = await servico.CriarPilotoAsync(contrato, CancellationToken.None);
//            var saved = await context.Pilotos.FirstAsync();
//            result.Id.Should().Be(saved.Id);
//            result.Documento.Should().Be("12345678901");
//        }

//        [Test]
//        public async Task CriarPilotoAsync_DuplicateThrows()
//        {
//            var contrato = new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5);
//            var p1 = conversor.ConverterParaEntidade(contrato);
//            await context.Pilotos.AddAsync(p1);
//            await context.SaveChangesAsync();

//            Func<Task> act = () => servico.CriarPilotoAsync(contrato, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("*Duplicado*");
//        }

//        [Test]
//        public async Task AtualizarPilotoAsync_AtualizacaoOk()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5));
//            var p2 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10));
//            await context.Pilotos.AddRangeAsync(p1, p2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Pilotos.FirstAsync(x => x.Nome == "Ana")).Id;

//            var result = await servico.AtualizarPilotoAsync(idDb, new AtualizarPilotoContrato("Ana Silva", DateOnly.FromDateTime(DateTime.Today), 6), CancellationToken.None);
//            result.Nome.Should().Be("Ana Silva");
//            result.QtdVoosRealizados.Should().Be(6);
//        }

//        [Test]
//        public void AtualizarPilotoAsync_NaoEncontrado()
//        {
//            var contrato = new AtualizarPilotoContrato("X", DateOnly.FromDateTime(DateTime.Today), 0);
//            Assert.ThrowsAsync<CustomException>(() => servico.AtualizarPilotoAsync(Guid.NewGuid(), contrato, CancellationToken.None));
//        }

//        [Test]
//        public async Task AtualizarPilotoAsync_EstaExcluido()
//        {
//            var ex = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5)); ex.Excluir();
//            await context.Pilotos.AddAsync(ex);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Pilotos.FirstAsync(x => x.Excluido)).Id;

//            var contrato = new AtualizarPilotoContrato("Ana Silva", DateOnly.FromDateTime(DateTime.Today), 6);
//            Assert.ThrowsAsync<CustomException>(() => servico.AtualizarPilotoAsync(idDb, contrato, CancellationToken.None));
//        }

//        [Test]
//        public async Task DeletarPilotoAsync_ExcluidoOk()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 5));
//            var p2 = conversor.ConverterParaEntidade(new CriarPilotoContrato("Bruno", "10987654321", DateOnly.FromDateTime(DateTime.Today), 10));
//            await context.Pilotos.AddRangeAsync(p1, p2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Pilotos.FirstAsync(x => x.Nome == "Ana")).Id;

//            var result = await servico.DeletarPilotoAsync(idDb, CancellationToken.None);
//            result.Should().Be(idDb);
//            (await context.Pilotos.FindAsync(idDb))!.Excluido.Should().BeTrue();
//        }

//        [Test]
//        public void DeletarPilotoAsync_NaoEncontrado()
//        {
//            Assert.ThrowsAsync<CustomException>(() => servico.DeletarPilotoAsync(Guid.NewGuid(), CancellationToken.None));
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
