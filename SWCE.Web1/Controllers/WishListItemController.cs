using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web1.Interfaces;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.WishListItem;
using System.Text.Json;
 

namespace SWCE.Web1.Controllers
{
    public class WishListItemController : Controller
    {
        private readonly IAPIWishListItemServices _api;

        public WishListItemController(IAPIWishListItemServices api)
        {
           _api = api;
        }

        // GET: WishListItemController1
        public async Task<ActionResult> Index()
        {
            var Address = await _api.GetAllItemasync();

            if (Address.isSuccess)
            {
                try
                {
                    return View(Address.data);
                }
                catch (JsonException ex)
                {
                    ViewBag.ErrorMessage = "Error al procesar los datos recibidos de la API.";
                    return View(new List<AddressModel>());
                }
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new List<AddressModel>()); // Devuelve una lista vacía a la vista
            }
        }

        // GET: WishListItemController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var Address = await _api.GetbyIdasync(id);

            if (Address.isSuccess)
            {
                try
                {
                    return View(Address.data);
                }
                catch (JsonException ex)
                {
                    ViewBag.ErrorMessage = "Error al procesar los datos recibidos de la API.";
                    return View(new AddressModel());
                }
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new AddressModel()); // Devuelve una lista vacía a la vista
            }
        }

        public async Task<ActionResult> DetailsByUserid(int id_Usuario)
        {
            var Address = await _api.GetbyUserid(id_Usuario);

            if (Address.isSuccess)
            {
                try
                {
                    return View(Address.data);
                }
                catch (JsonException ex)
                {
                    ViewBag.ErrorMessage = "Error al procesar los datos recibidos de la API.";
                    return View(new List<AddressModel>());
                }
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new List<AddressModel>()); // Devuelve una lista vacía a la vista
            }
        }


        // GET: WishListItemController1/Create
        public ActionResult Create()
        {
            var model = new CreateItemModel();
            return View(model);
        }

        // POST: WishListItemController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateItemModel model)
        {
            var Address = await _api.CreateItemasync(model);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: WishListItemController1/Edit/5
        //No Utilizado
        /*public ActionResult Edit(int id)
        {
          
            return View( );
        }

        // POST: WishListItemController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: WishListItemController1/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new DisableItemModel { id = id };
            return View(model);
        }

        // POST: WishListItemController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DisableItemModel model)
        {
            var Address = await _api.DisableItemAsync(model);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
