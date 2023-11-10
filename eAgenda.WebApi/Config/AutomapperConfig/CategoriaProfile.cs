using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.WebApi.Config.AutoMapperConfig
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<InserirCategoriaViewModel, Categoria>();

            CreateMap<EditarCategoriaViewModel, Categoria>();

            CreateMap<Categoria, ListarCategoriaViewModel>();

            CreateMap<Categoria, VisualizarCategoriaViewModel>();

            CreateMap<Categoria, CategoriaSelecionadaViewModel>();
        }
    }
}
