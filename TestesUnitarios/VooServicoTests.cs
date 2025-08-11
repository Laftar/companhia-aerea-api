//using Desafio001.Dominio.Contratos;
//using Desafio001.Dominio.Entidades;
//using Desafio001.Dominio.Entidades.Enums;
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
//    public class VooServicoTests
//    {
//        private AppDbContext context;
//        private IVooRepositorio vooRepo;
//        private IPilotoRepositorio pilotoRepo;
//        private IAviaoRepositorio aviaoRepo;
//        private VooConversor vooConv;
//        private PilotoConversor pilotoConv;
//        private AviaoConversor aviaoConv;
//        private FakeMensageria mensageria;
//        private VooServico servico;

//        [SetUp]
//        public void Setup()
//        {
//            var opts = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            context = new AppDbContext(opts);
//            vooRepo = new VooRepositorio(context);
//            pilotoRepo = new PilotoRepositorio(context);
//            aviaoRepo = new AviaoRepositorio(context);
//            vooConv = new VooConversor();
//            pilotoConv = new PilotoConversor();
//            aviaoConv = new AviaoConversor();
//            mensageria = new FakeMensageria();
//            var logger = NullLoggerFactory.Instance.CreateLogger<VooServico>();
//            servico = new VooServico(vooRepo, vooConv, pilotoRepo, aviaoRepo, logger, mensageria);
//        }

//        [TearDown]
//        public void TearDown() => context.Dispose();

//        [Test]
//        public async Task BuscarVooAsync_RetornaOk()
//        {
//            var av1 = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p1 = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av1);
//            await context.Pilotos.AddAsync(p1);
//            await context.SaveChangesAsync();
//            var c = new CriarVooContrato(
//                AviaoId: av1.Id,
//                PilotoId: p1.Id,
//                DataVoo: DateOnly.FromDateTime(DateTime.Today),
//                HorarioPrevSaida: new TimeOnly(8, 0),
//                HorarioPrevChegada: new TimeOnly(10, 0)
//            );
//            var criado = await servico.CriarVooAsync(c, CancellationToken.None);

//            var fetched = await servico.BuscarVooAsync(criado.Id, CancellationToken.None);
//            fetched.Id.Should().Be(criado.Id);
//            fetched.Status.Should().Be(VooStatus.Pendente);
//            fetched.AviaoId.Should().Be(av1.Id);
//            fetched.PilotoId.Should().Be(p1.Id);
//        }

//        [Test]
//        public void BuscarVooAsync_NaoEncontrando()
//        {
//            Func<Task> act = () => servico.BuscarVooAsync(Guid.NewGuid(), CancellationToken.None);
//            act.Should().ThrowAsync<CustomException>().WithMessage("*não encontrada*");
//        }

//        [Test]
//        public async Task BuscarVooAsync_EstaExcluido()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var voo = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            voo.Excluir();
//            await context.Voos.AddAsync(voo);
//            await context.SaveChangesAsync();

//            Func<Task> act = () => servico.BuscarVooAsync(voo.Id, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("{VooExcluido} Voo deletado");
//        }

//        [Test]
//        public async Task ListarVoosAsync_RetornaSomenteValidos()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var v1 = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            var v2 = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(9, 0), new TimeOnly(11, 0));
//            var ex = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(7, 0), new TimeOnly(9, 0)); ex.Excluir();
//            await context.Voos.AddRangeAsync(v1, v2, ex);
//            await context.SaveChangesAsync();

//            var list = await servico.ListarVoosAsync(new ListarVoosContrato(null, null, null), CancellationToken.None);
//            list.Should().HaveCount(2);
//        }

//        [Test]
//        public async Task CriarVooAsync_Sucesso()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var c = new CriarVooContrato(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));

//            var result = await servico.CriarVooAsync(c, CancellationToken.None);
//            result.Id.Should().NotBeEmpty();
//            result.Status.Should().Be(VooStatus.Pendente);
//        }

//        [Test]
//        public void CriarVooAsync_PilotoNaoEncontrado()
//        {
//            var c = new CriarVooContrato(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            Func<Task> act = () => servico.CriarVooAsync(c, CancellationToken.None);
//            act.Should().ThrowAsync<CustomException>().WithMessage("*PilotoId*");
//        }

//        [Test]
//        public async Task CriarVooAsync_AviaoNaoEncontrado()
//        {
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var c = new CriarVooContrato(Guid.NewGuid(), p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            Func<Task> act = () => servico.CriarVooAsync(c, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("*AviaoId*");
//        }

//        [Test]
//        public async Task AtualizarVooAsync_Sucesso()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();

//            var upd = new AtualizarVooContrato(DateOnly.FromDateTime(DateTime.Today.AddDays(1)), new TimeOnly(9, 0), new TimeOnly(11, 0));
//            var result = await servico.AtualizarVooAsync(v.Id, upd, CancellationToken.None);
//            result.DataVoo.Should().Be(upd.DataVoo);
//        }

//        [Test]
//        public void AtualizarVooAsync_NaoEncontrado()
//        {
//            var upd = new AtualizarVooContrato(DateOnly.FromDateTime(DateTime.Today), new TimeOnly(9, 0), new TimeOnly(11, 0));
//            Assert.ThrowsAsync<CustomException>(() => servico.AtualizarVooAsync(Guid.NewGuid(), upd, CancellationToken.None));
//        }

//        [Test]
//        public async Task AtualizarVooAsync_StatusProibido()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            v.ConcluirVoo(TimeOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();
//            var upd = new AtualizarVooContrato(DateOnly.FromDateTime(DateTime.Today), new TimeOnly(9, 0), new TimeOnly(11, 0));
//            Func<Task> act = () => servico.AtualizarVooAsync(v.Id, upd, CancellationToken.None);
//            await act.Should().ThrowAsync<CustomException>().WithMessage("*Status do voo não permite*");
//        }

//        [Test]
//        public async Task ConcluirVooAsync_Sucesso()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();

//            var concl = new ConcluirVooContrato(new TimeOnly(8, 15), new TimeOnly(10, 5));
//            var result = await servico.ConcluirVooAsync(v.Id, concl, CancellationToken.None);
//            result.Status.Should().Be(VooStatus.Concluido);
//            result.HorarioRealSaida.Should().Be(concl.HorarioRealSaida);
//        }

//        [Test]
//        public void ConcluirVooAsync_StatusProibido()
//        {
//            var v = new Voo(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            v.ConcluirVoo(new TimeOnly(8, 15), new TimeOnly(10, 5));
//            Assert.ThrowsAsync<CustomException>(() => servico.ConcluirVooAsync(v.Id, new ConcluirVooContrato(new TimeOnly(8, 15), new TimeOnly(10, 5)), CancellationToken.None));
//        }

//        [Test]
//        public async Task DeletarVooAsync_Pendente_Sucesso()
//        {
//            var av = aviaoConv.ConverterParaEntidade(new CriarAviaoContrato("Boeing", "737", 2020, 180, 0));
//            var p = pilotoConv.ConverterParaEntidade(new CriarPilotoContrato("Ana", "12345678901", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(p);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, p.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();

//            var idDb = (await context.Voos.FirstAsync()).Id;
//            var result = await servico.DeletarVooAsync(idDb, CancellationToken.None);
//            result.Should().Be(idDb);
//            (await context.Voos.FindAsync(idDb))!.Excluido.Should().BeTrue();
//            mensageria.Published.Should().ContainSingle(x => x.VooId == idDb);
//        }

//        [Test]
//        public void DeletarVooAsync_StatusProibido()
//        {
//            var v = new Voo(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            v.ConcluirVoo(TimeOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now));
//            Assert.ThrowsAsync<CustomException>(() => servico.DeletarVooAsync(v.Id, CancellationToken.None));
//        }

//        [Test]
//        public void DeletarVooAsync_NaoEncontrado()
//        {
//            Assert.ThrowsAsync<CustomException>(() => servico.DeletarVooAsync(Guid.NewGuid(), CancellationToken.None));
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
