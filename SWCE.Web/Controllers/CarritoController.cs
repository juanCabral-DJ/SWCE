using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Extension.Mapper;
using SWCE.Web.Models.Carrito;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoService _carritoService;

        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        // GET: CarritoController
        public async Task<IActionResult> Index()
        {
            var carritos = await _carritoService.GetAllCarritosAsync();
            if (carritos.isSuccess)
            {
                return View(carritos.data);
            }
            TempData["ErrorMessage"] = carritos.message ?? "Error retrieving carritos.";
            return View(new List<CarritoModel>());

        }

        // GET: CarritoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var carritos = await _carritoService.GetCarritoByIdAsync(id);
            if (carritos.isSuccess)
            {
                return View(carritos.data);
            }
            TempData["ErrorMessage"] = carritos.message ?? "Error retrieving carrito details.";
            return RedirectToAction(nameof(Index));

        }

        // GET: CarritoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarritoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCarritoModel model)
        {
            if (ModelState.IsValid)
            {
                var carrito = await _carritoService.CreateCarritoAsync(model);
                if (carrito.isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["ErrorMessage"] = "Error creating carrito. Please check the details and try again.";
            return View(model);
        }

        // GET: CarritoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var carrito = await _carritoService.GetCarritoByIdAsync(id);
            if (carrito.isSuccess)
            {

                var modelParaVista = carrito.data.ToEditViewModel(); 
                return View(modelParaVista);
            }
            TempData["ErrorMessage"] = carrito.message ?? "Error retrieving carrito for editing.";
            return RedirectToAction(nameof(Index));
        }

        // POST: CarritoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>Edit(EditCarritoModel model)
        {
            if (ModelState.IsValid)
            {
                var carrito = await _carritoService.UpdateCarritoAsync(model);
                if (carrito.isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["ErrorMessage"] = "Error updating carrito.";
            return View(model);
        }

        // GET: CarritoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var carrito = await _carritoService.GetCarritoByIdAsync(id);
            if (carrito.isSuccess)
            {
                return View(carrito.data);
            }
            TempData["ErrorMessage"] = carrito.message ?? "Error retrieving carrito for deletion.";
            return RedirectToAction(nameof(Index));

        }

        // POST: CarritoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DisableCarritoModel model)
        {
            var response = await _carritoService.DeleteCarritoAsync(model);
            
            if (response.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            
            TempData["ErrorMessage"] = response.message ?? "Error deleting carrito.";
            return RedirectToAction(nameof(Index));
        }

    }
}
