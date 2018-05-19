using BlogWeb.DAO;
using BlogWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BlogWeb.Areas.Admin.Controllers
{

    [Authorize]
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
                if (User.Identity.IsAuthenticated)
                {
                    post.AutorId = User.Identity.GetUserId();
                }
                dao.Incluir(post);
                return RedirectToAction("index", new { Area = "Admin" });
            }
            HttpContext.Response.StatusCode = 400;
            return View("Novo", post);
        }

        public ActionResult Remover(int id)
        {
            dao.Excluir(id);
            return RedirectToAction("index", new { Area = "Admin" });
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
                return RedirectToAction("index", new { Area = "Admin" });
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
                return RedirectToAction("index", new { Area = "Admin" });
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult CategoriaAutocomplete(string termo)
        {
            var categorias = dao.Listar()
                .Where(p => p.Categoria.ToLower().Contains(termo.ToLower()))
                .Select(p => new { label = p.Categoria })
                .Distinct()
                .ToList();
            return Json(categorias);
        }


    }
}