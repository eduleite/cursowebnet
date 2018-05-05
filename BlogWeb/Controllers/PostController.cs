using BlogWeb.DAO;
using BlogWeb.Models;
using System;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    public class PostController : Controller
    {
        private PostDaoEF dao;

        public PostController()
        {
            dao = new PostDaoEF();
        }

        // GET: Post
        public ActionResult Index()
        {
            var lista = dao.Lista();
            return View(lista);
        }

        public ActionResult Novo()
        {
            return View(new Post());
        }

        [HttpPost]
        public ActionResult Incluir(Post post)
        {
            if (ModelState.IsValid)
            {
                dao.Incluir(post);
                return RedirectToAction("index");
            }
            HttpContext.Response.StatusCode = 400;
            return View("Novo", post);
        }

        public ActionResult Remover(int id)
        {
            dao.Excluir(id);
            return RedirectToAction("index");
        }

        public ActionResult Detalhe(int id)
        {
            var post = dao.BuscaPorId(id);
            if (post != null)
            {
                return View(post);
            } 
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Alterar(Post post)
        {
            if (ModelState.IsValid)
            {
                dao.Atualizar(post);
                return RedirectToAction("index");
            }
            HttpContext.Response.StatusCode = 400;
            return View("detalhe", post);
        }

        public ActionResult Publicar(int id)
        {
            var post = dao.BuscaPorId(id);
            if (post != null)
            {
                post.DataPublicacao = DateTime.Now;
                post.Publicado = true;
                dao.Atualizar(post);
                return RedirectToAction("index");
            }
            return HttpNotFound();
        }


    }
}