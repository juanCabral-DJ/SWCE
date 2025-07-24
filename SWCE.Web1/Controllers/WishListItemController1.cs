using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWCE.Web1.Controllers
{
    public class WishListItemController1 : Controller
    {
        // GET: WishListItemController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: WishListItemController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WishListItemController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WishListItemController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WishListItemController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
        }

        // GET: WishListItemController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WishListItemController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
