using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Interfaces;
using SWCE.Web1.Models.User;
using System.Text.Json;


namespace SWCE.Web1.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _Client;
        private readonly IAPIUserServices _api;
        public UserController(IAPIUserServices api)
        {
            _api = api;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var Users = await _api.GetAllUsersAsync();

            if (Users.isSuccess)
            {
                try
                {
                    return View(Users.data);
                }
                catch (JsonException ex)
                {
                    ViewBag.ErrorMessage = "Error al procesar los datos recibidos de la API.";
                    return View(new List<UserModel>());
                }
            }
            else
            {

                ViewBag.ErrorMessage = Users.message;
                return View(new List<UserModel>()); // Devuelve una lista vacía a la vista
            }
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var Users = await _api.GetUserByIdAsync(id);

            if (Users.isSuccess)
            {
                try
                {
                    return View(Users.data);
                }
                catch (JsonException ex)
                {
                    ViewBag.ErrorMessage = "Error al procesar los datos recibidos de la API.";
                    return View(new UserModel());
                }
            }
            else
            {

                ViewBag.ErrorMessage = Users.message;
                return View(new UserModel()); // Devuelve una lista vacía a la vista
            }
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> DetailsByEmail(string email)
        {
            var Users = await _api.GetUserByEmailAsync(email);

            if (Users.isSuccess)
            {
                try
                {
                    return View(Users.data);
                }
                catch (JsonException ex)
                {
                    ViewBag.ErrorMessage = "Error al procesar los datos recibidos de la API.";
                    return View(new UserModel());
                }
            }
            else
            {

                ViewBag.ErrorMessage = Users.message;
                return View(new UserModel()); // Devuelve una lista vacía a la vista
            }
        }


        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserModelCreate model)
        {
            var Users = await _api.CreateUserAsync(model);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserModelEdit model)
        {
            var Users = await _api.UpdateUserAsync(model);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new DisableUserModel { id = id };
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DisableUserModel model)
        {
            var Users = await _api.DisableUserAsync(model);

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
