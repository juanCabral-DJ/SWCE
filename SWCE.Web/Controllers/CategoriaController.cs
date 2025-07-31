using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaHttpService _categoriaHttpService;

        public CategoriaController(ICategoriaHttpService categoriaHttpService)
        {
            _categoriaHttpService = categoriaHttpService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _categoriaHttpService.GetAllCategoriasAsync();
            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener las categorías.";
            return View(new List<CategoriaModel>());
        }

        // GET: CategoriaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _categoriaHttpService.GetCategoriaByIdAsync(id);

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener los detalles de la categoría.";
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaModel model)
        {
            var response = await _categoriaHttpService.CreateCategoriaAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Categoría creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.message ?? "Error al crear la categoría.";
                return View(model);
            }

        }

        // GET: CategoriaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _categoriaHttpService.GetCategoriaByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                var model = new CategoriaModel
                {
                    id = response.data.id,
                    nombre = response.data.nombre,
                    descripcion = response.data.descripcion
                };
                return View(model);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener la categoria a editar";
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoriaModel model)
        {
            var response = await _categoriaHttpService.UpdateCategoriaAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = response.message ?? "Categoría actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        // GET: CategoriaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoriaHttpService.GetCategoriaByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = $"Error al obtener la categoría para eliminar: {response.message}";
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoriaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _categoriaHttpService.DisableCategoriaAsync(id);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Categoría deshabilitada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productDetailsResponse = await _categoriaHttpService.GetCategoriaByIdAsync(id);
                if (productDetailsResponse.isSuccess && productDetailsResponse.data != null)
                {
                    ModelState.AddModelError(string.Empty, response.message ?? "Error al deshabilitar el producto.");
                    return View(productDetailsResponse.data);
                }
                else
                {
                    TempData["ErrorMessage"] = response.message ?? "Error al intentar deshabilitar el producto.";
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}
