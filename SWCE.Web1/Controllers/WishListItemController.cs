using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.HttpServices.Interfaces;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.User;
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
            var item = await _api.GetAllItemasync();

            if (item.isSuccess)
            {
                return View(item.data);

            }
            else
            {

                ViewBag.ErrorMessage = item.message;
                return View(new List<ItemModel>()); // Devuelve una lista vacía a la vista
            }
        }

        // GET: WishListItemController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var item = await _api.GetbyIdasync(id);

            if (item.isSuccess)
            {
                return View(item.data);

            }
            else
            {

                ViewBag.ErrorMessage = item.message;
                return View(new List<ItemModel>()); // Devuelve una lista vacía a la vista
            }
        }

        public async Task<ActionResult> DetailsByUserid(int id_Usuario)
        {
            var item = await _api.GetbyUserid(id_Usuario);

            if (item.isSuccess)
            {
                return View(item.data);

            }
            else
            {

                ViewBag.ErrorMessage = item.message;
                return View(new List<ItemModel>()); // Devuelve una lista vacía a la vista
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
            var Item = await _api.CreateItemasync(model);

            if (Item.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        // GET: WishListItemController1/Edit/5
        //No Utilizado
 

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
            var Item = await _api.DisableItemAsync(model);

            if (Item.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
    }
}
