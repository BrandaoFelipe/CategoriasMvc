using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace CategoriasMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private string token = string.Empty;

        public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _produtoService.GetAll(ObtemTokenJwt());

            if (result is null)
            {
                return View("Error");
            }

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> CriarNovoProduto()
        {
            ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel produtoVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.Create(produtoVM, ObtemTokenJwt());
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            }
            
            return View(produtoVM);
        }        

        [HttpGet]
        public async Task<IActionResult> DetalhesProduto(int id)
        {
            var result = await _produtoService.GetById(id, ObtemTokenJwt());
            
            if(result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<ProdutoViewModel>> AtualizarProduto(int id)
        {
            var result = await _produtoService.GetById(id, ObtemTokenJwt());
            if (result is null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> AtualizarProduto(int id, ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.Update(id, produto, ObtemTokenJwt());
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(produto);
        }

        [HttpGet]
        public async Task<ActionResult<ProdutoViewModel>> DeletarProduto(int id)
        {
            var result = await _produtoService.GetById(id, ObtemTokenJwt());
            if(result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost(), ActionName("DeletarProduto")]
        public async Task<ActionResult<ProdutoViewModel>> DeletarConfirmado(int id)
        {
            var result = await _produtoService.DeleteById(id, ObtemTokenJwt());
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        private string ObtemTokenJwt()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                token = HttpContext.Request.Cookies["x-Access-Token"].ToString();
            }
            return token;
        }
    }
}
