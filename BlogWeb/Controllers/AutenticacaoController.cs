using BlogWeb.Infra;
using BlogWeb.Models;
using BlogWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UserManager<Usuario> _manager;

        public UserManager<Usuario> Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = HttpContext.GetOwinContext().Get<UserManager<Usuario>>();
                }
                return _manager;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = Manager.Find(loginViewModel.LoginName, loginViewModel.Password);
                if (usuario != null)
                {
                    var identity = Manager.CreateIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(identity);
                    usuario.UltimoLogin = DateTime.Now;
                    var updateResult = Manager.Update(usuario);
                    if (updateResult.Succeeded)
                    {
                        return RedirectToAction("Index", new { Area = "Admin", Controller = "Post" });
                    }
                    else
                    {
                        HttpContext.GetOwinContext().Authentication.SignOut();
                        ModelState.AddModelError("", "Erro interno ao autenticar no sistema");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login e senha inválidos");
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(RegistroViewModel registroViewModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    UserName = registroViewModel.LoginName,
                    Email = registroViewModel.Email
                };
                var resultado = Manager.Create(usuario, registroViewModel.Senha);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(var erro in resultado.Errors)
                    {
                        ModelState.AddModelError("", erro);
                    }
                    return View(registroViewModel);
                }
            }
            return View(registroViewModel);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}