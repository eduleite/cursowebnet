using BlogWeb.DAO;
using System.Linq;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {

        private PostDaoEF dao = new PostDaoEF();

        // GET: Home
        public ActionResult Index()
        {
            var lista = dao.Listar().Where(p => p.Publicado).ToList();
            return View(lista);
        }

        public ActionResult Busca(string termo)
        {
            if (!string.IsNullOrEmpty(termo))
            {
                var lista = dao.Listar()
                    .Where(p => p.Publicado && (p.Titulo.ToLower().Contains(termo.ToLower()) || p.Resumo.ToLower().Contains(termo.ToLower())))
                    .ToList();
                return View("Index", lista);
            }
            return RedirectToAction("Index");
        }

    }
}