using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace ExemploASPNETCore.Controllers
{
    [Route("api/[controller]")]
    public class RegioesController : Controller
    {
        private IConfiguration _config;

        public RegioesController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public IEnumerable<Regiao> GetRegioes()
        {
            using (SqlConnection conexao = new SqlConnection(
                _config.GetConnectionString("ExemplosDapper")))
            {
                return conexao.Query<Regiao>(
                    "SELECT * " +
                    "FROM dbo.VW_DETALHESREGIOES " +
                    "ORDER BY NomeRegiao");
            }
        }
    }
}