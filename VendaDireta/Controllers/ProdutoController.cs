using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VendaDireta.Models;

namespace VendaDireta.Controllers
{
    public class ProdutoController : Controller
    {
        private Usuario usuario;

        private bool ValidaUsuario()
        {
            getUsuarioSession();
            if (usuario != null)
                return true;
            return false;
        }

        private void getUsuarioSession()
        {
            usuario = null;
            string jsonResposta = HttpContext.Session.GetString("usuario");
            if (jsonResposta != null)
            {
                usuario = (Usuario)JsonConvert.DeserializeObject(jsonResposta,typeof(Usuario));
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            if (ValidaUsuario())
            {
                ViewData.Add("DadosUsuario", usuario);

                //List<Produto> produtos = produtoModel.Listar(usuario.UsuarioId);
                using (ProdutoModel produtoModel = new ProdutoModel())
                {
                    List<Produto> produtos = produtoModel.Listar();
                    return View(produtos);
                }
            }
            else
                return RedirectToAction("index", "usuario");
        }

        public IActionResult Criar()
        {
            if (ValidaUsuario())
            {
                ViewData.Add("DadosUsuario", usuario);

                Produto p = new Produto
                {
                    ProdutoId = 0,
                    UsuarioId = usuario.UsuarioId
                };
                return View(p);
            }
            else
                return RedirectToAction("index", "usuario");
        }

        [HttpPost]
        public IActionResult Criar(Produto p)
        {
            if (ValidaUsuario())
            {
                if (ModelState.IsValid)
                {
                    using (ProdutoModel produtoModel = new ProdutoModel())
                    {
                        if (produtoModel.Criar(usuario.UsuarioId, p.Nome, p.Preco))
                        {
                            return RedirectToAction("listar");
                        }
                        else
                        {
                            ViewBag.Erro = "Não foi possível criar o produto";
                            return View();
                        }
                    }
                }
                else
                    return View();
            }
            else
                return RedirectToAction("index", "usuario");
        }

        public IActionResult Detalhes(int id)
        {
            if (ValidaUsuario())
            {
                ViewData.Add("DadosUsuario", usuario);

                using (ProdutoModel produtoModel = new ProdutoModel())
                {
                    Produto produto = produtoModel.Buscar(id);
                    if (produto != null)
                    {
                        return View(produto);
                    }
                    else
                    {
                        return RedirectToAction("listar");
                    }
                }
            }
            else
                return RedirectToAction("index", "usuario");
        }

        [HttpPost]
        public IActionResult Comprar(int id, Produto p)
        {
            if (ValidaUsuario())
            {
                using (ProdutoModel produtoModel = new ProdutoModel())
                {
                    if (produtoModel.Vender(id))
                    {
                        using (UsuarioModel usuarioModel = new UsuarioModel())
                        {
                            if (usuarioModel.Receita(id, p.Preco))
                            {
                                usuario = usuarioModel.Buscar(usuario.UsuarioId);
                                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario,Formatting.Indented));
                            }
                        }
                        return RedirectToAction("listar");
                    }
                    else
                    {
                        ViewBag.Error = "Não foi possível concluir a compra";
                        return View();
                    }
                }
            }
            else
            {
                return RedirectToAction("index","usuario");
            }
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "usuario");
        }
    }
}