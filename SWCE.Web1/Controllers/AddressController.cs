using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web1.HttpServices.Interfaces;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.User;
using System.Text.Json;
using static SWCE.Web1.Models.Address.DisableAddressModel;
using static SWCE.Web1.Models.WishListItem.DisableItemModel;

namespace SWCE.Web1.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAPIAddressServices _api;

        public AddressController(IAPIAddressServices api)
        {
            _api = api;
        }

        // GET: AddressController1
        public async Task<ActionResult> Index()
        {
            var Address = await _api.GetAllAddressesAsync();

            if (Address.isSuccess)
            {
                    return View(Address.data);  
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new List<AddressModel>()); // Devuelve una lista vacía a la vista
            }
        }

        // GET: AddressController1/Details/5
       public async Task<ActionResult> Details(int id)
        {
            var Address = await _api.GetAddressByIdAsync(id);

            if (Address.isSuccess)
            { 
                    return View(Address.data); 
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new AddressModel()); // Devuelve una lista vacía a la vista
            }
        }
           

        //Get Details Address Predeterminada
         public async Task<ActionResult> DetailsByPredeterminada(int iD_Usuario)
        {
            var Address = await _api.GetByUseridAdressPredeterminada(iD_Usuario);

            if (Address.isSuccess)
            {
                 
                    return View(Address.data); 
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new AddressModel()); // Devuelve una lista vacía a la vista
            }
        }

        //GetAll Details Address by userid
        public async Task<ActionResult> DetailsByUserid(int iD_Usuario)
        {
            var Address = await _api.GetAddressByUserIdAsync(iD_Usuario);

            if (Address.isSuccess)
            {
                    return View(Address.data);
            }
            else
            {

                ViewBag.ErrorMessage = Address.message;
                return View(new List<AddressModel>()); // Devuelve una lista vacía a la vista
            }
        }


        // GET: AddressController1/Create
        public ActionResult Create()
        {
            var model = new CreateAddressModel();
            return View(model);
        }

        // POST: AddressController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAddressModel model)
        {
            var Address = await _api.CreateAddressAsync(model);

            if (Address.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {

                return View(model);
            }
        } 

        // GET: AddressController1/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new EditAddressModel { id = id };
            return View(model);
        }

        // POST: AddressController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAddressModel model)
        {
            var Address = await _api.UpdateAddressAsync(model);

            if (Address.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            { 

                return View(model);
            }
        }

        // GET: AddressController1/Edit/5
         public ActionResult Delete(int id)
        {
            var model = new DisableAddressModel { id = id };
            return View(model);
        }

        // POST: AddressController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DisableAddressModel model)
        {
            var Address = await _api.DisableAddressAsync(model);

            if (Address.isSuccess)
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
