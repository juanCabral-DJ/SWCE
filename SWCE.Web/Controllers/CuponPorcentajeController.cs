using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.CuponMontoFijo;
using SWCE.Web.Models.CuponPorcentaje;
using System.Text.Json;

namespace SWCE.Web.Controllers
{
    public class CuponPorcentajeController : Controller
    {
        private readonly string _apiBaseUrl = "http://localhost:5134/api/";

        // GET: CuponPorcentajeController
        public async Task<IActionResult> Index()
        {
            GetAllCuponPorcentajeResponse getAllCuponPorcentajeResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync("CuponPorcentaje/GetAll");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getAllCuponPorcentajeResponse = JsonSerializer.Deserialize<GetAllCuponPorcentajeResponse>(content)!;
                    }
                    else
                    {
                        getAllCuponPorcentajeResponse = new GetAllCuponPorcentajeResponse
                        {
                            message = "Error al obtener los cupones de porcentaje.",
                            isSuccess = false
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllCuponPorcentajeResponse = new GetAllCuponPorcentajeResponse
                {
                    message = ex.Message,
                    isSuccess = false,
                    data = null
                };
            }
            return View(getAllCuponPorcentajeResponse.data);
        }

        // GET: CuponPorcentajeController/Details/5
        public async Task<IActionResult>Details(int id)
        {
            GetByIdCuponPorcentajeResponse getByIdCuponPorcentajeResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"CuponPorcentaje/GetCuponPorcentajeById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getByIdCuponPorcentajeResponse = JsonSerializer.Deserialize<GetByIdCuponPorcentajeResponse>(content)!;

                        if (getByIdCuponPorcentajeResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Cupón de porccentaje no encontrado.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener el cupón de porcentaje: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener el cupón de porcentaje: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            return View(getByIdCuponPorcentajeResponse.data);
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsJsonAsync("CuponPorcentaje/Create", model);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cupón de porcentaje creado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al crear el cupón de porcentaje: {errorContent}";
                        ModelState.AddModelError(string.Empty, $"Error: {errorContent}");
                        return View(model);
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al crear el cupón de porcentaje: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                return View(model);
            }
        }

        // GET: CuponPorcentajeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetByIdCuponPorcentajeResponse getByIdCuponPorcentajeResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"CuponPorcentaje/GetCuponPorcentajeById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getByIdCuponPorcentajeResponse = JsonSerializer.Deserialize<GetByIdCuponPorcentajeResponse>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        })!;

                        if (getByIdCuponPorcentajeResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Cupón de monto fijo no encontrado para editar.";
                            return RedirectToAction(nameof(Index));
                        }

                        var updateModel = new UpdateCuponPorcentajeModel
                        {
                            id = getByIdCuponPorcentajeResponse.data.id,
                            porcentaje = getByIdCuponPorcentajeResponse.data.porcentaje,
                            fechaExpiracion = getByIdCuponPorcentajeResponse.data.fechaExpiracion
                        };

                        return View(updateModel);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener el cupón de porcentaje para edición: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener el cupón de porcentaje para edición: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CuponPorcentajeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCuponPorcentajeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PutAsJsonAsync("CuponPorcentaje/Update", model);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cupón de porcentaje actualizado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al actualizar el cupón de porcentaje: {errorContent}";
                        ModelState.AddModelError(string.Empty, $"Error: {errorContent}");
                        return View(model);
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al actualizar el cupón de porcentaje: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                return View(model);
            }
        }

        // GET: CuponPorcentajeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetByIdCuponPorcentajeResponse getByIdCuponPorcentajeResponse = null!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"CuponPorcentaje/GetCuponPorcentajeById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getByIdCuponPorcentajeResponse = JsonSerializer.Deserialize<GetByIdCuponPorcentajeResponse>(content)!;

                        if (getByIdCuponPorcentajeResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Cupón de porcentaje no encontrado para deshabilitar.";
                            return RedirectToAction(nameof(Index));
                        }
                        return View(getByIdCuponPorcentajeResponse.data);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener el cupón de porcentaje para deshabilitar: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener el cupón de porcentaje para deshabilitar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CuponPorcentajeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsync($"CuponPorcentaje/DisableCuponPorcentaje?id={id}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cupón de porcentaje deshabilitado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al deshabilitar el cupón de porcentaje: {errorContent}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al deshabilitar el cupón de porcentaje: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
