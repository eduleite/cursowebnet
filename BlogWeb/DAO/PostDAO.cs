using BlogWeb.Infra;
using BlogWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace BlogWeb.DAO
{
    public class PostDAO
    {

        public Post BuscaPorId(int id)
        {
            using (var connection = ConnectionFactory.CriaConexao())
            {
                Post result = null;
                var command = connection.CreateCommand();
                command.CommandText = "select * from Posts where id = @id";
                AddParameter(command, "id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = ReadPostFromReader(reader);
                    }
                }
                return result;
            }

        }

        private Post ReadPostFromReader(IDataReader reader)
        {
            return new Post()
            {
                Id = Convert.ToInt32(reader["id"]),
                Titulo = Convert.ToString(reader["titulo"]),
                Resumo = Convert.ToString(reader["resumo"]),
                Categoria = Convert.ToString(reader["categoria"])
            };
        }

        public IList<Post> Lista()
        {
            var posts = new List<Post>();
            using (var connection = ConnectionFactory.CriaConexao())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from Posts";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var post = ReadPostFromReader(reader);
                        posts.Add(post);
                    }
                }
            }
            return posts;
        }

        public void Incluir(Post post)
        {
            using (var connection = ConnectionFactory.CriaConexao())
            {
                var command = connection.CreateCommand();
                command.CommandText = "insert into Posts(titulo, resumo, categoria) values (@titulo,@resumo,@categoria)";
                AddParameter(command, "titulo", post.Titulo);
                AddParameter(command, "resumo", post.Resumo);
                AddParameter(command, "categoria", post.Categoria);
                command.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (var connection = ConnectionFactory.CriaConexao())
            {
                var command = connection.CreateCommand();
                command.CommandText = "delete from Posts where id = @id";
                AddParameter(command, "id", id);
                command.ExecuteNonQuery();
            }

        }

        public void Atualizar(Post post)
        {
            using (var connection = ConnectionFactory.CriaConexao())
            {
                var command = connection.CreateCommand();
                command.CommandText = "update Posts set titulo = @titulo, resumo = @resumo, categoria = @categoria where id = @id";
                AddParameter(command, "titulo", post.Titulo);
                AddParameter(command, "resumo", post.Resumo);
                AddParameter(command, "categoria", post.Categoria);
                AddParameter(command, "id", post.Id);
                command.ExecuteNonQuery();
            }
        }

        private void AddParameter(IDbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }

    }
}