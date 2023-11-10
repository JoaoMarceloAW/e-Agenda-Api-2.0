using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/despesas")]
    [ApiController]
    public class DespesaController : ApiControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private readonly IMapper mapeador;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeadorDespesas)
        {
            this.servicoDespesa = servicoDespesa;
            this.mapeador = mapeadorDespesas;
        }

        [HttpGet("ultimos-30-dias")]
        [ProducesResponseType(typeof(ListarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarDespesasUltimos30Dias()
        {
            var despesaResult = servicoDespesa.SelecionarDespesasUltimos30Dias(DateTime.Now);

            var viewModel = mapeador.Map<List<ListarDespesaViewModel>>(despesaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("antigas")]
        [ProducesResponseType(typeof(ListarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarDespesasAntigas()
        {
            var despesaResult = servicoDespesa.SelecionarDespesasAntigas(DateTime.Now);

            var viewModel = mapeador.Map<List<ListarDespesaViewModel>>(despesaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();

            var viewModel = mapeador.Map<List<ListarDespesaViewModel>>(despesaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed)
                return NotFound(despesaResult.Errors);

            var viewModel = mapeador.Map<VisualizarDespesaViewModel>(despesaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirDespesaViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirDespesaViewModel despesaViewModel)
        {
            var despesa = mapeador.Map<Despesa>(despesaViewModel);

            var despesaResult = servicoDespesa.Inserir(despesa);

            return ProcessarResultado(despesaResult.ToResult(), despesaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarDespesaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarDespesaViewModel despesaViewModel)
        {
            var resultadoSelecao = servicoDespesa.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var despesa = mapeador.Map(despesaViewModel, resultadoSelecao.Value);

            var despesaResult = servicoDespesa.Editar(despesa);

            return ProcessarResultado(despesaResult.ToResult(), despesaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = servicoDespesa.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var despesaResult = servicoDespesa.Excluir(resultadoSelecao.Value);

            return ProcessarResultado(despesaResult);
        }

    }
}
