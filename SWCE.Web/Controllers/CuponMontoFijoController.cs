using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Models.Cupones.CuponMontoFijo;
using SWCE.Web.Models.Producto;
using SWCE.Web.Services;
using SWCE.Web.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace SWCE.Web.Controllers
{
    public class CuponMontoFijoController : Controller
    {
        private readonly ICuponMontoFijoHttpService _cuponMontoFijoHttpService;

        public CuponMontoFijoController(ICuponMontoFijoHttpService cuponMontoFijoHttpService)
        {
            _cuponMontoFijoHttpService = cuponMontoFijoHttpService;
        }

        // GET: CuponMontoFijoController
        public async Task<IActionResult> Index()
        {
            var response = await _cuponMontoFijoHttpService.GetAllCuponMontoFijoAsync();

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener los cupones de monto fijo.";
            return View(new List<CuponMontoFijoModel>());
        }

        // GET: CuponMontoFijoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _cuponMontoFijoHttpService.GetCuponMontoFijoByIdAsync(id);

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = $"Error al obtener el cupón de monto fijo.";
            return RedirectToAction(nameof(Index));
        }

        // GET: CuponMontoFijoController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuponMontoFijoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCuponMontoFijoModel model)
        {
            var response = await _cuponMontoFijoHttpService.CreateCuponMontoFijoAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Cupón de monto fijo creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.message ?? "Error al crear el cupon de monto fijo.";
                return View(model);
            }
        }

        // GET: CuponMontoFijoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _cuponMontoFijoHttpService.GetCuponMontoFijoByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                var updateModel = new UpdateCuponMontoFijoModel
                {
                    id = response.data.id,
                    monto = response.data.monto,
                    fechaExpiracion = response.data.fechaExpiracion
                };
                return View(updateModel);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener el cupón de monto fijo para edición.";
            return RedirectToAction(nameof(Index));
        }


        // POST: CuponMontoFijoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCuponMontoFijoModel model)
        {
            var response = await _cuponMontoFijoHttpService.UpdateCuponMontoFijoAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Cupón de monto fijo actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.message ?? "Error al actualizar el cupon.");
                return View(model);
            }
        }

        // GET: CuponMontoFijoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _cuponMontoFijoHttpService.GetCuponMontoFijoByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener el cupón de monto fijo para deshabilitar.";
            return RedirectToAction(nameof(Index));
        }

        // POST: CuponMontoFijoController/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _cuponMontoFijoHttpService.DisableCuponMontoFijoAsync(id);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Cupón de monto fijo deshabilitado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var cuponDetailsResponse = await _cuponMontoFijoHttpService.GetCuponMontoFijoByIdAsync(id);
                if (cuponDetailsResponse.isSuccess && cuponDetailsResponse.data != null)
                {
                    ModelState.AddModelError(string.Empty, response.message ?? "Error al deshabilitar el cupon de monto fijo.");
                    return View(cuponDetailsResponse.data);
                }
                else
                {
                    TempData["ErrorMessage"] = response.message ?? "Error al intentar deshabilitar el cupon de monto fijo.";
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}