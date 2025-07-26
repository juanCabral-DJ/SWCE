using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.ItemCarrito;

namespace SWCE.Web.Controllers
{
    public class ItemCarrito : Controller
    {
        private readonly string _apiBaseUrl = "http://localhost:5134/api/";
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
            var model = new AddItemCarritoModel
            {
                CarritoId = carritoId
            };
            return View(model);
        }

        // POST: ItemCarrito/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddItemCarritoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsJsonAsync("itemscarrito/AddItem", model);

                    if (response.IsSuccessStatusCode)
                    {
                        // Si es exitoso, redirige a la página de detalles del carrito
                        return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
                    }
                }
            }
            catch (Exception ex)
            {
                // Loggear el error (ex.Message)
                ModelState.AddModelError(string.Empty, "Ocurrió un error al agregar el producto.");
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
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Details", "Carrito", new { id = model.CarritoId });
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsJsonAsync("itemscarrito/UpdateItem", model);
                }
            }
            catch (Exception ex)
            {
                throw;
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
        public async Task<IActionResult> Delete(int id, int carritoId)
        {
            var model = new DisableItemCarritoModel { Id = id };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsJsonAsync("itemscarrito/RemoveItem", model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Details", "Carrito", new { id = carritoId });
        }
    }
}
