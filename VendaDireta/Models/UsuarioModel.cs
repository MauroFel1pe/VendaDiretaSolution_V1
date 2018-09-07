using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VendaDireta.Models
{
    public class UsuarioModel : MySqlConnection
    {
        public Usuario Buscar(int UsuarioId)
        {
            Usuario u = null;

            string sql = "select * from Usuario u where u.UsuarioId = @UsuarioId";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@UsuarioId", UsuarioId);

            SqlDataReader result = cmd.ExecuteReader();
            if (result.Read())
            {
                u = new Usuario
                {
                    UsuarioId = (int)result["UsuarioId"],
                    Nome = (string)result["Nome"],
                    EMail = (string)result["Email"],
                    Senha = (string)result["Senha"],
                    Receita = (decimal)result["Receita"]
                };
            }

            return u;
        }

        public Usuario Entrar(string email, string senha)
        {
            Usuario u = null;

            string sql = "select * from Usuario u " +
                "where upper(trim(u.Email)) = upper(trim(@Email)) and u.senha = @Senha";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);

            SqlDataReader result = cmd.ExecuteReader();
            if (result.Read())
            {
                u = new Usuario
                {
                    UsuarioId = (int)result["UsuarioId"],
                    Nome = (string)result["Nome"],
                    EMail = (string)result["Email"],
                    Senha = (string)result["Senha"],
                    Receita = (decimal)result["Receita"]
                };
            }

            return u;
        }

        public bool Criar(string nome, string email, string senha)
        {
            string sql = "insert into Usuario (Nome, Email, Senha, Receita) " +
                "values (@Nome, @Email, @Senha, 0.00)";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);

            int result = cmd.ExecuteNonQuery();

            return (result > 0);
        }

        public bool Receita(int ProdutoId, decimal valor)
        {
            string sql = "update Usuario set Receita = Receita + @Receita where UsuarioId = (select UsuarioId from Produto where ProdutoId = @ProdutoId)";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@Receita", valor);
            cmd.Parameters.AddWithValue("@ProdutoId", ProdutoId);

            int result = cmd.ExecuteNonQuery();

            return (result > 0);
        }
    }
}
