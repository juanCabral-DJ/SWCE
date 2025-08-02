using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Common;
using SWCE.Web.Models;
using SWCE.Web.Repositories.Interfaces;

namespace SWCE.Web.Controllers
{
    public class EnvioController : Controller
    {
        private readonly IEnvioHttpService _envioHttpService;
        public EnvioController(IEnvioHttpService envioHttpService)
        {
            _envioHttpService = envioHttpService;
        }

        // GET: EnvioController
        public async Task<IActionResult> Index()
        {
            var envios = await _envioHttpService.GetAllEnviosAsync();
            if (!envios.IsSuccess)
            {
                TempData["ErrorMessage"] = envios.Message;
                return View(new List<EnvioModel>());
            }

            return View(envios.Data);
        }

        // GET: EnvioController/Details/5
        public async Task<IActionResult> Details(int id)
        {         
           var envios = await _envioHttpService.GetEnvioByIdAsync(id);
            if (!envios.IsSuccess)
            {
            
            TempData["ErrorMessage"] = "Hubo un error inesperado: " + envios.Message;
            return View(new EnvioModel());

            }
           return View(envios.Data);
            
        }

        // GET: EnvioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnvioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnvioCreateModel model)
        {
            var envioResponse = await _envioHttpService.CreateEnvioAsync(model);
                if (envioResponse.IsSuccess)
                {
                  TempData["SucccessMessage"] = "Envío creado correctamente";
                  return RedirectToAction(nameof(Index));
                }
                else
                {
                  TempData["ErrorMessage"] = envioResponse.Message;
                }
                return View(model);
        }

        // GET: EnvioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var result = await _envioHttpService.GetEnvioByIdAsync(id);

            if (!result.IsSuccess || result.Data == null)
            {
                TempData["ErrorMessage"] = result.Message ?? "No se encontró el envío.";
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // POST: EnvioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EnvioEditModel model)
        {
            var envioResponse = await _envioHttpService.UpdateEnvioAsync(model);
            if (envioResponse.IsSuccess)
            {
                TempData["SucccessMessage"] = "Envío actualizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = envioResponse.Message;
            }
            return View(model);
        }

    }
}
