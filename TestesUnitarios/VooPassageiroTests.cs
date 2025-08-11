//using Desafio001.Dominio.Contratos;
//using Desafio001.Dominio.Entidades;
//using Desafio001.Dominio.Entidades.Enums;
//using Desafio001.Dominio.Exceptions;
//using Desafio001.Dominio.Interfaces;
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
//    public class VooPassageiroServicoTests
//    {
//        private AppDbContext context;
//        private IVooPassageiroRepositorio vooPassRep;
//        private IPassageiroRepositorio passageiroRep;
//        private IVooRepositorio vooRep;
//        private IPilotoRepositorio pilotoRep;
//        private IAviaoRepositorio aviaoRep;
//        private VooPassageiroConversor vpConv;
//        private PassageiroConversor pConv;
//        private VooConversor vConv;
//        private PilotoConversor pilConv;
//        private AviaoConversor avConv;
//        private FakeMensageria mensageria;
//        private VooPassageiroServico servico;

//        [SetUp]
//        public void Setup()
//        {
//            var opts = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            context = new AppDbContext(opts);
//            vooPassRep = new VooPassageiroRepositorio(context);
//            passageiroRep = new PassageiroRepositorio(context);
//            vooRep = new VooRepositorio(context);
//            pilotoRep = new PilotoRepositorio(context);
//            aviaoRep = new AviaoRepositorio(context);
//            vpConv = new VooPassageiroConversor();
//            pConv = new PassageiroConversor();
//            vConv = new VooConversor();
//            pilConv = new PilotoConversor();
//            avConv = new AviaoConversor();
//            mensageria = new FakeMensageria();
//            servico = new VooPassageiroServico(vpConv, vooPassRep, passageiroRep, vooRep, mensageria, NullLoggerFactory.Instance.CreateLogger<VooPassageiroServico>());
//        }

//        [TearDown]
//        public void TearDown() => context.Dispose();

//        [Test]
//        public async Task ListarPassagensAsync_RetornaApenasValidas()
//        {
//            var pa1 = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A1", "111", new DateOnly(1990, 1, 1), "a1@x.com"));
//            var pa2 = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A2", "222", new DateOnly(1990, 2, 2), "a2@x.com"));
//            var av1 = avConv.ConverterParaEntidade(new CriarAviaoContrato( "B", "C", 2020, 100, 0));
//            var pi1 = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "333", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddRangeAsync(pa1, pa2);
//            await context.Avioes.AddAsync(av1);
//            await context.Pilotos.AddAsync(pi1);
//            await context.SaveChangesAsync();
//            var v1 = new Voo(av1.Id, pi1.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v1);
//            await context.SaveChangesAsync();
//            var vp1 = vpConv.ConverterParaEntidade(pa1.Id, v1.Id);
//            var vp2 = vpConv.ConverterParaEntidade(pa2.Id, v1.Id);
//            var vpEx = vpConv.ConverterParaEntidade(pa2.Id, v1.Id); vpEx.Excluir();
//            await context.VoosPassageiros.AddRangeAsync(vp1, vp2, vpEx);
//            await context.SaveChangesAsync();

//            var res = await servico.ListarPassagensAsync(new ListarPassagensContrato(null, null, null), CancellationToken.None);
//            res.Should().HaveCount(2);
//        }

//        [Test]
//        public async Task ListarPassagensAsync_Filtros()
//        {
//            var pa1 = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A1", "111", new DateOnly(1990, 1, 1), "a1@x.com"));
//            var pa2 = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A2", "222", new DateOnly(1990, 2, 2), "a2@x.com"));
//            var av1 = avConv.ConverterParaEntidade(new CriarAviaoContrato("B", "C", 2020, 100, 0));
//            var pi1 = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "333", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddRangeAsync(pa1, pa2);
//            await context.Avioes.AddAsync(av1);
//            await context.Pilotos.AddAsync(pi1);
//            await context.SaveChangesAsync();
//            var v1 = new Voo(av1.Id, pi1.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            var v2 = new Voo(av1.Id, pi1.Id, DateOnly.FromDateTime(DateTime.Today.AddDays(1)), new TimeOnly(9, 0), new TimeOnly(11, 0));
//            await context.Voos.AddRangeAsync(v1, v2);
//            await context.SaveChangesAsync();
//            var vp1 = vpConv.ConverterParaEntidade(pa1.Id, v1.Id);
//            var vp2 = vpConv.ConverterParaEntidade(pa2.Id, v2.Id);
//            vp2.AlterarStatus(VooPassageiroStatus.Presente);
//            await context.VoosPassageiros.AddRangeAsync(vp1, vp2);
//            await context.SaveChangesAsync();

//            var r1 = await servico.ListarPassagensAsync(new ListarPassagensContrato(v1.Id, null, null), CancellationToken.None);
//            r1.Should().HaveCount(1);
//            var r2 = await servico.ListarPassagensAsync(new ListarPassagensContrato(null, pa2.Id, null), CancellationToken.None);
//            r2.Should().HaveCount(1);
//            var r3 = await servico.ListarPassagensAsync(new ListarPassagensContrato(null, null, VooPassageiroStatus.Presente), CancellationToken.None);
//            r3.Should().HaveCount(1);
//        }

//        [Test]
//        public async Task ComprarPassagemAsync_Sucesso()
//        {
//            var pa = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A", "11111111111", new DateOnly(1990, 1, 1), "a@x.com"));
//            var av = avConv.ConverterParaEntidade(new CriarAviaoContrato("B", "C", 2020, 2, 0));
//            var pi = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "22222222222", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddAsync(pa);
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(pi);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            typeof(Voo).GetProperty("QtdPassagensVendidas")!.SetValue(v, 1);
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();

//            var c = new ComprarPassagemContrato(pa.Id, v.Id);
//            var res = await servico.ComprarPassagemAsync(c, CancellationToken.None);
//            res.PassageiroId.Should().Be(pa.Id);
//            res.VooId.Should().Be(v.Id);
//            (await context.Voos.FindAsync(v.Id))!.QtdPassagensVendidas.Should().Be(2);
//        }

//        [Test]
//        public async Task ComprarPassagemAsync_Erros()
//        {
//            var pa = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A", "11111111111", new DateOnly(1990, 1, 1), "a@x.com"));
//            var av = avConv.ConverterParaEntidade(new CriarAviaoContrato("B", "C", 2020, 1, 0));
//            var pi = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "22222222222", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddAsync(pa);
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(pi);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();

//            // passageiro nao existe
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(Guid.NewGuid(), v.Id), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // passageiro excluido
//            pa.Excluir(); context.Update(pa); await context.SaveChangesAsync();
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(pa.Id, v.Id), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // voo nao existe
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(pa.Id, Guid.NewGuid()), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // voo excluido
//            v.Excluir(); context.Update(v); await context.SaveChangesAsync();
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(pa.Id, v.Id), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // status proibido
//            var v2 = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            typeof(Voo).GetProperty("Status")!.SetValue(v2, VooStatus.Concluido);
//            await context.Voos.AddAsync(v2);
//            await context.SaveChangesAsync();
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(pa.Id, v2.Id), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // limite de capacidade
//            var v3 = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            typeof(Voo).GetProperty("QtdPassagensVendidas")!.SetValue(v3, 2);
//            await context.Voos.AddAsync(v3);
//            await context.SaveChangesAsync();
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(pa.Id, v3.Id), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // duplicado
//            var vp = vpConv.ConverterParaEntidade(pa.Id, v.Id);
//            await context.VoosPassageiros.AddAsync(vp);
//            await context.SaveChangesAsync();
//            await servico.Invoking(s => s.ComprarPassagemAsync(new ComprarPassagemContrato(pa.Id, v.Id), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//        }

//        [Test]
//        public async Task CheckinPassagemAsync_Sucesso()
//        {
//            var pa = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A", "11111111111", new    DateOnly(1990, 1, 1), "a@x.com"));
//            var av = avConv.ConverterParaEntidade(new CriarAviaoContrato("B", "C", 2020, 100, 0));
//            var pi = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "22222222222", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddAsync(pa);
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(pi);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();
//            var vp = vpConv.ConverterParaEntidade(pa.Id, v.Id);
//            await context.VoosPassageiros.AddAsync(vp);
//            await context.SaveChangesAsync();

//            var ok = await servico.CheckinPassagemAsync(vp.Id, CancellationToken.None);
//            ok.Should().BeTrue();
//            vp.Status.Should().Be(VooPassageiroStatus.Presente);
//            mensageria.Published.Should().Contain(x => x.Queue == "notificar_overbooking");
//        }

//        [Test]
//        public async Task CheckinPassagemAsync_Erros()
//        {
//            // nao encontrado
//            await servico.Invoking(s => s.CheckinPassagemAsync(Guid.NewGuid(), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // excluido
//            var pa = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A", "11111111111", new DateOnly(1990, 1, 1), "a@x.com"));
//            var av = avConv.ConverterParaEntidade(new CriarAviaoContrato("B", "C", 2020, 100, 0));
//            var pi = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "22222222222", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddAsync(pa);
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(pi);
//            await context.SaveChangesAsync();
//            var v = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();
//            var vp = vpConv.ConverterParaEntidade(pa.Id, v.Id); vp.Excluir();
//            await context.VoosPassageiros.AddAsync(vp);
//            await context.SaveChangesAsync();
//            await servico.Invoking(s => s.CheckinPassagemAsync(vp.Id, CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//        }

//        [Test]
//        public async Task DeletarPassagemAsync_Sucesso()
//        {
//            var pa = pConv.ConverterParaEntidade(new CriarPassageiroContrato(
//                "A", "11111111111", new DateOnly(1990, 1, 1), "a@x.com"));
//            var av = avConv.ConverterParaEntidade(new CriarAviaoContrato(
//                "B", "C", 2020, 100, 0));
//            var pi = pilConv.ConverterParaEntidade(new CriarPilotoContrato(
//                "X", "22222222222", DateOnly.FromDateTime(DateTime.Today), 0));
//            await context.Passageiros.AddAsync(pa);
//            await context.Avioes.AddAsync(av);
//            await context.Pilotos.AddAsync(pi);
//            await context.SaveChangesAsync();

//            var v = new Voo(
//                av.Id,
//                pi.Id,
//                DateOnly.FromDateTime(DateTime.Today),
//                new TimeOnly(8, 0),
//                new TimeOnly(10, 0));
//            await context.Voos.AddAsync(v);
//            await context.SaveChangesAsync();

//            var vp = vpConv.ConverterParaEntidade(pa.Id, v.Id);
//            v.NovoPassageiro();

//            await context.VoosPassageiros.AddAsync(vp);
//            await context.SaveChangesAsync();

//            var res = await servico.DeletarPassagemAsync(vp.Id, CancellationToken.None);
//            res.Should().Be(vp.Id);

//            (await context.VoosPassageiros.FindAsync(vp.Id))!.Excluido.Should().BeTrue();
//            (await context.Voos.FindAsync(v.Id))!.QtdPassagensVendidas.Should().Be(0);
//        }

//        [Test]
//        public void DeletarPassagemAsync_ERROS()
//        {
//            // nao encontrado
//            servico.Invoking(s => s.DeletarPassagemAsync(Guid.NewGuid(), CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//            // excluido
//            var pa = pConv.ConverterParaEntidade(new CriarPassageiroContrato("A", "11111111111", new DateOnly(1990, 1, 1), "a@x.com"));
//            var av = avConv.ConverterParaEntidade(new CriarAviaoContrato("B", "C", 2020, 100, 0));
//            var pi = pilConv.ConverterParaEntidade(new CriarPilotoContrato("X", "22222222222", DateOnly.FromDateTime(DateTime.Today), 0));
//            context.Passageiros.Add(pa); context.Avioes.Add(av); context.Pilotos.Add(pi); context.SaveChanges();
//            var v = new Voo(av.Id, pi.Id, DateOnly.FromDateTime(DateTime.Today), new TimeOnly(8, 0), new TimeOnly(10, 0));
//            context.Voos.Add(v); context.SaveChanges();
//            var vp = vpConv.ConverterParaEntidade(pa.Id, v.Id); vp.AlterarStatus(VooPassageiroStatus.Presente);
//            context.VoosPassageiros.Add(vp); context.SaveChanges();

//            servico.Invoking(s => s.DeletarPassagemAsync(vp.Id, CancellationToken.None))
//                .Should().ThrowAsync<CustomException>();
//        }

//        public class FakeMensageria : IMensageriaProdutorServico
//        {
//            public List<(string Queue, Guid VooId)> Published { get; } = new();
//            public Task PublicarMensagemAsync<T>(T contrato, CancellationToken ct)
//            {
//                return Task.CompletedTask;
//            }
//        }
//    }
//}
