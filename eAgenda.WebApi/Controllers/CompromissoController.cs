using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/compromissos")]
    [ApiController]
    public class CompromissoController : ApiControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;
        private readonly IMapper mapeador;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeadorCompromissos)
        {
            this.servicoCompromisso = servicoCompromisso;
            this.mapeador = mapeadorCompromissos;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ListarCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos();

            var viewModel = mapeador.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed)
                return NotFound(compromissoResult.Errors);

            var viewModel = mapeador.Map<VisualizarCompromissoViewModel>(compromissoResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirCompromissoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirCompromissoViewModel compromissoViewModel)
        {
            var compromisso = mapeador.Map<Compromisso>(compromissoViewModel);

            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            return ProcessarResultado(compromissoResult.ToResult(), compromissoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarCompromissoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarCompromissoViewModel compromissoViewModel)
        {
            var resultadoSelecao = servicoCompromisso.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var compromisso = mapeador.Map(compromissoViewModel, resultadoSelecao.Value);

            var compromissoResult = servicoCompromisso.Editar(compromisso);

            return ProcessarResultado(compromissoResult.ToResult(), compromissoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = servicoCompromisso.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var compromissoResult = servicoCompromisso.Excluir(resultadoSelecao.Value);

            return ProcessarResultado(compromissoResult);
        }


        [HttpGet("hoje/{dataAtual}")]
        public async Task<IActionResult> SelecionarCompromissosDeHoje(DateTime dataAtual)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosFuturos(dataAtual, dataAtual);

            var viewModel = mapeador.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("futuros/{dataInicial}={dataFinal}")]
        public async Task<IActionResult> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosFuturos(dataInicial, dataFinal);

            var viewModel = mapeador.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("passados/{dataAtual}")]
        public async Task<IActionResult> SelecionarCompromissosPassados(DateTime dataAtual)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosPassados(dataAtual);

            var viewModel = mapeador.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value);

            return Ok(viewModel);
        }
    }
}
