using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.WebApi.ViewModels.ModuloTarefa
{

    public class InserirTarefaViewModel
    {
        public string Titulo { get; set; }

        public PrioridadeTarefaEnum Prioridade { get; set; }

        public List<ItemTarefaViewModel> Itens { get; set; }
    }

    /**
     * {
           "titulo": "Tarefa 04",
           "prioridade": 2,
           "itens": [
            {     
              "titulo": "item 01"            
            },
            {     
              "titulo": "item 02"            
            },
            {     
              "titulo": "item 03"            
            }
          ]
      }
     */
}
