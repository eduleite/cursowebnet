using BlogWeb.Infra;
using BlogWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new BlogContext())
            using (var store = new UserStore<Usuario>(context))
            using (var manager = new UserManager<Usuario>(store))
            {
                var usuarios = manager.Users.ToList();
                return View(usuarios);
            }
        }


    }
}