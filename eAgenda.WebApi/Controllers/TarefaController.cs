using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WebApi.ViewModels.ModuloTarefa;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/tarefas")]
    [ApiController]
    public class TarefaController : ApiControllerBase
    {
        private readonly ServicoTarefa servicoTarefa;
        private readonly IMapper mapeador;

        public TarefaController(ServicoTarefa servicoTarefa, IMapper mapeadorTarefas)
        {
            this.servicoTarefa = servicoTarefa;
            this.mapeador = mapeadorTarefas;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ListarTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarTodos(StatusTarefaEnum status)
        {
            var tarefaResult = servicoTarefa.SelecionarTodos(status);

            var viewModel = mapeador.Map<List<ListarTarefaViewModel>>(tarefaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarPorId(Guid id)
        {
            var tarefaResult = servicoTarefa.SelecionarPorId(id);

            if (tarefaResult.IsFailed)
                return NotFound(tarefaResult.Errors);

            var viewModel = mapeador.Map<VisualizarTarefaViewModel>(tarefaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirTarefaViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirTarefaViewModel tarefaViewModel)
        {
            var tarefa = mapeador.Map<Tarefa>(tarefaViewModel);

            var tarefaResult = servicoTarefa.Inserir(tarefa);

            return ProcessarResultado(tarefaResult.ToResult(), tarefaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarTarefaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarTarefaViewModel tarefaViewModel)
        {
            var resultadoSelecao = servicoTarefa.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var tarefa = mapeador.Map(tarefaViewModel, resultadoSelecao.Value);

            var tarefaResult = servicoTarefa.Editar(tarefa);

            return ProcessarResultado(tarefaResult.ToResult(), tarefaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = servicoTarefa.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var tarefaResult = servicoTarefa.Excluir(resultadoSelecao.Value);

            return ProcessarResultado(tarefaResult);
        }

    }
}
