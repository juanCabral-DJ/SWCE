using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.CuponMontoFijo;
using System.Net.Http.Json;
using System.Text.Json;

namespace SWCE.Web.Controllers
{
    public class CuponMontoFijoController : Controller
    {
        private readonly string _apiBaseUrl = "http://localhost:5134/api/";

        // GET: CuponMontoFijoController
        public async Task<IActionResult> Index()
        {
            GetAllCuponMontoFijoResponse getAllCuponMontoFijoResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync("CuponMontoFijo/GetAll");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getAllCuponMontoFijoResponse = JsonSerializer.Deserialize<GetAllCuponMontoFijoResponse>(content)!;
                    }
                    else
                    {
                        getAllCuponMontoFijoResponse = new GetAllCuponMontoFijoResponse
                        {
                            message = $"Error al obtener los cupones de monto fijo. Estado: {response.StatusCode}",
                            isSuccess = false,
                            data = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllCuponMontoFijoResponse = new GetAllCuponMontoFijoResponse
                {
                    message = $"Error inesperado al obtener los cupones de monto fijo: {ex.Message}",
                    isSuccess = false,
                    data = null
                };
            }
            return View(getAllCuponMontoFijoResponse?.data ?? new List<CuponMontoFijoModel>());
        }

        // GET: CuponMontoFijoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetByIdCuponMontoFijoResponse getByIdCuponMontoFijoResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"CuponMontoFijo/GetById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getByIdCuponMontoFijoResponse = JsonSerializer.Deserialize<GetByIdCuponMontoFijoResponse>(content)!;

                        if (getByIdCuponMontoFijoResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Cupón de monto fijo no encontrado.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener el cupón de monto fijo: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener el cupón de monto fijo: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            return View(getByIdCuponMontoFijoResponse.data);
        }

        // GET: CuponMontoFijoController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuponMontoFijoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCuponMontoFijoModel model) 
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
                    var response = await client.PostAsJsonAsync("CuponMontoFijo/Create", model);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cupón de monto fijo creado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al crear el cupón de monto fijo: {errorContent}";
                        ModelState.AddModelError(string.Empty, $"Error: {errorContent}");
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al crear el cupón de monto fijo: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                return View(model);
            }
        }

        // GET: CuponMontoFijoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetByIdCuponMontoFijoResponse getByIdCuponMontoFijoResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"CuponMontoFijo/GetById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getByIdCuponMontoFijoResponse = JsonSerializer.Deserialize<GetByIdCuponMontoFijoResponse>(content)!;
                        if (getByIdCuponMontoFijoResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Cupón de monto fijo no encontrado para editar.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener el cupón de monto fijo para edición: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener el cupón de monto fijo para edición: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

            return View(getByIdCuponMontoFijoResponse.data);
        }

        // POST: CuponMontoFijoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCuponMontoFijoModel model)
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
                    var response = await client.PutAsJsonAsync("CuponMontoFijo/Update", model);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cupón de monto fijo actualizado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al actualizar el cupón de monto fijo: {errorContent}";
                        ModelState.AddModelError(string.Empty, $"Error: {errorContent}");
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al actualizar el cupón de monto fijo: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                return View(model);
            }
        }

        // GET: CuponMontoFijoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetByIdCuponMontoFijoResponse getByIdCuponMontoFijoResponse = null!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"CuponMontoFijo/GetById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getByIdCuponMontoFijoResponse = JsonSerializer.Deserialize<GetByIdCuponMontoFijoResponse>(content)!;

                        if (getByIdCuponMontoFijoResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Cupón de monto fijo no encontrado para deshabilitar.";
                            return RedirectToAction(nameof(Index));
                        }
                        return View(getByIdCuponMontoFijoResponse.data);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener el cupón de monto fijo para deshabilitar: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener el cupón de monto fijo para deshabilitar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CuponMontoFijoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsync($"CuponMontoFijo/DisableCuponMontoFijo?id={id}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cupón de monto fijo deshabilitado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al deshabilitar el cupón de monto fijo: {errorContent}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al deshabilitar el cupón de monto fijo: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}