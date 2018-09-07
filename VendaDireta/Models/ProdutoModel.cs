using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VendaDireta.Models
{
    public class ProdutoModel : MySqlConnection
    {
        public List<Produto> Listar()
        {
            List<Produto> lista = new List<Produto>();

            string sql = "select * from Produto p where p.Vendido = 0";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                Produto p = new Produto
                {
                    ProdutoId = (int)result["ProdutoId"],
                    UsuarioId = (int)result["UsuarioId"],
                    Nome = (string)result["Nome"],
                    Preco = (decimal)result["Preco"],
                    Vendido = (bool)result["Vendido"]
                };
                lista.Add(p);
            }

            return lista;
        }
        public List<Produto> Listar(int usuarioId)
        {
            List<Produto> lista = new List<Produto>();

            string sql = "select * from Produto p where p.UsuarioId <> @id and p.Vendido = 0";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@id", usuarioId);

            SqlDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                Produto p = new Produto
                {
                    ProdutoId = (int)result["ProdutoId"],
                    UsuarioId = (int)result["UsuarioId"],
                    Nome = (string)result["Nome"],
                    Preco = (decimal)result["Preco"],
                    Vendido = (bool)result["Vendido"]
                };
                lista.Add(p);
            }

            return lista;
        }

        public bool Criar(int UsuarioId, string Nome, decimal Preco)
        {
            string sql = "insert into Produto (UsuarioId, Nome, Preco, Vendido) " +
                "values (@UsuarioId, @Nome, @Preco, 0)";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@UsuarioId", UsuarioId);
            cmd.Parameters.AddWithValue("@Nome", Nome);
            cmd.Parameters.AddWithValue("@Preco", Preco);

            int result = cmd.ExecuteNonQuery();

            return (result > 0);
        }

        public Produto Buscar(int ProdutoId)
        {
            Produto p = null;

            string sql = "select * from Produto p where p.ProdutoId = @ProdutoId";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@ProdutoId", ProdutoId);

            SqlDataReader result = cmd.ExecuteReader();

            if (result.Read())
            {
                p = new Produto
                {
                    ProdutoId = (int)result["ProdutoId"],
                    UsuarioId = (int)result["UsuarioId"],
                    Nome = (string)result["Nome"],
                    Preco = (decimal)result["Preco"],
                    Vendido = (bool)result["Vendido"]
                };
            }

            return p;
        }

        public bool Vender(int ProdutoId)
        {
            string sql = "update Produto set Vendido = 1 where ProdutoId = @ProdutoId";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@ProdutoId", ProdutoId);

            int result = cmd.ExecuteNonQuery();

            return (result > 0);
        }
    }
}