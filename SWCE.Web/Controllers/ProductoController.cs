using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Models.Producto;
using SWCE.Web.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace SWCE.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoHttpService _productoHttpService;

        public ProductoController(IProductoHttpService productoHttpService)
        {
            _productoHttpService = productoHttpService;
        }

        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            var response = await _productoHttpService.GetAllProductosAsync();

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener la lista de productos.";
            return View(new List<ProductoModel>());
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _productoHttpService.GetProductoByIdAsync(id);

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener los detalles del producto.";
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductoModel model)
        {
            var response = await _productoHttpService.CreateProductoAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.message ?? "Error al crear el producto.";
                return View(model);
            }
        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _productoHttpService.GetProductoByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                var model = new UpdateProductoModel
                {
                    id = response.data.id,
                    nombre = response.data.nombre,
                    marca = response.data.marca,
                    idCategoria = response.data.idCategoria,
                    precio = response.data.precio,
                    stock = response.data.stock
                };
                return View(model);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener el producto a editar";
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductoModel model)
        {
            var response = await _productoHttpService.UpdateProductoAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = response.message ?? "Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productoHttpService.DisableProductoAsync(id);

            if (response.isSuccess && response.data != null)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener el producto a deshabilitar";
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _productoHttpService.DisableProductoAsync(id);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = response.message ?? "Producto deshabilitado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.message ?? "Error al intentar deshabilitar el producto.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}