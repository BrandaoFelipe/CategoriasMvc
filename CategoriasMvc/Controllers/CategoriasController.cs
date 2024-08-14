using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CategoriasMvc.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoria;


        public CategoriasController(ICategoriaService categoria)
        {
            _categoria = categoria;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
        {            
                var result = await _categoria.GetCategorias();
                
                if (result is null)
                {
                    return View("Error");
                }

                return View("Index", result);              
        }

        [HttpGet]   
        public IActionResult CriarNovaCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> CriarNovaCategoria(CategoriaViewModel categoriaVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _categoria.Create(categoriaVM);
                if(result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }

            ViewBag.Erro = "Erro ao criar categoria";
            return View(categoriaVM);
        }

        [HttpGet]
        public async Task<ActionResult<CategoriaViewModel>>AtualizarCategoria(int id)
        {
            var result = await _categoria.GetCategoriaById(id);
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> AtualizarCategoria(int id, CategoriaViewModel categoriaVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoria.Update(id, categoriaVM);
                if(result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Erro = "Erro ao atualizar Categoria";
            return View(categoriaVM);
        }

        [HttpGet]
        public async Task<ActionResult<CategoriaViewModel>> DeletarCategoria(int id)
        {
            var result = await _categoria.GetCategoriaById(id);
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost(), ActionName("DeletarCategoria")]
        public async Task<ActionResult<CategoriaViewModel>> DeletaConfirmado(int id)
        {            
                var result = await _categoria.Delete(id);
                if (result is true)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            return View();
        }

    }
}
