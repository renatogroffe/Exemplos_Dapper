using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper.Contrib.Extensions;

namespace ExemploASPNETCoreDapperContrib.Controllers
{
    [Route("api/[controller]")]
    public class EstadosController : Controller
    {
        private IConfiguration _config;

        public EstadosController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("todos")]
        public IEnumerable<Estado> GetEstados()
        {
            using (SqlConnection conexao = new SqlConnection(
                _config.GetConnectionString("ExemplosDapper")))
            {
                return conexao.GetAll<Estado>();
            }
        }

        [HttpGet("detalhes/{siglaEstado}")]
        public Estado GetDetalhesEstado(string siglaEstado)
        {
            using (SqlConnection conexao = new SqlConnection(
                _config.GetConnectionString("ExemplosDapper")))
            {
                return conexao.Get<Estado>(siglaEstado);
            }
        }
    }
}