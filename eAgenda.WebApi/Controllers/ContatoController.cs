using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;

namespace eAgenda.WebApi.Controllers
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatoController : ApiControllerBase
    {
        private ServicoContato servicoContato;
        private IMapper mapeador;

        public ContatoController(ServicoContato servicoContato, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoContato = servicoContato;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarTodos(StatusFavoritoEnum statusFavorito)
        {
            var contatoResult = await servicoContato.SelecionarTodosAsync(statusFavorito);

            var viewModel = mapeador.Map<List<ListarContatoViewModel>>(contatoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SeleciontarPorId(Guid id)
        {
            var contatoResult = await servicoContato.SelecionarPorIdAsync(id);

            if (contatoResult.IsFailed)
                return NotFound(contatoResult.Errors);

            var viewModel = mapeador.Map<VisualizarContatoViewModel>(contatoResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirContatoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirContatoViewModel contatoViewModel)
        {
            var contato = mapeador.Map<Contato>(contatoViewModel);

            var contatoResult = await servicoContato.InserirAsync(contato);

            return ProcessarResultado(contatoResult.ToResult(), contatoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarContatoViewModel contatoViewModel)
        {
            var resultadoSelecao = await servicoContato.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var contato = mapeador.Map(contatoViewModel, resultadoSelecao.Value);

            var contatoResult = await servicoContato.EditarAsync(contato);

            return ProcessarResultado(contatoResult.ToResult(), contatoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = await servicoContato.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var contatoResult = await servicoContato.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(contatoResult);
        }


        [HttpPut("favoritos/{id}")]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Favoritar(Guid id)
        {
            var resultadoSelecao = await servicoContato.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var contatoResult = await servicoContato.FavoritarAsync(resultadoSelecao.Value);

            return ProcessarResultado(contatoResult.ToResult());
        }
    }
}