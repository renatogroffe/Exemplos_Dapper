using System;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace AtualizacaoPrecoProdutos
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection conexao = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BaseExemplosDapper"]
                    .ConnectionString))
            {
                Console.WriteLine("Atualizando o preço dos produtos...");

                conexao.Open();
                var transacao = conexao.BeginTransaction();

                conexao.Execute("UPDATE dbo.Produtos SET PrecoAnterior = Preco",
                    transaction: transacao);
                conexao.Execute("UPDATE dbo.Produtos SET Preco = Preco * 1.1",
                    transaction: transacao);

                transacao.Commit();
                conexao.Close();

                Console.WriteLine("Processo finalizado!");
                Console.ReadKey();
            }
        }
    }
}
