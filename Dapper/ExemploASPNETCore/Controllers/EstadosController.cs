using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace ExemploASPNETCore.Controllers
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
                return conexao.Query<Estado>(
                    "SELECT E.SiglaEstado, E.NomeEstado, E.NomeCapital, " +
                           "R.NomeRegiao " +
                    "FROM dbo.Estados E " +
                    "INNER JOIN dbo.Regioes R ON R.IdRegiao = E.IdRegiao " +
                    "ORDER BY E.NomeEstado");
            }
        }

        [HttpGet("detalhes/{siglaEstado}")]
        public Estado GetDetalhesEstado(string siglaEstado)
        {
            using (SqlConnection conexao = new SqlConnection(
                _config.GetConnectionString("ExemplosDapper")))
            {
                return conexao.QueryFirstOrDefault<Estado>(
                    "SELECT E.SiglaEstado, E.NomeEstado, E.NomeCapital, " +
                           "R.NomeRegiao " +
                    "FROM dbo.Estados E " +
                    "INNER JOIN dbo.Regioes R ON R.IdRegiao = E.IdRegiao " +
                    "WHERE E.SiglaEstado = @CodEstado",
                    new { CodEstado = siglaEstado });
            }
        }

        [HttpGet("detalhes2/{siglaEstado}")]
        public Estado GetDetalhesEstado2(string siglaEstado)
        {
            using (SqlConnection conexao = new SqlConnection(
                _config.GetConnectionString("ExemplosDapper")))
            {
                return conexao.QueryFirstOrDefault<Estado>(
                    "dbo.PRC_SEL_DETALHES_ESTADO",
                    new { CodEstado = siglaEstado },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}