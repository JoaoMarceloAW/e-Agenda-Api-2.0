using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;

namespace eAgenda.WebApi.Config.AutomapperConfig
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<Contato, ListarContatoViewModel>();
            CreateMap<Contato, VisualizarContatoViewModel>();

            CreateMap<InserirContatoViewModel, Contato>();
            CreateMap<EditarContatoViewModel, Contato>();
        }
    }
}
