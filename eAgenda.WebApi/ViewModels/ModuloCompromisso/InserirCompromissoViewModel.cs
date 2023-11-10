using eAgenda.Dominio.ModuloCompromisso;

namespace eAgenda.WebApi.ViewModels.ModuloCompromisso
{
    public class InserirCompromissoViewModel
    {
        public string Assunto { get; set; }

        public string Local { get; set; }

        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        public string Link { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraTermino { get; set; }

        public Guid? ContatoId { get; set; }
    }

    /**
     * {
    "assunto": "Feedback Cristian Ribeiro",
    "local": "www.meet.com.br/feedback",
    "tipoLocal": 0,
    "link": "http://meet.com.br/feedbacks",
    "data": "2023-10-12T14:00:00",
    "horaInicio": "14:00",
    "horaTermino": "15:00"
}
     */
}
