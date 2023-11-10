using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissoController : ControllerBase
    {
        private ServicoCompromisso servicoCompromisso;

        private ServicoContato servicoContato;

        public CompromissoController()
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);

            var repositorioCompromisso = new RepositorioCompromissoOrm(contextoPersistencia);

            var repositorioContato = new RepositorioContatoOrm(contextoPersistencia);

            servicoCompromisso = new ServicoCompromisso(repositorioCompromisso, contextoPersistencia);

            servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);
        }

        [HttpGet]

        public List<ListarCompromissoViewModel> SelecionarTodos()
        {
            var compromissos = servicoCompromisso.SelecionarTodos().Value;

            var compromisoViewModel = new List<ListarCompromissoViewModel>();

            foreach (var c in compromissos)
            {
                var compromissoViewModel = new ListarCompromissoViewModel
                {
                    Id = c.Id,
                    Assunto = c.Assunto,
                    Data = c.Data.ToShortDateString(),
                    HoraInicio = c.HoraInicio.ToString(@"hh\:mm\:ss"),
                    HoraTermino = c.HoraTermino.ToString(@"hh\:mm\:ss"),
                    nomeContato = c.Contato.Nome
                };

                compromisoViewModel.Add(compromissoViewModel);
            }
            return compromisoViewModel;


        }

        [HttpGet("{id}}")]

        public FormsCompromissoViewModel SelecionarPorId(Guid id)
        {
            var compromisso = servicoCompromisso.SelecionarPorId(id).Value;
            var compromissoViewModel = new FormsCompromissoViewModel
            {
                Assunto = compromisso.Assunto,
                Data = compromisso.Data,
                Local = compromisso.Local,
                TipoLocal = compromisso.TipoLocal,
                Link = compromisso.Link,
                HoraInicio = compromisso.HoraInicio.ToString(@"hh\:mm\:ss"),
                HoraTermino = compromisso.HoraTermino.ToString(@"hh\:mm\:ss"),
                ContatoId = compromisso.Contato.Id
            };
            return compromissoViewModel;
        }

     





    }
}
