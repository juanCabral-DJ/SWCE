using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Cupones;
using SWCE.Web.Models.Cupones.CuponMontoFijo;
using SWCE.Web.Models.Cupones.CuponPorcentaje;
using SWCE.Web.Services;
using SWCE.Web.Services.Interfaces;
using System.Text.Json;

namespace SWCE.Web.Controllers
{
    public class CuponPorcentajeController : Controller
    {
        private readonly ICuponPorcentajeHttpService _cuponPorcentajeHttpService;

        public CuponPorcentajeController(ICuponPorcentajeHttpService cuponPorcentajeHttpService)
        {
            _cuponPorcentajeHttpService = cuponPorcentajeHttpService;
        }

        // GET: CuponPorcentajeController
        public async Task<IActionResult> Index()
        {
            var response = await _cuponPorcentajeHttpService.GetAllCuponPorcentajeAsync();

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener los cupones de porcentaje.";
            return View(new List<CuponPorcentajeModel>());
        }

        // GET: CuponPorcentajeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _cuponPorcentajeHttpService.GetCuponPorcentajeByIdAsync(id);

            if (response.isSuccess)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = $"Error al obtener el cupón de porcentaje con ID {id}.";
            return RedirectToAction(nameof(Index));
        }

        // GET: CuponPorcentajeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuponPorcentajeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCuponPorcentajeModel model)
        {
            var response = await _cuponPorcentajeHttpService.CreateCuponPorcentajeAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Cupón de porcentaje creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.message ?? "Error al crear el cupón de porcentaje.";
                return View(model);
            }
        }

        // GET: CuponPorcentajeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _cuponPorcentajeHttpService.GetCuponPorcentajeByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                var updateModel = new UpdateCuponPorcentajeModel
                {
                    id = response.data!.id,
                    porcentaje = response.data.porcentaje,
                    fechaExpiracion = response.data.fechaExpiracion
                };
                return View(updateModel);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener el cupón de porcentaje para edición.";
            return RedirectToAction(nameof(Index));
        }

        // POST: CuponPorcentajeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCuponPorcentajeModel model)
        {
            var response = await _cuponPorcentajeHttpService.UpdateCuponPorcentajeAsync(model);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Cupón de porcentaje actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.message ?? "Error al actualizar el cupon de porcentaje");
                return View(model);
            }
        }

        // GET: CuponPorcentajeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _cuponPorcentajeHttpService.GetCuponPorcentajeByIdAsync(id);

            if (response.isSuccess && response.data != null)
            {
                return View(response.data);
            }
            TempData["ErrorMessage"] = response.message ?? "Error al obtener el cupón de porcentaje para deshabilitar.";
            return RedirectToAction(nameof(Index));
        }

        // POST: CuponPorcentajeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _cuponPorcentajeHttpService.DeleteCuponPorcentajeAsync(id);

            if (response.isSuccess)
            {
                TempData["SuccessMessage"] = "Cupón de porcentaje deshabilitado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var cuponDetailsResponse = await _cuponPorcentajeHttpService.GetCuponPorcentajeByIdAsync(id);
                if (cuponDetailsResponse.isSuccess && cuponDetailsResponse.data != null)
                {
                    ModelState.AddModelError(string.Empty, response.message ?? "Error al deshabilitar el cupon de porcentaje.");
                    return View(cuponDetailsResponse.data);
                }
                else
                {
                    TempData["ErrorMessage"] = response.message ?? "Error al intentar deshabilitar el cupon de porcentaje.";
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}
