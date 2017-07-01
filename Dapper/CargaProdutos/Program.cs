using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CargaProdutos
{
    class Program
    {
        private static void InserirProduto(
            string nomeProduto, double preco)
        {
            using (SqlConnection conexao = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BaseExemplosDapper"]
                    .ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NomeProduto", nomeProduto);
                parametros.Add("@Preco", preco);
                parametros.Add("@IdProduto",
                    dbType: DbType.Int32,
                    direction: ParameterDirection.Output);

                conexao.Execute("dbo.PRC_INS_PRODUTO", parametros,
                    commandType: CommandType.StoredProcedure);

                Console.WriteLine(
                    $"Cadastro o Produto { parametros.Get<int>("@IdProduto") }");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Inserindo produtos...");

            InserirProduto("C# Fundamentos - Livro", 100);
            InserirProduto("Domain-Driven Design - Livro", 150);

            Console.WriteLine("Produtos inseridos com sucesso...");
            Console.ReadKey();
        }
    }
}