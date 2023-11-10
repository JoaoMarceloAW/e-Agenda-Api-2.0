using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/saudacao")]
    public class SaudacaoController : ControllerBase
    {
        [HttpGet("bom-dia")]
        public Saudacao BomDia()
        {
            return new Saudacao
            {
                Data = DateTime.Now,
                Mensagem = "Bom dia Galera da Academia"
            };
        }

        [HttpGet("boa-tarde")]
        public Saudacao BoaTarde()
        {
            return new Saudacao
            {
                Data = DateTime.Now,
                Mensagem = "Bom tarde Galera da Academia"
            };
        }
    }

    public class Saudacao
    {
        public DateTime Data { get; set; }

        public string Mensagem { get; set; }
    }
}
