using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.ItemCarrito;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Controllers
{
    public class ItemCarritoController : Controller
    {
        private readonly IItemCarritoService _itemCarritoService;

        public ItemCarritoController(IItemCarritoService itemCarritoService)
        {
            _itemCarritoService = itemCarritoService;
        }


        // GET: ItemCarrito
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: ItemCarrito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemCarrito/Create
        public ActionResult Create(int carritoId)
        {
            var model = new AddItemCarritoModel { CarritoId = carritoId };
            return View(model);
        }

        // POST: ItemCarrito/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddItemCarritoModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _itemCarritoService.AddItemCarritoAsync(model);
                if (item.isSuccess)
                {
                    
                    return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
                }

                TempData["ErrorMessage"] = "Error al agregar producto al carrito.";
            }
            return View(model);
        }

        // GET: ItemCarrito/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemCarrito/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateItemCantidadModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _itemCarritoService.UpdateItemCarritoAsync(model);
                if (item.isSuccess) 
                {
                    return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
                }               
            }
            else
            {
                TempData["errorMessage"] = "Los datos enviados para la actualización no son válidos.";
            }

            return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
        }

        // GET: ItemCarrito/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemCarrito/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisableItemCarritoModel model)
        {

            var item = await _itemCarritoService.DeleteItemCarritoAsync(model);
            if (item.isSuccess)
            {
                return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
            }
            
            TempData["errorMessage"] = item.message ?? "Error al eliminar el producto.";
            return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
        }
    }
}
