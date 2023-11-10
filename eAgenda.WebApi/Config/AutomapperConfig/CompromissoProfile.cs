using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;

namespace eAgenda.WebApi.Config.AutomapperConfig
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            ConfigurarMapeamentoEntidadeParaViewModel();

            ConfigurarMapeamentoViewModelParaEntidade();
        }

        private void ConfigurarMapeamentoViewModelParaEntidade()
        {
            CreateMap<InserirCompromissoViewModel, Compromisso>();
            CreateMap<EditarCompromissoViewModel, Compromisso>();
        }

        private void ConfigurarMapeamentoEntidadeParaViewModel()
        {
            CreateMap<Compromisso, ListarCompromissoViewModel>()
                            .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                            .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                            .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Compromisso, VisualizarCompromissoViewModel>();
        }
    }
}
