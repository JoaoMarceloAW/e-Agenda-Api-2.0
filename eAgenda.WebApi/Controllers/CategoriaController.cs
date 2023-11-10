using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ApiControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeador;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeadorCategorias)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeador = mapeadorCategorias;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarTodos()
        {
            var categoriaResult = servicoCategoria.SelecionarTodos();

            var viewModel = mapeador.Map<List<ListarCategoriaViewModel>>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarPorId(Guid id)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            var viewModel = mapeador.Map<VisualizarCategoriaViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirCategoriaViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirCategoriaViewModel categoriaViewModel)
        {
            var categoria = mapeador.Map<Categoria>(categoriaViewModel);

            var categoriaResult = servicoCategoria.Inserir(categoria);

            return ProcessarResultado(categoriaResult.ToResult(), categoriaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarCategoriaViewModel categoriaViewModel)
        {
            var resultadoSelecao = servicoCategoria.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var categoria = mapeador.Map(categoriaViewModel, resultadoSelecao.Value);

            var categoriaResult = servicoCategoria.Editar(categoria);

            return ProcessarResultado(categoriaResult.ToResult(), categoriaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = servicoCategoria.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var categoriaResult = servicoCategoria.Excluir(resultadoSelecao.Value);

            return ProcessarResultado(categoriaResult);
        }

    }
}
