using eAgenda.Dominio.ModuloDespesa;

namespace eAgenda.WebApi.ViewModels.ModuloDespesa
{
    public class EditarDespesaViewModel
    {
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<Guid> CategoriasSelecionadas { get; set; }
    }

    /**
     *  {
          "descricao": "Compra de Carne pro churrasco da academia",
          "valor": 500,
          "data": "2022-09-29T16:06:10.788Z",
          "formaPagamento": 2,
          "categoriasSelecionadas": [    
              "0d3d95c7-8106-46b0-c129-08dbc8f6e56f",
              "11111111-8106-46b0-c129-08dbc8f6e56f"
          ]
        }
     */

}
