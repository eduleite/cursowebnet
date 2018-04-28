using BlogWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    public class PostController : Controller
    {

        // GET: Post
        public ActionResult Index()
        {
            var posts = new List<Post>();
            string stringConexao = ConfigurationManager.ConnectionStrings["blog"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from Posts";
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Post post = new Post()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Titulo = Convert.ToString(reader["titulo"]),
                        Resumo = Convert.ToString(reader["resumo"]),
                        Categoria = Convert.ToString(reader["categoria"])
                    };
                    posts.Add(post);
                }
            }
            return View(posts);
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(Post post)
        {
            string stringConexao = ConfigurationManager.ConnectionStrings["blog"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into Posts(titulo, resumo, categoria) values ('"+post.Titulo+"','"+post.Resumo+"','"+post.Categoria+"')";
                command.ExecuteNonQuery();
            }
        
            return RedirectToAction("index");
        }
    }
}