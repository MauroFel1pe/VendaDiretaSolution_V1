using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VendaDireta.Models;

namespace VendaDireta.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            Acesso acesso = new Acesso();
            return View(acesso);
        }

        [HttpPost]
        public IActionResult Entrar(Acesso a)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = null;
                using (UsuarioModel usuarioModel = new UsuarioModel())
                {
                    usuario = usuarioModel.Entrar(a.EMail, a.Senha);
                    if (usuario != null)
                    {
                        HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario, Formatting.Indented));
                        return RedirectToAction("Listar", "Produto");
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário e/ou senha não localizado!";
                        return View("Index");
                    }
                }
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Criar()
        {
            Usuario usuario = new Usuario()
            {
                UsuarioId = 1,
                Receita = 0b0
            };
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Criar(Usuario u)
        {
            if (ModelState.IsValid)
            {
                using (UsuarioModel usuarioModel = new UsuarioModel())
                {
                    if (usuarioModel.Criar(u.Nome, u.EMail, u.Senha))
                    {
                        return View("index");
                    }
                    else
                    {
                        ViewBag.Erro = "Problemas ao cadastrar o usuário!";
                        return View("Index");
                    }
                }
            }
            else
            {
                return View("Criar");
            }
        }

        [HttpPost]
        public IActionResult Vender(int ProdutoId)
        {
            return View();
        }
    }
}