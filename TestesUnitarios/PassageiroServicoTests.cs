//using Desafio001.Dominio.Contratos;
//using Desafio001.Dominio.Exceptions;
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
//    public class PassageiroServicoTests
//    {
//        private AppDbContext context;
//        private IPassageiroRepositorio repo;
//        private PassageiroConversor conversor;
//        private PassageiroServico servico;

//        [SetUp]
//        public void Setup()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            context = new AppDbContext(options);
//            repo = new PassageiroRepositorio(context);
//            conversor = new PassageiroConversor();
//            var logger = NullLoggerFactory.Instance.CreateLogger<PassageiroServico>();
//            servico = new PassageiroServico(repo, conversor, logger);
//        }

//        [TearDown]
//        public void TearDown() => context.Dispose();

//        [Test]
//        public async Task BuscarPassageiroAsync_RetornaOk()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            var p3 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Caio", "22233344455", new DateOnly(1992, 3, 3), "caio@example.com"));
//            await context.Passageiros.AddRangeAsync(p1, p2, p3);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Passageiros.FirstAsync(x => x.Nome == "Ana")).Id;

//            var result = await servico.BuscarPassageiroAsync(idDb, CancellationToken.None);
//            result.Should().NotBeNull();
//            result.Id.Should().Be(idDb);
//            result.Nome.Should().Be("Ana");
//            result.Documento.Should().Be("12345678901");
//        }

//        [Test]
//        public async Task BuscarPassageiroAsync_NaoEncontrando()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            await context.Passageiros.AddRangeAsync(p1, p2);
//            await context.SaveChangesAsync();

//            Func<Task> act = () => servico.BuscarPassageiroAsync(Guid.NewGuid(), CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("Entidade passageiro não localizado");
//        }

//        [Test]
//        public async Task BuscarPassageiroAsync_EstaExcluido()
//        {
//            var ex = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com")); ex.Excluir();
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            await context.Passageiros.AddRangeAsync(ex, p1);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Passageiros.FirstAsync(x => x.Excluido)).Id;

//            Func<Task> act = () => servico.BuscarPassageiroAsync(idDb, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("Entidade passageiro já excluida");
//        }

//        [Test]
//        public async Task ListarPassageirosAsync_RetornaLista_ApenasValidos()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            var ex = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Caio", "22233344455", new DateOnly(1992, 3, 3), "caio@example.com")); ex.Excluir();
//            var p3 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Dara", "33344455566", new DateOnly(1995, 7, 7), "dara@example.com"));
//            await context.Passageiros.AddRangeAsync(p1, p2, ex, p3);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarPassageirosAsync(new ListarPassageirosContrato(null, null), CancellationToken.None);
//            lista.Should().HaveCount(3);
//        }

//        [Test]
//        public async Task ListarPassageirosAsync_RetornaLista_ComFiltros_ApenasValidos()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            var ex = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Caio", "22233344455", new DateOnly(1992, 3, 3), "caio@example.com")); ex.Excluir();
//            await context.Passageiros.AddRangeAsync(p1, p2, ex);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarPassageirosAsync(new ListarPassageirosContrato(Documento: "12345678901", Nome: null), CancellationToken.None);
//            lista.Should().HaveCount(1).And.ContainSingle(x => x.Documento == "12345678901");
//        }

//        [Test]
//        public async Task ListarPassageirosAsync_RetornaVazio()
//        {
//            var ex1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com")); ex1.Excluir();
//            await context.Passageiros.AddAsync(ex1);
//            await context.SaveChangesAsync();

//            var lista = await servico.ListarPassageirosAsync(new ListarPassageirosContrato(null, null), CancellationToken.None);
//            lista.Should().BeEmpty();
//        }

//        [Test]
//        public async Task CriarPassageiroAsync_CriacaoOk()
//        {
//            var contrato = new CriarPassageiroContrato(
//                Nome: "Ana",
//                Documento: "123.456.789-01",
//                DataNascimento: new DateOnly(1990, 1, 1),
//                Email: "ana@example.com"
//            );
//            var result = await servico.CriarPassageiroAsync(contrato, CancellationToken.None);
//            var saved = await context.Passageiros.FirstAsync();

//            result.Id.Should().Be(saved.Id);
//            result.Documento.Should().Be("12345678901");
//            saved.Documento.Should().Be("12345678901");
//        }

//        [Test]
//        public async Task CriarPassageiroAsync_DuplicateThrows()
//        {
//            var contrato = new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com");
//            var p1 = conversor.ConverterParaEntidade(contrato);
//            await context.Passageiros.AddAsync(p1);
//            await context.SaveChangesAsync();

//            Func<Task> act = () => servico.CriarPassageiroAsync(contrato, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("CPF Duplicado");
//        }

//        [Test]
//        public async Task AtualizarPassageiroAsync_AtualizacaoOk()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            await context.Passageiros.AddRangeAsync(p1, p2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Passageiros.FirstAsync(x => x.Nome == "Ana")).Id;

//            var result = await servico.AtualizarPassageiroAsync(
//                idDb,
//                new AtualizarPassageiroContrato("Ana Silva", new DateOnly(1990, 1, 1), "ana.silva@example.com"),
//                CancellationToken.None
//            );
//            result.Nome.Should().Be("Ana Silva");
//            result.Email.Should().Be("ana.silva@example.com");
//        }

//        [Test]
//        public async Task AtualizarPassageiroAsync_NaoEncontrado()
//        {

//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            var p3 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Caio", "22233344455", new DateOnly(1992, 3, 3), "caio@example.com"));
//            await context.Passageiros.AddRangeAsync(p1, p2, p3);
//            await context.SaveChangesAsync();

//            var contrato = new AtualizarPassageiroContrato("X", DateOnly.FromDateTime(DateTime.Today), "x@example.com");
//            Assert.ThrowsAsync<CustomException>(() => servico.AtualizarPassageiroAsync(Guid.NewGuid(), contrato, CancellationToken.None));
//        }

//        [Test]
//        public async Task AtualizarPassageiroAsync_EstaExcluido()
//        {
//            var ex = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com")); ex.Excluir();
//            await context.Passageiros.AddAsync(ex);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Passageiros.FirstAsync(x => x.Excluido)).Id;

//            var contrato = new AtualizarPassageiroContrato("Ana Silva", DateOnly.FromDateTime(DateTime.Today), "ana.silva@example.com");
//            Assert.ThrowsAsync<CustomException>(() => servico.AtualizarPassageiroAsync(idDb, contrato, CancellationToken.None));
//        }

//        [Test]
//        public async Task DeletarPassageiroAsync_ExcluidoOk()
//        {
//            var p1 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Ana", "12345678901", new DateOnly(1990, 1, 1), "ana@example.com"));
//            var p2 = conversor.ConverterParaEntidade(new CriarPassageiroContrato("Bruno", "10987654321", new DateOnly(1985, 5, 5), "bruno@example.com"));
//            await context.Passageiros.AddRangeAsync(p1, p2);
//            await context.SaveChangesAsync();
//            var idDb = (await context.Passageiros.FirstAsync(x => x.Nome == "Ana")).Id;

//            var result = await servico.DeletarPassageiroAsync(idDb, CancellationToken.None);
//            result.Should().Be(idDb);
//            (await context.Passageiros.FindAsync(idDb))!.Excluido.Should().BeTrue();
//        }

//        [Test]
//        public void DeletarPassageiroAsync_NaoEncontrado()
//        {
//            Assert.ThrowsAsync<CustomException>(() => servico.DeletarPassageiroAsync(Guid.NewGuid(), CancellationToken.None));
//        }
//    }
//}